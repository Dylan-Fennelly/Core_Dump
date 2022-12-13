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
    public Sprite fullHeart;
    public Sprite EmptyHeart;
    public Image[] healthBar;
    public int health = 100;
    public AudioSource death;
    public float speed = 12f;
    public int jumpHeight =19;
    public int knockBackDistance = 10;
    public int knockBackHeight = 5;
    public bool facingRight;
    public bool playerIsDead;
    private Rigidbody2D body;
    public bool grounded;
    private float deathTimer;
    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;
    

    void Awake()
    {
        for (int i = 0; i < healthBar.Length; i++)
        {
            healthBar[i].sprite = fullHeart;
        }
        playerDeathEffect.GetComponent<GameObject>();
        playerCollider.GetComponent<Collider>();
        body = GetComponent<Rigidbody2D>();
        spriteRend = GetComponent<SpriteRenderer>();
        // anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        if (!playerIsDead)
        {
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
        switch (health)
        {
            case 100:
            {
                for (int i = 0; i < healthBar.Length; i++)
                {
                    healthBar[i].sprite = fullHeart;
                }

                break;
            }
            case >= 80:
                healthBar[0].sprite = fullHeart;
                healthBar[1].sprite = fullHeart;
                healthBar[2].sprite = fullHeart;
                healthBar[3].sprite = fullHeart;
                healthBar[4].sprite = EmptyHeart;
                break;
            case >= 60:
                healthBar[0].sprite = fullHeart;
                healthBar[1].sprite = fullHeart;
                healthBar[2].sprite = fullHeart;
                healthBar[3].sprite = EmptyHeart;
                healthBar[4].sprite = EmptyHeart;
                break;
            case >= 40:
                healthBar[0].sprite = fullHeart;
                healthBar[1].sprite = fullHeart;
                healthBar[2].sprite = EmptyHeart;
                healthBar[3].sprite = EmptyHeart;
                healthBar[4].sprite = EmptyHeart;
                break;
            case >= 20:
                healthBar[0].sprite = fullHeart;
                healthBar[1].sprite = EmptyHeart;
                healthBar[2].sprite = EmptyHeart;
                healthBar[3].sprite = EmptyHeart;
                healthBar[4].sprite = EmptyHeart;
                break;
            default:
            {
                for (int i = 0; i < healthBar.Length; i++)
                {
                    healthBar[i].sprite = fullHeart;
                }

                break;
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
    public void Die()
    {
        if (playerIsDead) return;
        death.Play();
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
    
}
