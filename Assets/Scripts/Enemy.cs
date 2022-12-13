using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public int damageAmount;
    public Rigidbody2D body;
    public GameObject deathEffect;
    public GameObject leftCheckPoint;
    public GameObject rightCheckPoint;
    public GameObject Player;
    public bool isPatroling = true;
    private bool movingRight;
    private bool facingRight;
    public int knockBackDistance = 10;
    public int knockBackHeight = 5;
    public int speed = 75;
    public AudioSource death;
    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;
    


    void Awake()
    {
        body.GetComponent<Rigidbody2D>();
        deathEffect.GetComponent<GameObject>();
        leftCheckPoint.GetComponent<GameObject>();
        rightCheckPoint.GetComponent<GameObject>();
        Player.GetComponent<GameObject>();
        spriteRend = GetComponent<SpriteRenderer>();

    }

     void FixedUpdate()
    {
        if (isPatroling)
        {
            if (transform.position.x > rightCheckPoint.transform.position.x)
            {
                movingRight = false;
            }
            if (transform.position.x < leftCheckPoint.transform.position.x)
            {
                movingRight = true;
            }
        
            if (movingRight)
            {
                if (facingRight)
                {
                    Flip();
                }
                MoveRight();
            }
            else
            {
                if (!facingRight)
                {
                    Flip();
                }
                MoveLeft();
            }
             
        }
        else
        {
            AttackPlayer();
        }
        
        
    }

     public void AttackPlayer()
     {
         isPatroling = false;
         speed = 100;
         if (transform.position.x > Player.transform.position.x)
         {
             movingRight = false;
         }
         if (transform.position.x < Player.transform.position.x)
         {
             movingRight = true;
         }
     
         if (movingRight)
         {
             if (facingRight)
             {
                 Flip();
             }
             MoveRight();
         }
         else
         {
             if (!facingRight)
             {
                 Flip();
             }
             MoveLeft();
         }
     }


     public void TakeDamage(int damage)
    {
        health -= damage;
        AttackPlayer();
        StartCoroutine(Invunerability());
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        death.Play();
        Instantiate(deathEffect,transform.position,quaternion.identity);
        Destroy(gameObject);
    }

    public void KnockBack(Vector2 direction)
    {
        Vector2 knockDirection = ((Vector2)transform.position - direction).normalized;
        body.velocity = ((knockDirection * knockBackDistance) + Vector2.up * knockBackHeight);
    }

    private void MoveRight()
    {
        body.AddForce(Vector2.right * speed);

    }
    private void MoveLeft()
    {
        body.AddForce(Vector2.left * speed);
    }
    private void  OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            characterMovement player = other.gameObject.GetComponent<characterMovement>();
            player.KnockBack(gameObject.transform.position);
            player.TakeDamage(damageAmount);
            AttackPlayer();
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f,180f,0f);
    }

    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(7, 8, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(7, 8, false);
    }
    
}
    