using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;

    public GameObject bottomExplosionPrefab;

    //[SerializeField]
    //GameObject explosionPrefab;

    Rigidbody2D rb2D;
    SpriteRenderer spriteRenderer;
    Collider2D collider2D;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();

    }

    void Update()
    {
        Vector2 newPosition = (Vector2)gameObject.transform.position + Vector2.down * moveSpeed;

        rb2D.MovePosition(newPosition);
    }

    void OnTriggerEnter2D(Collider2D theOtherCollider)
    {
        if (theOtherCollider.gameObject.tag != "Octopus" && theOtherCollider.gameObject.tag != "Crab" && theOtherCollider.gameObject.tag != "Squid")
        {
            if (theOtherCollider.gameObject.tag != "Shredder")
            {
                //Instantiate(explosionPrefab, gameObject.transform.position,Quaternion.identity);

                if (theOtherCollider.gameObject.tag == "BottomShredder")
                {
                    GameObject tempExplosion = Instantiate(bottomExplosionPrefab, gameObject.transform.position, Quaternion.identity);
                    Destroy(tempExplosion, 0.3f);
                }

                if (theOtherCollider.gameObject.tag == "Shield Pieces")
                {
                    Destroy(theOtherCollider.gameObject);
                }
            }


            spriteRenderer.enabled = false;
            collider2D.enabled = false;


            Destroy(gameObject, 1f);
        }
    }
}