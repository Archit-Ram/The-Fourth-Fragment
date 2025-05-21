using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExitDoorScript : MonoBehaviour
{
    public GameObject ExitDoorUI;
    public TextMeshProUGUI InputTextbox;

    public string CorrectText;
    // Start is called before the first frame update
    void Start()
    {
        ExitDoorUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            ExitDoorUI.SetActive(true);
        }
    }

    public void AcceptInput()
    {
        ExitDoorUI.SetActive(false);
        string InputText = InputTextbox.text;
        InputText = InputText.Substring(0, InputText.Length - 1);


        if (InputText == "520795")
        {
            FindAnyObjectByType<SceneLoader>().LoadNextLevel();
            Debug.Log("Correct Text, level passed");
        }
    }
}
