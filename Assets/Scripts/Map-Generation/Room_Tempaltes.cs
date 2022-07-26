using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Tempaltes : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] rightRooms;
    public GameObject[] leftRooms;

    public List<GameObject> rooms;

    public float waitTime;
    private bool spawnedDoor;
    public GameObject door;

    Collider2D obj_colliders;


    void DisableColliders(){
        // Disable the 4's openings spawning colliders so they don't auto spawn enemies
        for(int i = 0; i < 4; i++){
            obj_colliders = rooms[i].GetComponent<Collider2D>();
            obj_colliders.enabled = false;
        }
    }

    void Update(){
        if(waitTime <= 0 && spawnedDoor == false){
            for (int i = 0; i < rooms.Count; i++){
                if(i == rooms.Count-1){
                    Instantiate(door,rooms[i].transform.position,Quaternion.identity);
                    spawnedDoor = true;
                    DisableColliders();
                }
            }
        } else {
            waitTime -= Time.deltaTime;
        }
    }
    
}
