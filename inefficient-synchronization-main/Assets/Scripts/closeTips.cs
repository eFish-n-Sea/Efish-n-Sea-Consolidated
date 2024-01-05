using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeTips : MonoBehaviour
{
    GameObject tips;
    
    void Start(){
        tips = GameObject.FindWithTag("Tips");
    }

    public void OnButtonPress(){
        tips.GetComponent<Animator>().SetBool("tipping", false);
        StartCoroutine(deactivate());
    }

    IEnumerator deactivate(){
        yield return new WaitForSeconds(1);
        tips.SetActive(false);
    }
}
