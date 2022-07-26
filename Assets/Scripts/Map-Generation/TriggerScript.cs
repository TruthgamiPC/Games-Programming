using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    public GameObject[] enemies;
    private Enemy_Manager enemyScript;

    void Start(){
        enemyScript = GameManager.instance.GetComponent<Enemy_Manager>();
    }

    void OnTriggerEnter2D(Collider2D detected){
        if (detected.tag == "Player"){
            bool spawn = GameManager.instance.GetComponent<Enemy_Manager>().enemy_spawned();
            
            if (spawn){
                GameObject test_enemy = Instantiate(enemies[Random.Range(0,2)], gameObject.transform.position , Quaternion.identity);
                enemyScript.enemies.Add(test_enemy);
                test_enemy.transform.parent = GameObject.Find("Enemies").transform;
            }
        }
    }
}
