using System.Collections;
using UnityEngine;

public class ChangeBar : MonoBehaviour
{
    public int time;
    public RectTransform bar;
    public float TimePassed;
    public float w;

    public GameObject HintText;
    void Start() {
        TimePassed = time;
        w = bar.rect.width;
        HintText.SetActive(false);
    }
    void Update() {
        TimePassed -= Time.deltaTime;
        if (TimePassed < 0) {
            TimePassed = time;
            StartCoroutine(ShowHintText());
        }
        bar.sizeDelta = new Vector2(w*(TimePassed/time), bar.sizeDelta.y);
    }

    IEnumerator ShowHintText()
    {
        HintText.SetActive(true);
        yield return new WaitForSeconds(2);
        HintText.SetActive(false);
    }
    
}
