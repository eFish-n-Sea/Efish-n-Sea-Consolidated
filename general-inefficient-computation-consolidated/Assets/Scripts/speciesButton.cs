using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class speciesButton : MonoBehaviour
{
    public bool active;
    public gameController gc; 
    dialogueBehavior dialogueScript;

    // Start is called before the first frame update
    void Start()
    {
        active = false;
        gc = GameObject.FindWithTag("GameController").GetComponent<gameController>();
        dialogueScript = GameObject.FindWithTag("shrimplockHolmes").GetComponentInChildren<dialogueBehavior>();
    }

    public void OnButtonPress()
    {
        active = true;
        GameObject.FindWithTag("nameButton").GetComponent<nameButton>().active = true;
        GameObject.FindWithTag("colorButton").GetComponent<colorButton>().active = true;
        GameObject.FindWithTag("forwardButton").GetComponent<forwardButton>().active = true;
        dialogueScript.sortButtonsDialogue("species");
        gc.swimming = true;
        StartCoroutine(startSortingSequence());
    }

IEnumerator startSortingSequence()
    {
        if(gc.firstPress)
        {
            gc.sorting = true;
            for(int i = 0; i < 18; i++)
            {
                if(gc.fishAlive[i].activeSelf)
                {
                    gc.fishAlive[i].GetComponent<Animator>().SetBool("swim", true);
                }
            }
            yield return new WaitForSeconds(1.65f);
            gc.fishAlive.Sort((a, b) => a.GetComponent<fishBehavior>().species.CompareTo(b.GetComponent<fishBehavior>().species));
            gc.xStartPoint = 10.28f;
            gc.swimming = false;
            yield return new WaitForSeconds(.1f);
            gc.xStartPoint = -6.86f;
            gc.swimming = true;
            foreach(GameObject fish in gc.fishAlive)
            {
                if(fish.activeSelf)
                {
                    fish.GetComponent<Animator>().SetBool("swim", true);
                }
            }
            yield return new WaitForSeconds(1.65f);
            foreach(GameObject fish in gc.fishAlive)
            {
                if(fish.activeSelf)
                {
                    fish.GetComponent<Animator>().SetBool("swim", false);
                }
            }
            gc.swimming = false;
            gc.sorting = false;
        }
        else
            {
                gc.openingSequenceHappening = false;
                gc.firstPress = true;
                foreach(GameObject fish in gc.fishAlive)
                {
                    fish.GetComponent<fishBehavior>().getAway = true;
                }
                yield return new WaitForSeconds(4f);
                foreach(GameObject fish in gc.fishAlive)
                {
                    fish.GetComponent<fishBehavior>().yFlip = 0;
                    fish.GetComponent<fishBehavior>().targetAngle = new Vector3(0f, 0f, 0f);
                    fish.GetComponent<fishBehavior>().currentAngle = new Vector3(0f, 0f, 0f);
                    fish.transform.eulerAngles = new Vector3(0f, 0f, 0f);
                    fish.transform.rotation = Quaternion.Euler(0, 0, 0);
                    fish.GetComponent<fishBehavior>().getAway = false;
                }
                gc.fishAlive.Sort((a, b) => a.GetComponent<fishBehavior>().species.CompareTo(b.GetComponent<fishBehavior>().species));
                gc.xStartPoint = 10.28f;
                gc.swimming = false;
                yield return new WaitForSeconds(.1f);
                gc.xStartPoint = -6.86f;
                gc.swimming = true;
                foreach(GameObject fish in gc.fishAlive)
                {
                    if(fish.activeSelf)
                    {
                        fish.GetComponent<Animator>().SetBool("swim", true);
                    }
                }
                yield return new WaitForSeconds(3.3f);
                foreach(GameObject fish in gc.fishAlive)
                {
                    if(fish.activeSelf)
                    {
                        fish.GetComponent<Animator>().SetBool("swim", false);
                    }
                }
                gc.swimming = false;
            }
            GameObject.FindWithTag("nameButton").GetComponent<nameButton>().active = false;
            GameObject.FindWithTag("colorButton").GetComponent<colorButton>().active = false;
            GameObject.FindWithTag("forwardButton").GetComponent<forwardButton>().active = false;
    }

    // Update is called once per frame
    void Update()
    {
       GetComponent<Button>().interactable = (!active && !gc.getAwayGlobal && !gc.startSequenceHappening);
    }
}
