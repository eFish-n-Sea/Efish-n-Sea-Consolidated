using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool playing;
    public int numTrans;
    public int max4stars, max3stars, max2stars, max1star;

    // Sprites that can be loaded onto tiles to show puzzle solutions
    public Sprite[] puzzleTiles;
    Dictionary<char, Sprite> spriteTable;
    results res;

    // Start is called before the first frame update
    void Start(){
        playing = false;
        numTrans = 0;
        Transform[] doors = findDoors();
        initializeSpriteTable();
        generateCodes(doors);
        res = GameObject.FindWithTag("Results").GetComponent<results>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Returns array of transforms attached to all "Door" GameObjects (must be under empty "Doors" object)
    Transform[] findDoors(){
        Transform parent = GameObject.Find("Doors").transform;
        Transform[] doors = new Transform[parent.childCount];
        for (int i = 0; i < parent.childCount; i++){
            doors[i] = parent.GetChild(i);
        }
        return doors;
    }

    public void win(){
        int stars;
        string text;
        if (numTrans <= max4stars){
            stars = 3;
            text = "Wow! You did that so fast, you must have already known the solution to the final puzzle! I have nothing more to say, you did beyond perfectly!";
        }
        else if (numTrans <= max3stars){
            stars = 3;
            text = "Nice job! By keeping track of every puzzle you solved, you were able to reach the treasure without ever needing to backtrack! Instead of " +
            "needing to backtrack and relearn previous codes, recording notes in your notepad to have quick access to the information you need is much more efficient.";
        }
        else if (numTrans <= max2stars){
            stars = 2;
            text = "Way to find your way to the treasure! However, you needed to backtrack a couple more times than would have been optimal. Try using your notepad " +
            "more to record the solutions to puzzles you've already solved so you can refer back to them more quickly and easily.";
        }
        else if (numTrans <= max1star){
            stars = 1;
            text = "You managed to find the treasure. Nice job! However, you went through doors a lot more times than you really needed to. If you want a better score, " +
            "try using your notepad to record the info you find on the way to the end of the maze. You'll be able to get through it much more easily!";
        }
        else{
            stars = 0;
            text = "You managed to find the treasure. Nice job! However, you went through doors a lot more times than you really needed to. If you want a better score, " +
            "try using your notepad to record the info you find on the way to the end of the maze. You'll be able to get through it much more easily!";
        }
        playing = false;
        StartCoroutine(res.win(stars, text));
    }

    // Given array of "Door" transforms (as found by method above), generates random codes for each door.
    // As some codes are of different input types, and some codes are meant to depend on other codes, it is best to generate each in its own method, one line at a time.
    // Refer to table shown in initializeSpriteTable to choose characters in hints correctly for tile sprite selection.
    void generateCodes(Transform[] doors){
        (string, string) door0 = genDoor0();
        doors[0].GetComponent<DoorBehavior>().setCode(door0.Item1, door0.Item2);
        doors[1].GetComponent<DoorBehavior>().setCode(door0.Item1, "");
        ((string, string), (string, string)) doors2n3 = genDoors2n3();
        doors[2].GetComponent<DoorBehavior>().setCode(doors2n3.Item1.Item1, doors2n3.Item1.Item2);
        doors[3].GetComponent<DoorBehavior>().setCode(doors2n3.Item2.Item1, doors2n3.Item2.Item2);
        (string, string) door4 = genDoor4();
        doors[4].GetComponent<DoorBehavior>().setCode(door4.Item1, door4.Item2);
        (string, string) door5 = genDoor5(door4.Item1);
        doors[5].GetComponent<DoorBehavior>().setCode(door5.Item1, door5.Item2);
        string door6 = genDoor6(door0.Item1);
        doors[6].GetComponent<DoorBehavior>().setCode(door6, "");
        string door7 = genDoor7(doors2n3.Item1.Item1, doors2n3.Item2.Item1);
        doors[7].GetComponent<DoorBehavior>().setCode(door7, "");
        string door8 = genDoor8(doors2n3.Item1.Item1, doors2n3.Item2.Item1);
        doors[8].GetComponent<DoorBehavior>().setCode(door8, "");
        string door9 = genDoor9(door5.Item1, door6);
        doors[9].GetComponent<DoorBehavior>().setCode(door9, "");
    }

    (string, string) genDoor0(){
        string output = "";
        // Colors
        char[] options = {'b', 'g', 'e', 'y'};

        // Ensures that each option is used once in the first four options
        List<char> first = new List<char>(options);
        while (first.Count > 0){
            int sel = Random.Range(0, first.Count);
            output += first[sel];
            first.RemoveAt(sel);
        }

        // Second four options are chosen truly randomly
        for (int i = 0; i < 4; i ++){
            output += options[Random.Range(0, 4)];
        }

        // Code plaintext is identical to hint plaintext
        return (output, output);
    }

    ((string, string), (string, string)) genDoors2n3(){
        string output1 = "";
        string output2 = "";

        for (int i = 0; i < 4; i++){
            int sel1 = Random.Range(1, 10);
            int sel2 = Random.Range(1, 10);
            while (sel1 == sel2 || sel1 + sel2 > 9){
                sel1 = Random.Range(1, 10);
                sel2 = Random.Range(1, 10);
            }
            if (sel1 > sel2){
                output1 += sel1;
                output2 += sel2;
            }
            else{
                output1 += sel2;
                output2 += sel1;
            }
        }

        return ((output1, output1), (output2, output2));
    }

    (string, string) genDoor4(){
        string output = "";
        List<char> options = new List<char>(new char[] {'c', 'h', 'a', 's'});
        while (options.Count > 0){
            int sel = Random.Range(0, options.Count);
            output += options[sel];
            options.RemoveAt(sel);
        }
        return (output, output);
    }

    (string, string) genDoor5(string input){
        string code = "";
        string hint = "";
        List<int> options = new List<int>(new int[] {1, 2, 3, 4});
        while (hint == "" || hint == "1234"){
            hint = "";
            while (options.Count > 0){
                int sel = Random.Range(0, options.Count);
                hint += options[sel];
                code += input[options[sel]-1];
                options.RemoveAt(sel);
            }
        }
        return (code, hint);
    }

    string genDoor6(string input){
        string output = "";
        for (int i = 7; i > 3; i--){
            output += input[i];
        }
        return output;
    }

    string genDoor7(string in1, string in2){
        int val1 = int.Parse(in1);
        int val2 = int.Parse(in2);
        return (val1 + val2).ToString();
    }

    string genDoor8(string in1, string in2){
        int val1 = int.Parse(in1);
        int val2 = int.Parse(in2);
        return (val1 - val2).ToString();
    }

    string genDoor9(string left, string right){
        return left + right;
    }

    // Called by doors to get Sprites to put over puzzle tiles. Translates a character from a code to the corresponding tile.
    public Sprite charToTile(char input){
        return spriteTable[input];
    }

    // Below table contains the translations used by charToTile. Refer to this for code generation.
    void initializeSpriteTable(){
        spriteTable = new Dictionary<char, Sprite>()
        {
            {'b', // Blue
                puzzleTiles[2]},
            {'g', // Green
                puzzleTiles[3]},
            {'e', // Red
                puzzleTiles[0]},
            {'y', // Yellow
                puzzleTiles[1]},
            
            {'i', // Circle
                puzzleTiles[15]},
            {'q', // Square
                puzzleTiles[11]},
            
            {'d', // Down
                puzzleTiles[17]},
            {'l', // Left
                puzzleTiles[18]},
            {'r', // Right
                puzzleTiles[19]},
            {'u', // Up
                puzzleTiles[16]},

            {'c', // Club
                puzzleTiles[7]},
            {'a', // Diamond
                puzzleTiles[8]},
            {'h', // Heart
                puzzleTiles[10]},
            {'s', // Spade
                puzzleTiles[9]},
            
            {'1', // One
                puzzleTiles[4]},
            {'2', // Two
                puzzleTiles[5]},
            {'3', // Three
                puzzleTiles[6]},
            {'4', // Four
                puzzleTiles[12]},
            {'5', // Five
                puzzleTiles[13]},
            {'6', // Six
                puzzleTiles[14]},
            {'7', // Seven
                puzzleTiles[20]},
            {'8', // Eight
                puzzleTiles[21]},
            {'9', // Nine
                puzzleTiles[22]},
            {'0', // Zero
                puzzleTiles[23]},
            
            {'+', // Plus
                puzzleTiles[24]},
            {'-', // Minus
                puzzleTiles[25]}
        };
    }
}
