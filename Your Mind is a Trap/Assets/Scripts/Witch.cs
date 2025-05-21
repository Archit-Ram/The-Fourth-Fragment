using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Witch : MonoBehaviour
{
    private GameObject player;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public RawImage healthBar;
    private float initial_health_width;
    private Health health;
    private int states = 1;
    public GameObject projectiles;
    public float projectile_speed = 1f;
    private List<GameObject> cloned_witches;
    public GameObject cloned_witch;
    private bool cloning_started = false;
    private bool blood_moon_ritual_started = false;
    private GameObject hexagonalShield;
    public GameObject hexagonalShieldPrefab;
    public float spawnRadius;
    public int numberOfMeteoroids = 6;
    private float lastRitualEndTime = 0;
    private GameObject[] meteoroids;
    private PlayerHealth playerHealth;
    public float ritualDuration = 40f;
    public GameObject meteoroidPrefab;
    public GameObject nerfBallPrefab;
    public GameObject batsPrefab;
    private bool dead;

    public AudioClip RitualClip;
    public AudioSource MusicPlayer;
    void Start()
    {
        meteoroids = new GameObject[6];
        health = GetComponent<Health>();
        initial_health_width = healthBar.rectTransform.rect.width;
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(Attack());
        cloned_witches = new List<GameObject>();
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    public IEnumerator Destroy()
    {
        animator.SetBool("Death", true);
        dead = true;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(falldown());

    }
    IEnumerator falldown()
    {
        if (spriteRenderer.flipX) {
            do
            {
                print(transform.rotation.eulerAngles.z);
                transform.Rotate(new Vector3(0, 0, -1.5f));
                yield return new WaitForSeconds(0.02f);
            } while (transform.rotation.eulerAngles.z > 270);
        } else {
            do
            {
                print(transform.rotation.eulerAngles.z);
                transform.Rotate(new Vector3(0, 0, 1.5f));
                yield return new WaitForSeconds(0.02f);
            } while (transform.rotation.eulerAngles.z < 90);
        }
        while (transform.position.y > -3.5)
        {
            transform.position -= new Vector3(0, 0.07f);
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("CutScene 3");
    }

    void Update()
    {
        healthBar.rectTransform.sizeDelta = new Vector2(initial_health_width*health.health/100f, healthBar.rectTransform.rect.height);
        StartCoroutine(Hover());
        switch (states)
        {
            case 1:
            StartCoroutine(FlyUp());
            states = 0;
            break;
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
        for (int i = 0;!dead; i++)
        {
            transform.position += new Vector3(0, 0.002f*(float)(Math.Sin(0.08*(i+1)) - Math.Sin(0.08*i)), 0);
            yield return new WaitForSeconds(0.02f);
        }
    }
    IEnumerator Attack()
    {
        for (int i = 0;!dead; i++)
        {
            if ((cloning_started && health.health > 30) || (health.health > 70) || (Time.time - lastRitualEndTime < 20f))
            {
                for (int j = 0; j < 6; j++)
                {
                    GameObject projectile = Instantiate(projectiles);
                    projectile.transform.position = transform.position;
                    projectile.GetComponent<Rigidbody2D>().linearVelocity = (player.transform.position - transform.position).normalized * projectile_speed;
                    yield return new WaitForSeconds(0.3f);
                }
            } else if (health.health > 30) {
                cloning_started = true;
                StartCoroutine(CloneWitch());
                print("here");
            } else {
                print("Hello");
                cloning_started = false;
                StartBloodMoonRitual();
            }
            yield return new WaitForSeconds(5f);
        }
    }
    void StartBloodMoonRitual()
    {
        MusicPlayer.clip = RitualClip;
        MusicPlayer.Play();
        print("current, " + Time.time);
        print("last, " + lastRitualEndTime);
        if (blood_moon_ritual_started || (Time.time - lastRitualEndTime < 20f)) return;
        blood_moon_ritual_started = true;
        StartCoroutine(GoToMiddle());
    }
    IEnumerator GoToMiddle()
    {
        while (Math.Abs(transform.position.x) > 2)
        {
            transform.position += new Vector3(1, 0, 0) * 0.2f *Math.Sign(-transform.position.x);
            yield return new WaitForSeconds(0.02f); 
        }
        //FreezeWitch();
        SpawnMeteoroids(); // Spawn meteoroids
        SpawnHexagonalShield(); // Spawn the shield
        // ritualProgressBar.gameObject.SetActive(true); // Show the progress bar
        // ritualProgressBar.maxValue = ritualDuration; // Set progress bar duration
        // ritualProgressBar.value = 0f;
        StartCoroutine(PerformRitual());
    }
//     void FreezeWitch()
// {
//     Rigidbody2D rb = GetComponent<Rigidbody2D>();
//     if (rb != null)
//     {
//         rb.bodyType = RigidbodyType2D.Static; // Freeze the witch
//     }
// }
// void UnfreezeWitch()
// {
//     Rigidbody2D rb = GetComponent<Rigidbody2D>();
//     if (rb != null)
//     {
//         rb.bodyType = RigidbodyType2D.Dynamic; // Restore witch's movement
//     }
// }

void SpawnHexagonalShield()
{
    // Create the shield around the witch
    hexagonalShield = Instantiate(hexagonalShieldPrefab, transform.position, Quaternion.identity);

    // Scale the shield to match the meteoroid pattern
    float scale = spawnRadius * 3.14f / Mathf.Sqrt(3); // Circumradius scaling
    hexagonalShield.transform.localScale = new Vector3(scale, scale, 1);

    // Attach the shield to the witch for positioning
    hexagonalShield.transform.SetParent(transform);

    // Ensure the shield is impenetrable
    Collider2D shieldCollider = hexagonalShield.GetComponent<Collider2D>();
    if (shieldCollider != null)
    {
        shieldCollider.isTrigger = false; // Make it impenetrable
    }

    // Ensure the shield is fixed in position (disable rigidbody effects)
    Rigidbody2D rb = hexagonalShield.GetComponent<Rigidbody2D>();
    if (rb != null)
    {
        rb.bodyType = RigidbodyType2D.Static; // Make the shield immovable
    }

    // Make the shield invisible by disabling its SpriteRenderer
    SpriteRenderer shieldRenderer = hexagonalShield.GetComponent<SpriteRenderer>();
    if (shieldRenderer != null)
    {
        shieldRenderer.enabled = false; // Hide the shield visually
    }
}





    void EndBloodMoonRitual(bool success)
    {
        lastRitualEndTime = Time.time; // Track the end time for cooldown
        blood_moon_ritual_started = false; // Resume witch movement
        //UnfreezeWitch();
        //ritualProgressBar.gameObject.SetActive(false); // Hide the progress bar
            GiantBall();

        if (hexagonalShield != null)
        {
            Destroy(hexagonalShield); // Remove the shield
        }

        if (success)
        {
            Debug.Log("Player successfully interrupted the ritual!");
        }
        else
        {
            Debug.Log("Ritual completed. Witch's health increased!");
            health.health = 40f;
            playerHealth.health -= playerHealth.health * 0.4f; // Reduce player's health by 40% after ritual
        }

        // Destroy remaining meteoroids
        foreach (GameObject meteoroid in meteoroids)
        {
            if (meteoroid != null)
            {
                Destroy(meteoroid);
            }
        }
    }

    IEnumerator PerformRitual()
    {
        float elapsedTime = 0f;

        while (elapsedTime < ritualDuration)
        {
            elapsedTime += Time.deltaTime;
            //ritualProgressBar.value = elapsedTime;

            // Check if all meteoroids are destroyed
            bool allDestroyed = true;
            foreach (GameObject meteoroid in meteoroids)
            {
                if (meteoroid != null)
                {
                    allDestroyed = false;
                    break;
                }
            }

            if (allDestroyed)
            {
                EndBloodMoonRitual(true); // Ritual interrupted
                yield break;
            }

            yield return null;
        }

        // Ritual completes if not interrupted
        EndBloodMoonRitual(false);
    }

    void SpawnMeteoroids()
    {
        // Clear any existing meteoroids
        foreach (GameObject meteoroid in meteoroids)
        {
            if (meteoroid != null) Destroy(meteoroid);
        }

        // Calculate and spawn meteoroids at the vertices of the hexagon
        for (int i = 0; i < numberOfMeteoroids; i++)
        {
            // Calculate angle for each vertex
            float angle = i * Mathf.PI * 2 / numberOfMeteoroids; // Divide full circle into 6 parts

            // Calculate position based on angle and spawnRadius
            Vector2 spawnPosition = new Vector2(
                transform.position.x + Mathf.Cos(angle) * spawnRadius,
                transform.position.y + Mathf.Sin(angle) * spawnRadius
            );

            // Instantiate the meteoroid at the calculated position
            meteoroids[i] = Instantiate(meteoroidPrefab, spawnPosition, Quaternion.identity);

            // Optional: Parent the meteoroid to the witch for better organization
            meteoroids[i].transform.SetParent(transform);
        }
    }
    void GiantBall()
    {
        // Create the giant ball
        GameObject giantBall = Instantiate(nerfBallPrefab, transform.position, Quaternion.identity);

        // Scale the ball to make it huge
        giantBall.transform.localScale = new Vector3(3f, 3f, 1f); // Adjust the scale for a giant size
        float sp = (giantBall.GetComponent<NerfBall>().speed);
        // Set the direction towards the player
        Vector2 direction = (player.transform.position - transform.position).normalized;

        // Add Rigidbody2D and move the ball
        Rigidbody2D rb = giantBall.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * sp; // Adjust speed as needed
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        // Add a script to handle collision and engulfing behavior
        GiantBallBehavior ballBehavior = giantBall.AddComponent<GiantBallBehavior>();
        ballBehavior.player = player.transform;
        ballBehavior.batsPrefab = batsPrefab;
        ballBehavior.numberOfBats = 8; // Number of bats to spawn
        ballBehavior.playerHealthReduction = 30f; // Amount to reduce player health
        
    }
    IEnumerator CloneWitch()
    {
        print("here2");
        while (health.health > 30f) {
                if (cloned_witches.Count < 3)
                {
                    print("Cloned");
                    GameObject clone = Instantiate(cloned_witch);
                    clone.GetComponent<Health>().health = 15f*(health.health/100f);
                    clone.GetComponent<ClonedWitch>().distance = UnityEngine.Random.Range(2, 6);
                    cloned_witches.Add(clone);
                    Vector2 position;
                    foreach (GameObject cloned_witch in cloned_witches) {
                        position = new Vector2(UnityEngine.Random.Range(-90, 90)/10f, transform.position.y);
                        while (true)
                        {
                            if (!Physics2D.OverlapBox(position, new Vector2(1, 1), 0))
                            {
                                break;
                            }
                            position = new Vector2(UnityEngine.Random.Range(-90, 90)/10f, transform.position.y);
                        }
                        cloned_witch.transform.position = position;
                    }
                    position = new Vector2(UnityEngine.Random.Range(-90, 90)/10f, transform.position.y);
                    while (true)
                    {
                        if (!Physics2D.OverlapBox(position, new Vector2(1, 1), 0))
                        {
                            break;
                        }
                        position = new Vector2(UnityEngine.Random.Range(-90, 90)/10f, transform.position.y);
                    }
                    transform.position = position;
                }
            yield return new WaitForSeconds(30f);

        }
    }
    IEnumerator FlyUp()
    {
        for (int i = 0; i < 40; i++)
        {
            transform.position += new Vector3(0, 0.07f, 0);
            yield return new WaitForSeconds(0.02f);
        }
        states = 2;
    }

    IEnumerator FollowPlayer()
    {
        for (int i = 0; !dead; i++)
        {
            if (!blood_moon_ritual_started) {
                if ((player.transform.position.x -2 < -8.5) || (transform.position.x > player.transform.position.x && player.transform.position.x + 2 < 8.5)) {
                    if (transform.position.x < player.transform.position.x + 2.5) {
                        transform.position += new Vector3(0.07f, 0, 0);
                    } else if (transform.position.x > player.transform.position.x + 3.5) {
                        transform.position -= new Vector3(0.07f, 0, 0);
                    }
                } else {
                    if (transform.position.x > player.transform.position.x - 2.5) {
                        transform.position -= new Vector3(0.07f, 0, 0);
                    } else if (transform.position.x < player.transform.position.x - 3.5) {
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
        if (!dead)
        {
            if (transform.position.x > player.transform.position.x) {
                spriteRenderer.flipX = true;
            } else {
                spriteRenderer.flipX = false;
            }
        }
    }
}
