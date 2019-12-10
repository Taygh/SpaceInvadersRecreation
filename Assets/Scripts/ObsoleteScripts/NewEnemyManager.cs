using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemyManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //This script is called by the individual enemies as they are destroyed.
    public void HandleEnemyDestroyed()
    {
        foreach (Transform child in transform)
        {
            EnemyRowController row = child.gameObject.GetComponent<EnemyRowController>();
            row.remainingEnemies--;
        }

        //remainingEnemies.Remove(theEnemyThatWasDestroyed);
        //Debug.Log("Now there are " + remainingEnemies.Count + " enemies in the list.");
        //if (remainingEnemies.Count <= 0)
        //{
        //    //youWinText.enabled = true;
        //}
    }
}
