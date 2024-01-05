using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Redo : MonoBehaviour
{
    NotepadBehavior nb;
    Button self;

    void Start(){
        nb = GameObject.FindWithTag("Notepad").GetComponent<NotepadBehavior>();
        self = GetComponent<Button>();
    }

    void Update(){
        self.interactable = nb.undone.Count > 0;
    }

    public void OnButtonPress(){
        nb.redo();
    }
}
