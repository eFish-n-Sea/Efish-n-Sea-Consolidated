using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour
{
    public bool playing;
    public bool tipOpen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void win(int stars, string message)
    {
        playing = false;
            Debug.Log(stars);
            StartCoroutine(GameObject.FindWithTag("Results").GetComponent<results>().win(stars,message));    

    }
}
