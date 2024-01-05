using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class openTips : MonoBehaviour
{
    TMP_Text tiptext;
    GameObject tips;
    gameController gc;
    
    void Start(){
        tips = GameObject.FindWithTag("Tips");
        tips.SetActive(false);
        tiptext = transform.GetChild(1).GetComponent<TMP_Text>();
        gc = GameObject.FindWithTag("GameController").GetComponent<gameController>();
    }

    public void OnButtonPress(){
        tips.SetActive(true);
        GameObject.FindWithTag("Tips").GetComponent<Animator>().SetBool("tipping", true);
    }

    public void Display(string text)
    {
        gc.tipOpen = true;
        tiptext.text = text;

         tips.SetActive(true);
        GameObject.FindWithTag("Tips").GetComponent<Animator>().SetBool("tipping", true);
    }
}
