using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotepadToggle : MonoBehaviour
{
    GameObject notepad, controls;
    NotepadBehavior nb;
    PlayerBehavior player;
    Animator curtain;
    Button self;
    GameController gc;

    void Start(){
        gc = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        notepad = GameObject.FindWithTag("Notepad");
        controls = GameObject.FindWithTag("Controls");
        nb = notepad.GetComponent<NotepadBehavior>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerBehavior>();
        curtain = GameObject.FindWithTag("Curtain").GetComponent<Animator>();
        self = GetComponent<Button>();

        controls.SetActive(false);
    }

    void Update(){
        self.interactable = gc.playing;
    }

    public void OnButtonPress(){
        bool set = !notepad.activeSelf;
        if (set){
            notepad.SetActive(true);
            nb.renderPage();
        }
        else{
            nb.savePage();
            notepad.SetActive(false);
        }
        controls.SetActive(set);
        player.inNotes = set;
        curtain.SetBool("darken", set);
    }
}