using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openTips : MonoBehaviour
{
    GameObject tips;
    
    void Start(){
        tips = GameObject.FindWithTag("Tips");
        tips.SetActive(false);
    }

    public void OnButtonPress(){
        tips.SetActive(true);
        GameObject.FindWithTag("Tips").GetComponent<Animator>().SetBool("tipping", true);
    }
}
