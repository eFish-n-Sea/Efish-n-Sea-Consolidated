using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextObjectBehavior : MonoBehaviour
{
    // public Sprite[] objects;

    // int index = 0;

    // Declare internal components
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        // Assign internal components
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // public void NextSprite()
    // {
    //     if (index < 19)
    //     {
    //         Debug.Log(index);
    //         index++;
    //         sr.sprite = objects[index];
    //     }
    //     else
    //     {
    //         sr.enabled = false;
    //     }
    // }

    public void NextSprite(Sprite newSprite) {
        sr.sprite = newSprite;
    }

    public void DisableSprite() {
        sr.enabled = false;
    }
}
