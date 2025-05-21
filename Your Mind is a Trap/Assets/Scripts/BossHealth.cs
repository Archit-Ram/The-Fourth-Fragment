using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour
{

	public float max_health = 500;
	public float health;

	public GameObject deathEffect;
	public bool isEnraged = false;

	public bool isInvulnerable = false;
	public RawImage healthBar;
	public GameObject healthBarFull;
	private float initial_health_length;
	public bool isHidden = true;

	public bool LoadNextScene = false;

	public GameObject PaperScroll;

	void Start(){
		initial_health_length = healthBar.rectTransform.rect.width;
		Hide();
	}

    private void Update()
    {
        if ((SceneManager.GetActiveScene().buildIndex == 5) && (health <= 50)&&(LoadNextScene==false))
        {
			FindAnyObjectByType<PlayerHealth>().SetHealthToMax();
			LoadNextScene = true;
			StartCoroutine(StartPhase2());
        }
    }
    public void Hide(){
		healthBarFull.SetActive(false);
		isHidden = true;
	}
	public void Show(){
		healthBarFull.SetActive(true);
		isHidden = false;
	}
	public float GetHealth(){
		return health;
	}
	public void SetHealth(float Newhealth){
		health = Newhealth;
	}
	public bool GetIsEnraged()
    {
        return isEnraged;
    }
	public void TakeDamage(int damage)
	{
		if (isInvulnerable)
			return;

		health -= damage;

		if (health <= 200)
		{
			/*GetComponent<Animator>().SetBool("IsEnraged", true);*/
			isEnraged = true;
		}

		if (!isHidden) {
			print(initial_health_length * health / max_health);
			healthBar.rectTransform.sizeDelta = new Vector2(initial_health_length * health / max_health, healthBar.rectTransform.rect.height);
		}

		if (health <= 0)
        {
            Instantiate(PaperScroll, transform.position, Quaternion.Euler(0,0,90f));
            Die();
		}
	}

	void Die()
    {
		GameObject.FindGameObjectWithTag("Guardian").GetComponent<GuardianMovement>().StopAllCoroutines();
        GameObject.FindGameObjectWithTag("Guardian").GetComponent<BossWeapon>().StopAllCoroutines();
        GetComponent<GuardianMovement>().enabled = false;
		GetComponent<BossWeapon>().enabled = false;	
		GetComponent<Animator>().SetBool("Die" , true);
		Instantiate(deathEffect, transform.position, Quaternion.identity);
		Destroy(gameObject.GetComponent<BossHealth>());
	//	SceneManager.LoadScene("WitchFight");
	}

	IEnumerator StartPhase2()
	{
		yield return new WaitForSeconds(1f);
		FindAnyObjectByType<SceneLoader>().LoadNextLevel();
    }

}

