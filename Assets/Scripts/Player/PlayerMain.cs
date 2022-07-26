using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMain : MonoBehaviour
{
    private float health;
    private float max_health;
    private Weapon_Attach weapscript;
    private bool no_dmg;
    SpriteRenderer m_SpriteRenderer;
    Color m_NewColor;

    void Start(){
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        health = GameManager.instance.player_health;
        max_health = GameManager.instance.player_max_health;
        GameManager.instance.updateUI();
        weapscript = this.GetComponent<Weapon_Attach>();
        weapscript.EquipWeapon();
    }

    // Detect player reaching the end destination ( pink circle) after enemeis have been killed
    void OnTriggerEnter2D(Collider2D test){
        if(test.CompareTag("Finish")){
            enabled = false;
            if(GameManager.instance.get_level() == 9){
                // This would be Scene 2 to direct to a boss - boss stages have a pre build map and should spawn bosses int the middle
                // Currently it would lead to a scene with nothing on it and a camera that doesn't follow the player
                // Addionally for now I will loop through Scene 1 as I don't want to fill in more memory space from an additional scene that wasn't worked on
                // However this is the principal to how thje game reloads levels with newly generated stages
                nextScene();
            } else if(GameManager.instance.get_level() == 10) {
                GameObject.FindWithTag("EditorOnly").GetComponent<pauseMenu>().winGame();
            } else {
                nextScene();
            }
        }
    }

    public void nextScene(){
        SceneManager.LoadScene(1);
    }

    // Coroutine that is used to make the player take no damage for half a second after he gets hit
    // note melee enemy attacks CD is 0.7 seconds which means a melee enemy hits will always hit you, however multiple enemies will hit only once
    IEnumerator invincible(){
        m_SpriteRenderer.color = Color.yellow;
        yield return new WaitForSeconds(0.5f);
        m_SpriteRenderer.color = Color.white;
        no_dmg = false;
    }

    // player gets damaged - here this could be updated to link to more effects if items are added similar to how the invincible state of the player can be extended as such by changing the procedure call
    public void PlayerDamaged(float damage){
        if(no_dmg == true){
            return;
        } else {
            no_dmg = true;
            StartCoroutine(invincible());
            FindObjectOfType<AudioManager>().Play("Damaged");
            health -= damage;
            updateStats();
            GameManager.instance.updateUI();
            CheckIfGameOver();
        }
    }

    // Update the stats at the game instance.
    private void updateStats(){
        if(GameManager.instance != null){
            GameManager.instance.player_health = health;
            GameManager.instance.player_max_health = max_health;
        }
    }

    // Check if the player is killed by enemy projectiles
    private void CheckIfGameOver(){
        if (health <= 0){
            FindObjectOfType<AudioManager>().Play("Defeated");
            enabled = false;
            GameManager.instance.GameOver();
        }
    }

    // Scene transition safety net to update stats
    private void OnDisable(){
        updateStats();
    }
}
