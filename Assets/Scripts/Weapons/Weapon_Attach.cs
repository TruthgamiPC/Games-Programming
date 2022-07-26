using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Attach : MonoBehaviour
{
    public GameObject[] weapons;
    private int chosen;
    
    public void EquipWeapon(){
        chosen = GameManager.instance.get_weapon();
        GameObject find_parent = GameObject.FindWithTag("weapon_store");
        Vector3 test_pos = weapons[chosen].transform.position;
        
        GameObject temp_wep = Instantiate(weapons[chosen],find_parent.transform.position + test_pos,Quaternion.identity, find_parent.transform);
    }
}
