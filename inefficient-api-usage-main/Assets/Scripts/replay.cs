using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class replay : MonoBehaviour
{
    GameObject curtain;

    void Start(){
        curtain = GameObject.FindWithTag("Curtain");
        StartCoroutine(start());
    }

    public void OnButtonPress(){
        StartCoroutine(restart());
    }

    IEnumerator start(){
        yield return new WaitForSeconds(0.5f);
        curtain.SetActive(false);
    }

    IEnumerator restart(){
        curtain.SetActive(true);
        curtain.GetComponent<Animator>().SetBool("resetting", true);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(0);
    }
}
