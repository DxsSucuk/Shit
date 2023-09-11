using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Manager : MonoBehaviour
{
    public GameObject quitButton;
    public GameObject mainMenuGroup;
    public GameObject creditMenuGroup;

    public GameObject wallasObject;
    public Sprite wallasBad;

    public void Awake()
    {
        mainMenuGroup.SetActive(true);
        creditMenuGroup.SetActive(false);

        if (PlayerPrefs.GetInt("wellasEnding", 0) == 1)
        {
            Image image = wallasObject.GetComponent<Image>();
            image.sprite = wallasBad;

            wallasObject.GetComponent<Button>().interactable = false;
        }
    }

    public void ShitFuckNo()
    {
        // Application.Quit();
    }

    public void TimeToFuck()
    {
        SceneManager.LoadScene("SecretGamingShit");
    }

    public void FuckYou()
    {
        quitButton.transform.position = new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height));
    }

    public void LoadNextShit()
    {
        SceneManager.LoadScene("TITTY_SHIT");
    }


    public void LoadMySuffering()
    {
        SceneManager.LoadScene("WELLONTHESEBALLS");
    }

    public void LoadMyBallsHAGOTHIM()
    {
        SceneManager.LoadScene("CBT");
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
