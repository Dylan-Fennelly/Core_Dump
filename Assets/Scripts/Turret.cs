using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Turret : MonoBehaviour
{
    
    public int health = 100;
    [SerializeField] private int damageAmount;
    public GameObject deathEffect;
    public GameObject Player;
    public GameObject firePoint;
    public GameObject bullet;
    public float minTimeBetweenShots = 1f;
    public float maxTimeBetweenShots = 3f;
    private bool aimingRight;
    private bool facingRight;
    public AudioSource death;
    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;
    


    void Awake()
    {
        firePoint.GetComponent<GameObject>();
        bullet.GetComponent<GameObject>();
        deathEffect.GetComponent<GameObject>();
        Player.GetComponent<GameObject>();
        spriteRend = GetComponent<SpriteRenderer>();

    }

    void Start()
    {
        StartCoroutine(ShootBullet());
    }

     void FixedUpdate()
    {
        AttackPlayer();
        
    }

     public void AttackPlayer()
     {
         if (transform.position.x > Player.transform.position.x)
         {
             aimingRight = false;
         }
         if (transform.position.x < Player.transform.position.x)
         {
             aimingRight = true;
         }
     
         if (aimingRight)
         {
             if (facingRight)
             {
                 Flip();
             }
         }
         else
         {
             if (!facingRight)
             {
                 Flip();
             }
         }
     }


     public void TakeDamage(int damage)
    {
        health -= damage;
        StartCoroutine(Invunerability());
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        death.Play();
        Instantiate(deathEffect,transform.position,quaternion.identity);
        Destroy(gameObject);
    }
    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f,180f,0f);
    }
    private void  OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            characterMovement player = other.gameObject.GetComponent<characterMovement>();
            player.KnockBack(gameObject.transform.position);
            player.TakeDamage(damageAmount);
        }
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

    private IEnumerator ShootBullet()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTimeBetweenShots, maxTimeBetweenShots));
            Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
        }
    }
    
}
    