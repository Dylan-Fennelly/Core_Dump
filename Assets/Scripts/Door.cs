using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float height = 5;
    public float speed = 1f;
    public bool isDoorOpened;
    private Rigidbody2D body;
    public  GameObject movePoint;
    
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDoorOpened)
        {
            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position,movePoint.transform.position,step);
        }
    }

    public void OpenDoor()
    {
        if (!isDoorOpened)
        {
            isDoorOpened = true;
        }
    }
}
