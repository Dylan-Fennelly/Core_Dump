using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class characterMovement : MonoBehaviour
{
    // private Animator anim;
    public GameObject playerDeathEffect;
    public CapsuleCollider2D playerCollider;
    public int numofHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite EmptyHeart;
    
    public int health = 100;
    public float speed = 12f;
    public int jumpHeight =19;
    public int knockBackDistance = 10;
    public int knockBackHeight = 5;
    public bool facingRight;
    public bool playerIsDead;
    private Rigidbody2D body;
    public bool grounded;
    private float deathTimer;
    
    [Header("Dash")]
    private bool canDash;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCoolDown = 1f;
    

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;
    

    void Awake()
    {
        playerDeathEffect.GetComponent<GameObject>();
        playerCollider.GetComponent<Collider>();
        body = GetComponent<Rigidbody2D>();
        spriteRend = GetComponent<SpriteRenderer>();
        // anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        if (!playerIsDead)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            {
                StartCoroutine(Dash());
            }
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            body.AddForce(new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y));
            if (horizontalInput > 0 && facingRight)
            {
                Flip();
            }
            else if (horizontalInput < 0 && !facingRight)
            {
                Flip();
            }

            if (Input.GetKey(KeyCode.Space) && grounded)
            {
                Jump();
            }
        }
        // anim.SetBool("running", horizontalInput!=0);

    }

    void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (health > numofHearts)
            {
                health = numofHearts;
            }
            if (i < numofHearts)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = EmptyHeart;
            }

            if (i < numofHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
        
        if (playerIsDead &&(Time.realtimeSinceStartup - deathTimer) > 3)
        {
            SceneManager.LoadScene(0);
        }
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
        if (playerIsDead) return;
        Instantiate(playerDeathEffect, transform.position, Quaternion.identity);
        playerIsDead = true;
        spriteRend.enabled = false;
        playerCollider.enabled = false;
        body.simulated = false;
        deathTimer = Time.realtimeSinceStartup;


    }
    private void  OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            grounded = true;
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
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = body.gravityScale;
        body.gravityScale = 0f;
        body.AddForce(new Vector2(transform.localScale.x * dashingPower, 0f),ForceMode2D.Impulse);
        yield return new WaitForSeconds(dashingTime);
        body.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCoolDown);
        canDash = true;
    }
}
