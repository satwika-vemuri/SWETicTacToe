using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public GameObject playboxOriginal;
    public GameObject XOriginal;
    public GameObject OOriginal;

    public GameObject[] playboxList = new GameObject[9];
    public float[] xPositionsList = new float[9];
    public float[] yPositionsList = new float[9];

    public enum Turn{X,O};
    public Turn turn;

    // Start is called before the first frame update
    void Start()
    {

        turn = Turn.X;
        //Instantiate(playboxOriginal, new Vector3(0, 0, 0), playboxOriginal.transform.rotation);
        
        createBoard();
        
    }

    //creates 9 tiles
    //updates playboxList to include references to all 9 tiles
    //updates xPositionsList to have the x positions of all the tiles 
    //updates yPositionsList to have the y positions of all the tiles
    void createBoard()
    {

        float[] xPositions = new float[] {playboxOriginal.transform.position.x, playboxOriginal.transform.position.x + 300, playboxOriginal.transform.position.x + 600};
        float[] yPositions = new float[] {playboxOriginal.transform.position.y, playboxOriginal.transform.position.y - 300, playboxOriginal.transform.position.y - 600};
        
        int count = 0; 
        for(int y = 0; y <= 2; y++)
        {
            for(int x = 0; x <=2; x++)
            {
                GameObject playboxClone = Instantiate(playboxOriginal, new Vector3(xPositions[x], yPositions[y], playboxOriginal.transform.position.z), playboxOriginal.transform.rotation); 
                playboxClone.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
                
                playboxList[count] = playboxClone;
                xPositionsList[count] = xPositions[x];
                yPositionsList[count] = yPositions[y];
                count++;
            }
        }

        addButtonClickFunctions(playboxList);
        
    }
    
    //adds an on click event listener to each button
    void addButtonClickFunctions(GameObject[] playboxList)
    {

        for(int i = 0; i <= 8; i++){
            float xPos = xPositionsList[i];
            float yPos = yPositionsList[i];
            Button btn = (playboxList[i]).GetComponent<Button>();
            btn.onClick.AddListener(delegate {FunctionOnClick(xPos, yPos); });
        }

    }
    
    //function that is called for each button event listener
    //creates an X or O sprite at the specified xPos and yPos
    void FunctionOnClick(float xPos, float yPos)
    {
        if(turn == Turn.X)
        {
            GameObject XClone = Instantiate(XOriginal, new Vector3(xPos, yPos, 0), XOriginal.transform.rotation);
            XClone.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
            turn = Turn.O;
        }    
        else
        {
            GameObject OClone = Instantiate(OOriginal, new Vector3(xPos, yPos, 0), OOriginal.transform.rotation);
            OClone.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
            turn = Turn.X;
        }
        
    }
}
