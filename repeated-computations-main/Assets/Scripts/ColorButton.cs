using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorButton : MonoBehaviour
{
    NotepadBehavior nb;

    Color color;

    void Start(){
        nb = GameObject.FindWithTag("Notepad").GetComponent<NotepadBehavior>();
        color = GetComponent<Image>().color;
    }

    public void OnButtonPress(){
        nb.lineColor = color;
    }
}
