using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Missile : MonoBehaviour
{
    [SerializeField]
    float movementSpeed;    public GameObject gameManager;    public GameObject topExplosionPrefab;    public GameObject bottomExplosionPrefab;
    Rigidbody2D rb2D;
    AudioSource audioSource;
    SpriteRenderer spriteRenderer;
    Collider2D collider2D;
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();        gameManager = GameObject.Find("GameManager");
    }
    void Update()
    {
        Vector2 newPosition = (Vector2)gameObject.transform.position + Vector2.up * movementSpeed;
        rb2D.MovePosition(newPosition);
    }
    void OnTriggerEnter2D(Collider2D theOtherCollider)
    {
        if (theOtherCollider.gameObject.tag != "Player")
        {
            if (theOtherCollider.gameObject.tag != "Shredder")
            {
                //if it hits the enemy
                audioSource.Play();
                //Instantiate(explosionPrefab, gameObject.transform.position,Quaternion.identity);


                if (theOtherCollider.gameObject.tag == "Octopus")
                {
                    gameManager.GetComponent<GameManager>().score += 10;
                    theOtherCollider.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    theOtherCollider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }

                if (theOtherCollider.gameObject.tag == "Crab")
                {
                    gameManager.GetComponent<GameManager>().score += 20;
                    theOtherCollider.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    theOtherCollider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }


                if (theOtherCollider.gameObject.tag == "Squid")
                {
                    gameManager.GetComponent<GameManager>().score += 30;
                    theOtherCollider.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    theOtherCollider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }

                if(theOtherCollider.gameObject.tag == "TopShredder")
                {
                    GameObject tempExplosion = Instantiate(topExplosionPrefab, gameObject.transform.position, Quaternion.identity);
                    Destroy(tempExplosion, 0.3f);
                }

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
