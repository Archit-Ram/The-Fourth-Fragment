using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    public float health;
    private RawImage healthBar;
    private float inital_health_width;

    bool Isdead=false;
    float Maxhealth;
    void Start()
    {
        Maxhealth = health;
        healthBar = GetComponentInChildren<RawImage>();
        inital_health_width = healthBar.rectTransform.rect.size.x;
    }
    public void TakeDamage(float damage)
    {
        FindAnyObjectByType<AudioController>().PlayAudioOnce(3);
        health -= damage;
        Debug.Log("Player took damage: " + damage + ", Health: " + health);
        if ((health <= 0)&&(Isdead==false))
        {
            Die();
            Isdead = true;
        }
        healthBar.rectTransform.sizeDelta = new Vector2(inital_health_width*health/100f, healthBar.rectTransform.rect.size.y);
    }

    public void SetHealthToMax()
    {
        health = Maxhealth;
        healthBar.rectTransform.sizeDelta = new Vector2(inital_health_width * health / 100f, healthBar.rectTransform.rect.size.y);
    }

    void Die()
    {
        FindAnyObjectByType<SceneLoader>().ReloadLevel();
    }
}
