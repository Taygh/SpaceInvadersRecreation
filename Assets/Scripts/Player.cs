using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{    [SerializeField]
    GameObject enemyGrouping, missilePrefab, lifeOne, lifeTwo, playerPrefab;
    [SerializeField]
    GameObject[] enemyRows;
    [SerializeField]
    float movementSpeed, weaponCooldownTime, deathTime;
    [SerializeField]
    Sprite originalPlayerSprite;
    Rigidbody2D rb2D;
    Vector2 newPosition;    public Text lifeText;
    public Text gameOverText;    int lifeCount;
    bool canFire;    bool justDied;    Animator animController;
    enum Direction {RIGHT, LEFT};    Direction currentDirection;
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();        justDied = false;
        canFire = true;        animController = gameObject.GetComponent<Animator>();        animController.enabled = false;        lifeCount = 3;        lifeText.text = lifeCount.ToString();
    }

    void Update()    {
        //player movement
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            currentDirection = Direction.LEFT;            Move(currentDirection);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            currentDirection = Direction.RIGHT;
            Move(currentDirection);
        }

        //fire missile
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }

        if(gameObject.transform.position.x < -4.249998f)
        {
            gameObject.transform.position = new Vector3(-4.249998f, -3.5f, 0);
        }

        if (gameObject.transform.position.x > 4.249998f)
        {
            gameObject.transform.position = new Vector3(4.249998f, -3.5f, 0);
        }

        if (justDied == true)
        {
            SetDead();
            ResetDeath();
            LivesUpdate();
        }        
    }    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Missile" && collision.tag != "Wall")
        {
            justDied = true;
        }
    }

    void Move(Direction movement)
    {

        if(movement == Direction.LEFT)
        {
            newPosition = (Vector2) gameObject.transform.position + Vector2.left * movementSpeed;
        }

        if(movement == Direction.RIGHT)
        {
            newPosition = (Vector2) gameObject.transform.position + Vector2.right * movementSpeed;
        }

        rb2D.MovePosition(newPosition);
    }

    void Fire()
    {
        if(canFire)
        {
            Instantiate(missilePrefab, gameObject.transform.position, Quaternion.identity);
            canFire = false;
            StartCoroutine(ResetWeapon());
        }
    }

    IEnumerator ResetWeapon()
    {
        //wait for cooldown
        yield return new WaitForSeconds(weaponCooldownTime);

        canFire = true;
    }

    void LivesUpdate()
    {
        if (lifeCount == 2)
        {
            Destroy(lifeOne);
        }

        else if (lifeCount == 1)
        {
            Destroy(lifeTwo);
        }

        else if (lifeCount == 0)
        {
            gameOverText.GetComponent<Text>().enabled = true;

            for (int i = 0; i < enemyRows.Length; i++)
            {
                enemyRows[i].GetComponent<EnemyRowController>().enemySpeed = 0;
            }

            Destroy(gameObject);
        }
    }

    void SetDead()
    {
        canFire = false;
        rb2D.constraints = RigidbodyConstraints2D.FreezePositionX;
        animController.enabled = true;
        gameObject.GetComponent<Collider2D>().enabled = false;
        lifeCount--;
        lifeText.text = lifeCount.ToString();
    }

    void ResetDeath()
    {
        StartCoroutine(WaitForDeath());
        justDied = false;
    }

    IEnumerator WaitForDeath()
    {
        yield return new WaitForSeconds(2f);
        animController.enabled = false;
        gameObject.transform.position = new Vector3(0, -3.5f, 0);
        gameObject.GetComponent<SpriteRenderer>().sprite = originalPlayerSprite;
        canFire = true;
        rb2D.constraints = RigidbodyConstraints2D.None;
        gameObject.GetComponent<Collider2D>().enabled = true;
    }
}
