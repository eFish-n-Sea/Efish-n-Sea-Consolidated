using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollbar : MonoBehaviour
{
    public float edge;
    bool clicked;
    gameController gc;
    // Start is called before the first frame update
    void Start()
    {
        clicked = false;
        gc = GameObject.FindWithTag("GameController").GetComponent<gameController>();
    }

    void OnMouseDown()
    {
        if(gc.playing)
        {
            if(Input.GetMouseButtonDown(0))
            {
                clicked = true;
            }
        }
    }


    // Update is called once per frame
    void Update()
    { 
        if(Input.GetMouseButtonUp(0))
        {
            clicked = false;
        }

        if(clicked)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if(mousePosition.x < -edge)
            {
                transform.position = new Vector2(-edge, transform.position.y);
            }

            else if(mousePosition.x > edge)
            {
                transform.position = new Vector2(edge, transform.position.y);
            }
            
            else
            {
                transform.position = new Vector2(mousePosition.x, transform.position.y);
            }
        }
    }
}
