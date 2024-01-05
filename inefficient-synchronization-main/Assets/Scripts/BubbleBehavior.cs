using System.Collections;
using UnityEngine;

public class BubbleBehavior : MonoBehaviour
{
    bool atStart = true;
    bool startToMiddle = false;
    bool atMiddle = false;
    bool middleToEnd = false;
    
    Rigidbody2D rb;

    public float floatSpeedUp;
    public float floatSpeedIn;
    public float bobbleSpeed;
    public float bobbleTime;

    float bobbleTimeRemaining;

    Vector2 floatUp;
    Vector2 bobbleUp;
    Vector2 bobbleDown;
    Vector2 stop = new Vector2(0.0f, 0.0f);

    Vector2 origin;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // floatUp = new Vector2(0.0f, 1.0f) * floatSpeedUp;

        if (tag == "LeftBubble") {
            floatUp = new Vector2(floatSpeedIn, floatSpeedUp);
        } else if (tag == "RightBubble") {
            floatUp = new Vector2(-floatSpeedIn, floatSpeedUp);
        }

        bobbleUp = new Vector2(0.0f, 1.0f) * bobbleSpeed;
        bobbleDown = new Vector2(0.0f, -1.0f) * bobbleSpeed;

        origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (atMiddle) {
            if (bobbleTimeRemaining > 0) {
                bobbleTimeRemaining -= Time.deltaTime;
            } else {
                if (rb.velocity == bobbleDown) {
                    rb.velocity = bobbleUp;
                } else {
                    rb.velocity = bobbleDown;
                }

                bobbleTimeRemaining = bobbleTime;
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "BubbleStop" && startToMiddle) {
            startToMiddle = false;

            rb.velocity = stop;

            atMiddle = true;
        } else if (col.tag == "BubbleRoof" && middleToEnd) {
            middleToEnd = false;

            rb.velocity = stop;
            rb.position = origin;

            atStart = true;
        }
    }

    public IEnumerator FloatToMiddle() {
        yield return new WaitUntil(() => atStart);

        atStart = false;
        rb.velocity = floatUp;
        startToMiddle = true;
    }

    public void FloatToEnd() {
        atMiddle = false;
        rb.velocity = floatUp;
        bobbleTimeRemaining = 0;
        middleToEnd = true;
    }
}
