using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpaceToContinue : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public String text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(AnimateText(text));
    }

    IEnumerator AnimateText(String text)
    {
        int n = 0;
        while (true)
        {
            switch (n++)
            {
                case 1:
                Text.text = text;
                break;
                case 2:
                Text.text = text + ".";
                break;
                case 3:
                Text.text = text + "..";
                n = 0;
                break;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

}
