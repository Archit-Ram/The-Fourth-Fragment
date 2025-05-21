using System.Collections;
using UnityEngine;
using TMPro;

public class GuardianPhase2 : MonoBehaviour
{
    public AudioSource AudioPlayer;
    public Transform Pos1;
    public Transform Pos2;
    public GameObject LazerBeam;
    public GameObject MovingLazer;
    public GameObject HintPrefab;
    public GameObject Barrier;

    public GameObject RandomTexts;
    public TextMeshProUGUI ProgrammedTexts;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioPlayer.gameObject.SetActive(false);
        StartCoroutine(GameProgression());
        Barrier.SetActive(true);
        RandomTexts.SetActive(false);
        ProgrammedTexts.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GameProgression()
    {
        ProgrammedTexts.text = "Hello."; 
        yield return new WaitForSeconds(2);
        ProgrammedTexts.text = "Welcome To Phase 2.";
        yield return new WaitForSeconds(3);
        ProgrammedTexts.text = string.Empty;
        AudioPlayer.gameObject.SetActive(true);
        RandomTexts.SetActive(true);
        SpawnLazerRandom();
        yield return new WaitForSeconds(4);
        SpawnLazerRandom();
        SpawnLazerRandom();
        yield return new WaitForSeconds(3.5f);
        FindAnyObjectByType<MiscGuardianFight>().StopReduction();
        SpawnLazerRandom();
        SpawnLazerRandom();
        SpawnLazerRandom();
        SpawnLazerRandom();
        yield return new WaitForSeconds(1f);
        SpawnLazerRandom();
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.5f);
        SpawnLazerRandom();
        SpawnLazerRandom();
        yield return new WaitForSeconds(2f);
        SpawnLazerRandom();
        SpawnLazerRandom();
        yield return new WaitForSeconds(1f);
        SpawnLazerRandom();
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.5f);
        SpawnLazerRandom();
        SpawnLazerRandom();
        yield return new WaitForSeconds(1f);
        SpawnLazerRandom();
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.25f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.1f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.1f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.1f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.1f);
        SpawnLazerRandom();
        yield return new WaitForSeconds(0.5f);
        SpawnMovingLazer();
        yield return new WaitForSeconds(0.5f);
        SpawnMovingLazer();
        yield return new WaitForSeconds(0.5f);
        SpawnMovingLazer();
        yield return new WaitForSeconds(0.5f);
        SpawnMovingLazer();
        yield return new WaitForSeconds(0.5f);
        SpawnMovingLazer();
        yield return new WaitForSeconds(0.5f);
        SpawnMovingLazer();
        yield return new WaitForSeconds(0.5f);
        SpawnMovingLazer();
        yield return new WaitForSeconds(0.5f);
        SpawnMovingLazer();
        yield return new WaitForSeconds(0.5f);
        SpawnMovingLazer();
        yield return new WaitForSeconds(0.5f);
        SpawnMovingLazer();
        yield return new WaitForSeconds(0.5f);
        SpawnMovingLazer();
        yield return new WaitForSeconds(0.5f);
        SpawnMovingLazer();
        yield return new WaitForSeconds(0.5f);
        SpawnMovingLazer();
        yield return new WaitForSeconds(0.5f);
        SpawnMovingLazer();
        yield return new WaitForSeconds(0.5f);
        SpawnMovingLazer();
        yield return new WaitForSeconds(2f);
        SpawnLazerRandom();
        SpawnLazerRandom();
        SpawnLazerRandom();
        SpawnLazerRandom();
        SpawnLazerRandom();
        SpawnLazerRandom();
        SpawnLazerRandom();
        SpawnLazerRandom();
        SpawnLazerRandom();
        SpawnLazerRandom();
        SpawnLazerRandom();
        SpawnLazerRandom();
        SpawnLazerRandom();
        SpawnLazerRandom();
        SpawnLazerRandom();
        SpawnLazerRandom();
        SpawnLazerRandom();
        SpawnLazerRandom();
        SpawnLazerRandom();
        SpawnLazerRandom();
        yield return new WaitForSeconds(6f);
        SpawnLazerRandom();
        SpawnLazerRandom();
        SpawnLazerRandom();
        SpawnLazerRandom();
        SpawnLazerRandom();
        SpawnLazerRandom();
        SpawnLazerRandom();
        SpawnLazerRandom();
        SpawnLazerRandom();
        SpawnLazerRandom();
        SpawnLazerRandom();
        SpawnLazerRandom();
        SpawnLazerRandom();
        SpawnLazerRandom();
        SpawnLazerRandom();
        SpawnLazerRandom();
        SpawnLazerRandom();
        SpawnLazerRandom();
        SpawnLazerRandom();
        SpawnLazerRandom();
        FindAnyObjectByType<GuardianMovement>().gameObject.SetActive(true);

        RandomTexts.SetActive(false);
        yield return new WaitForSeconds(2f);
        ProgrammedTexts.text = "THAT's it!";
        yield return new WaitForSeconds(2);
        ProgrammedTexts.text = "Stop the music.";
        yield return new WaitForSeconds(2f);
        FindAnyObjectByType<MiscGuardianFight>().StopReduction();
        Barrier.SetActive(false);
        ProgrammedTexts.text = "I'm done with you.";
        yield return new WaitForSeconds(2f);
        ProgrammedTexts.text = string.Empty;


    }
    void SpawnLazerRandom()
    {
        Vector3 RandomPos = Pos1.position + ((Pos2.position-Pos1.position)*Random.Range(0,100))/100;
        Instantiate(HintPrefab, RandomPos, Quaternion.identity);
        StartCoroutine(InstantiateLazer(RandomPos,LazerBeam));
    }
    void SpawnMovingLazer()
    {
        Vector3 RandomPos = Pos1.position + ((Pos2.position - Pos1.position) * Random.Range(0, 100)) / 100;
        Instantiate(HintPrefab, RandomPos, Quaternion.identity);
        StartCoroutine(InstantiateLazer(RandomPos, MovingLazer));
    }

    IEnumerator InstantiateLazer(Vector2 RandomPos, GameObject Obj)
    {
        yield return new WaitForSeconds(1f);
        Instantiate(Obj, RandomPos, Quaternion.identity);

    }
}
