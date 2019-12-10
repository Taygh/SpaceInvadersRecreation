using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TayghEnemyManager : MonoBehaviour
{
    //Will store all enemies by row/column
    public GameObject[,] enemies;

    //Gets prefabs users want to instantiate
    public GameObject[] enemyTypes;
    //Defines how many enemies will be instantiated in a row
    public int enemiesPerRow;
    //Defines how many total rows there will be
    public int numberOfRows;
    //Defines the starting position for the first enemy (left) of the first row
    public Vector2 start;
    //Defines how far apart rows will be horizontally and vertically (depends on float inputs from designer)
    Vector2 horizontalDistanceApart, verticalDistanceApart;
    //Lets user define horizontal/vertical spacing, delay between horizontal/vertical movement, and how far enemy rows move per horizntal movement
    [SerializeField]
    float horizontalSpacing, verticalSpacing, distancePerRowMovement, horizontalMoveDelay, verticalMoveDelay;
    //Lets user define what horizontal point triggers a vertical move down for the rows
    [SerializeField]
    float flipPointX;
    //Gets names of prefabs to display in Inspector popups
    [HideInInspector]
    public string[] rowOptions;
    //Makes a new row of enemies based on assigned enemy type from rowOptions
    [HideInInspector]
    public int[] enemyRowAssignment;
    //Used to track which row the code is currently moving
    int rowCounter;

    // Start is called before the first frame update
    void Start()
    {
        //Convert user defined row spacing from float to Vector equivalent
        horizontalDistanceApart = new Vector2(horizontalSpacing, 0);
        verticalDistanceApart = new Vector2(0, -verticalSpacing);
        //Instantiate enemies
        EnemyRowInstantiation();
        //Ensure we start the program at the first row
        ResetRowCounter();
        //Begin enemy movement
        InvokeRepeating("MovementController", horizontalMoveDelay, horizontalMoveDelay);
    }

    //Function to instantiate enemy rows based on user defined row enemy types
    void EnemyRowInstantiation()
    {
        //Create empty parent object to organize rows
        GameObject newRowParent;
        enemies = new GameObject[numberOfRows, enemiesPerRow];

        //Instantiate each row as children of the empty parent object
        for (int i = 0; i < numberOfRows; i++)
        {
            newRowParent = new GameObject("Row: " + (i + 1));
            newRowParent.transform.parent = gameObject.transform;

            for (int j = 0; j < enemiesPerRow; j++)
            {
                enemies[i, j] = Instantiate(EnemyRowDecoder(i), start, Quaternion.identity);
                enemies[i, j].transform.parent = newRowParent.transform;
            }

        }

        //Take the instantiated enemies and organize them based on row spacing
        EnemyRowOrganizer();
    }

    //Function to organize enemies based on user defined spacing (horizontal distance between enemies, vertical spacing between rows)
    void EnemyRowOrganizer()
    {
        Vector2 verticalOffset;
        Vector2 horizontalOffset;

        verticalOffset = new Vector2(0, 0);

        for (int i = 0; i < numberOfRows; i++)
        {
            horizontalOffset = new Vector2(0, 0);

            //Set spacing between enemies in the row
            for (int j = 0; j < enemiesPerRow; j++)
            {
                enemies[i, j].transform.position += (Vector3) horizontalOffset + (Vector3) verticalOffset;
                horizontalOffset += horizontalDistanceApart;
            }
            //Set spacing between this row and the others
            verticalOffset += verticalDistanceApart;
        }
    }

    //Function to see what kind of enemy needs to be instantiated based on the current row and user defined enemy for that row
    GameObject EnemyRowDecoder(int row)
    {
        int currentEnemy = enemyRowAssignment[row];
        return enemyTypes[currentEnemy];
    }

    //Function that controls all movement of enemies in the game
    void MovementController()
    {
        Vector2 move = new Vector2(distancePerRowMovement, 0);
        MoveRowsHorizontally(move);
    }

    //Function that moves the enemy rows horizontally (bottom to top)
    void MoveRowsHorizontally(Vector2 moveHorizontal)
    {
        //Move all the enemies in the current row
        for (int i = 0; i < enemiesPerRow; i++)
        {
            enemies[rowCounter, i].transform.position += (Vector3)moveHorizontal; 
        }

        //Go up a row
        rowCounter--;

        //Reset the row counter if it reaches the last row and check if there should be a vertical movement of rows
        if (rowCounter < 0)
        {
            //Check if rows need to move vertically and call function for it if so
            if (CheckVertical())
            {
                ResetRowCounter();
                CancelInvoke("MovementController");
                InvokeRepeating("MoveRowsVertically", verticalMoveDelay, verticalMoveDelay);
            }

            //Just reset row counter if no vertical movement is needed
            else
            {
                ResetRowCounter();
            }
        }

    }

    //Function to move rows vertically (bottom to top)
    void MoveRowsVertically()
    {
        Vector2 moveVertical = new Vector2(0, -verticalSpacing);

        //Move rows vertically
        for (int i = 0; i < enemiesPerRow; i++)
        {
            enemies[rowCounter, i].transform.position += (Vector3)moveVertical;
        }

        //Go up a row
        rowCounter--;

        //After the last row, reset the row counter and reverse their horizontal movement
        if (rowCounter < 0)
        {
            ResetRowCounter();
            distancePerRowMovement = -distancePerRowMovement;
            CancelInvoke("MoveRowsVertically");
            InvokeRepeating("MovementController", horizontalMoveDelay, horizontalMoveDelay);
        }
    }
    
    //Function to check if a verrtical movement of rows is needed
    bool CheckVertical()
    {
        //Checks based on user defined x-coordinate to flip at (negative and positive)
        for(int i = 0; i < numberOfRows; i++)
        {
            for(int j = 0; j < enemiesPerRow; j++)
            {
                if(enemies[i, j].transform.position.x > flipPointX || enemies[i, j].transform.position.x < -flipPointX)
                {
                    return true;
                }
            }
        }

        return false;
    }

    //Function to reverse row counter after last row is moved
    void ResetRowCounter()
    {
        rowCounter = numberOfRows - 1;
    }

}
