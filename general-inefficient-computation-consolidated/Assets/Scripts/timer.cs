using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class timer : MonoBehaviour
{
    public float time;
    int ms, seconds, minutes;
    public int threeStars, twoStars, oneStar;
    bool won;
    public TMP_Text stopWatchText;
    gameController gc;
    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.FindWithTag("GameController").GetComponent<gameController>();
        time = 121;
        timerCalc();
    }

    void timerCalc()
     {
        string fmt = "00";
        time -= Time.deltaTime;
        seconds = (int) (time % 60);
        minutes = (int) (time / 60);
        stopWatchText.text = "<mspace=0.55em>" + minutes + "</mspace>:<mspace=0.55em>" + seconds.ToString(fmt);
    }

    // Update is called once per frame
    void Update()
    {
        if((time >= 0) && !gc.startSequenceHappening)
        {
            timerCalc();
        }
        else if((time <= 0) && !won)
        {
            if(gc.playerScore >= threeStars)
            {
                StartCoroutine(GameObject.FindWithTag("Results").GetComponent<results>().win(3,  "Wow! Great job! You helped Shrimplock Holmes Catch " +gc.playerScore.ToString()+ " criminals! That's way more than we expected. Our sea's will be a lot safer thanks to you! I don't think there's any tips i could have for you."));
            }
            else if(gc.playerScore >= twoStars && gc.playerScore <= threeStars)
            {
                StartCoroutine(GameObject.FindWithTag("Results").GetComponent<results>().win(2,  "Good job! You caught " +gc.playerScore.ToString()+ " criminals. I think there are still more out there though. Try again and make sure to use the sort buttons whenever the criminal is going to be in the front of the lines. For example if a criminal named Alex is chosen, sort by name and he will always be at the front!"));
            }
            else if(gc.playerScore >= oneStar && gc.playerScore <= twoStars)
            {
                StartCoroutine(GameObject.FindWithTag("Results").GetComponent<results>().win(1,  "Good job! You caught " +gc.playerScore.ToString()+ " criminals. I think there are still more out there though. Try again and make sure to use the sort buttons whenever the criminal is going to be in the front of the lines. For example if a criminal named Alex is chosen, sort by name and he will always be at the front!"));
            }
            else
            {
                StartCoroutine(GameObject.FindWithTag("Results").GetComponent<results>().win(0,  "You didn't catch any criminals? Are you on their team or did you get distracted? Try again and make sure to read the instructions so you know what to do."));
            }
            won = true;
            gc.startSequenceHappening = true;
        }
    }
}
