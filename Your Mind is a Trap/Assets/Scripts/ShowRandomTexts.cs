using UnityEngine;
using TMPro;
using System.Collections;

public class ShowRandomTexts : MonoBehaviour
{
    public string[] Texts;
    public TextMeshProUGUI Textbox;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(RandomTextChanger());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator RandomTextChanger()
    {
        yield return new WaitForSeconds(5f);
        Textbox.text = Texts[Random.Range(0, Texts.Length)];
        StartCoroutine(RandomTextChanger());
    }
}
