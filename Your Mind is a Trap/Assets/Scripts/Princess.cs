using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Princess : MonoBehaviour
{
    public ParticleSystem hearts;
    public GameObject bubbles;
    public Sprite witch;
    private Transform player;
    private SpriteRenderer spriteRenderer;

    public TextMeshProUGUI ProgrammedTexts;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        spriteRenderer.flipX = transform.position.x > player.position.x;
    }
    public void StartDescent()
    {
        print("Starting");
        StartCoroutine(Descension());
    }
    IEnumerator Descension()
    {
        while (transform.position.y > -1.9) {
            transform.position -= new Vector3(0, 0.06f, 0);
            yield return new WaitForSeconds(0.02f);
        }
        ProgrammedTexts.text = "Thank you for saving me <3";
        hearts.Stop();
        yield return new WaitForSeconds(5f);
        bubbles.SetActive(true);
        ProgrammedTexts.text = "You thought you had all the heart pieces....";
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1f);
        GetComponent<SpriteRenderer>().sprite = witch;
        transform.localScale = new Vector3(2, 2, 2);
        transform.position = new Vector3(transform.position.x, -1.75f, transform.position.z);
        yield return new WaitForSeconds(3f);
        ProgrammedTexts.text = "Because of you, now I roam free!";
        yield return new WaitForSeconds(4f);
        FindAnyObjectByType<SceneLoader>().LoadNextLevel();
    }
}
