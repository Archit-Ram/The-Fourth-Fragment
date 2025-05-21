using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float health = 100f;
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Witch witch = transform.GetComponent<Witch>();
            if (witch != null)  {
                StartCoroutine(witch.Destroy());
            } else {
                Destroy(gameObject);
            }
        }
    }
}
