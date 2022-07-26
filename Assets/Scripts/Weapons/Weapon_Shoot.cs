using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Shoot : MonoBehaviour
{
    public int magazine;
    private int current_mag;
    private float readyToShoot;
    public bool autoFire;

    private bool reloading;
    
    public float projectile_speed;
    public int projectile_damage;
    public float fire_rate;
    public GameObject projectile;

    public GameObject shootPosition;

    private Vector2 target_position;

    public void damage_add(int extra){
        projectile_damage = projectile_damage + extra;
    }

    void Awake(){
        damage_add(GameManager.instance.dmg_add);
        GameManager.instance.max_mag = magazine;
        Reload();
    }

    void Update(){
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target_position = mousePos - (Vector2)this.transform.position;

        if(!pauseMenu.GameIsPaused){
            if(!reloading){ 
                if(autoFire){
                    if(Input.GetMouseButton(0)){
                        if(current_mag != 0){
                            if(Time.time > readyToShoot){
                                readyToShoot = Time.time + 1/fire_rate;
                                Fire();
                            }
                        } else {
                            reloadSequence();
                        }
                    }
                } else {
                    if(Input.GetMouseButtonDown(0)){
                        if(current_mag != 0){
                            if(Time.time > readyToShoot){
                                readyToShoot = Time.time + 1/fire_rate;
                                Fire();
                            }
                        } else {
                            reloadSequence();
                        }
                    }
                }

                if(Input.GetKey(KeyCode.R)){
                    reloadSequence();
                }
            }
        }
    }

    public void reloadSequence(){
        reloading = true;
        if(GameManager.instance.get_weapon() == 0 || GameManager.instance.get_weapon() == 2){
            FindObjectOfType<AudioManager>().Play("Pistol_Reload");
        } else {
            FindObjectOfType<AudioManager>().Play("SMG_Reload");
        }
        Invoke("Reload",0.5f);   
    }

    public void Reload(){

        current_mag = magazine;
        GameManager.instance.update_UI_Mag(current_mag);
        reloading = false;
    }

    public int return_current_mag(){
        return current_mag;
    }

    private void Fire(){
        if(GameManager.instance.get_weapon() == 0){
            FindObjectOfType<AudioManager>().Play("Pistol");
        } else if(GameManager.instance.get_weapon() == 1){
            FindObjectOfType<AudioManager>().Play("SMG");
        } else if(GameManager.instance.get_weapon() == 2){
            FindObjectOfType<AudioManager>().Play("Sniper");
        }
        GameObject projectilePlayer = Instantiate(projectile, shootPosition.transform.position, Quaternion.identity);
        current_mag--;
        GameManager.instance.update_UI_Mag(current_mag);
        projectilePlayer.GetComponent<Rigidbody2D>().velocity = target_position.normalized * projectile_speed;
        projectilePlayer.GetComponent<CollideEnemy>().damage = projectile_damage;
    }
}
