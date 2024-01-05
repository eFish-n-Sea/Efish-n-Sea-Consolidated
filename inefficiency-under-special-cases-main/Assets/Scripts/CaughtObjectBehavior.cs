using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaughtObjectBehavior : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    GameController gc;

    Vector2 initialVelocity;
    Vector2 stop = new Vector2(0.0f, 0.0f);

    float rotationMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        gc = GameObject.FindWithTag("GameController").GetComponent<GameController>();

        initialVelocity = SetInitialVelocity(gc.GetClosestBucketPosition(tag));
        rotationMultiplier = -90.0f / initialVelocity.y;

        rb.velocity = initialVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.y > -initialVelocity.y) {
            rb.SetRotation(rb.velocity.y * rotationMultiplier);
        }
    }

    Vector2 SetInitialVelocity(Vector2 bucketPosition) {
        // Change this to be algorithmic
        float time = 2.3510975339514f;

        float xVelocity = -(0.1f + transform.position.x - bucketPosition.x) / time;
        float yVelocity = 12.0f;

        return new Vector2(xVelocity, yVelocity);
    }
    
    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Fish Bucket" || col.tag == "Shark Bucket" || col.tag == "Turtle Bucket" || col.tag == "Trash Bucket") {
            StartCoroutine(LandInBucket());
        } else if (col.tag == "Dock") {
            StartCoroutine(LandOnDock());
        } 
    }

    IEnumerator LandInBucket() {
        rb.gravityScale = 0;
        sr.enabled = false;
        rb.velocity = stop;
        
        yield return new WaitForSeconds(1.0f);

        DestroyObject();
    }

    IEnumerator LandOnDock() {
        rb.gravityScale = 0;
        rb.velocity = stop;

        // TODO: Play flopping animation
        yield return new WaitForSeconds(1.0f);

        rb.gravityScale = 1;
        
        yield return new WaitForSeconds(1.0f);

        DestroyObject();
    }

    void DestroyObject() {
        Destroy(gameObject);
        gc.objectsCaught++;
    }
}
