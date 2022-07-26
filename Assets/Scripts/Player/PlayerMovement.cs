using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public Rigidbody2D playerChar;
    public float speed;
    private Vector2 direction;
    private Vector2 directionHistory;
    private Animator animatorP;
    private float runS;

    void Start(){
        animatorP = GetComponent<Animator>();

    }
    void Update(){
        TakeInput();
        Move();

    }

    private void Move(){
        playerChar.velocity = new Vector2(direction.x, direction.y);
        if(direction.x != 0 || direction.y != 0){
            SetAnimatorMovement(directionHistory);
        } else {
            animatorP.SetLayerWeight(1,0);
        }
    }

    private void TakeInput(){
        direction = Vector2.zero;

        if(Input.GetKey(KeyCode.W)){
            direction += Vector2.up;
        }

        if(Input.GetKey(KeyCode.A)){
            direction += Vector2.left;
        }

        if(Input.GetKey(KeyCode.S)){
            direction += Vector2.down;
        }

        if(Input.GetKey(KeyCode.D)){
            direction += Vector2.right;
        }

        if (direction != Vector2.zero){
            directionHistory = direction;
        }
    }

    private void SetAnimatorMovement(Vector2 direction){
        animatorP.SetLayerWeight(1,1);
        animatorP.SetFloat("xDir", direction.x);
        animatorP.SetFloat("yDir", direction.y);
        
    }

}
