using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Melee : MonoBehaviour
{
    // public GameObject castPoint;
    // public float agroRange;
    private Vector2 playerPos;
    public float attackRange;
    public int damage;
    private float lastAttackTime;
    public float attackDelay;

    void Start(){
       damage = GameManager.instance.scale_stat(damage);
    }

    void Update(){
        playerPos = ((GameObject.FindWithTag("Player")).transform.position);
        attackEnemy();
    }

    private void attackEnemy(){
        float distanceToPlayer = Vector2.Distance(transform.position, playerPos);
        if (distanceToPlayer < attackRange){
            if(lineOfSight()){
                if(Time.time > lastAttackTime + attackDelay){
                    FindObjectOfType<AudioManager>().Play("Melee");
                    GameObject.FindWithTag("Player").GetComponent<PlayerMain>().PlayerDamaged(damage);
                    lastAttackTime = Time.time;
                    // Add code for animator on enemy melee attack
                }
            }
        }
    }

    private bool lineOfSight(){
        Vector2 myPos = transform.position;
        RaycastHit2D hit = Physics2D.Linecast(myPos,playerPos, 1 << 7 | 1 << 9);
        if(hit.collider != null){
            if(hit.collider.gameObject.CompareTag("Player")){
                return true;
            }
        }
        return false;
    }
}


//LayerMask.NameToLayer("playable")
