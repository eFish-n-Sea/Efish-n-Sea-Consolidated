using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartBehavior : MonoBehaviour
{
    List<GameObject> cart = new List<GameObject>();

    // Declare prefabs (for later instantiation)
    public GameObject fishBucket;
    public GameObject sharkBucket;
    public GameObject turtleBucket;
    public GameObject trashBucket;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToCart(string bucketTag) {
        Vector2 nextItemPosition = NextItemPosition();

        switch (bucketTag) {
            case "Fish Bucket":
                cart.Add(Instantiate(fishBucket, nextItemPosition, Quaternion.identity));
                break;
            case "Shark Bucket":
                cart.Add(Instantiate(sharkBucket, nextItemPosition, Quaternion.identity));
                break;
            case "Turtle Bucket":
                cart.Add(Instantiate(turtleBucket, nextItemPosition, Quaternion.identity));
                break;
            case "Trash Bucket":
                cart.Add(Instantiate(trashBucket, nextItemPosition, Quaternion.identity));
                break;
            default:
                Debug.Log("Bucket tag not recognized.");
                break;
        }
    }

    Vector2 NextItemPosition() {
        return new Vector2(-6.0f + 2.0f * cart.Count, -4.0f);
    }

    public void RemoveFromCart(GameObject oldBucket) {
        int oldBucketIndex = cart.IndexOf(oldBucket);

        // Remove and destroy old bucket
        cart.Remove(oldBucket);
        Destroy(oldBucket);

        // Slide later items to the left
        for (int i = oldBucketIndex; i < cart.Count; i++) {
            cart[i].transform.position = cart[i].transform.position + new Vector3(-2.0f, 0.0f, 0.0f);
        }
    }

    public bool CartFull() {
        return cart.Count == 5;
    }

    public List<string> GetBucketTags() {
        List<string> bucketTags = new List<string>();

        foreach (GameObject bucket in cart) {
            bucketTags.Add(bucket.tag);
        }

        return bucketTags;
    }
}
