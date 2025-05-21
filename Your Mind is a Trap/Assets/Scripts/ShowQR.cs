using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;

public class ShowQR : MonoBehaviour
{
    public GameObject Level;
    public GameObject QRCode;
    public GameObject Player;
    public GameObject ContinueText;
    public GameObject ShowQRButton;

    [SerializeField] Sprite[] ButtonSprites;

    public Animator FadeInOutAnim;

    public CinemachineVirtualCamera PlayerCamera;
    
    public float TransitionSpeed;
    public float DefaultZoom;
    // Start is called before the first frame update
    void Start()
    {
        ShowQRButton.GetComponent<SpriteRenderer>().sprite = ButtonSprites[0];
        Level.SetActive(true);
        QRCode.SetActive(false);
        ContinueText.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (ContinueText.activeInHierarchy == true)
        {
            if (Input.GetKeyDown("e")) {
                FadeInOutAnim.SetTrigger("Exit");
                StartCoroutine(AnimationInOut(2f));
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            ShowQRButton.GetComponent<SpriteRenderer>().sprite = ButtonSprites[1];
            Debug.Log(collision.collider.name);
            Level.SetActive(false);
            QRCode.SetActive(true);
            Player.GetComponent<PlayerControllerConfusion>().enabled = false;
            FadeInOutAnim.SetTrigger("Start");
            StartCoroutine(AnimationIn(2f));
        }
    }

    IEnumerator AnimationInOut(float Duration) {

        FadeInOutAnim.SetTrigger("Exit");
        yield return new WaitForSeconds(Duration);
        
        Player.transform.position = new Vector3(Player.transform.position.x + 0.5f, Player.transform.position.y, Player.transform.position.z);
        

        Level.SetActive(true);
        QRCode.SetActive(false);
        Player.GetComponent<PlayerControllerConfusion>().enabled = true;
        ContinueText.SetActive(false);
        ShowQRButton.GetComponent<SpriteRenderer>().sprite = ButtonSprites[0];

    }

    IEnumerator AnimationIn(float Duration)
    {
        yield return new WaitForSeconds(Duration);
        ContinueText.SetActive(true);

    }

}
