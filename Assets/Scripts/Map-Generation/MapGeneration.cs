using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{

    public int gen_rooms;
    public GameObject Spawn_Prefab;

    void Start(){
        Vector3 pos = new Vector3(0f,0f,0f);

        Instantiate(Spawn_Prefab,pos,Quaternion.identity);
    }
}
