using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public bool playing = false;

    // Declare bucket (dock) prefabs (for later instantiation)
    public GameObject fishBucket;
    public GameObject sharkBucket;
    public GameObject turtleBucket;
    public GameObject trashBucket;

    // Declare fish, shark, turtle, and trash prefabs (for later instantiation)
    public List<GameObject> fish;
    public List<GameObject> sharks;
    public List<GameObject> turtles;
    public List<GameObject> special;
    public List<GameObject> trash;

    List<GameObject> dockBuckets = new List<GameObject>();
    List<string> fishingObjects = new List<string>();

    Vector3 fishingSceneCameraPosition = new Vector3(25.0f, 0.0f, -10.0f);
    Vector2 caughtObjectSpawnPosition = new Vector2(31.5f, -3.5f);
    Vector2 dockPosition = new Vector2(17.25f, -2.4f);

    public float timeBetweenFish;

    // Declare external components
    GameObject mainCamera;
    CartBehavior cart;
    GameObject curtain;
    Animator curtainAnimator;
    GameObject checkoutButton;
    GameObject scoreDisplay;
    TMP_Text scoreText;
    GameObject speechBubble;

    public int objectsCaught;
    public int score;
    public int threeStarScore;
    public int twoStarScore;
    public int oneStarScore;

    int displayedScore;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");
        cart = GameObject.FindWithTag("Cart").GetComponent<CartBehavior>();
        curtain = GameObject.FindWithTag("Curtain");
        curtainAnimator = curtain.GetComponent<Animator>();
        checkoutButton = GameObject.FindWithTag("Checkout Button");
        scoreDisplay = GameObject.FindWithTag("Score Display");
        scoreText = scoreDisplay.transform.GetChild(1).GetComponent<TMP_Text>();
        speechBubble = GameObject.FindWithTag("Speech Bubble");

        scoreDisplay.SetActive(false);
        // speechBubble.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (displayedScore < score) {
            displayedScore += 5;
            scoreText.text = displayedScore.ToString("n0");
        }

        if (objectsCaught == 21) {
            win();
        }
    }

    public void Checkout() {
        if (playing) {
            StartCoroutine(FishingSequence());
        }
    }

    public IEnumerator FishingSequence() {
        // Close curtain
        curtain.SetActive(true);
        curtainAnimator.SetBool("resetting", true);
        yield return new WaitForSeconds(0.5f);

        // Set objects and camera
        speechBubble.SetActive(false);
        checkoutButton.SetActive(false);
        scoreDisplay.SetActive(true);
        OrderFishingObjects();
        PlaceBucketsOnDock();
        mainCamera.transform.position = fishingSceneCameraPosition;
        yield return new WaitForSeconds(0.5f);

        // Open curtain
        curtainAnimator.SetBool("resetting", false);
        yield return new WaitForSeconds(1.0f);
        curtain.SetActive(false);

        // Start spawning fish/sharks/turtles/trash
        while (fishingObjects.Count > 0) {
            switch (fishingObjects[0]) {
                case "Fish":
                    Instantiate(fish[Random.Range(0, fish.Count)], caughtObjectSpawnPosition, Quaternion.identity);
                    break;
                case "Shark":
                    Instantiate(sharks[Random.Range(0, sharks.Count)], caughtObjectSpawnPosition, Quaternion.identity);
                    break;
                case "Turtle":
                    Instantiate(turtles[Random.Range(0, turtles.Count)], caughtObjectSpawnPosition, Quaternion.identity);
                    break;
                case "Special":
                    Instantiate(special[Random.Range(0, special.Count)], caughtObjectSpawnPosition, Quaternion.identity);
                    break;
                case "Trash":
                    Instantiate(trash[Random.Range(0, trash.Count)], caughtObjectSpawnPosition, Quaternion.identity);
                    break;
                default:
                    Debug.Log("Fishing object not recognized.");
                    break;
            }

            fishingObjects.RemoveAt(0);

            yield return new WaitForSeconds(timeBetweenFish);
        }
    }

    void PlaceBucketsOnDock() {
        List<string> bucketTags = cart.GetBucketTags();
        
        float bucketSpacingX = 1.25f;
        float bucketPositionX = 19.0f + bucketSpacingX * (5 - bucketTags.Count);
        float bucketPositionY = -2.4f;

        for (int i = 0; i < bucketTags.Count; i++) {
            Debug.Log("Placing " + bucketTags[i] + " on dock.");

            switch (bucketTags[i]) {
                case "Fish Bucket":
                    dockBuckets.Add(Instantiate(fishBucket, new Vector2(bucketPositionX + (bucketSpacingX * i), bucketPositionY), Quaternion.identity));
                    break;
                case "Shark Bucket":
                    dockBuckets.Add(Instantiate(sharkBucket, new Vector2(bucketPositionX + (bucketSpacingX * i), bucketPositionY), Quaternion.identity));
                    break;
                case "Turtle Bucket":
                    dockBuckets.Add(Instantiate(turtleBucket, new Vector2(bucketPositionX + (bucketSpacingX * i), bucketPositionY), Quaternion.identity));
                    break;
                case "Trash Bucket":
                    dockBuckets.Add(Instantiate(trashBucket, new Vector2(bucketPositionX + (bucketSpacingX * i), bucketPositionY), Quaternion.identity));
                    break;
                default:
                    Debug.Log("Bucket tag not recognized.");
                    break;
            }
        }
    }

    // Determines the order of objects which are caught while fishing (currently static, to be randomized)
    void OrderFishingObjects() {
        List<string> bucketTags = cart.GetBucketTags();

        fishingObjects.Add("Fish");
        fishingObjects.Add("Fish");
        fishingObjects.Add("Trash");
        fishingObjects.Add("Fish");
        fishingObjects.Add("Trash");
        fishingObjects.Add("Trash");
        fishingObjects.Add("Fish");
        // fishingObjects.Add("Shark");
        fishingObjects.Add("Trash");
        fishingObjects.Add("Fish");
        fishingObjects.Add("Trash");
        fishingObjects.Add("Trash");
        fishingObjects.Add("Trash");
        fishingObjects.Add("Fish");

        // fishingObjects.Add("Turtle");
        if (bucketTags.Contains("Shark Bucket") && !bucketTags.Contains("Turtle Bucket")) {
            fishingObjects.Add("Turtle");
        } else if (!bucketTags.Contains("Shark Bucket") && bucketTags.Contains("Turtle Bucket")) {
            fishingObjects.Add("Shark");
        } else {
            fishingObjects.Add("Special");
        }
        
        
        fishingObjects.Add("Fish");
        fishingObjects.Add("Trash");
        fishingObjects.Add("Trash");
        fishingObjects.Add("Fish");
        fishingObjects.Add("Trash");
        fishingObjects.Add("Fish");
        fishingObjects.Add("Fish");
    }

    // Returns the position of the closest (to the fisher), non-full, matching (fish, trash, etc.) bucket,
    // or the position of the open space on the dock, if no buckets match
    public Vector2 GetClosestBucketPosition(string caughtObjectTag) {
        for (int i = dockBuckets.Count - 1; i >= 0; i--) {
            GameObject db = dockBuckets[i];
            DockBucketBehavior dbb = db.GetComponent<DockBucketBehavior>();

            if (!dbb.IsFull()) {
                if (db.tag == "Fish Bucket" && caughtObjectTag == "Fish") {
                    dbb.AddItem();
                    return db.transform.position;
                } else if (db.tag == "Shark Bucket" && caughtObjectTag == "Shark") {
                    dbb.AddItem();
                    return db.transform.position;
                } else if (db.tag == "Turtle Bucket" && caughtObjectTag == "Turtle") {
                    dbb.AddItem();
                    return db.transform.position;
                } else if (db.tag == "Trash Bucket" && caughtObjectTag == "Trash") {
                    dbb.AddItem();
                    return db.transform.position;
                }
            }
        }

        return dockPosition;
    }
    
    void win() {
        int stars;
        string tips;

        if (score >= threeStarScore) {
            stars = 3;
            tips = "Great job! You got a perfect score, which means you had a bucket for everything we caught! You were ready for anything, good thinking!";
        }
        else if (score >= twoStarScore) {
            stars = 2;
            tips = "Good work! You had a bucket for almost everything we caught, but some things slipped by. Try to make sure we have enough room for every type of fish next time.";
        }
        else if (score >= oneStarScore) {
            stars = 1;
            tips = "Nice try! You had a bucket for some of the things we caught, but a lot of things slipped by. Try to make sure we have enough room for every type of fish next time.";
        }
        else {
            stars = 0;
            tips = "Uh oh! We didn't have a bucket for a lot of the things we caught, so they went back into the lagoon! Try choosing more types of buckets next time.";
        }

        StartCoroutine(GameObject.FindWithTag("Results").GetComponent<results>().win(stars, tips));
    }

    public void AddPoints(int newPoints) {
        score += newPoints;
    }
}
