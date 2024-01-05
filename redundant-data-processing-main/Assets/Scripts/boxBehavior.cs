using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxBehavior : MonoBehaviour
{
    public bool clicked;
    public float scaleFactor;
    public bool placeable;
    public bool placed;
    public bool freezeRotation;
    bool canShake;
    List<Collider2D> collisions;
    ContactFilter2D Nofilter;
    ContactFilter2D Scrollfilter;
    ContactFilter2D filter;
    gameController gc;

    
    void Start()
    {
        clicked = false;
        canShake = true;
        collisions = new List<Collider2D>();
        Nofilter = new ContactFilter2D();
        Scrollfilter = new ContactFilter2D();
        filter = new ContactFilter2D();
        gc = GameObject.FindWithTag("GameController").GetComponent<gameController>();
        
        Scrollfilter.SetLayerMask(LayerMask.GetMask("scroll"));
        filter.SetLayerMask(LayerMask.GetMask("box", "wall"));
        Nofilter.NoFilter();
    }

    void OnMouseEnter()
    {
        if(!gc.boxHeld)
        {
            Cursor.SetCursor( gc.cursorHover, gc.cursorFix, CursorMode.Auto);
        }
    }
    void OnMouseExit()
    {
        if(!gc.boxHeld)
        {
            Cursor.SetCursor( gc.cursorPoint, gc.cursorFix, CursorMode.Auto);
        }
    }

    void OnMouseDown()
    {
        if(gc.playing)
        {
            if(Input.GetMouseButtonDown(0))
            {
                if(!clicked && !gc.boxHeld) //in scroll or in truck
                {
                    clicked = true;
                    gc.boxHeld = true;
                    GetComponent<SpriteRenderer>().sortingLayerName = "Temp";
                    GetComponent<Animator>().enabled = true;
                    Cursor.SetCursor( gc.cursorGrab, gc.cursorFix, CursorMode.Auto);
                    if(!placed)
                    {
                        transform.localScale *= scaleFactor;
                    } 
                    if(placed)
                    {
                        gc.placedBoxes.Remove(this.gameObject);
                    } 
                } 
                else if(clicked) //when attached to mouse
                {
                    if(placeable)
                    {
                        placed = true;
                        freezeRotation = false;
                        GetComponent<Collider2D>().isTrigger = false;
                        GetComponent<Rigidbody2D>().isKinematic = false;
                        Unclick();
                    }
                    if(GetComponent<Collider2D>().OverlapCollider(filter, collisions) > 0)
                    {
                        GetComponent<Animator>().SetBool("shaking", true);
                        if(canShake)
                        {
                            canShake = false;
                            StartCoroutine(Unshake());
                        }
                    }
                    if(GetComponent<Collider2D>().OverlapCollider(Scrollfilter, collisions) > 0)
                    {
                        placed = false;
                        Unclick();
                        transform.localScale /= scaleFactor;
                    }
                }

                if(freezeRotation)
                {
                    GetComponent<Rigidbody2D>().freezeRotation = true;
                    GetComponent<Rigidbody2D>().SetRotation(0);
                }
                else
                {
                    GetComponent<Rigidbody2D>().freezeRotation = false;
                }
            }   
        }
    }
    void Unclick()
    {
        clicked = false;
        gc.boxHeld = false;
        Cursor.SetCursor( gc.cursorPoint, gc.cursorFix, CursorMode.Auto);
        if(placed)
        {
            GetComponent<SpriteRenderer>().sortingLayerName = "BoxPlaced";
            gc.placedBoxes.Add(this.gameObject);
        }
        else
        {
            GetComponent<SpriteRenderer>().sortingLayerName = "Box";
        }
        GetComponent<Animator>().SetBool("shaking", false);
        GetComponent<Animator>().enabled = false;
    }

    IEnumerator Unshake()
    {
        yield return new WaitForSeconds(1);
        GetComponent<Animator>().SetBool("shaking", false);
        canShake = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<Collider2D>().OverlapCollider(Nofilter, collisions) == 0)
        {
            placeable = true;
        }
        else if(GetComponent<Collider2D>().OverlapCollider(Nofilter, collisions) > 0)
        {
            placeable = false;
            if(placed)
            {
                GetComponent<Collider2D>().isTrigger = false;
            }
        }
        if(clicked)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(mousePosition.x, mousePosition.y);
            freezeRotation = true;
            GetComponent<Collider2D>().isTrigger = true;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        if(freezeRotation)
        {
            GetComponent<Rigidbody2D>().freezeRotation = true;
            GetComponent<Rigidbody2D>().SetRotation(0);
        }
        else
        {
            GetComponent<Rigidbody2D>().freezeRotation = false;
        }
    }
}
