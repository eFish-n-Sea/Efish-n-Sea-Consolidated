using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishBehavior : MonoBehaviour
{
    public string color, species, handle;
    public bool getAway, clicked;
    public gameController gc; 
    public Vector3 targetAngle;
    public Vector3 currentAngle;
    public timer timer;
    dialogueBehavior dialogueScript;
    public float yFlip;
    public float test;
    GameObject popUpActive;
    

    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.FindWithTag("GameController").GetComponent<gameController>();
        dialogueScript = GameObject.FindWithTag("shrimplockHolmes").GetComponentInChildren<dialogueBehavior>();
        currentAngle = transform.eulerAngles;
        timer = GameObject.FindWithTag("timer").GetComponent<timer>();
        StartCoroutine(openingSequence());
    }

    void OnMouseEnter()
    {
        if(!gc.swimming && !gc.getAwayGlobal && !gc.startSequenceHappening)
        {
            dialogueScript.hoveredFishDialogue(handle);
        }
    }
    
    void OnMouseDown()
    {
        if(!gc.swimming && !gc.startSequenceHappening && !clicked)
        {
            if(handle == gc.criminal.GetComponent<fishBehavior>().handle)
            {
                clicked = true;
                gc.getAwayGlobal = true;
                StartCoroutine(startGetAwaySequence());
                dialogueScript.chaseDialogue();
                gc.initChaseSequence(this.gameObject);
            }
            else
            {
                timer.time -= 2;
                popUpActive = (Instantiate(gc.wrongFishPopUp, transform.position, Quaternion.identity));
                popUpActive.GetComponent<wrongFishPopUpBehavior>().startPopUp();
                // GameObject.FindWithTag("wrongPop").GetComponent<wrongFishPopUpBehavior>().startPopUp();
            }
        }
    }

    void forwardButtonMovement()
    {
        if(gc.sorting)
        {
            transform.Translate(Vector2.left*10*Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left*5*Time.deltaTime);
        } 
    }

    IEnumerator startGetAwaySequence()
    {
        int randRotation = Random.Range(30, 70);
        if(randRotation > 50)
        {
            randRotation = 20 - randRotation;
        }
        targetAngle =  new Vector3(0f, yFlip, randRotation);
        GetComponent<Animator>().SetBool("swim", true);
        getAway = true;
        yield return new WaitForSeconds(.75f);
        targetAngle =  new Vector3(0f, yFlip, -randRotation);
        yield return new WaitForSeconds(3.25f);
        gameObject.SetActive(false);
        gc.getAwayGlobal = false;
        Destroy(gc.criminal);
        gc.pickCriminal();
    }

    void getawayMovement()
    {
        transform.Translate(Vector2.left*6*Time.deltaTime);
        currentAngle = new Vector3(
            Mathf.LerpAngle(currentAngle.x, targetAngle.x, Time.deltaTime),
            Mathf.LerpAngle(currentAngle.y, targetAngle.y, Time.deltaTime),
            Mathf.LerpAngle(currentAngle.z, targetAngle.z, Time.deltaTime));
        transform.eulerAngles = currentAngle;
    }

    IEnumerator openingSequence()
    {
        GetComponent<Animator>().SetBool("swim", true);
        while(true)
        {
            // determine x pos and fix if out of bounds
            if(transform.position.x <= gc.leftBound)
            {
                yFlip = 180;
                transform.position = new Vector2(gc.leftBound + 2, transform.position.y);
            }
            else if(transform.position.x >= gc.rightBound)
            {
               yFlip = 0;
               transform.position = new Vector2(gc.rightBound - 2, transform.position.y);
            }

            //determine y pos and fix angle to get it to swim in bounds
            if(transform.position.y >= gc.upperBound)
            {
                int randDownRotation = Random.Range(30, 50);
                targetAngle =  new Vector3(0f, yFlip, randDownRotation);
                yield return new WaitForSeconds(.75f);
            }
            else if(transform.position.y <= gc.lowerBound)
            {
                int randUpRotation = Random.Range(-50, -30);
                targetAngle =  new Vector3(0f, yFlip, randUpRotation);
                yield return new WaitForSeconds(.75f);
            }
            else // if in bounds rand angle
            {
                int randRotation = Random.Range(30, 70);
                if(randRotation > 50)
                {
                    randRotation = 20 - randRotation;
                }
                targetAngle =  new Vector3(0f, yFlip, randRotation);
                yield return new WaitForSeconds(.75f);
            }
            

            if(!gc.openingSequenceHappening)
            {
                GetComponent<Animator>().SetBool("swim", false);
                break;
            }
                
        }
    }

    void openingSequenceMovement()
    {
        transform.Translate(Vector2.left*2*Time.deltaTime);
        currentAngle = new Vector3(
            Mathf.LerpAngle(currentAngle.x, targetAngle.x, Time.deltaTime),
            Mathf.LerpAngle(currentAngle.y, targetAngle.y, Time.deltaTime),
            Mathf.LerpAngle(currentAngle.z, targetAngle.z, Time.deltaTime));
        transform.eulerAngles = currentAngle;
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<Animator>().GetBool("swim") && !getAway && !gc.openingSequenceHappening)
        {
           forwardButtonMovement();
        }
        if(getAway)
        {
            getawayMovement();
        }
        if(!getAway && gc.openingSequenceHappening)
        {
            openingSequenceMovement();
        }
    }
}
