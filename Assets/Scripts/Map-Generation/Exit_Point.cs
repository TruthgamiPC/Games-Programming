using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Exit_Point : MonoBehaviour
{
    void Start(){
        this.GetComponent<Collider2D>().enabled = false;
    }
}
