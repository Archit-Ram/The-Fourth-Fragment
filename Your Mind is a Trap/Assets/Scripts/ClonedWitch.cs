using System;
using System.Collections;
using UnityEngine;

public class ClonedWitch : MonoBehaviour
{
    private GameObject player;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    // private RawImage healthBar;
    // private float initial_health_width;
    private Health health;
    private int states = 2;
    public GameObject projectiles;
    public float projectile_speed = 1f;
    public float distance;
    void Start()
    {
        health = GetComponent<Health>();
        //healthBar = GetComponentInChildren<RawImage>();
        // initial_health_width = healthBar.rectTransform.rect.width;
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(Attack());
    }

    void Update()
    {
        //healthBar.rectTransform.sizeDelta = new Vector2(initial_health_width*health.health/15f, healthBar.rectTransform.rect.height);
        StartCoroutine(Hover());
        switch (states)
        {
            case 2:
            StartCoroutine(FollowPlayer());
            states = 0;
            break;
            default:
            break;
        }
        PlayAnimations();
    }

    IEnumerator Hover()
    {
        for (int i = 0;true; i++)
        {
            transform.position += new Vector3(0, 0.002f*(float)(Math.Sin(0.08*(i+1)) - Math.Sin(0.08*i)), 0);
            yield return new WaitForSeconds(0.02f);
        }
    }
    IEnumerator Attack()
    {
        for (int i = 0;true; i++)
        {
            yield return new WaitForSeconds(5f);
            for (int j = 0; j < 3; j++)
            {
                GameObject projectile = Instantiate(projectiles);
                projectile.transform.position = transform.position;
                projectile.GetComponent<Rigidbody2D>().linearVelocity = (player.transform.position - transform.position).normalized * projectile_speed;
                yield return new WaitForSeconds(0.3f);
            }
        }
    }

    IEnumerator FollowPlayer()
    {
        for (int i = 0; true; i++)
        {
            if (true) {
                if ((player.transform.position.x - distance < -8.5) || (transform.position.x > player.transform.position.x && player.transform.position.x + distance < 8.5)) {
                    if (transform.position.x < player.transform.position.x + distance - 0.5f) {
                        transform.position += new Vector3(0.07f, 0, 0);
                    } else if (transform.position.x > player.transform.position.x + distance + 0.5f) {
                        transform.position -= new Vector3(0.07f, 0, 0);
                    }
                } else {
                    if (transform.position.x > player.transform.position.x - (distance - 0.5f)) {
                        transform.position -= new Vector3(0.07f, 0, 0);
                    } else if (transform.position.x < player.transform.position.x - (distance + 0.5f)) {
                        transform.position += new Vector3(0.07f, 0, 0);
                    }
                }
            }
            transform.position = new Vector3(Math.Clamp(transform.position.x, -9, 9), Math.Clamp(transform.position.y, -3.28f, 4.22f), 0);
            
            yield return new WaitForSeconds(0.02f);
        }
    }

    void PlayAnimations()
    {
        if (transform.position.y > -3.10f)
        {
            animator.SetBool("Flying", true);
        }
        if (transform.position.x > player.transform.position.x) {
            spriteRenderer.flipX = true;
        } else {
            spriteRenderer.flipX = false;
        }
    }
}
