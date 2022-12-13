using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 20;
    public Rigidbody2D rb;
    public AudioSource hit;
    public GameObject bulletType;
    void Start()
    {
        rb.velocity = transform.right * speed;
        Destroy(gameObject,4f);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if(hitInfo.gameObject.CompareTag("Door"))
        {
            Door door = hitInfo.GetComponent<Door>();
            door.OpenDoor();
            hit.Play();
            Destroy(gameObject);
        }

        if (hitInfo.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = hitInfo.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
            enemy.KnockBack(gameObject.transform.position);
            hit.Play();
            Destroy(gameObject);
        }
        if (hitInfo.gameObject.CompareTag("Turret"))
        {
            Turret enemy = hitInfo.GetComponent<Turret>();
            enemy.TakeDamage(damage);
            hit.Play();
            Destroy(gameObject);
        }

        if (hitInfo.gameObject.CompareTag("BigBullet"))
        {
            hit.Play();
            Destroy(hitInfo.gameObject);
        }
        
    }
}
