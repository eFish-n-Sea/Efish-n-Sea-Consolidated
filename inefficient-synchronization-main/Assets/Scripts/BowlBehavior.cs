using UnityEngine;

public class BowlBehavior : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeFromBowl() {
        int currentBowlFill = animator.GetInteger("BowlFill");

        if (currentBowlFill > 0) {
            animator.SetInteger("BowlFill", currentBowlFill - 2);
        }
    }

    public int GetBowlFill() {
        return animator.GetInteger("BowlFill");
    }
}
