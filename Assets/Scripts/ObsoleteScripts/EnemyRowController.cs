using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRowController : MonoBehaviour
{
    //declarations
    Transform enemyRowTransform;
    public float repeatRate;
    public float delay;
    public int remainingEnemies = 55;
    bool[] speedUpSwitch;

    //List<Enemy> remainingEnemies;

    //serialized declarations
    [SerializeField]
    float enemyMovementRange;
    [SerializeField]
    public float enemySpeed;
    [SerializeField]
    float startDelay;
    [SerializeField]
    float moveRate = 0.3f;
    [SerializeField]
    public float speedUp = 0.87f;


    // Start is called before the first frame update
    void Start()
    {
        speedUpSwitch = new bool[4];

        for (int i = 0; i < 4; i++) 
        {
            speedUpSwitch [i] = false;
        }

        InvokeRepeating("MoveEnemyRow", 0.1f + startDelay, moveRate); //borrowed code from Stephen Barr
        //StartCoroutine(MoveEnemyRowReeat(delay, repeatRate));

        enemyRowTransform = GetComponent<Transform>();

        foreach (Transform child in enemyRowTransform)
        {
            //child is your child transform
            Enemy childEnemy = child.gameObject.GetComponent<Enemy>();
            childEnemy.AnimateEnemy();
        }

        //var remainingEnemies = FindObjectsOfType<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MoveEnemyRow()
    {
        enemyRowTransform.position += Vector3.right * enemySpeed;


        if (enemyRowTransform.position.x < -enemyMovementRange || enemyRowTransform.position.x > enemyMovementRange)
        {
            enemySpeed = -enemySpeed;
            enemyRowTransform.position += Vector3.down * 0.5f;
            return;
        }

        //manage speed up
        if (remainingEnemies == 44)
        {
            if (!speedUpSwitch[0])
            {
                CancelInvoke();
                InvokeRepeating("MoveEnemyRow", 0f, moveRate * speedUp);

                speedUpSwitch[0] = true;
            }
        }
        else if (remainingEnemies == 33)
        {
            if (!speedUpSwitch[1])
            {
                CancelInvoke();
                InvokeRepeating("MoveEnemyRow", 0f, moveRate * speedUp);

                speedUpSwitch[1] = true;
            }
        } 
        else if (remainingEnemies == 22)
        {
            if (!speedUpSwitch[2])
            {
                CancelInvoke();
                InvokeRepeating("MoveEnemyRow", 0f, moveRate * speedUp);

                speedUpSwitch[2] = true;
            }
        } 
        else if (remainingEnemies == 11)
        {
            if (!speedUpSwitch[3])
            {
                CancelInvoke();
                InvokeRepeating("MoveEnemyRow", 0f, moveRate * speedUp);

                speedUpSwitch[3] = true;
            }
        }
    }


    //public IEnumerator MoveEnemyRowRepeat(float delayStart, float repeat)
    //{
    //    yield return new WaitForSeconds(delayStart);

    //    while (true)
    //    {
    //        MoveEnemyRow();
    //        yield return new WaitForSeconds(repeat);
    //    }
    //}
}
