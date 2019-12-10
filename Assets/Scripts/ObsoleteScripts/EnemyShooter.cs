using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//
/// <summary>
/// 
/// SCRIPT NOT USED!!!!!!!
/// 
/// </summary>

public class EnemyShooter : MonoBehaviour
{

    [SerializeField]
    float minimum = 0f;

    [SerializeField]
    float maximum = 10f;

    void Start()
    {
        //script modified from danz1ka19
        float rand = Random.Range(minimum, maximum);
        Invoke("SelectForFire", rand);
    }

    void ShootSelect()
    {
        //script modified from danz1ka19
        if (transform.childCount > 0)
        {
            transform.GetChild(0).gameObject.GetComponent<Enemy>().Invoke("EnemyShoot", 0f);

            float rand = Random.Range(minimum, maximum / 4);
            Invoke("ShootSelect", rand);
        }
    }
}
