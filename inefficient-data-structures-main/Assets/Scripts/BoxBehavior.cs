using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBehavior : MonoBehaviour
{
    public Vector2 origin;
    public int width;
    public int height;
    public bool clicked = false;
    public bool InZone = false;
    public bool placed = false;
    public bool boxanimation;
    // public float moveSpeed;

    int pointMult;
    Vector2 stop = new Vector2(0,0);
    Vector2 roundleave = new Vector2(20,0);
    // Vector2 movement;
    // float endx = 0;

    // Declare internal components
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;

    // Declare external components
    GameController gc;
    CurrentObjectBehavior fallingObject;
    GameObject rug;

    public List<GameObject> popUpScores;
    
    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position;

        // Assign internal components
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();

        // Assign external components
        gc = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        fallingObject = GameObject.FindWithTag("item").GetComponent<CurrentObjectBehavior>(); 
        rug = GameObject.FindWithTag("Zone");
    }

    // Update is called once per frame
    void Update()
    {   
        // if (!clicked && InZone && !placed)
        // {
        //     FinalPlacement();
        // }

        if (clicked)
        {
            // Debug.Log("in clicked");
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);             
            transform.position = new Vector2(mousePosition.x, mousePosition.y);
        }

        // if (clicked && Input.GetMouseButtonDown(0))
        // {
        //     clicked = false;
        // }
    }
    void OnMouseDown()
    {
        // if (Input.GetMouseButtonDown(0) && gc.playing && !gc.itemInZone)
        // if (gc.playing && !gc.itemInZone)
        // if (gc.playing && !fallingObject.inZone)
        if (gc.playing && !gc.inAnimation)
        {
            // Debug.Log("in Mousedown");
            clicked = true;

            if (gc.boxPlaced && placed)
            {
                gc.boxPlaced = false;
                placed = false;
            }
        }

        // float x = transform.position.x;
        // float y = transform.position.y; 
        // float disx = end.x-x;
        // float disy = end.y-y;
        // float movedis = (float) System.Math.Sqrt(System.Math.Pow(disx,2)+System.Math.Pow(disy,2));
        // float ratio = moveSpeed/movedis;
        // rb.velocity = new Vector2(ratio*disx,ratio*disy);
    }
    
    void OnMouseUp()
    {
        clicked = false;

        // if (InZone && !gc.itemInZone)
        if (InZone && !fallingObject.inZone)
        {
            SnapOntoRug();
        }  
        else
        {
            transform.position = origin;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // if (col.tag == "floor")
        // {
        //     rb.velocity = stop;
        // }
        if (col.tag == "Zone")
        {
            InZone = true;
        }
        // else if (col.tag == "item" && gc.boxPlaced && placed)
        else if (col.tag == "item" && placed)
        {
            pointMult = CheckBoxSize();
            gc.addPoints(pointMult);
            Instantiate(popUpScores[pointMult], transform.position, Quaternion.identity);

        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Zone")
        {
            InZone = false;
        }
        // else if (col.tag == "item" && gc.boxPlaced && placed)
        else if (col.tag == "item" && placed)
        {
            // Debug.Log("in exit");
            // rb.velocity = roundleave;

            if (pointMult > 0)
            {
                // StartCoroutine(CloseBox());
            }
            else
            {
                sr.enabled = false;
                gc.boxPlaced = false;
                placed = false;
                animator.SetTrigger("unsquish");
                transform.position = origin;
                sr.enabled = true;
               boxanimation = true;

                // if (fallingObject.index >= 19)
                // {
                //     gc.win();
                // }
                // else
                // {
                    fallingObject.SetObjects();
                // }
            }
        }
        else if (col.tag == "reset")
        {
            gc.boxPlaced = false;
            placed = false;
            animator.SetTrigger("reset");
            rb.velocity = stop;
            transform.position = origin;
            boxanimation= false;

            // if(fo.index >= 19)
            // {
            //     gc.win();
            // }
            // else
            // {
            fallingObject.SetObjects();
            // }
        }
    }

    // Formerly FinalPlacement(), I think this name is more self-explanatory
    void SnapOntoRug()
    {
        transform.position = new Vector2(rug.transform.position.x, rug.transform.position.y + 0.1f + (1.2f * (transform.localScale.y - 1)));
        gc.setPlace(this.tag);
        // gc.boxPlaced = true;
        placed = true;
    }

    int CheckBoxSize()
    {
        // Debug.Log(this.tag);
        boxanimation = true;

        if (fallingObject.width > width)
        {
            animator.SetTrigger("squish");
            return 0;
        }
        else if (fallingObject.width == width && fallingObject.height == height)
        {
            StartCoroutine(CloseBox());
            return 2;
        }
        else
        {
            StartCoroutine(CloseBox());
            return 1;
        }
    }

    public IEnumerator CloseBox()
    {
        yield return new WaitForSeconds(1.0f);
        animator.SetTrigger("close box");
        yield return new WaitForSeconds(2.0f);
        rb.velocity = roundleave;
    }
}
    
