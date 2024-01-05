using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catfish_behavior : MonoBehaviour
{
    public float speed;
    public float fast, slow;
    public Transform[] waypoints;
    float[] angles;
    Vector2[] waypointPos;
    int waypointIndex;
    Rigidbody2D rb;
    game_controller gc;
    bool playing;

    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.FindWithTag("GameController").GetComponent<game_controller>();
        playing = false;
        waypointIndex = 1;
        angles = new float[] {0f, 90f, 180f, 270f, 180f, 90f, 180f, 270f};
        waypointPos = new Vector2[] {new Vector2(-10, -3), new Vector2(10, -3), new Vector2(10, 0), new Vector2(-5, 0),
                                     new Vector2(-5, 3), new Vector2(10, 3), new Vector2(10, 6), new Vector2(-10, 6)};
        speed = fast;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        setVel();
        setRot();
        playing = gc.playing;
        // Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // if (Input.GetMouseButtonDown(0))
        // {
        //     transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
        // }
    }

    void setVel(){
        if (playing){
            float angle = rb.rotation * (float) System.Math.PI / 180f;
            rb.velocity = new Vector2(speed * (float) System.Math.Sin(angle), speed * (float) -System.Math.Cos(angle));
        }
        else{
            rb.velocity = new Vector2(0f, 0f);
        }
    }

    void setRot(){
        if ((waypointIndex == 0 && transform.position.y < -2.99f) ||
            (waypointIndex == 1 && transform.position.x > 9.99f) ||
            (waypointIndex == 2 && transform.position.y > -0.01f) ||
            (waypointIndex == 3 && transform.position.x < -4.99f) ||
            (waypointIndex == 4 && transform.position.y > 2.99f) ||
            (waypointIndex == 5 && transform.position.x > 9.99f) ||
            (waypointIndex == 6 && transform.position.y > 5.99f) ||
            (waypointIndex == 7 && transform.position.x < -9.99f)){
                transform.position = waypointPos[waypointIndex];
                waypointIndex ++;
                waypointIndex %= 8;
                rb.rotation = angles[waypointIndex];
            }       
    }
}
