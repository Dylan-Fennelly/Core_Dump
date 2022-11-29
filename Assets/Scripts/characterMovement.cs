using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMovement : MonoBehaviour
{
    // private Animator anim;
    public int health = 100;
    public float speed = 12f;
    public int jumpHeight =19;
    public int knockBackDistance = 10;
    public int knockBackHeight = 5;
    public bool facingRight;
    private Rigidbody2D body;
    public bool grounded;
    
    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRend = GetComponent<SpriteRenderer>();
        // anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        body.AddForce(new Vector2(Input.GetAxis("Horizontal")*  speed, body.velocity.y));
        if(horizontalInput> 0 && facingRight)
        {
            Flip();
        }
        else if(horizontalInput < 0 && !facingRight)
        {
            Flip();
        }
        if(Input.GetKey(KeyCode.Space) && grounded)
        {
            Jump();
        }
        // anim.SetBool("running", horizontalInput!=0);

    }
    private void Jump()
    {
        body.AddForce(Vector2.up*jumpHeight , ForceMode2D.Impulse);
        grounded = false;
        // anim.SetTrigger("jumped");
    }
    public void KnockBack(Vector2 direction)
    {
        Vector2 knockDirection = ((Vector2)transform.position - direction).normalized;
        body.velocity = ((knockDirection * knockBackDistance) + Vector2.up * knockBackHeight);
    }
    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f,180f,0f);
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
        Destroy(gameObject);
    }
    private void  OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(30);
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
}
