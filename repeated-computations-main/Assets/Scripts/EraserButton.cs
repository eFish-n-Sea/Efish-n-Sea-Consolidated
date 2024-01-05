using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EraserButton : MonoBehaviour
{
    NotepadBehavior nb;

    public float width;

    void Start(){
        nb = GameObject.FindWithTag("Notepad").GetComponent<NotepadBehavior>();
    }

    public void OnButtonPress(){
        nb.lineWidth = width;
        nb.lineColor = Color.white;
    }
}