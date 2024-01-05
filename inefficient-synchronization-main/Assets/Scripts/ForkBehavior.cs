using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkBehavior : MonoBehaviour
{
    // Movement & rotation speed during controls & animations of fork
    public float controlSpeed, bowlToFishSpeed, toOriginSpeed;
    float controlRotationSpeed, bowlToFishRotationSpeed, fishToOriginRotationSpeed, bowlToOriginRotationSpeed;

    // Duration and speed of jiggle animation
    public float jiggleTime, jiggleRotationSpeed;

    // Rigidbody component of fork (for applying velocity)
    Rigidbody2D rb;

    // External components
    GameController gc;
    SpriteRenderer macSR;
    BowlBehavior leftBowl, rightBowl;
    FishBehavior leftFish, rightFish;

    // Vector2's for left and right velocities & stop
    Vector2 controlLeft, controlRight, bowlToFishLeft, bowlToFishRight, toOriginLeft, toOriginRight;
    Vector2 stop = new Vector2(0.0f, 0.0f);

    // If fork is in jiggle animation
    bool inJiggleAnimation = false;

    // Timers for each phase of jiggle
    float subJiggleTimeA, subJiggleTimeB, subJiggleTimeC, subJiggleTimeD;

    // If fork is in feeding animation
    public bool inFeedingAnimation = false;

    // Animation phases (for rotating fork appropriately)
    bool leftBowlToFish = false, leftFishToOrigin = false, leftBowlToOrigin = false;
    bool rightBowlToFish = false, rightFishToOrigin = false, rightBowlToOrigin = false;

    // Start is called before the first frame update
    void Start()
    {
        // Assign rotation speeds (adjusted for movement speeds)
        controlRotationSpeed = (180 / 5) * controlSpeed;
        bowlToFishRotationSpeed = (135f / 2.1f) * bowlToFishSpeed;
        fishToOriginRotationSpeed = (70f / 7.1f) * toOriginSpeed;
        bowlToOriginRotationSpeed = (180 / 5) * toOriginSpeed;

        // Assign rigidbody component for fork
        rb = GetComponent<Rigidbody2D>();

        // Assign external components
        gc = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        macSR = GameObject.FindWithTag("Mac").GetComponent<SpriteRenderer>();
        leftBowl = GameObject.FindWithTag("LeftBowl").GetComponent<BowlBehavior>();
        rightBowl = GameObject.FindWithTag("RightBowl").GetComponent<BowlBehavior>();
        leftFish = GameObject.FindWithTag("LeftFish").GetComponent<FishBehavior>();
        rightFish = GameObject.FindWithTag("RightFish").GetComponent<FishBehavior>();

        // Assign vector2's for left and right velocities (with speed multiplier)
        controlLeft = new Vector2(-1.0f, 0.0f) * controlSpeed;
        controlRight = new Vector2(1.0f, 0.0f) * controlSpeed;
        bowlToFishLeft = new Vector2(-1.0f, 0.0f) * bowlToFishSpeed;
        bowlToFishRight = new Vector2(1.0f, 0.0f) * bowlToFishSpeed;
        toOriginLeft = new Vector2(-1.0f, 0.0f) * toOriginSpeed;
        toOriginRight = new Vector2(1.0f, 0.0f) * toOriginSpeed;

        // Assign sub jiggle phase times
        SetSubJiggleTimers();

        // Disable mac sprite to start
        macSR.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gc.playing) {
            // If fork is in jiggle animation, perform jiggle animation
            if (inJiggleAnimation) {
                if (subJiggleTimeA > 0) {
                    subJiggleTimeA -= Time.deltaTime;
                    rb.rotation += jiggleRotationSpeed * Time.deltaTime;
                } else if (subJiggleTimeB > 0) {
                    subJiggleTimeB -= Time.deltaTime;
                    rb.rotation -= jiggleRotationSpeed * Time.deltaTime;
                } else if (subJiggleTimeC > 0) {
                    subJiggleTimeC -= Time.deltaTime;
                    rb.rotation += jiggleRotationSpeed * Time.deltaTime;
                } else if (subJiggleTimeD > 0) {
                    subJiggleTimeD -= Time.deltaTime;
                    rb.rotation -= jiggleRotationSpeed * Time.deltaTime;
                } else {
                    inJiggleAnimation = false;
                    SetSubJiggleTimers();
                }
            // If fork is in feeding animation, rotate according to animation phase
            } else if (inFeedingAnimation) {
                if (leftBowlToFish) {
                    rb.rotation -= bowlToFishRotationSpeed * Time.deltaTime;
                } else if (rightBowlToFish) {
                    rb.rotation += bowlToFishRotationSpeed * Time.deltaTime;
                } else if (leftFishToOrigin) {
                    rb.rotation -= fishToOriginRotationSpeed * Time.deltaTime;
                } else if (rightFishToOrigin) {
                    rb.rotation += fishToOriginRotationSpeed * Time.deltaTime;
                } else if (leftBowlToOrigin) {
                    rb.rotation -= bowlToOriginRotationSpeed * Time.deltaTime;
                } else if (rightBowlToOrigin) {
                    rb.rotation += bowlToOriginRotationSpeed * Time.deltaTime;
                }
            // If fork is not in any animation, check player inputs
            } else {
                if (Input.GetKey("q") && Input.GetKey("p")) {
                    // Jiggle
                    rb.velocity = stop;
                    inJiggleAnimation = true;
                } else if (Input.GetKey("q")) {
                    rb.velocity = controlLeft;
                    rb.rotation += controlRotationSpeed * Time.deltaTime;
                } else if (Input.GetKey("p")) {
                    rb.velocity = controlRight;
                    rb.rotation -= controlRotationSpeed * Time.deltaTime;
                } else {
                    rb.velocity = stop;
                }
            }
        }
    }

    void SetSubJiggleTimers() {
        subJiggleTimeA = jiggleTime / 6;
        subJiggleTimeB = subJiggleTimeA * 2;
        subJiggleTimeC = subJiggleTimeA * 2;
        subJiggleTimeD = subJiggleTimeA;
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "LeftBowl" && !inFeedingAnimation)
        {
            StartCoroutine(TakeFromLeftBowl());
        }
        else if (col.tag == "RightBowl" && !inFeedingAnimation)
        {
            StartCoroutine(TakeFromRightBowl());
        }
        else if (col.tag == "LeftFish")
        {
            StartCoroutine(FeedLeftFish());
        }
        else if (col.tag == "RightFish")
        {
            StartCoroutine(FeedRightFish());
        }
        else if (col.tag == "ForkOrigin" && inFeedingAnimation)
        {
            rb.velocity = stop;
            rb.position = new Vector2 (0.0f, 1.0f);
            rb.rotation = 0.0f;
            inFeedingAnimation = false;
            leftFishToOrigin = false;
            rightFishToOrigin = false;
            leftBowlToOrigin = false;
            rightBowlToOrigin = false;
        }
    }

    IEnumerator TakeFromLeftBowl() {
        if (leftBowl.GetBowlFill() != 0) {
            rb.velocity = stop;
            LoadFork();
            leftBowl.TakeFromBowl();

            // Wait for fish to be done chewing
            yield return new WaitUntil(() => !leftFish.IsChewing());

            leftBowlToFish = true;
            rb.velocity = bowlToFishLeft;
            leftFish.OpenMouth();
        } else {
            // Return to ForkOrigin
            inFeedingAnimation = true;
            rb.velocity = toOriginRight;
            leftBowlToOrigin = true;
        }
    }

    IEnumerator TakeFromRightBowl() {
        if (rightBowl.GetBowlFill() != 0) {
            rb.velocity = stop;
            LoadFork();
            rightBowl.TakeFromBowl();

            // Wait for fish to be done chewing
            yield return new WaitUntil(() => !rightFish.IsChewing());

            rightBowlToFish = true;
            rb.velocity = bowlToFishRight;
            rightFish.OpenMouth();
        } else {
            // Return to ForkOrigin
            inFeedingAnimation = true;
            rb.velocity = toOriginLeft;
            rightBowlToOrigin = true;
        }
    }

    IEnumerator FeedLeftFish() {
        // Stop fork
        rb.velocity = stop;
        leftBowlToFish = false;

        // Start fish feeding animation
        leftFish.CloseMouth();
        yield return new WaitForSeconds(1.0f);
        UnloadFork();

        // Start fish chewing animation
        leftFish.StartChewing();

        // Return to ForkOrigin
        Debug.Log("transform.rotation.z: " + rb.rotation);
        Debug.Log("transform.position.x: " + transform.position.x);
        fishToOriginRotationSpeed = (rb.rotation / transform.position.x) * toOriginSpeed * -1;
        rb.velocity = toOriginRight;
        leftFishToOrigin = true;
    }

    IEnumerator FeedRightFish() {
        // Stop fork
        rb.velocity = stop;
        rightBowlToFish = false;

        // Start fish feeding animation
        rightFish.CloseMouth();
        yield return new WaitForSeconds(1.0f);
        UnloadFork();
        
        // Start fish chewing animation
        rightFish.StartChewing();

        // Return to ForkOrigin
        Debug.Log("transform.rotation.z: " + rb.rotation);
        Debug.Log("transform.position.x: " + transform.position.x);
        fishToOriginRotationSpeed = (rb.rotation / transform.position.x) * toOriginSpeed * -1;
        rb.velocity = toOriginLeft;
        rightFishToOrigin = true;
    }

    void LoadFork() {
        inFeedingAnimation = true;
        macSR.enabled = true;
    }

    void UnloadFork() {
        macSR.enabled = false;
    }

}