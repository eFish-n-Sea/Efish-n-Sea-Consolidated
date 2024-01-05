using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotepadBehavior : MonoBehaviour
{
    public GameObject linePrefab;
    public Color lineColor;
    public float lineWidth;
    int layerOrder;

    public int activePage;
    GameObject[] pages;

    public bool onPage;

    public Stack<List<GameObject>> done;
    public Stack<List<GameObject>> undone;

    public float minX, maxX, minY, maxY, aspectOffset;

    Coroutine drawing;

    void Start(){
        layerOrder = 0;
        done = new Stack<List<GameObject>>();
        undone = new Stack<List<GameObject>>();

        pages = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
            pages[i] = transform.GetChild(i).gameObject;
        
        StartCoroutine(waitStart());
    }

    IEnumerator waitStart(){
        yield return new WaitForSeconds(0.01f);
        gameObject.SetActive(false);
    }

    void Update(){
        if (Input.GetMouseButtonDown(0) && onPage)
            StartLine();
        if (Input.GetMouseButtonUp(0))
            EndLine();
    }

    void StartLine(){
        if (drawing != null)
            StopCoroutine(drawing);
        drawing = StartCoroutine(DrawLine());
    }

    void EndLine(){
        StopCoroutine(drawing);
    }

    IEnumerator DrawLine(){
        List<GameObject> drawingGroup = new List<GameObject>();
        done.Push(drawingGroup);

        GameObject newObject = createLine();
        drawingGroup.Add(newObject);

        LineRenderer line = newObject.GetComponent<LineRenderer>();
        line.positionCount = 0;

        while (true){
            if (onPage){
                Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                position.z = -6;
                line.positionCount++;
                line.SetPosition(line.positionCount-1, position);
            }
            else if (line.positionCount != 0){
                newObject = createLine();
                drawingGroup.Add(newObject);

                line = newObject.GetComponent<LineRenderer>();
                line.positionCount = 0;
            }
            yield return null;
        }
    }

    public void pageUp(){
        savePage();
        activePage--;
        renderPage();
    }

    public void pageDown(){
        savePage();
        activePage++;
        renderPage();
    }

    public void undo(){
        List<GameObject> set = done.Pop();
        undone.Push(set);
        foreach(GameObject line in set)
            line.GetComponent<LineRenderer>().enabled = false;
    }

    public void redo(){
        List<GameObject> set = undone.Pop();
        done.Push(set);
        foreach(GameObject line in set)
            line.GetComponent<LineRenderer>().enabled = true;
    }
    
    public void savePage(){
        while (done.Count > 0){
            foreach(GameObject line in done.Pop())
                line.transform.parent = pages[activePage].transform;
        }
        clearUndone();
        pages[activePage].SetActive(false);
    }

    void clearUndone(){
        while (undone.Count > 0){
            foreach(GameObject line in undone.Pop())
                Destroy(line);
        }
    }

    public void renderPage(){
        pages[activePage].SetActive(true);
    }

    GameObject createLine(){
        GameObject obj = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        LineRenderer line = obj.GetComponent<LineRenderer>();
        line.startColor = lineColor;
        line.endColor = lineColor;
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        line.sortingOrder = ++layerOrder;
        return obj;
    }
}
