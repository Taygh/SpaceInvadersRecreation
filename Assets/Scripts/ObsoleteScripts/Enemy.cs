using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //It is set by the EnemyManager script when it spawns a new Enemy.
    [SerializeField]
    NewEnemyManager enemyManager;

    //public Sprite firstSprite;
    //public Sprite secondSprite;

    [SerializeField]
    EnemyMissile enemyMissilePrefeb;

    [SerializeField]
    Sprite explosion;

    [SerializeField]
    Sprite firstSprite;
    [SerializeField]
    Sprite secondSprite;

    [SerializeField]
    float shootMinimum = 0f;

    [SerializeField]
    float shootMaximum = 10f;

    SpriteRenderer currentSprite;

    void Start()
    {
        currentSprite = GetComponent<SpriteRenderer>();

        Invoke("SelectWhenShoot", 1f);
    }

    void Update()
    {
    }


    void OnTriggerEnter2D(Collider2D theOtherCollider)
    {
        //Tell EnemyManager that I am dying
        if (theOtherCollider.gameObject.tag == "Missile")
        {
            currentSprite.sprite = explosion;
            enemyManager.HandleEnemyDestroyed();
            Destroy(theOtherCollider.gameObject, 0.01f);
            Destroy(gameObject, 0.075f);
        }
    }

    //animate
    public void AnimateEnemy()
    {
        //animation
        if (currentSprite.sprite == firstSprite)
        {
            currentSprite.sprite = secondSprite;
        }
        else
        {
            currentSprite.sprite = firstSprite;
        }
    }

    void SelectWhenShoot()
    {
        float time = Random.Range(shootMinimum, shootMaximum);
        Invoke("EnemyShoot", time);
    }

    void EnemyShoot()
    {
        float x = transform.position.x;
        float y = transform.position.y - 0.4f;
        Instantiate(enemyMissilePrefeb, new Vector2(x, y), Quaternion.identity);

        float time = Random.Range(shootMinimum, shootMaximum);
        Invoke("ChooseWhenFire", time);
    }
}
