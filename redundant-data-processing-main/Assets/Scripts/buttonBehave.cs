using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonBehave : MonoBehaviour
{
    gameController gc;
    public int trucksSent;
    public int boxesSent;
    public int totalBoxes;
    public int oneStar, twoStar, threeStar;
    

    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.FindWithTag("GameController").GetComponent<gameController>();
        trucksSent = 0;
        boxesSent = 0;
    }

    public void OnButtonPress()
    { 
        Cursor.lockState = CursorLockMode.Locked;
        GameObject.FindWithTag("Door").GetComponent<Animator>().SetBool("resetTruck", true);
        StartCoroutine(deleteBoxes());
        trucksSent++;
    }

    IEnumerator deleteBoxes()
    {
        yield return new WaitForSeconds(1f);
        GameObject.FindWithTag("MainCamera").GetComponent<Animator>().SetBool("shakingScreen", true);
        yield return new WaitForSeconds(.5f);
        foreach(GameObject box in gc.placedBoxes)
        {
            box.SetActive(false);
            boxesSent++;
        }
        gc.placedBoxes.Clear();
        yield return new WaitForSeconds(2.5f);
        GameObject.FindWithTag("MainCamera").GetComponent<Animator>().SetBool("shakingScreen", false);
        yield return new WaitForSeconds(1f);
        GameObject.FindWithTag("Door").GetComponent<Animator>().SetBool("resetTruck", false);
        Cursor.lockState = CursorLockMode.None;
        if(boxesSent >= totalBoxes)
        {
            int stars = 0;
            if(trucksSent <= threeStar)
            {
                stars = 3;
            }
            else if(trucksSent <= twoStar)
            {
                stars = 2;
            }
            else if(trucksSent <= oneStar)
            {
                stars = 1;
            }
            string message;
            if (stars == 3){
                message = "Wow, good job at packing that truck up so <i>eFish'n'tly</i>! By cutting down on the number of trips the truck had to make back and forth between the two houses, you were able to " +
                "save a lot of resources! Thankfully, we were just moving to the next lagoon over, but imagine how big of a difference that would make if we were moving all the way across the Pacific Ocean!";
            }
            else if (stars >= 1){
                message = "Thanks for your help with packing up the moving truck! However, we probably could've done it in fewer trips if we left a little less empty space in the trucks. It takes a lot " +
                "of effort to get the truck from one house to the other, so even cutting down on one or two repeated drives would really help out a lot!";
            }
            else{
                message = "Thanks for your help with packing up the moving truck! We probably could've done it in quite a few less trips if we stuffed as many boxes as possible in the trucks. It takes a lot " +
                "of effort to get the truck from one house to the other, so even cutting down on a couple repeated drives would really make a huge difference!";
            }
            gc.playing = false;
            StartCoroutine(GameObject.FindWithTag("Results").GetComponent<results>().win(stars, message));
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Button>().interactable = !(gc.boxHeld || GameObject.FindWithTag("Door").GetComponent<Animator>().GetBool("resetTruck") || !gc.playing);
    }
}