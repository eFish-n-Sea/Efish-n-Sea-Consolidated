using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationChange : MonoBehaviour
{
    Transform cam;
    gameController gc;
    SpriteRenderer glow;
    Vector3 patio = new Vector3(0,11.8f,-10);
    Vector3 kitchen = new Vector3(0,0,-10);
    // Start is called before the first frame update
    void Start()
    {
      cam= GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
      gc = GameObject.FindWithTag("GameController").GetComponent<gameController>();
      glow = this.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown() 
    {
      if(this.tag == "door" && gc.playing)
      {
      cam.position = patio;
      }
      else
      {
        cam.position = kitchen;
      }
      

    }
    void OnMouseOver()
    {
      if(gc.playing)
      {
        glow.enabled = true;
      }
    }
    void OnMouseExit()
    {
        glow.enabled = false;
    }
}
