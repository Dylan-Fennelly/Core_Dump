using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 50;
    public Rigidbody2D rb;
    public GameObject bulletType;
    void Start()
    {
        rb.velocity = transform.right * speed;
        Destroy(gameObject,6f);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if(hitInfo.gameObject.CompareTag("Door"))
        {
            Door door = hitInfo.GetComponent<Door>();
            door.OpenDoor();
        }

        if (hitInfo.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = hitInfo.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
