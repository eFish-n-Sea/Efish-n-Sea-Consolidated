using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class policeBehavior : MonoBehaviour
{
    public bool chaseSequenceHappening, flipped;
    public GameObject fish;
    public float speed;
    public float angleFix, angleFixFlip;
    private float distance;
    public gameController gc;

    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.FindWithTag("GameController").GetComponent<gameController>();
    }

    public void startChaseSequence(GameObject f)
    {
        fish = f;
        chaseSequenceHappening = true;
        StartCoroutine(chaseSequence());
    }

    IEnumerator chaseSequence()
    {
        yield return new WaitForSeconds(3.25f);
        chaseSequenceHappening = false;
        gc.policeAlive.Remove(this.gameObject);
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if(chaseSequenceHappening)
        {
            distance = Vector2.Distance(transform.position, fish.transform.position);
            Vector2 direction = fish.transform.position - transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.position = Vector2.MoveTowards(this.transform.position, fish.transform.position, speed * Time.deltaTime);
            if(!flipped)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, -180, -1 * angle + angleFix));
            }
            else
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 1 * angle + angleFixFlip));
            }
        }
    }
}
