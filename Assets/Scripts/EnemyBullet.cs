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
    public int damageAmount;

    void Start()
    {
        rb.velocity = transform.right * speed;
        Destroy(gameObject,4f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            characterMovement player = other.gameObject.GetComponent<characterMovement>();
            player.KnockBack(gameObject.transform.position);
            player.TakeDamage(damageAmount);
        }
        if(other.gameObject.CompareTag("Door"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("BigBullet"))
        {
            Destroy(other.gameObject);
        }
        
    }
}
