using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(TayghEnemyManager))]
public class TayghEnemyManagerEditor : Editor
{
    //Reference to original manager
    TayghEnemyManager manager;


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        //Setup the manager for user input
        manager = (TayghEnemyManager) target;
        manager.rowOptions = new string[manager.numberOfRows];

        //Direction for user
        GUILayout.Label("Select an enemy type for each row");

        //Dynamically allocate rows for assignment based on how many rows the user has defined there to be in all
        if(manager.enemyRowAssignment == null || manager.enemyRowAssignment.Length != manager.numberOfRows)
        {
            manager.enemyRowAssignment = new int[manager.numberOfRows];
        }

        //Create Inspector dropdowns for enemy row assignment based on number of total rows
        for(int i = 0; i < manager.numberOfRows; i++)
        {
            EnemySelections(i, manager);
        }
    }

    //Function to create dropdown menus for enemy row assignment based on number of total rows and number of enemy prefabs
    void EnemySelections(int index, TayghEnemyManager manager)
    {
        GameObject[] enemyChoices;

        if (manager.enemyTypes != null)
        {
            //Get enemy prefab names to put into dropdown menus
            enemyChoices = manager.enemyTypes;
            manager.rowOptions = new string[enemyChoices.Length];
            for (int i = 0; i < enemyChoices.Length; i++)
            {
                if (enemyChoices[i] != null)
                {
                    manager.rowOptions[i] = enemyChoices[i].name;
                }
            }

            //Create dropdown menus with numbered rows for clarity
            GUILayout.BeginHorizontal("Row Selection");

            EditorGUILayout.LabelField("Row " + (index + 1) + ":");
            GUILayout.Space(-260);
            manager.enemyRowAssignment[index] = EditorGUILayout.Popup(manager.enemyRowAssignment[index], manager.rowOptions);

            GUILayout.EndHorizontal();
        }
    }




}
