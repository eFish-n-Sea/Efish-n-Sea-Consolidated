using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    enum Direction{
        Down,
        Up,
        Left,
        Right
    }

    public bool doorVertical;
    public bool opened;
    public string solution, entry;
    public GameObject buttonPanel;

    public float transWaitTime, transSpeed;
    
    public GameObject lockPanel;
    public CloseLock cl;

    GameObject player;
    GameController gc;
    GameObject mainCam, camControl;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        gc = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        entry = "";
        mainCam = GameObject.FindWithTag("MainCamera");
        camControl = GameObject.FindWithTag("CamControl");
    }

    // Update is called once per frame
    void Update()
    {
        checkSolution();
    }

    void checkSolution(){
        if (entry.Length >= solution.Length){
            if (!opened && entry[^solution.Length..] == solution)
                puzzleSolve();
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject == player){
            if (!opened)
                puzzleOpen();
        }
    }

    void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject == player){
            if (opened)
                transition();
        }
    }

    void puzzleOpen(){
        player.GetComponent<PlayerBehavior>().inDoor = true;
        lockPanel.SetActive(true);
        buttonPanel.SetActive(true);
        cl.door = this;
        linkButtons();
    }

    public void puzzleClose(){
        if (!opened)
            entry = "";
        buttonPanel.SetActive(false);
        lockPanel.SetActive(false);
        player.GetComponent<PlayerBehavior>().inDoor = false;
    }

    void puzzleSolve(){
        opened = true;
        puzzleClose();
        GetComponent<Animator>().SetBool("opened", true);
    }
    
    void transition(){
        StartCoroutine(movePlayer(findDirection()));
        gc.numTrans++;
    }

    void linkButtons(){
        for (int i = 0; i < buttonPanel.transform.childCount; i++){
            buttonPanel.transform.GetChild(i).GetComponent<LockButton>().door = this;
        }
    }

    Direction findDirection(){
        if (doorVertical){
            if (player.transform.position.y < transform.position.y)
                return Direction.Up;
            else
                return Direction.Down;
        }
        else{
            if (player.transform.position.x < transform.position.x)
                return Direction.Right;
            else
                return Direction.Left;
        }
    }

    IEnumerator movePlayer(Direction transDir){
        player.GetComponent<PlayerBehavior>().transitioning = true;
        player.GetComponent<Collider2D>().enabled = false;

        Vector2 newVel = new Vector2(transSpeed, transSpeed);
        switch (transDir){
            case Direction.Down:
                newVel *= Vector2.down;
                break;
            case Direction.Up:
                newVel *= Vector2.up;
                break;
            case Direction.Left:
                newVel *= Vector2.left;
                break;
            case Direction.Right:
                newVel *= Vector2.right;
                break;
        }
        player.GetComponent<Rigidbody2D>().velocity = newVel;
        yield return new WaitForSeconds(transWaitTime);

        player.GetComponent<Collider2D>().enabled = true;
        player.GetComponent<PlayerBehavior>().transitioning = false;
    }

    // Called by GameController on startup.
    // Receives code, sets 'solution' to 'code', and updates tile visuals to match 'hint'.
    public void setCode(string code, string hint){
        // Sets global property 'solution' to code
        solution = code;

        // Sets corresponding tiles to correspond to hint
        for (int i = 0; i < hint.Length; i++){
            transform.GetChild(0).GetChild(i).GetComponent<SpriteRenderer>().sprite = gc.charToTile(hint[i]);
        }
    }
}
