using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class dialogueBehavior : MonoBehaviour
{
    public TMP_Text dialogueText;
    public gameController gc; 
    public float writingSpeed;
    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.FindWithTag("GameController").GetComponent<gameController>();
    }

    public void hoveredFishDialogue(string name)
    {
        clearDialogue();
        StartCoroutine(Writing("Thats " + name));
    }
    
    public void chaseDialogue()
    {
        clearDialogue();
        int min = 0;
        int max = 5;
        int randDialogue = Random.Range(min, max);
        switch(randDialogue)
        {
            case 0:
                StartCoroutine(Writing("They're getting away!!"));
                break;
            case 1:
                StartCoroutine(Writing("After 'em!!!"));
                break;
            case 2:
                StartCoroutine(Writing("Get 'em Shrimp Police!!"));
                break;
            case 3:
                StartCoroutine(Writing("GO GO GO!!"));
                break;
            case 4:
                StartCoroutine(Writing("They're trying to escape!"));
                break;
        }
    }

    public void nextButtonDialogue()
    {
        clearDialogue();
        int min = 0;
        int max = 5;
        int randDialogue = Random.Range(min, max);
        switch(randDialogue)
        {
            case 0:
                StartCoroutine(Writing("Move along."));
                break;
            case 1:
                StartCoroutine(Writing("NEXT!!!"));
                break;
            case 2:
                StartCoroutine(Writing("Get it movin' fishies!"));
                break;
            case 3:
                StartCoroutine(Writing("Hurry it up!"));
                break;
            case 4:
                StartCoroutine(Writing("Start movin!"));
                break;
        }
    }

    public void sortButtonsDialogue(string sortName)
    {
        clearDialogue();
        
        int min = 0;
        int max = 6;
        int randDialogue = Random.Range(min, max);
        switch(randDialogue)
        {
            case 1:
                StartCoroutine(Writing("Line 'em up in " + sortName + " order!"));
                break;
            case 2:
                StartCoroutine(Writing("C'mon fishies, get in " + sortName + " order!"));
                break;
            case 3:
                StartCoroutine(Writing("GET IN " + sortName.ToUpper() + " ORDER!"));
                break;
            default:
                StartCoroutine(Writing("Alright fishies get in " + sortName + " order!"));
                break;
        }
    }

    void clearDialogue()
    {
        dialogueText.text = "";
    }

    public void startSequenceDialogue()
    {
        GameObject.FindWithTag("shrimplockHolmes").GetComponent<Animator>().SetBool("talking", true);
        StartCoroutine(startSequence());
    }

    IEnumerator Writing(string dialogue)
    {
        //this makes it so that the text is typed letter by letter, leads to a lot of issues but is definetly a better experience. might come back to it.
        // foreach(char c in dialogue)
        // {
        //     yield return new WaitForSeconds(writingSpeed);
        //     dialogueText.text += c;
        // }
        GameObject.FindWithTag("shrimplockHolmes").GetComponent<Animator>().SetBool("talking", true);
        dialogueText.text = dialogue;
        yield return new WaitForSeconds(.5f);
        GameObject.FindWithTag("shrimplockHolmes").GetComponent<Animator>().SetBool("talking", false);
    }
    IEnumerator startSequence()
    {
        dialogueText.text = "Hello! Im Shrimplock Holmes!";
        yield return new WaitForSeconds(2.5f);
        dialogueText.text = "Help me catch the criminals!";
        yield return new WaitForSeconds(2.5f);
        dialogueText.text = "They'll be the ones shown at the top!";
        yield return new WaitForSeconds(2.5f);
        dialogueText.text = "Hover over a fish to see if its name matches.";
        yield return new WaitForSeconds(2.5f);
        gc.startSequenceHappening = false;
        GameObject.FindWithTag("shrimplockHolmes").GetComponent<Animator>().SetBool("talking", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
