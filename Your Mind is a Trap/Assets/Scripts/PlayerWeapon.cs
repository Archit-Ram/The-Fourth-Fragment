using System.Collections;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public int attackDamage = 25; // Damage dealt by the player
    public float attackRange = 1f; // Range of the player's attack
    public LayerMask enemyLayer; // Layer mask to identify the witch
    public float attackCooldown = 1f; // Cooldown between attacks

    public GameObject AttackParticlesPrefab;
    private float lastAttackTime = 0f;

    SpriteRenderer PlayerSpriteRenderer;

    Animator PlayerAnimations;

    public Transform weapon;

    GameObject Guardian;

    private void Start()
    {
        PlayerSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        PlayerAnimations = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= lastAttackTime + attackCooldown)
        {
            FindAnyObjectByType<AudioController>().PlayAudioOnce(2);
            Attack();
            PlayerAnimations.SetTrigger("IsSwinging");

        }
    }

    void Attack()
    {
        if (PlayerSpriteRenderer.flipX == false){
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 0.8f, enemyLayer); 
            if (hit.collider != null)
            {
                BossHealth HPscript = hit.transform.GetComponent<BossHealth>();
                if (HPscript)
                {
                    Guardian = hit.collider.gameObject;
                    Guardian.GetComponent<SpriteRenderer>().color = Color.red;
                    Instantiate(AttackParticlesPrefab, (Guardian.transform.position + new Vector3(0.5f, 0, 0)), Quaternion.identity);
                    HPscript.TakeDamage(attackDamage);
                    StartCoroutine(DelayClock(0.5f));
                    Debug.Log("Witch was hit for " + attackDamage + " damage");
                }
                lastAttackTime = Time.time;
            }
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, 0.8f, enemyLayer); 
            if (hit.collider != null)
            {
                BossHealth HPscript = hit.transform.GetComponent<BossHealth>();
                if (HPscript)
                {
                    Guardian = hit.collider.gameObject;
                    Guardian.GetComponent<SpriteRenderer>().color = Color.red;
                    Instantiate(AttackParticlesPrefab, (Guardian.transform.position- new Vector3(0.5f,0,0)),Quaternion.identity);
                    HPscript.TakeDamage(attackDamage);
                    StartCoroutine(DelayClock(0.5f));
                    Debug.Log("Witch was hit for " + attackDamage + " damage");
                }
                lastAttackTime = Time.time;
            }
        }
        //Collider2D hit = Physics2D.OverlapCircle(transform.position, 0.8f, enemyLayer);
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * attackRange);
    }

    IEnumerator DelayClock(float duration)
    {
        yield return new WaitForSeconds(duration);
        Guardian.GetComponent<SpriteRenderer>().color = Color.white;

    }
}
