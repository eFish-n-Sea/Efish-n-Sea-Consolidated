using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerBehavior : MonoBehaviour
{
    enum Direction{
        Down,
        Up,
        Left,
        Right
    }

    Vector3 movement;
    bool walking;
    public bool inDoor, inNotes, transitioning;
    int dir;
    public float speed;

    Rigidbody2D rb;
    Animator anim;
    GameController gc;
    GameObject camControl;
    CinemachineFramingTransposer cft;
    CinemachineConfiner cc;
    float xFDamp, yFDamp, cDamp;

    // Start is called before the first frame update
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        gc = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        cft =  GameObject.FindWithTag("CamControl").GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
        xFDamp = cft.m_XDamping;
        yFDamp = cft.m_YDamping;
        cc = GameObject.FindWithTag("CamControl").GetComponent<CinemachineConfiner>();
        cDamp = cc.m_Damping;
    }

    // Update is called once per frame
    void Update(){
        if (gc.playing && !transitioning){
            movement = getMovement();
            rb.velocity = movement;
            if (movement == Vector3.zero)
                walking = false;
            else{
                walking = true;
                dir = (int) getDir(movement);
            }
            animUpdate(walking, dir);
        }
        else if (!transitioning){
            rb.velocity = Vector3.zero;
            walking = false;
            animUpdate(walking, dir);
        }
    }

    // Collects keyboard input and translates into player movement
    Vector3 getMovement(){
        if (inDoor || inNotes){
            cft.m_XDamping = float.MaxValue;
            cft.m_YDamping = float.MaxValue;
            cc.m_Damping = float.MaxValue;
            return Vector3.zero;
        }
        else{
            cft.m_XDamping = xFDamp;
            cft.m_YDamping = yFDamp;
            cc.m_Damping = cDamp;
        }
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 output = new Vector3(x, y, 0);
        if (output.magnitude != 0){
            output.Normalize();
            output *= speed;
        }
        return output;
    }

    // Determines sprite orientation based on player movement (only called while moving)
    Direction getDir(Vector3 movement){
        if (movement.y < 0) // vertically down
        {
            if (-Mathf.Abs(movement.x) < movement.y) // more horizontal than vertical
            {
                if (movement.x > 0) // right
                    return Direction.Right;
                else
                    return Direction.Left;
            }
            else
                return Direction.Down;
        }
        else // vertically up/neutral
        {
            if (Mathf.Abs(movement.x) > movement.y) // more horizontal than vertical
            {
                if (movement.x > 0) // right
                    return Direction.Right;
                else
                    return Direction.Left;
            }
            else
                return Direction.Up;
        }
    }

    // Sets animator flags for player based on direction and whether or not moving
    void animUpdate(bool walking, int dir){
        anim.SetBool("Walking", walking);
        anim.SetInteger("Direction", dir);
    }
}
