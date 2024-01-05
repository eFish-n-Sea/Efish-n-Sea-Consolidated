using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingObject : MonoBehaviour
{

    public int stars;
    public string message;
    gameController gc;
    SpriteRenderer glow;
    Animator animator;
    GameObject bread;
    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.FindWithTag("GameController").GetComponent<gameController>();
        glow = this.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        bread = GameObject.FindWithTag("bread");

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown() {
        if (gc.playing)
        {
            // animator.SetTrigger("toaster");
            // gc.win(stars, message);
            if( this.tag == "toaster")
            {
            StartCoroutine(ToastCloseAn());
            }
            else 
            {
              StartCoroutine(OvenCloseAn());  
            }
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
    
    IEnumerator ToastCloseAn()
    {
        animator.SetTrigger("toaster");
        yield return new WaitForSeconds(1.3f);
        bread.GetComponent<Animator>().SetTrigger(this.tag);
        yield return new WaitForSeconds(4.8f);
        animator.SetTrigger("reset");
        yield return new WaitForSeconds(2.6f);
            gc.win(stars, message);
    }
    IEnumerator OvenCloseAn()
    {
        bread.GetComponent<Animator>().SetTrigger(this.tag);
        yield return new WaitForSeconds(6.0f);
        yield return new WaitForSeconds(2.6f);
        gc.win(stars, message);

    }

}
