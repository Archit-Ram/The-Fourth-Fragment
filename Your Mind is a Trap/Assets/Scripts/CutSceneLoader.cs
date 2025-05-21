using System;
using System.Collections;
using UnityEngine;

public class CutSceneLoader : MonoBehaviour
{
    private float delay = 0f;
    public SpriteRenderer[] images;
    private int i = 0;
    void Update()
    {
        if (Time.time - delay > 2f && Input.GetKeyDown(KeyCode.Space))
        {
            if (i < images.Length)
            {
                delay = Time.time;
                StartCoroutine(MakeTransparent(images[i++]));
            }
        }
    }
    IEnumerator MakeTransparent(SpriteRenderer image)
    {
        for (int i = 0; i < 50; i++)
        {
            image.color -= new Color(0, 0, 0, 0.02f);
            yield return new WaitForSeconds(0.02f);
        }
        if (i == images.Length)
        {
            FindAnyObjectByType<SceneLoader>().LoadNextLevel();
        }
    }
}
