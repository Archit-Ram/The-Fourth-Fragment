using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
	public int attackDamage = 20;
	public float EnragedInterval = 4f;
    
    public Transform shakeTransform;
	public Vector3 attackOffset;
    public Animator animator;
	public float attackRange = 1f;
	public LayerMask attackMask;
	public float attackInterval = 6f;
	private BossHealth bossHealth;
	private PlayerHealth playerHealth;
	private SpriteRenderer spriteRenderer;
	private Transform player;

    public GameObject CircleAttackObj;

    Animator GuardianAnimator;

    void Start()
    {
        GuardianAnimator = GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		bossHealth = gameObject.GetComponent<BossHealth>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		bossHealth = gameObject.GetComponent<BossHealth>();
        StartCoroutine(WaitForPlayer());
    }
    IEnumerator WaitForPlayer()
    {
        while (player.transform.position.x < 38f) {
            yield return new WaitForSeconds(0.2f);
        };
        bossHealth.Show();
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {

        while (true)
        {
            yield return new WaitForSeconds(1f);
            Instantiate(CircleAttackObj, transform.position, Quaternion.identity);
            FindAnyObjectByType<AudioController>().PlayAudioOnce(5);
            yield return new WaitForSeconds(1f);
            Instantiate(CircleAttackObj, transform.position, Quaternion.identity);
            FindAnyObjectByType<AudioController>().PlayAudioOnce(5);
            yield return new WaitForSeconds(1f);
            Instantiate(CircleAttackObj, transform.position, Quaternion.identity);
            FindAnyObjectByType<AudioController>().PlayAudioOnce(5);
            yield return new WaitForSeconds(1f);
            Instantiate(CircleAttackObj, transform.position, Quaternion.identity);
            FindAnyObjectByType<AudioController>().PlayAudioOnce(5);
            yield return new WaitForSeconds(1f);
            Instantiate(CircleAttackObj, transform.position, Quaternion.identity);
            FindAnyObjectByType<AudioController>().PlayAudioOnce(5);
            yield return new WaitForSeconds(1f);
            StartCoroutine(GroundHitAttack());
            yield return new WaitForSeconds(5f);
        }
    }
	IEnumerator GroundHitAttack()
    {
        // Change the sprite color to indicate the attack is starting
        Color originalColor = spriteRenderer.color;
		spriteRenderer.color = Color.red;
        Debug.Log("Boss is preparing to hit the ground!");



        // Wait for 0.35 second before completing the attack
        yield return new WaitForSeconds(0.35f);


        GuardianAnimator.SetTrigger("ShakeGroundAttack");

        FindAnyObjectByType<AudioController>().PlayAudioOnce(4);

        if (shakeTransform != null)
        {
            StartCoroutine(Shake(0.7f, 0.3f));
        }
        // Simulate the boss hitting the ground
        Debug.Log("Boss hits the ground!");

        // Check if the player is on the ground
        if (FindAnyObjectByType<PlayerMovement2D>().IsGrounded)
        {
            float damage = attackDamage;
        //  damage = Mathf.Abs(transform.position.x-player.position.x)/10<0.5 ? damage*Mathf.Abs(transform.position.x-player.position.x)/10 : damage*0.5f;
            playerHealth.TakeDamage(damage);    
            Debug.Log("Player was hit by the shockwave for " + damage + " damage");
        }

        // Revert the sprite color back to the original color
        spriteRenderer.color = originalColor;
    }
    IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPosition = shakeTransform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            shakeTransform.localPosition = new Vector3(x, y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        shakeTransform.localPosition = originalPosition;
    }


	void OnDrawGizmosSelected()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Gizmos.DrawWireSphere(pos, attackRange);
	}

}

