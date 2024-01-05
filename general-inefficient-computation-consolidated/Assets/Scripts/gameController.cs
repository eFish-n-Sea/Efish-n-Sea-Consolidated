using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gameController : MonoBehaviour
{
    public GameObject[] fishs;
    public List<GameObject> fishAlive, policeAlive;
    public GameObject criminal, police, wrongFishPopUp;
    public float spacing, xStartPoint, xSecondRow, xThirdRow, yStartPoint, ySecondRow, yThirdRow;
    public float upperBound, lowerBound, leftBound, rightBound; 
    public int playerScore;
    public bool swimming, sorting, firstPress, getAwayGlobal, startSequenceHappening, openingSequenceHappening, playing;
    public Color red, orange, yellow, green, blue, violet;
    public Vector2 policeSpawn; 
    public TMP_Text scoreText, criminalName;
    List<string> fishNames = new List<string> {"Dory", "Bubbles", "Finley", "Splash", "Buddy", "Flash", "Sunny", "Squirt", "Ziggy",
                           "Sushi", "Sashimi", "Wasabi", "Sable", "Pepper", "Ginger", "Oscar", "Guppy", "Finn",
                           "Jaws", "Sharky", "Moby", "Ninja", "Jelly", "Pebbles", "Zippy", "Zigzag", "Sparkle", "Glitter",
                           "Neon", "Rainbow", "Sapphire", "Opal", "Topaz", "Ruby", "Emerald", "Pearl", "Coral", "Onyx",
                           "Titanium", "Steel", "Platinum", "Silver", "Gold", "Bronze", "Copper", "Rusty", "Lucky", "Happy",
                           "Sunny", "Cloudy", "Stormy", "Wavy", "Breezy", "Sandy", "Rocky", "Canyon", "River", "Stream",
                           "Tsunami", "Whirlpool", "Tide", "Current", "Drift", "Flint", "Pebble", "Stone", "Rock", "Cave",
                           "Grotto", "Lagoon", "Reef", "Cliff", "Coast", "Shore", "Bay", "Harbor", "Anemone"};
    
    // Start is called before the first frame update
    void Start()
    {
        startOpeningSequence();
    }

    void populate() 
    {
        for(int i = 0; i < 72; i++)
        {
            int randIndex = Random.Range(0, fishs.Length);
            fishAlive.Add(Instantiate(fishs[randIndex], Vector2.zero, Quaternion.identity));
        }
    }
    void assignNames()
    {
        foreach(GameObject fish in fishAlive)
        {
            int randIndex = Random.Range(0, fishNames.Count);
            fish.GetComponent<fishBehavior>().handle = fishNames[randIndex];
            fishNames.RemoveAt(randIndex);
        }
    }
    void colorize()
    {
        for(int i = 0; i < fishAlive.Count; i++)
        {
            int min = 0, max = 6;
            int randIndex = Random.Range(min, max);
            switch (randIndex)
            {
                case 0:
                    foreach(SpriteRenderer sprite in fishAlive[i].GetComponentsInChildren<SpriteRenderer>())
                    {
                        if(sprite.sprite.name == "eye-opened" || sprite.sprite.name == "eyes-opened")
                        {
                            continue;
                        }
                        sprite.color = red;  
                    }
                    fishAlive[i].GetComponent<fishBehavior>().color = "a red";
                    break;
                case 1:
                    foreach(SpriteRenderer sprite in fishAlive[i].GetComponentsInChildren<SpriteRenderer>())
                    {
                        if(sprite.sprite.name == "eye-opened" || sprite.sprite.name == "eyes-opened")
                        {
                            continue;
                        }
                        sprite.color = orange;
                    }
                    fishAlive[i].GetComponent<fishBehavior>().color = "b orange";
                    break;
                case 2:
                    foreach(SpriteRenderer sprite in fishAlive[i].GetComponentsInChildren<SpriteRenderer>())
                    {
                        if(sprite.sprite.name == "eye-opened" || sprite.sprite.name == "eyes-opened")
                        {
                            continue;
                        }
                        sprite.color = yellow;
                    }
                    fishAlive[i].GetComponent<fishBehavior>().color = "c yellow";
                    break;
                case 3:
                    foreach(SpriteRenderer sprite in fishAlive[i].GetComponentsInChildren<SpriteRenderer>())
                    {
                        if(sprite.sprite.name == "eye-opened" || sprite.sprite.name == "eyes-opened")
                        {
                            continue;
                        }
                        sprite.color = green;
                    }
                    fishAlive[i].GetComponent<fishBehavior>().color = "d green";
                    break;
                case 4:
                    foreach(SpriteRenderer sprite in fishAlive[i].GetComponentsInChildren<SpriteRenderer>())
                    {
                        if(sprite.sprite.name == "eye-opened" || sprite.sprite.name == "eyes-opened")
                        {
                            continue;
                        }
                        sprite.color = blue;
                    }
                    fishAlive[i].GetComponent<fishBehavior>().color = "e blue";
                    break;
                case 5:
                    foreach(SpriteRenderer sprite in fishAlive[i].GetComponentsInChildren<SpriteRenderer>())
                    {
                        if(sprite.sprite.name == "eye-opened" || sprite.sprite.name == "eyes-opened")
                        {
                            continue;
                        }
                        sprite.color = violet;
                    }
                    fishAlive[i].GetComponent<fishBehavior>().color = "f violet";
                    break;
            }
        }
    }

    public void fishPosition()
    {
        float x = xStartPoint;
        float y = yStartPoint;
        float index = 0;

        foreach(GameObject fish in fishAlive)
        {
            if (index % 3 == 0)
            {
                fish.transform.position = new Vector2(x, y);
            }
            else if (index % 3 == 1)
            {
                fish.transform.position = new Vector2(x + xSecondRow, y + ySecondRow);
            }
            else if (index % 3 == 2)
            {
                fish.transform.position = new Vector2(x + xThirdRow, y + yThirdRow);
            }
            x += spacing/3;
            index++;
        }
    }

    public void pickCriminal()
    {
        int randIndex = Random.Range(0, fishAlive.Count);
        if(fishAlive[randIndex].activeSelf)
        {
            if(fishAlive[randIndex].GetComponent<fishBehavior>().species == "flounder")
            {
                criminal = Instantiate(fishAlive[randIndex], new Vector2(0f, 3.3333333f), Quaternion.identity);
            }
            else if(fishAlive[randIndex].GetComponent<fishBehavior>().species == "clownfish")
            {
                criminal = Instantiate(fishAlive[randIndex], new Vector2(0f, 3.53f), Quaternion.identity);
            }
            else if(fishAlive[randIndex].GetComponent<fishBehavior>().species == "spikefish")
            {
                criminal = Instantiate(fishAlive[randIndex], new Vector2(0.05f, 3.44f), Quaternion.identity);
            }
            else if(fishAlive[randIndex].GetComponent<fishBehavior>().species == "butterfly")
            {
                criminal = Instantiate(fishAlive[randIndex], new Vector2(0f, 3.3333333f), Quaternion.identity);
            }
            else if(fishAlive[randIndex].GetComponent<fishBehavior>().species == "longangel")
            {
                criminal = Instantiate(fishAlive[randIndex], new Vector2(0f, 3.51f), Quaternion.identity);
            }
            else if(fishAlive[randIndex].GetComponent<fishBehavior>().species == "roundangel")
            {
                criminal = Instantiate(fishAlive[randIndex], new Vector2(.16f, 3.31f), Quaternion.identity);
            }
            else if(fishAlive[randIndex].GetComponent<fishBehavior>().species == "seahorse")
            {
                criminal = Instantiate(fishAlive[randIndex], new Vector2(.8f, 3.5f), Quaternion.identity);
                criminal.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 41.98f));
            }
            else if(fishAlive[randIndex].GetComponent<fishBehavior>().species == "discus")
            {
                criminal = Instantiate(fishAlive[randIndex], new Vector2(0f, 3.3333333f), Quaternion.identity);
            }
            else if(fishAlive[randIndex].GetComponent<fishBehavior>().species == "angler")
            {
                criminal = Instantiate(fishAlive[randIndex], new Vector2(-.13f, 3.11f), Quaternion.identity);
            }
        }
        else
        {
            pickCriminal();
        }
        criminal.GetComponent<Collider2D>().enabled = false;   
        criminal.GetComponent<fishBehavior>().enabled = false;  
        //foreach(SpriteRenderer sprite in fishAlive[i].GetComponentsInChildren<SpriteRenderer>())
        foreach(SpriteRenderer sprite in criminal.GetComponentsInChildren<SpriteRenderer>())
        {
            sprite.sortingLayerName = "criminal";
        }
        criminalName.text = criminal.GetComponent<fishBehavior>().handle;
    }

    public void displayScore()
    {
        scoreText.text = playerScore.ToString();
    }

    public void initChaseSequence(GameObject f)
    {
        playerScore += 1;
        displayScore();
        StartCoroutine(chaseSequenceSpawnPolice(f));
    }

    IEnumerator chaseSequenceSpawnPolice(GameObject f)
    {
        if(f.GetComponent<fishBehavior>().yFlip == 180)
        {
            for(int i = 0; i < 2; i++)
            {
                yield return new WaitForSeconds(.25f);
                policeAlive.Add(Instantiate(police, new Vector2(-policeSpawn.x, policeSpawn.y), Quaternion.identity));
                policeAlive[i].GetComponent<policeBehavior>().flipped = true;
                policeAlive[i].GetComponent<policeBehavior>().transform.rotation = transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                policeAlive[i].GetComponent<policeBehavior>().startChaseSequence(f);
            }
        }
        else
        {
            for(int i = 0; i < 2; i++)
            {
                yield return new WaitForSeconds(.25f);
                policeAlive.Add(Instantiate(police, policeSpawn, Quaternion.identity));
                policeAlive[i].GetComponent<policeBehavior>().startChaseSequence(f);
            }
        }
    }

    public void initRandPosition()
    {
        foreach(GameObject fish in fishAlive)
        {
            float y = Random.Range(lowerBound, upperBound);
            float x = Random.Range(leftBound, rightBound);
            int flip = Random.Range(0, 2);
            fish.transform.position = new Vector2(x, y);
            if(flip == 1)
            {
                fish.transform.rotation= Quaternion.Euler(0, -180, 0);
                fish.GetComponent<fishBehavior>().yFlip = 180;
            }
        }
    }

    public void startOpeningSequence()
    {
        openingSequenceHappening = true;
        populate();
        colorize();
        assignNames();
        initRandPosition();
        pickCriminal();
    }

    // Update is called once per frame
    void Update()
    {
        if(!(swimming || getAwayGlobal || openingSequenceHappening))
        {
            fishPosition();
        }
    }
}
