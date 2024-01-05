using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour
{
    public bool boxHeld;
    public List<GameObject> placedBoxes;
    public bool playing;
    public Texture2D cursorPoint;
    public Texture2D cursorGrab;
    public Texture2D cursorHover;
    public Vector2 cursorFix = new Vector2(0,0);
    // Start is called before the first frame update
    void Start()
    {
        boxHeld = false;
        placedBoxes = new List<GameObject>();
        playing = false;
        Cursor.SetCursor(cursorPoint, cursorFix, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
