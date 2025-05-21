using Cinemachine;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HeartBeat : MonoBehaviour
{
    public Image RedOverlay;
    public Vector2[] safePath;
    public AudioSource audioSource;
    public AudioSource JumpScare;
    private Transform player;
    public Camera camera_for_zoom;
    public LayerMask ghostLayer;
    public int times_hit = 0;
    public GameObject JumpScareImage;
    public CinemachineVirtualCamera virtualCamera;
    public float JumpScareTime = 0f;
    public Vector3 gatePosition = new Vector3(310, 165, 0);

    bool Restart = false;
    bool Loading = false;
    void Start() {
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void FixedUpdate()
    {
        float min_dist = float.MaxValue;
        for (int i = 0; i < safePath.Length; i++) {
            min_dist = Math.Min((safePath[i] - new Vector2(player.position.x, player.position.y)).magnitude, min_dist);
        }
        if (JumpScareTime > 0) {
            JumpScareTime -= Time.deltaTime;
            if (JumpScareTime < 0) {
                JumpScareImage.SetActive(false);
            }
        }
        min_dist = Math.Clamp(min_dist, 10, 50);
        audioSource.pitch = (float)(1.5f / Math.Log10(min_dist));
        camera_for_zoom.fieldOfView = (float)(20f * Math.Log(min_dist));
        Collider2D[] cir_cast = Physics2D.OverlapCircleAll(new Vector2(player.position.x, player.position.y), 2.7f, ghostLayer);
        if (cir_cast.Length > 0) {
            JumpScareImage.SetActive(true);
            JumpScareTime = 5;
            if (!JumpScare.isPlaying) {
                JumpScare.Play();
            }
            RedOverlay.color = new Color(RedOverlay.color.r, RedOverlay.color.g, RedOverlay.color.b,times_hit*0.125f);
            times_hit += 1;
            Destroy(cir_cast[0].gameObject);
            virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain += 0.5f;
            if ((times_hit >= 5)&&(Restart==false))
            {
                Restart = true;
                StartCoroutine(GameOver());
            }
        }
        Collider2D[] cir_cast_2 = Physics2D.OverlapCircleAll(new Vector2(player.position.x, player.position.y), 10f, ghostLayer);
        foreach (Collider2D cir in cir_cast_2) {
            StartCoroutine(cir.GetComponent<Ghost>().GoesInvisAndDestroys());
        }
        if (((transform.position - gatePosition).magnitude < 10f)&&(Loading==false))
        {
            FindAnyObjectByType<SceneLoader>().LoadNextLevel();
            //SceneManager.LoadScene("Guardian Fight");
            Loading = true;
        }
    }
    IEnumerator GameOver() {
        yield return new WaitForSeconds(5);
        FindAnyObjectByType<SceneLoader>().ReloadLevel();
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        for (int i = 0; i < safePath.Length; i++) {
            Gizmos.DrawSphere(safePath[i], 0.3f);
        }
    }
#endif
}