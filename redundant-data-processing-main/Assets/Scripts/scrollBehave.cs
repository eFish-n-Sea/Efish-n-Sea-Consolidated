using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollBehave : MonoBehaviour
{
    public GameObject[] boxes;
    GameObject scrollBar;
    public float spacing;
    public float scaleFactor;
    public float ypos;
    public float xpos;
    public float scrollOffset;
    public float startPoint;

    // Start is called before the first frame update
    void Start()
    {
        scrollBar = transform.GetChild(0).gameObject;
        populate();
    }
    void populate()
    {
        float x = xpos;
        foreach(GameObject box in boxes)
        {
            box.GetComponent<Rigidbody2D>().isKinematic = true;
            box.transform.localScale /= scaleFactor;
        }
    }
    void boxPosition()
    {
        float scrollpos = scrollBar.transform.position.x;
        float x = xpos + scrollpos*scrollOffset + startPoint;

        foreach(GameObject box in boxes)
        {
            if(!box.GetComponent<boxBehavior>().clicked && !box.GetComponent<boxBehavior>().placed)
            {
                box.transform.position = new Vector2(x, ypos);
            }
            x += spacing;
        }
    }
    // Update is called once per frame
    void Update()
    {
        boxPosition();
    }
}
