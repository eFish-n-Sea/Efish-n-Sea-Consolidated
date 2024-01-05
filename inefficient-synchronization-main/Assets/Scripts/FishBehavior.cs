using System.Collections;
using UnityEngine;

public class FishBehavior : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;

    BubbleBehavior bubble;

    public float chewingTime;
    float chewingTimeRemaining = 0;

    int growthVelocityMultiplier;
    Vector2 growthVelocity;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        if (tag == "LeftFish") {
            bubble = GameObject.FindWithTag("LeftBubble").GetComponent<BubbleBehavior>();
            growthVelocityMultiplier = -1;
        } else if (tag == "RightFish") {
            bubble = GameObject.FindWithTag("RightBubble").GetComponent<BubbleBehavior>();
            growthVelocityMultiplier = 1;
        }

        growthVelocity = new Vector2(0.034f, 0.0f) * growthVelocityMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("IsChewing")) {
            if (chewingTimeRemaining > 0) {
                chewingTimeRemaining -= Time.deltaTime;
            } else {
                StopChewing();
                bubble.FloatToEnd();
            }
        }
    }

    public void OpenMouth() {
        animator.SetTrigger("OpenMouth");
    }

    public void CloseMouth() {
        animator.SetTrigger("CloseMouth");
    }

    public void StartChewing() {
        chewingTimeRemaining = chewingTime;
        animator.SetBool("IsChewing", true);
        StartCoroutine(bubble.FloatToMiddle());
        StartCoroutine(Grow());
    }

    IEnumerator Grow() {
        yield return new WaitForSeconds(1.0f);
        animator.SetTrigger("Grow");
        rb.velocity = growthVelocity;
        yield return new WaitForSeconds(5.0f);
        rb.velocity = Vector2.zero;
    }

    void StopChewing() {
        animator.SetBool("IsChewing", false);
    }

    public bool IsChewing() {
        return animator.GetBool("IsChewing");
    }
}
