using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SizeButton : MonoBehaviour
{
    NotepadBehavior nb;

    public float width;

    void Start(){
        nb = GameObject.FindWithTag("Notepad").GetComponent<NotepadBehavior>();
    }

    public void OnButtonPress(){
        nb.lineWidth = width;
    }
}
