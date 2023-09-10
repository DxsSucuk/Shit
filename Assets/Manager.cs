using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Manager : MonoBehaviour
{
    public GameObject quitButton;
    public GameObject mainMenuGroup;
    public GameObject creditMenuGroup;

    public void Awake()
    {
        mainMenuGroup.SetActive(true);
        creditMenuGroup.SetActive(false);
    }

    public void ShitFuckNo()
    {
        // Application.Quit();
    }

    public void FuckYou()
    {
        quitButton.transform.position = new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height));
    }

    public void LoadNextShit()
    {
        SceneManager.LoadScene("TITTY_SHIT");
    }

    public void SwitchCredits()
    {
        mainMenuGroup.SetActive(false);
        creditMenuGroup.SetActive(true);
    }

    public void SwitchMain()
    {
        mainMenuGroup.SetActive(true);
        creditMenuGroup.SetActive(false);
    }
}
