using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public int damage;

    void Start(){
        Destroy(gameObject, 2.5f); // destroy projectile after 2 seconds flight
    }

    void OnTriggerEnter2D(Collider2D collision){
        if (collision.tag != "Enemy"){
            if (collision.tag == "Wall"){
                Destroy(gameObject);
            } else if (collision.tag == "Player"){
                if(collision.GetComponent<PlayerMain>() != null){
                    collision.GetComponent<PlayerMain>().PlayerDamaged(damage);
                    Destroy(gameObject);
                }
                Destroy(gameObject);
            }
        }
    }
}

