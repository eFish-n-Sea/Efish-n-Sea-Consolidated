using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class play : MonoBehaviour
{
    GameObject ins;
    // GameObject speechBubble;
    SpeechBubbleBehavior sbb;

    public void Start(){
        ins = GameObject.FindWithTag("Instructions");
        // speechBubble = GameObject.FindWithTag("Speech Bubble");
        sbb = GameObject.FindWithTag("Speech Bubble").GetComponent<SpeechBubbleBehavior>();
    }

    public void OnButtonPress(){
        ins.GetComponent<Animator>().SetBool("Started", true);
        StartCoroutine(disappear());
    }

    public IEnumerator disappear() {
        yield return new WaitForSeconds(1);
        // Replace following line with however your game "starts".
        GameObject.FindWithTag("GameController").GetComponent<GameController>().playing = true;
        // speechBubble.SetActive(true);
        // speechBubble.GetComponent<SpeechBubbleBehavior>().UpdateSpeechWithTag("Play");
        sbb.UpdateSpeechWithTag("Play");
        ins.SetActive(false);
    }
}
