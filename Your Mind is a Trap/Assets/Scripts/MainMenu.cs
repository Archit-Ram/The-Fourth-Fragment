using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject CreditsScreen;
    // Start is called before the first frame update
    void Start()
    {
        CreditsScreen.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGameButtonPress()
    {
        FindAnyObjectByType<SceneLoader>().LoadNextLevel();
        SceneManager.LoadScene("CutScene 1");
    }

    public  void QuitButtonPress()
    {
        Application.Quit();
    }

    public void CloseCreditsScreen()
    {
        CreditsScreen.SetActive(false);
    }

    public void OpenCreditsScreen()
    {
        CreditsScreen.SetActive(true);
    }
}
