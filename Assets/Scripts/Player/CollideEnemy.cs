using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideEnemy : MonoBehaviour
{
    public int damage;

    void OnTriggerEnter2D(Collider2D collision){
        if (collision.tag != "Player"){
             if (collision.tag == "Wall"){
                Destroy(gameObject);
            } else if (collision.tag == "Enemy"){
                if(collision.GetComponent<Enemy_Script>() != null){
                    collision.GetComponent<Enemy_Script>().DealDamage(damage);
                    Destroy(gameObject);
                }
                Destroy(gameObject);
            }
        }
    }

    void Update(){
        Destroy(gameObject, 2.5f); // destroy projectile after 2 seconds flight
    }
}
