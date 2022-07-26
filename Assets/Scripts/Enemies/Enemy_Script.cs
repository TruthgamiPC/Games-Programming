using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Script : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public float speed;

    public int enemy_type;

    public GameObject healthBar;
    public Slider healthBarSlider;

    private Rigidbody2D rb;
    private Vector2 movement;

    GameObject player;

    void Start()
    {
        scaleEnemy();
        rb = this.GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        health = maxHealth;
    }

    private void scaleEnemy(){
        maxHealth = GameManager.instance.scale_stat(maxHealth);
    }

    IEnumerator slow(){
        float temp_speed = speed; 
        if(Stats.stats_obj.weapon == 0){
            speed = 0.1f;
            yield return new WaitForSeconds(0.1f);
        } else if (Stats.stats_obj.weapon == 1){
            yield break;
        } else {
            speed = 0.1f;
            yield return new WaitForSeconds(0.4f);
        }
        speed = temp_speed;
    }

    public void DealDamage(int damage){
        healthBar.SetActive(true);
        if(enemy_type != 0){
            StartCoroutine(slow());
        }
        health -= damage;
        healthBarSlider.value = CalculateHealthPercentDmg();
        CheckDeath();
    }

    private float CalculateHealthPercentDmg(){
        float temp_health = (float)health;
        float temp_max = (float)maxHealth;
        return temp_health / temp_max;
    }

    public void CheckDeath(){
        if (health <= 0){
            if (enemy_type == 0){
                 GameManager.instance.calculateScore(50);
            } else {
                GameManager.instance.calculateScore(75);
            }
            GameManager.instance.GetComponent<Enemy_Manager>().remove_enemy(gameObject);
            GameManager.instance.GetComponent<Enemy_Manager>().kill_enemy();
            Destroy(gameObject);
        }
    }

    private void FixedUpdate(){
        if(enemy_type != 0){
            moveCharacter(movement);
        }
    }

    void moveCharacter(Vector2 direction){
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }

    void Update(){
        if (player != null){
            Vector2 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;
        } else {
            speed = 0;
        }
    }
}
