using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game_controller : MonoBehaviour
{
    public int collectedShells;
    public GameObject[] prefabShells;
    public int shellsPerRow;
    public float genStartX, genStartY, genSpaceX, genSpaceY;
    public float time3Stars, time2Stars, time1Star;
    public bool playing;
    GameObject[] shells;

    // Start is called before the first frame update
    void Start(){
        generateShells();
        placeShells();
        playing = false;
    }

    // Update is called once per frame
    void Update(){
        if (collectedShells == 20)
            win();
    }

    void generateShells(){
        for (int i = 0; i < 20; i++){
            int shellIndex = Random.Range(0, 6);
            Instantiate(prefabShells[shellIndex]);
        }

        shells = GameObject.FindGameObjectsWithTag("Shell");

        int[] dirtyChoices = new int[] {2, 1, 4, 4, 10, 5, 6, 8, 6, 7, 7, 9, 8, 9, 10, 7, 4, 2, 4, 5};
        for (int i = 0; i < 20; i++){
            shells[i].GetComponent<shell_behavior>().dirtiness = dirtyChoices[i];
        }
    }

    void placeShells(){
        float x = genStartX;
        float y = genStartY;
        for (int i = 0; i < 20; i++){
            shells[i].transform.position = new Vector2(x, y);
            if (i % 5 == 4){
                x = genStartX;
                y += genSpaceY;
            }
            else{
                x += genSpaceX;
            }
        }
    }

    void win(){
        GameObject.FindWithTag("Catfish").GetComponent<catfish_behavior>().speed = 0;
        float time = GameObject.FindWithTag("Timer").GetComponent<timer>().win();
        int star = stars(time);
        string msg = message(star);
        StartCoroutine(GameObject.FindWithTag("Results").GetComponent<results>().win(star, msg));
    }

    int stars(float time){
        if (time < time3Stars)
            return 3;
        else if (time < time2Stars)
            return 2;
        else if (time < time1Star)
            return 1;
        else
            return 0;
    }

    string message(int stars){
        if (stars == 3){
            return "Good job! As you saw, removing steps you don't need to do anymore in a process that you may need to repeat many times will make everything much faster. " +
            "Imagine how long it would take if you had thousands of shells, with some of them needing hundreds of passes to clean, but you never put any shells in the bag until the end!";
        }
        else {
            return "Nice work, you got all those seashells sparkly clean! However, you could have done it a bit faster. When a shell starts sparkling, you can put it in the bag right away. " +
            "By taking the shells out of the cycle sooner, our catfish friend can spend less time cleaning the shells that are already done! That should make things go a lot quicker!";
        }
    }
}
