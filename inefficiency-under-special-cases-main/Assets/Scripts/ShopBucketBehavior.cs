using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBucketBehavior : MonoBehaviour
{
    Animator animator;
    GameController gc;
    CartBehavior cart;
    SpeechBubbleBehavior sbb;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        gc = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        cart = GameObject.FindWithTag("Cart").GetComponent<CartBehavior>();
        sbb = GameObject.FindWithTag("Speech Bubble").GetComponent<SpeechBubbleBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseEnter() {
        sbb.UpdateSpeechWithTag(tag);
    }

    void OnMouseDown() {
        if (gc.playing) {
            if (!cart.CartFull()) {
                cart.AddToCart(tag);
            } else {
                animator.SetTrigger("Jiggle");
            }
        }
    }
}
