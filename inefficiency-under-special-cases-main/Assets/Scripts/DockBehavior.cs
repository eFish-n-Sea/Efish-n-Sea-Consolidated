using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockBehavior : MonoBehaviour
{
    GameController gc;

    public GameObject zero;

    Vector2 zeroPosition = new Vector2(17.21777f, -2.507382f);

    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Fish" || col.tag == "Shark" || col.tag == "Turtle" || col.tag == "Trash") {
            Instantiate(zero, zeroPosition, Quaternion.identity);
        }
    }
}
