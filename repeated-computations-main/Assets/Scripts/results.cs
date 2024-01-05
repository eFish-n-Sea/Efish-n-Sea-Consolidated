using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class results : MonoBehaviour
{
    public TMP_Text tipText;

    public IEnumerator win(int stars, string tips){
        tipText.text = tips;
        GetComponent<Animator>().SetBool("earned", true);
        for (int i = 0; i < stars && i < 3; i++){
            yield return new WaitForSeconds(1);
            transform.GetChild(i).GetComponent<Animator>().SetBool("earned", true);
        }
    }
}
