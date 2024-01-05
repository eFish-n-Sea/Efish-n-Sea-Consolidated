using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public bool playing = false;
    public bool boxPlaced;
    public bool inAnimation;
    // public string curBoxTag = "";
    // public bool itemInZone = false;

    int points;

    // Declare external components
    TMP_Text score;
    BoxBehavior currentBox;
    // fallingobject fo;

    int displayedPoints;

    // Start is called before the first frame update
    void Start()
    {
        // Assign external components
        score = GameObject.FindWithTag("Score").GetComponent<TMP_Text>();
        
        points = 0;
        score.text = "Points: " + points.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (displayedPoints < points) {
            displayedPoints += 100;
            score.text = displayedPoints.ToString("n0");
        }
    }

    public void win()
    {
        playing = false;
        StartCoroutine(GameObject.FindWithTag("Results").GetComponent<results>().win(stars(),"Nice Try! When selecting boxes make sure that the boxes fit the items appropiatly."));
    }

    int stars()
    {
        if (points >= 900000)
        {
            return 3;
        }
        else if (points >= 600000)
        {
            return 2;
        }
        else if (points >= 300000)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public void addPoints(int mult)
    {
        points = points + (25000 * mult);
        // score.text = "Points: " + points.ToString("n0");
    }

    public void setPlace(string tag)
    {
        // if (tag != curBoxTag && curBoxTag != "")
        if (boxPlaced && !currentBox.boxanimation)
        {
            currentBox.transform.position = currentBox.origin;
            currentBox = GameObject.FindWithTag(tag).GetComponent<BoxBehavior>();
            // curBoxTag = tag;
            // boxPlaced = true;
        }
        else
        {
            currentBox = GameObject.FindWithTag(tag).GetComponent<BoxBehavior>();
            // curBoxTag = tag;
            boxPlaced = true;
        }
    }
}

