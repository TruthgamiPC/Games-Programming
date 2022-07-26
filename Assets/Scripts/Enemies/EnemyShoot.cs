using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject enemy_projectile;
    public float projectileForce;
    public float cooldown;
    public int damage;
    
    private void scaleEnemyDamage(){
        damage = GameManager.instance.scale_stat(damage);
    }

    void Start(){
        scaleEnemyDamage();
        StartCoroutine(ShootPlayer());
    }


    IEnumerator ShootPlayer(){
        yield return new WaitForSeconds(cooldown);
        GameObject player = GameObject.FindWithTag("Player");

        if(player != null){
            FindObjectOfType<AudioManager>().Play("Ranged");
            GameObject projectile_e = Instantiate(enemy_projectile, transform.position, Quaternion.identity);

            Vector2 playerPos = player.transform.position;
            Vector2 myPos = transform.position;
            Vector2 direction = (playerPos - myPos).normalized;

            projectile_e.GetComponent<Rigidbody2D>().velocity = direction * projectileForce;
            projectile_e.GetComponent<EnemyProjectile>().damage = damage;
            StartCoroutine(ShootPlayer());
        }
    }

}
