using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wrongFishPopUpBehavior : MonoBehaviour
{
    public bool popUpHappening;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void startPopUp()
    {
        popUpHappening = true;
        StartCoroutine(popUp());
    }
    IEnumerator popUp()
    {
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(Disappear());
    }

    IEnumerator Disappear() 
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(popUpHappening)
        {
            transform.Translate(Vector2.up*2*Time.deltaTime);
        }
    }
}
