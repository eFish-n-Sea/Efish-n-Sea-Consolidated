using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class play : MonoBehaviour
{
    GameObject ins;

    public void Start(){
        ins = GameObject.FindWithTag("Instructions");
    }

    public void OnButtonPress(){
        ins.GetComponent<Animator>().SetBool("Started", true);
        StartCoroutine(disappear());
    }

    public IEnumerator disappear(){
        yield return new WaitForSeconds(1);
        // Replace following line with however your game "starts".
        GameObject.FindWithTag("GameController").GetComponent<GameController>().playing = true;
        ins.SetActive(false);
    }
}
