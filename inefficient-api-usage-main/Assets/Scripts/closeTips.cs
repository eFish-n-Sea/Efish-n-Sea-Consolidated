using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeTips : MonoBehaviour
{
    GameObject tips;
    gameController gc;
    
    void Start(){
        tips = GameObject.FindWithTag("Tips");
        gc = GameObject.FindWithTag("GameController").GetComponent<gameController>();
    }

    public void OnButtonPress(){
        tips.GetComponent<Animator>().SetBool("tipping", false);
        StartCoroutine(deactivate());
        gc.tipOpen = false;
    }

    IEnumerator deactivate(){
        yield return new WaitForSeconds(1);
        tips.SetActive(false);
    }
}
