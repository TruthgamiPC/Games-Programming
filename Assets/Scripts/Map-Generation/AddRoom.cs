using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour
{
    private Field_Generation maanger_rooms;

    void Start(){
        maanger_rooms = GameManager.instance.GetComponent<Field_Generation>();
        maanger_rooms.rooms.Add(this.gameObject);
    }
}
