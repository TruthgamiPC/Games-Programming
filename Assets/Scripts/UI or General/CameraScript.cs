using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Transform player;
    public float smoothing;
    public Vector3 offset;

    void FixedUpdate(){
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null){
            Vector3 newPos = Vector3.Lerp(transform.position, player.transform.position + offset, smoothing);
            transform.position = player.transform.position + offset;
        }
    }
}