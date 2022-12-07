using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement; 

public class GameController : MonoBehaviour
{
    public GameObject playboxOriginal;
    public GameObject XOriginal;
    public GameObject OOriginal;
    public GameObject BlackScreen;

    public GameObject XWinnerText;
    public GameObject OWinnerText;
    public GameObject DrawText;      

    public GameObject[] playboxList = new GameObject[9];
    public float[] xPositionsList = new float[9];
    public float[] yPositionsList = new float[9];

    public enum Turn{X,O};
    public Turn turn;

    public Button resetButton; 
    
    public bool gameWon = false; 
    public bool gameOver = false; 
    public string gameWinner = ""; 

    

    public int[,] grid = new int[3,3];

    // Start is called before the first frame update
    void Start()
    {

        turn = Turn.X;
        
        createBoard();

        resetButton.gameObject.SetActive(true);

                
    }

    void Update()
    {
        if (gameOver)
        {
            StartCoroutine(Fade());
        }
    }
    
    
    

    //creates 9 tiles
    //updates playboxList to include references to all 9 tiles
    //updates xPositionsList to have the x positions of all the tiles 
    //updates yPositionsList to have the y positions of all the tiles
    void createBoard()
    {
        XWinnerText.SetActive(false);
        OWinnerText.SetActive(false);
        DrawText.SetActive(false);

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
        
        if(playboxIsEmpty(xPos, yPos))
        {
            if(turn == Turn.X)
            {
                GameObject XClone = Instantiate(XOriginal, new Vector3(xPos, yPos, 0), XOriginal.transform.rotation);
                XClone.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
                addToGrid(xPos, yPos); 
                turn = Turn.O;
            }    
            else
            {
                GameObject OClone = Instantiate(OOriginal, new Vector3(xPos, yPos, 0), OOriginal.transform.rotation);
                OClone.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
                addToGrid(xPos, yPos); 
                turn = Turn.X;
            }
        }

    }

    // function that determines if a cell is empty 
    // empty cells are marked with 0
    bool playboxIsEmpty(float xPos, float yPos)
    {
        
        return (grid[findXCoordinate(xPos), findYCoordinate(yPos)] == 0);

    }
    
    // function that adds 1 or 2 to array
    void addToGrid(float xPos, float yPos) 
    {

        // 1 = X, 2 = O
        int symbol = (turn == Turn.X) ? 1 : 2;

        grid[findXCoordinate(xPos), findYCoordinate(yPos)] = symbol; 
        
        checkWinStatus(); 

    }

    // function that ... 
    int findXCoordinate(float xPos)
    {
        int xCoord = 0;  

        if (Math.Abs(xPos+334.1088)<1) xCoord = 0; 
        else if (Math.Abs(xPos+34.10876)<1) xCoord = 1; 
        else if (Math.Abs(xPos-265.8912)<1) xCoord = 2; 

        return xCoord;
    }

    // function that ... 
    int findYCoordinate(float yPos)
    {
        int yCoord = 0;  

        if (Math.Abs(yPos-234.306)<1) yCoord = 0; 
        else if (Math.Abs(yPos+65.6939)<1) yCoord = 1; 
        else if (Math.Abs(yPos+365.694)<1) yCoord = 2;

        return yCoord;
    }

    // function that checks for win status
    void checkWinStatus() 
    {
        int symbol = (turn == Turn.X) ? 1 : 2;

        // horizontal 
        for (int i = 0; i<3; i++) {
            for (int j = 0; j<3; j++) {
                if (grid[i,j]!=symbol) break; 
                if (j==2) endGame(symbol);
            }
        }
        // vertical 
        for (int i = 0; i<3; i++) {
            for (int j = 0; j<3; j++) {
                if (grid[j,i]!=symbol) break; 
                if (j==2) endGame(symbol); 
            }
        }

        // diagonal 
        if (grid[0,0]==symbol && grid[1,1]==symbol && grid[2,2]==symbol) endGame(symbol); 
        if (grid[0,2]==symbol && grid[1,1]==symbol && grid[2,0]==symbol) endGame(symbol); 
        
        bool gridFull = GridFull(); 
        
        if (gridFull && !gameWon) endGame(0); 

    }

    // function that checks if array is full 
    bool GridFull() {

        for (int i = 0; i<3; i++)
        {
            for (int j = 0; j<3; j++)
            {
                if (grid[i,j]==0) return false; 
            }
        }
        return true; 

    }

    // function that ends the game
    void endGame(int symbol) 
    {

        switch(symbol)
        {
            case 0:
                printOnConsole("DRAW"); 
                gameWinner = "DRAW"; 
                DrawText.SetActive(true);
                break; 
            case 1:
                printOnConsole("X WINS"); 
                gameWon = true; 
                gameWinner = "X"; 
                XWinnerText.SetActive(true);
                break; 
            case 2:
                printOnConsole("O WINS"); 
                gameWon = true;
                gameWinner = "O"; 
                OWinnerText.SetActive(true);
                break; 
        }
        
        gameOver = true; 

    }

    // function that prints stuff on console
    void printOnConsole(string message) 
    {
        
        Debug.Log(message);

    }
    
    public IEnumerator Fade()
    {
        int frameCount = 0; 
        while(frameCount<100)
        {
            if (frameCount%2==0)
            {
                BlackScreen.SetActive(false); 
            } else
            {
                BlackScreen.SetActive(true);
            }
            frameCount++;
            yield return null;
        }
        
        for (int i = 0; i<9; i++)
        {
            playboxList[i].SetActive(false);
        }
     
    }


}
