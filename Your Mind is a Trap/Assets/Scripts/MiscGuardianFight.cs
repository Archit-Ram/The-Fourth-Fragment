using System.Collections;
using UnityEngine;

public class MiscGuardianFight : MonoBehaviour
{
    Camera Maincam;
    float InitialValue;
    bool IsReducing = false;
    public GameObject ScrollObj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Maincam = FindAnyObjectByType<Camera>();
        InitialValue = Maincam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "AttackCircle")
        {            
            gameObject.GetComponent<PlayerHealth>().TakeDamage(5f);
            Destroy(other.gameObject);
        }
        else if(other.tag == "Scroll")
        {
            StartCoroutine(Scroll(other.gameObject));
        }
    }

    public void StopReduction()
    {
        IsReducing = !IsReducing;
        if(IsReducing == false)
        {
            StartCoroutine(CameraIncrease());
        }
        else
        {
            StartCoroutine(CameraShrink());
        }
    }

    IEnumerator CameraShrink()
    {
        while (IsReducing)
        {
            Maincam.orthographicSize -= 0.004f;
            yield return new WaitForSeconds(0.1f);
        }
        
    }
    IEnumerator CameraIncrease()
    {
        while (Maincam.orthographicSize < InitialValue)
        {
            Maincam.orthographicSize += 0.1f;
            yield return new WaitForSeconds(0.01f);
        }

    }

    IEnumerator Scroll(GameObject Other)
    {
        yield return new WaitForSeconds(2);
        ScrollObj.SetActive(true);
        yield return new WaitForSeconds(2);
        GameObject.FindGameObjectWithTag("Witch").GetComponent<WaveMotion>().StopAllCoroutines();
        GameObject.FindGameObjectWithTag("Witch").GetComponent<Princess>().StartDescent();
        Destroy(Other);
    }
}
