using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseLock : MonoBehaviour
{
    public DoorBehavior door;

    public void OnButtonPress(){
        door.puzzleClose();
    }
}
