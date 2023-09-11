﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class DionUtil
{

    public static bool blocker;

    public static void LoadEasterEgg(bool luckCheck)
    {
        if (luckCheck)
        {
            if (Random.Range(2, 500) % 2 == 0 
                && PlayerPrefs.GetInt("eternityEnding", 0) == 3 
                && PlayerPrefs.GetInt("wellasEnding", 0) == 1)
            {
                SceneManager.LoadScene("SecretGamingShit");
            }
            else
            {
                blocker = false;
                Application.Quit();
            }
        }
        else
        {
            SceneManager.LoadScene("SecretGamingShit");
        }
    }
    
    public static void setBlocker(bool value)
    {
        blocker = value;
    }
    
    public static bool isDion()
    {
        // TODO:: waiting on Dion.
        return true;
    }
    
    static bool WantsToQuit()
    {
        return !blocker;
    }

    [RuntimeInitializeOnLoadMethod]
    static void RunOnStart()
    {
        Application.wantsToQuit += WantsToQuit;
    }
}