using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageBehavior : MonoBehaviour
{
    SpriteRenderer sr;

    void Start(){
        sr = GetComponent<SpriteRenderer>();
    }

    public void invertOrder(){
        sr.sortingOrder *= -1;
    }
    
    public void flipUp(){
        StartCoroutine(linesUp());
    }

    IEnumerator linesUp(){
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).GetComponent<Animator>().SetBool("flipUp", true);

        yield return new WaitForSeconds(0.7f);

        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).GetComponent<Animator>().SetBool("flipUp", false);
    }

    public void flipDown(){
        StartCoroutine(linesDown());
    }

    IEnumerator linesDown(){
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).GetComponent<Animator>().SetBool("flipDown", true);

        yield return new WaitForSeconds(0.7f);
        
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).GetComponent<Animator>().SetBool("flipDown", false);
    }
}
