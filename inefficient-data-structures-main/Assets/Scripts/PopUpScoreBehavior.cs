using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpScoreBehavior : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        rb.velocity = Vector2.up * 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Pop Up Score Ceiling") {
            StartCoroutine(Disappear());
        }
    }

    IEnumerator Disappear() {
        animator.SetTrigger("disappear");
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }
}
