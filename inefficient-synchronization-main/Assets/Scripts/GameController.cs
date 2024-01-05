using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // If the game is playing
    public bool playing = false;

    // Maximum times allowed for each star rating
    public int threeStarTime, twoStarTime, oneStarTime;

    // External components
    ForkBehavior fork;
    BowlBehavior leftBowl, rightBowl;
    FishBehavior leftFish, rightFish;

    // Start is called before the first frame update
    void Start()
    {
        fork = GameObject.FindWithTag("Fork").GetComponent<ForkBehavior>();
        leftBowl = GameObject.FindWithTag("LeftBowl").GetComponent<BowlBehavior>();
        rightBowl = GameObject.FindWithTag("RightBowl").GetComponent<BowlBehavior>();
        leftFish = GameObject.FindWithTag("LeftFish").GetComponent<FishBehavior>();
        rightFish = GameObject.FindWithTag("RightFish").GetComponent<FishBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playing && leftBowl.GetBowlFill() == 0 && rightBowl.GetBowlFill() == 0 && !leftFish.IsChewing() && !rightFish.IsChewing() && !fork.inFeedingAnimation) {
            win();
        }
    }

    void win() {
        playing = false;

        float time = GameObject.FindWithTag("Timer").GetComponent<timer>().win();
        int stars;
        string tips;

        if (time < threeStarTime) {
            stars = 3;
            tips = "Wow! You two got a perfect score! You filled up both of the pufferfish, and you did it really fast too! Amazing teamwork!";
        }
        else if (time < twoStarTime) {
            stars = 2;
            tips = "Good job! You two filled up the pufferfish pretty fast, but I think you can go even faster! Make sure each pufferfish is always chewing some mac & cheese!";
        }
        else if (time < oneStarTime) {
            stars = 1;
            tips = "Nice try! You two filled up the pufferfish, but I think you can do it a little faster! Make sure you're sharing the fork!";
        }
        else {
            stars = 0;
            tips = "Uh oh! You two filled up the pufferfish, but you took a while to do it. Try to make sure you're always feeding one of the fish!";
        }

        StartCoroutine(GameObject.FindWithTag("Results").GetComponent<results>().win(stars, tips));
    }

    int stars(float time) {
        if (time < threeStarTime)
            return 3;
        else if (time < twoStarTime)
            return 2;
        else if (time < oneStarTime)
            return 1;
        else
            return 0;
    }
}
