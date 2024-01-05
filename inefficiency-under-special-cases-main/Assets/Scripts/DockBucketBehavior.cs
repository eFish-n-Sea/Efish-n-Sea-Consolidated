using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockBucketBehavior : MonoBehaviour
{
    float capacity;
    float currentFill;

    GameController gc;

    public List<GameObject> popUpScores;

    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.FindWithTag("GameController").GetComponent<GameController>();

        switch (tag) {
            case "Fish Bucket":
                capacity = 5;
                break;
            case "Shark Bucket":
                capacity = 1;
                break;
            case "Turtle Bucket":
                capacity = 1;
                break;
            case "Trash Bucket":
                capacity = 10;
                break;
            default:
                Debug.Log("Dock bucket tag not recognized.");
                capacity = 0;
                break;
        }

        currentFill = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsFull() {
        return currentFill == capacity;
    }
    
    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Fish") {
            gc.AddPoints(100);
            Instantiate(popUpScores[1], transform.position, Quaternion.identity);
        } else if (col.tag == "Shark" || col.tag == "Turtle") {
            gc.AddPoints(250);
            Instantiate(popUpScores[2], transform.position, Quaternion.identity);
        } else if (col.tag == "Trash") {
            Instantiate(popUpScores[0], transform.position, Quaternion.identity);
            gc.AddPoints(50);
        }
    }

    public void AddItem() {
        currentFill++;
    }
}
