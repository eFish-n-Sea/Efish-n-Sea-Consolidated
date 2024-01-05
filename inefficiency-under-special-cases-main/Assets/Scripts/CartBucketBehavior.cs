using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartBucketBehavior : MonoBehaviour
{
    CartBehavior cart;

    // Start is called before the first frame update
    void Start()
    {
        cart = GameObject.FindWithTag("Cart").GetComponent<CartBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnMouseDown() {
        cart.RemoveFromCart(gameObject);
    }
}
