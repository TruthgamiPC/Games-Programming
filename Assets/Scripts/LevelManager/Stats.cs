using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public static Stats stats_obj;

    public int weapon;
    public int weapon_damage;
    public int difficulty;

    void Awake(){
        if (stats_obj == null){
            stats_obj = this;
        } else if (stats_obj != this){
            Destroy(gameObject);
        }

        difficulty = 4;
        weapon = 4;
        DontDestroyOnLoad(gameObject);
    }

    public void set_difficulty(int pass_dif){
        difficulty = pass_dif;
    }

    public void setWeapon(int button_clicked){
        weapon = button_clicked;
    }
}
