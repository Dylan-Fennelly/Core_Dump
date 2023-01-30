using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlane : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.Die();
        }
        if(other.gameObject.CompareTag("Player"))
        {
            characterMovement player = other.gameObject.GetComponent<characterMovement>();
            player.Die();
        }
    }
}
