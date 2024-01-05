using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class results : MonoBehaviour
{
    public float buttonMinX, buttonMaxX, buttonMinY, buttonMaxY;
    GameObject tip;
    gameController gc;
     public void Start(){
       tip = GameObject.FindWithTag("Tips");
       gc = GameObject.FindWithTag("GameController").GetComponent<gameController>();
    }

    public IEnumerator win(int stars, string tips){
        Debug.Log(tip.tag);
        // tip.GetComponent<TMPro.TextMeshProUGUI>().text = "hello";
        // tipText.text = tips;
        tip.GetComponent<openTips>().Display(tips);
        while(gc.tipOpen)
        {
            yield return null;
        }
        yield return new WaitForSeconds(1);
        GetComponent<Animator>().SetBool("earned", true);
        for (int i = 0; i < stars && i < 3; i++){
            yield return new WaitForSeconds(1);
            transform.GetChild(i).GetComponent<Animator>().SetBool("earned", true);
        }
    }
}
