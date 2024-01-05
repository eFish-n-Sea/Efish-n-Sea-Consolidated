using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class forwardButton : MonoBehaviour
{
    public gameController gc; 
    dialogueBehavior dialogueScript;
    public bool active;
    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.FindWithTag("GameController").GetComponent<gameController>();
        dialogueScript = GameObject.FindWithTag("shrimplockHolmes").GetComponentInChildren<dialogueBehavior>();
    }

    public void OnButtonPress()
    {
        gc.swimming = true;
        GameObject.FindWithTag("speciesButton").GetComponent<speciesButton>().active = true;
        GameObject.FindWithTag("nameButton").GetComponent<nameButton>().active = true;
        GameObject.FindWithTag("colorButton").GetComponent<colorButton>().active = true;
        dialogueScript.nextButtonDialogue();
        StartCoroutine(movementSequence());
    }

    IEnumerator movementSequence()
    {
        active = true;
        for(int i = 0; i < 36; i++)
        {
            if(gc.fishAlive[i].activeSelf)
            {
                gc.fishAlive[i].GetComponent<Animator>().SetBool("swim", true);
            }
        }
        yield return new WaitForSeconds(3.3f);
        foreach(GameObject fish in gc.fishAlive)
        {
            if(fish.activeSelf)
            {
                fish.GetComponent<Animator>().SetBool("swim", false);
            }
        }
        List<GameObject> first18 = gc.fishAlive.GetRange(0,18);
        gc.fishAlive.RemoveRange(0,18);
        gc.fishAlive.AddRange(first18);
        gc.swimming = false;
        active = false;
        GameObject.FindWithTag("speciesButton").GetComponent<speciesButton>().active = false;
        GameObject.FindWithTag("nameButton").GetComponent<nameButton>().active = false;
        GameObject.FindWithTag("colorButton").GetComponent<colorButton>().active = false;
    }
    // Update is called once per frame
    void Update()
    {
        GetComponent<Button>().interactable = (!active && !gc.getAwayGlobal && !gc.startSequenceHappening && !gc.openingSequenceHappening);
    }
}
