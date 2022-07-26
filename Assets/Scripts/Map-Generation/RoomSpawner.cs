using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    // private Room_Tempaltes room_stor;
    // private GameObject map_parent;

    private Field_Generation room_stor;
    // private Transform map_objects;

    private int rand;
    public float waitTime = 0.5f;
    public bool spawned = false;

    /// 1-> bottom
    /// 2-> top
    /// 3 -> left
    /// 4 -> right
    void Start()
    {
        Destroy(gameObject,waitTime);
        room_stor = GameObject.FindWithTag("GameManager").GetComponent<Field_Generation>();
        // map_parent = GameObject.Find("Map");
        
        Invoke("Spawn", 0.1f);
    }

    // Update is called once per frame
    void Spawn()
    {
        if(spawned == false){
            int count_gen = room_stor.rooms.Count;

            // MapGeneration count_gen = map_parent.GetComponent<MapGeneration>();
            //print(count_gen);
            // print(room_stor.bottomRooms.Length);
            if(openingDirection == 1){
                if(count_gen <= 5){
                    rand = Random.Range(0,room_stor.bottomRooms.Length);
                    GameObject test = Instantiate(room_stor.bottomRooms[rand],transform.position, room_stor.bottomRooms[rand].transform.rotation);

                    test.transform.parent = GameObject.Find("Map").transform;

                } else if (count_gen <= 10){
                    rand = Random.Range(0,4);
                    GameObject test = Instantiate(room_stor.bottomRooms[rand],transform.position, room_stor.bottomRooms[rand].transform.rotation);
                    test.transform.parent = GameObject.Find("Map").transform;

                } else if (count_gen >= 11){
                    rand = 0;
                    GameObject test = Instantiate(room_stor.bottomRooms[rand],transform.position, room_stor.bottomRooms[rand].transform.rotation);
                    test.transform.parent = GameObject.Find("Map").transform;

                }
                
            } else if(openingDirection == 2){
                if(count_gen <= 5){
                    rand = Random.Range(0,room_stor.topRooms.Length);
                    GameObject test = Instantiate(room_stor.topRooms[rand],transform.position, room_stor.topRooms[rand].transform.rotation);
                    test.transform.parent = GameObject.Find("Map").transform;

                } else if (count_gen <= 10){
                    rand = Random.Range(0,4);
                    GameObject test = Instantiate(room_stor.topRooms[rand],transform.position, room_stor.topRooms[rand].transform.rotation);
                    test.transform.parent = GameObject.Find("Map").transform;

                } else if (count_gen >= 11){
                    rand = 0;
                    GameObject test = Instantiate(room_stor.topRooms[rand],transform.position, room_stor.topRooms[rand].transform.rotation);
                    test.transform.parent = GameObject.Find("Map").transform;

                }

            } else if(openingDirection == 3){
                if(count_gen <= 5){
                    rand = Random.Range(0,room_stor.leftRooms.Length);
                    GameObject test = Instantiate(room_stor.leftRooms[rand],transform.position, room_stor.leftRooms[rand].transform.rotation);
                    test.transform.parent = GameObject.Find("Map").transform;

                } else if (count_gen <= 10){
                    rand = Random.Range(0,4);
                    GameObject test = Instantiate(room_stor.leftRooms[rand],transform.position, room_stor.leftRooms[rand].transform.rotation);
                    test.transform.parent = GameObject.Find("Map").transform;

                } else if (count_gen >= 11){
                    rand = 0;
                    GameObject test = Instantiate(room_stor.leftRooms[rand],transform.position, room_stor.leftRooms[rand].transform.rotation);
                    test.transform.parent = GameObject.Find("Map").transform;

                }

            } else if(openingDirection == 4){
                if(count_gen <= 5){
                    rand = Random.Range(0,room_stor.rightRooms.Length);
                    GameObject test = Instantiate(room_stor.rightRooms[rand],transform.position, room_stor.rightRooms[rand].transform.rotation);
                    test.transform.parent = GameObject.Find("Map").transform;

                } else if (count_gen <= 10){
                    rand = Random.Range(0,4);
                    GameObject test = Instantiate(room_stor.rightRooms[rand],transform.position, room_stor.rightRooms[rand].transform.rotation);
                    test.transform.parent = GameObject.Find("Map").transform;

                } else if (count_gen >= 11){
                    rand = 0;
                    GameObject test = Instantiate(room_stor.rightRooms[rand],transform.position, room_stor.rightRooms[rand].transform.rotation);
                    test.transform.parent = GameObject.Find("Map").transform;

                }
            }
            spawned = true;  
        }
       

    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("SpawnPoint") && (other.GetComponent<RoomSpawner>().spawned == true)){
            Destroy(gameObject);
        }
        

        if(other.CompareTag("SpawnPoint") && (spawned == false && other.GetComponent<RoomSpawner>().spawned == false)){
            int val = other.GetComponent<RoomSpawner>().openingDirection;
            if(val != openingDirection){
                if(val != 0 && openingDirection != 0){
                    if(val > openingDirection){
                        Destroy(gameObject);
                        // if(val == 4){
                        //     if(openingDirection == 1){
                        //         GameObject test_2 = Instantiate(room_stor.additionalRooms[5],transform.position, room_stor.additionalRooms[5].transform.rotation);
                        //         test_2.transform.parent = GameObject.Find("Map").transform;
                        //     }

                        //     if(openingDirection == 2){
                        //         GameObject test_2 = Instantiate(room_stor.additionalRooms[6],transform.position, room_stor.additionalRooms[6].transform.rotation);
                        //         test_2.transform.parent = GameObject.Find("Map").transform;
                        //     }

                        //     if(openingDirection == 3){
                        //         GameObject test_2 = Instantiate(room_stor.additionalRooms[1],transform.position, room_stor.additionalRooms[1].transform.rotation);
                        //         test_2.transform.parent = GameObject.Find("Map").transform;
                        //     }
                            
                        // } else if (val == 3){
                        //     if(openingDirection == 1){
                        //         GameObject test_2 = Instantiate(room_stor.additionalRooms[4],transform.position, room_stor.additionalRooms[4].transform.rotation);
                        //         test_2.transform.parent = GameObject.Find("Map").transform;
                        //     } else if(openingDirection == 2){
                        //         GameObject test_2 = Instantiate(room_stor.additionalRooms[3],transform.position, room_stor.additionalRooms[3].transform.rotation);
                        //         test_2.transform.parent = GameObject.Find("Map").transform;
                        //     }

                        // } else if (val == 2){
                        //     GameObject test_2 = Instantiate(room_stor.additionalRooms[0],transform.position, room_stor.additionalRooms[0].transform.rotation);
                        //     test_2.transform.parent = GameObject.Find("Map").transform;
                        // }
                        
                    } else {
                        // other.GetComponent<RoomSpawner>().spawned = true;
                        Destroy(other);
                        // GameObject test_2 = Instantiate(room_stor.additionalRooms[6],transform.position, room_stor.additionalRooms[6].transform.rotation);
                        // test_2.transform.parent = GameObject.Find("Map").transform;
                    }
                }
            }
        }
    }
}
