using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeechBubbleBehavior : MonoBehaviour
{
    Animator animator;
    TMP_Text speech;
    GameController gc;

    // public float writingSpeed;
    // bool cancelSpeech;

    string currentSpeechTag;
    public float startBuffer;
    bool inBuffer;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        speech = transform.GetChild(0).GetComponent<TMP_Text>();
        gc = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gc.playing) {
            if (startBuffer > 0) {
                startBuffer -= Time.deltaTime;
            } else {
                inBuffer = false;
            }
        }
    }

    public void UpdateSpeechWithTag(string hoverTag) {
        // if (gc.playing && !cancelSpeech) {
        if (hoverTag != currentSpeechTag && !inBuffer && gc.playing) {
            currentSpeechTag = hoverTag;
            switch (hoverTag) {
                case "Play":
                    StartCoroutine(UpdateSpeechTo("Aye there! Welcome to me shop! I got the finest selection of buckets here. Which one would you like?"));
                    inBuffer = true;
                    break;
                case "Fish Bucket":
                    StartCoroutine(UpdateSpeechTo("That there's a fish bucket. Good for holding all types of small fish, up to 5 in total."));
                    break;
                case "Shark Bucket":
                    StartCoroutine(UpdateSpeechTo("That one's a shark bucket. Big enough to hold 1 shark, but one is more than you're likely to see!"));
                    break;
                case "Turtle Bucket":
                    StartCoroutine(UpdateSpeechTo("This one's a turtle bucket. It'll hold 1 turtle, not that you'll be lucky enough to catch one!"));
                    break;
                case "Trash Bucket":
                    StartCoroutine(UpdateSpeechTo("This here's a trash bucket. Cans, bottles, bags, you name it. Holds up to 10 pieces of junk."));
                    break;
                case "Checkout Button":
                    StartCoroutine(UpdateSpeechTo("Are ya sure you're ready to checkout? Best double check that cart of yers, just in case!"));
                    break;
                default:
                    break;
            }
        }
    }

    // Old implementation of UpdateSpeechTo() with text typing
    // IEnumerator UpdateSpeechTo(string newSpeech) {
    //     // speech.text = newSpeech;
    //     cancelSpeech = true;

    //     yield return new WaitForSeconds(writingSpeed * 2);

    //     cancelSpeech = false;
    //     speech.text = "";

    //     foreach (char c in newSpeech)
    //     {
    //         if (cancelSpeech) {
    //             cancelSpeech = false;
    //             yield break;
    //         }
    //         yield return new WaitForSeconds(writingSpeed);
    //         speech.text += c;
    //     }
    // }

    // New implementation of UpdateSpeechTo() with speech bubble growing
    IEnumerator UpdateSpeechTo(string newSpeech) {
        animator.SetTrigger("grow");
        yield return new WaitForSeconds(0.0f);
        speech.text = newSpeech;
    }
}
