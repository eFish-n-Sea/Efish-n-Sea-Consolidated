using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentObjectBehavior : MonoBehaviour
{
    public float fallSpeed;
    public Sprite[] objects;
    public int index = 0;
    public int width;
    public int height;
    public bool inZone;

    Vector2 down;
    Vector2 stop = new Vector2(0, 0);
    Vector2 origin;
    int[] heights = {3,3,2,2,2,1,1,3,2,1,2,2,3,3,1,3,2,2,1,1};
     int[] widths = {2,2,1,2,1,2,2,2,1,2,3,3,2,2,1,2,1,3,1,1};

    // Declare internal components
    Rigidbody2D rb;
    SpriteRenderer sr;
    BoxCollider2D boxCollider;

    // Declare external components
    GameController gc;
    NextObjectBehavior nextObject;

    // Start is called before the first frame update
    void Start()
    {
        // Assign internal components
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        // Assign external components
        gc = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        nextObject = GameObject.FindWithTag("nextObject").GetComponent<NextObjectBehavior>();

        sr.sprite = objects[index];
        width = widths[index];
        height = heights[index];
        down = new Vector2(0.0f, -fallSpeed);
        origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Zone")
        {
            Debug.Log("itemInZone");
            // gc.itemInZone = true;
            inZone = true;
            gc.inAnimation = true;
        }
        else if (col.tag == "floor")
        {
            StartCoroutine(LandOnFloor());
            // rb.velocity = stop; 
            // rb.position = origin;
            // index++;

            // if (index <= 19)
            // {
            //     StartCoroutine(changeSprite());
            //     width = widths[index];
            //     height = heights[index];
            //     boxCollider.size = new Vector2(width, height);
            // }

            // if (!gc.boxPlaced)
            // {
            //     if (index == 20)
            //     {
            //         gc.win();
            //     }
            //     else
            //     {
            //         StartFall();
            //     }
            // }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Zone")
        {
            // gc.itemInZone = false;
            inZone = false;
        }
    }

    // Formerly StartFall()
    public void SetObjects()
    {
        // If there are more objects
        if (index < 20)
        {
            // Update sprites
            sr.sprite = objects[index];
            UpdateNextObject();

            // Update dimensions
            width = widths[index];
            height = heights[index];
            boxCollider.size = new Vector2(width, height);

            rb.velocity = down;

            gc.inAnimation = false;
        }
        else
        {
            gc.win();
        }

        // gc.itemInZone = false;

        // if (index <= 19)
        // {
            // nextObject.NextSprite();
            // if (index < 19)
            // {
            //     nextObject.NextSprite(objects[index + 1]);
            // }
            // else
            // {
            //     nextObject.DisableSprite();
            // }

            // rb.velocity = down;
        // }
        // else
        // {
        //     gc.win();
        // }
    }

    // public int GetWidth()
    // {
    //     return width;
    // }

    // public int GetHeight()
    // {
    //     return height;
    // }

    // IEnumerator changeSprite()
    // {
    //     yield return new WaitForSeconds(0.2f);
    //     sr.sprite = objects[index];
    // }

    IEnumerator LandOnFloor()
    {
        // Stop the object
        rb.velocity = stop;
        yield return new WaitForSeconds(1.0f);

        // Return it to the origin
        rb.position = origin;
        yield return new WaitForSeconds(0.1f);

        // Increment the index
        index++;

        if (!gc.boxPlaced)
        {
            SetObjects();
        }
    }

    void UpdateNextObject() {
        if (index < 19)
        {
            nextObject.NextSprite(objects[index + 1]);
        }
        else
        {
            nextObject.DisableSprite();
        }
    }
}
