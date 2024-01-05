using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBehavior : MonoBehaviour
{
    GameController gc;

    void Start(){
        gc = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    public void OnTriggerEnter2D(Collider2D col){
        if (col.tag == "Player"){
            gc.win();
        }
    }
}
