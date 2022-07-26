using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
    public int enemy_total;
    public int enemy_left;
    public int enemy_limit;
    public int enemies_alive;
    public int enemies_killed;

    public List<GameObject> enemies;

    private Transform enemy_objects;

    private bool level_finished;
    
    // Update stats for enemy_manager when an enemy gets killed
    public void kill_enemy(){
        if(enemies_killed == -1){
            enemies_killed = 0;
        }
        enemies_killed++;
        enemies_alive--;
    }

    // remove an enemy object from the list of enemies
    public void remove_enemy(GameObject enemy_pass){
        enemies.Remove(enemy_pass);    
    }

    // function called to update the parameters of enemy manager when an enemy is being spawned
    public bool enemy_spawned() {
        if (enemy_left > 0){
            if (enemies_alive < enemy_limit){
                enemies_alive++;
                enemy_left--;
                return true;
            }
            return false;
        } else {
            return false;
        }
    }

    // Calculate the amoutn of enemies that are required to spawn on the floor for a clear to be counted, has scaling factors with difficulty / size of the room
    // Floor and level are not included in scaling as they alreayd impact the enemy's stats for damage and health
    private void set_up(){
        int floor_multiplier = 1;
        Field_Generation fieldScript = GameManager.instance.fieldScript;
        if (GameManager.instance.difficulty == 1 || GameManager.instance.difficulty == 2){
            floor_multiplier = 2;
        } else if (GameManager.instance.difficulty == 3){
            floor_multiplier = 3;
        }
        enemy_total = (fieldScript.rooms.Count) + (GameManager.instance.get_floor() * floor_multiplier);
        enemy_limit = enemy_total / Random.Range(2,5);
        enemy_left = enemy_total;
    }


    // Standard start function
    public void Start() {
        level_finished = false;
        enemy_objects = new GameObject("Enemies").transform;
        enemies.Clear();
        Invoke("set_up",1f);
        
        // print(enemy_total);
        // print(enemy_limit);
        enemies_alive = 0;
        enemies_killed = -1;   
    }


    // Check if level was finished by verifying that enemies_killed = enemies_total 
    void Update() {
        if(!level_finished){
            if( enemies_killed == enemy_total){
                level_finished = true;
                GameObject end_point = GameObject.FindWithTag("Finish");
                end_point.GetComponent<Collider2D>().enabled = true;
            }   
        }
        
    }
}
