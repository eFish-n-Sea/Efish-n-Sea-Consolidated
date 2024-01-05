using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasResize : MonoBehaviour
{
    void Update(){
        float scale = ((float) Screen.width) / ((float)Screen.height);
        GetComponent<RectTransform>().sizeDelta = new Vector2(600f*scale, 600);
    }
}
