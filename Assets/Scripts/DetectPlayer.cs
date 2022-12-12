using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DetectPlayer : MonoBehaviour
{
    public Enemy enemy;
    
    // Start is called before the first frame update
    void Awake()
    {
        enemy.GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D hitInfo)
    {
        if (hitInfo.gameObject.CompareTag("Player"))
        {
            enemy.isPatroling = false;
        }
    }
}
