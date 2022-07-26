using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field_Generation : MonoBehaviour
{
    // room generation center
    public GameObject Spawn_Prefab;

    // additional rooms for procedural generation
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] rightRooms;
    public GameObject[] leftRooms;

    // Storing all the rooms generated for a specitic level so they are accessable within a list
    // rather than having to search for them in a non organised list of gameobjects
    public List<GameObject> rooms;

    // Delay spawn_exit;
    public float exit_waitTime = 1f;
    public GameObject exit;
    // private bool spawnedExit = false;

    private Transform map_objects;

    Collider2D spawn_collider;

    // Spawn a 1x1 block that is used as part of the playable map - first generation to allow followup procedural generation
    private void GenerateSpawnPoint(){
        GameObject spawn_point = Instantiate(Spawn_Prefab,new Vector3(0f,0f,0f),Quaternion.identity);
        spawn_point.transform.SetParent(map_objects);
    }

    // IEnumerator GenerateExitPoint(){
    //     if(spawnedExit){
    //         yield break;
    //     } else {
    //         yield return new WaitForSeconds(exit_waitTime);
    //         spawnedExit = true;
    //         Instantiate(exit,rooms[rooms.Count-1].transform.position,Quaternion.identity);
    //     }
    // }

    private void GenerateExitPoint(){
        // spawnedExit = true;
        Instantiate(exit,rooms[rooms.Count-1].transform.position,Quaternion.identity);
    }

    public void disableSpawns(){
        for (int i = 0; i < rooms.Count; i++){
            // print("run");
            spawn_collider = rooms[i].GetComponent<Collider2D>();
            spawn_collider.enabled = !spawn_collider.enabled;
        }
    }

    // Restrict it spawning enemies over the player right as he spawns in gives him a 1 sec break to move away else he might get instantly damaged off spawn.
    public void disableFirstSpawns(){
        for (int i = 0; i < 5; i++){
            // print("run");
            spawn_collider = rooms[i].GetComponent<Collider2D>();
            spawn_collider.enabled = !spawn_collider.enabled;
        }
    }


    public void SetupScene(int level){
        if (level != 12){
            map_objects = new GameObject("Map").transform;
            GenerateSpawnPoint();
            
            // StartCoroutine(GenerateExitPoint());
            Invoke("GenerateExitPoint", 0.7f);
            Invoke("disableFirstSpawns", 1f);
            Invoke("disableFirstSpawns", 4f);

        }
    }
}
