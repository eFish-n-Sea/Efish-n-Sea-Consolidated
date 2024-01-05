using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleBehavior : MonoBehaviour
{
    Animator bubble;

    void Start(){
        bubble = transform.GetChild(0).GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D col){
        if (col.tag == "Player")
            bubble.SetBool("open", true);
    }

    void OnTriggerExit2D(Collider2D col){
        if (col.tag == "Player")
            bubble.SetBool("open", false);
    }
}
