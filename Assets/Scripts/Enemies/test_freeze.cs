using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_freeze : MonoBehaviour
{
    Quaternion rotation;
    Vector3 positionOffset;
    
    void Awake() {
        rotation = transform.parent.rotation;
        positionOffset = transform.localPosition;
    }
    void LateUpdate() {
        transform.rotation = rotation;
        transform.position = transform.parent.position + positionOffset;
    }
}