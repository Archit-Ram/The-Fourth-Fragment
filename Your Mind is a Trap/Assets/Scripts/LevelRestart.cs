using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
public class LevelRestart : MonoBehaviour
{
    public Sprite[] RageImages;
    public GameObject RedOverlay;
    public GameObject RageText;
    public GameObject PlayerPrefab;
    Vector3 PlayerIntialPos;
    public bool isExit = false;
    CinemachineVirtualCamera virtualCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        virtualCamera = FindAnyObjectByType<CinemachineVirtualCamera>();
        RedOverlay.SetActive(false);
        RageText.SetActive(false);
        PlayerIntialPos = PlayerPrefab.transform.position;
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            if (isExit) {
                FindAnyObjectByType<SceneLoader>().LoadNextLevel();
            } else
            {
            virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 10;
                StartCoroutine(ShowRageText());
                FindAnyObjectByType<ShrinkPlatform>().Shrink();
                FindAnyObjectByType<ShrinkPlatform>().Deaths += 1;
            }
        }
    }

    IEnumerator ShowRageText()
    {
        FindAnyObjectByType<AudioController>().PlayAudioOnce(2);
        RageText.GetComponent<Image>().sprite = RageImages[Random.Range(0,RageImages.Length)];
        RedOverlay.SetActive(true);
        RageText.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
        RageText.SetActive(false);
        RedOverlay.SetActive(false);
        PlayerPrefab.transform.position = PlayerIntialPos;
    }


}
