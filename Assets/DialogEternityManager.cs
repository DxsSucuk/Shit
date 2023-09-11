using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class DialogEternityManager : MonoBehaviour
{
    public AudioSource soundEffect;
    public AudioClip badEnding;
    public AudioClip neutralEnding;
    public AudioClip neutralEnding2;
    public AudioClip goodEnding;

    public GameObject goodEndingObject;
    public GameObject neutralEndingObject;
    public GameObject badEndingObject;

    public GameObject eternityImage;
    
    public GameObject dionObject;
    public GameObject eternityObject;

    public TMP_Text dionText;
    public TMP_Text eternityText;

    public GameObject eternityImageObject;
    public Sprite eternityBad;

    public GameObject options;
    
    public TMP_Text optionA;
    public TMP_Text optionB;
    public TMP_Text optionC;
    
    private int step = 1;

    private int lastDecision = -1;

    private bool disableInput = false;

    public void Awake()
    {
        eternityImage.SetActive(false);
        dionObject.SetActive(false);
        eternityObject.SetActive(true);

        dionText.text = "No";
        eternityText.text = "Whats your opinion on Pink Floyd?";
    }

    public void DianAnswer(int i)
    {
        lastDecision = i;
        DionFinish();
    }

    public void PointerEnter(int i)
    {
        if (i == 0)
        {
            optionA.text = "> " + optionA.text;
        } else if (i == 1)
        {
            optionB.text = "> " + optionB.text;
        }
        else
        {
            optionC.text = "> " + optionC.text;
        }
    }
    
    public void PointerExit(int i)
    {
        if (i == 0)
        {
            optionA.text = optionA.text.TrimStart('>').TrimStart();
        } else if (i == 1)
        {
            optionB.text = optionB.text.TrimStart('>').TrimStart();
        }
        else
        {
            optionC.text = optionC.text.TrimStart('>').TrimStart();
        } 
    }

    public void DionFinish()
    {
        if (disableInput) return;
        soundEffect.Stop();
        dionObject.SetActive(false);
        eternityObject.SetActive(true);
        soundEffect.Play();

        if (step == 1)
        {
            if (lastDecision == 0)
            {
                eternityImage.SetActive(true);
            }
            else if (lastDecision == 2)
            {
                eternityText.text = "No?";
            }
            else
            {
                eternityText.text = "Damn!";
            }
            
            step = 2;
        }
    }

    public void EternityFinish()
    {
        if (disableInput) return;
        soundEffect.Stop();
        
        if (lastDecision != 1)
        {
            eternityImage.SetActive(false);
            dionObject.SetActive(true);
            eternityObject.SetActive(false);
            soundEffect.Play();
        }

        if (step == 1)
        {
            optionA.text = "Great!";
            optionB.text = "Fuck him!";
            optionC.text = "Wait you are musician?";
        } else if (step == 2)
        {
            if (lastDecision == 2)
            {
                dionText.text = "See ya chump!";
                dionText.gameObject.SetActive(true);
                options.SetActive(false);
                soundEffect.clip = new Random().Next(0, 1) == 1 ? neutralEnding : neutralEnding2;
                soundEffect.Play();
                disableInput = true;
                neutralEndingObject.SetActive(true);
                Invoke(nameof(KillYourself), 5);
            } else if (lastDecision == 1)
            {
                Image image = eternityImageObject.GetComponent<Image>();
                image.sprite = eternityBad;
                soundEffect.clip = badEnding;
                soundEffect.Play();
                disableInput = true;
                badEndingObject.SetActive(true);
                Invoke(nameof(KillYourself), 10);
                PlayerPrefs.SetInt("eternityEnding", 1);
                PlayerPrefs.Save();
            }
            else
            {
                dionText.text = "Well then, see you later!";
                dionText.gameObject.SetActive(true);
                options.SetActive(false);
                soundEffect.clip = goodEnding;
                soundEffect.Play();
                disableInput = true;
                goodEndingObject.SetActive(true);
                Invoke(nameof(KillYourself), 5);
            }
        }
    }

    public void KillYourself()
    {
        Application.Quit();
    }
}
