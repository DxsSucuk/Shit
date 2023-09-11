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
    
    public GameObject eternityObject;
    public Sprite eternityHostage;
    public Sprite eternityDead;
    public Sprite eternityNuke;

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

        if (PlayerPrefs.GetInt("eternityEnding", 0) != 0)
        {
            int i = PlayerPrefs.GetInt("eternityEnding", 0);

            if (i == 1)
            {
                Image image = eternityObject.GetComponent<Image>();
                image.sprite = eternityDead;
            } else if (i == 2)
            {
                Image image = eternityObject.GetComponent<Image>();
                image.sprite = eternityHostage;
            }
            else
            {
                Image image = eternityObject.GetComponent<Image>();
                image.sprite = eternityNuke;
                
                eternityObject.GetComponent<Button>().interactable = false;
            }
        }

        Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        Screen.fullScreen = true;
    }

    public void ShitFuckNo()
    {
        // Application.Quit();
    }

    public void TimeToFuck()
    {
        DionUtil.LoadEasterEgg(false);
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
