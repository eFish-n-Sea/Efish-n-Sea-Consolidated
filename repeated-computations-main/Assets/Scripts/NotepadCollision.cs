using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NotepadCollision : MonoBehaviour
    , IPointerEnterHandler
    , IPointerExitHandler
{
    public NotepadBehavior nb;
    RectTransform rt;

    void Start(){
        rt = GetComponent<RectTransform>();
    }

    void Update(){
        if (nb.gameObject.activeSelf)
            rt.localScale = Vector3.one;
        else
            rt.localScale = Vector3.zero;
    }

    public void OnPointerEnter(PointerEventData eventData){
        if (nb.gameObject.activeSelf)
            nb.onPage = true;
    }

    public void OnPointerExit(PointerEventData eventData){
        nb.onPage = false;
    }
}
