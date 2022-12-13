using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 20;
    public Rigidbody2D rb;
    public GameObject bulletType;
    public AudioSource hit;
    public AudioSource beam;
    public int damageAmount = 10;

    void Start()
    {
        rb.velocity = transform.right * speed;
        beam.Play();
        Destroy(gameObject,4f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            characterMovement player = other.gameObject.GetComponent<characterMovement>();
            player.TakeDamage(damageAmount);
            player.KnockBack(gameObject.transform.position);
            hit.Play();

            Destroy(gameObject);
        }
        if(other.gameObject.CompareTag("Door"))
        {
            hit.Play();
            Destroy(gameObject);
        }
        if(other.gameObject.CompareTag("Ground"))
        {
            hit.Play();
            Destroy(gameObject);
        }
        

        if (other.gameObject.CompareTag("BigBullet"))
        {
            hit.Play();
            Destroy(other.gameObject);
        }
        
    }
}
