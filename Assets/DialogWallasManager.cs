using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class DialogWallasManager : MonoBehaviour
{
    public AudioSource soundEffect;
    public AudioClip badEnding;
    public AudioClip neutralEnding;
    public AudioClip neutralEnding2;
    public AudioClip goodEnding;

    public GameObject goodEndingObject;
    public GameObject neutralEndingObject;
    public GameObject badEndingObject;

    public GameObject wallasImage;
    
    public GameObject dionObject;
    public GameObject wellasObject;

    public TMP_Text dionText;
    public TMP_Text wellasText;

    public GameObject wallasImageObject;
    public Sprite wallasBad;
    public Material wallasBadMaterial;
    
    public GameObject options;
    
    public TMP_Text optionA;
    public TMP_Text optionB;
    public TMP_Text optionC;
    
    private int step = 1;

    private int lastDecision = -1;

    private bool disableInput = false;

    private bool isDissolving;

    private float fade = 1;

    public void Awake()
    {
        wallasImage.SetActive(false);
        dionObject.SetActive(false);
        wellasObject.SetActive(true);

        dionText.text = "No";
        wellasText.text = "Whats your opinion on Pink Floyd?";
    }

    private void Update()
    {
        if (isDissolving)
        {
            fade -= Time.deltaTime;
            if (fade <= 0f)
            {
                fade = 0;
                isDissolving = false;
            }
            
            wallasBadMaterial.SetFloat("_Fade", fade);
        }
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
        wellasObject.SetActive(true);
        soundEffect.Play();

        if (step == 1)
        {
            if (lastDecision == 0)
            {
                wallasImage.SetActive(true);
            }
            else if (lastDecision == 2)
            {
                wellasText.text = "IM NOT PINK FLOYD!";
            }
            else
            {
                wellasText.text = "Damn!";
            }
            
            step = 2;
        }
    }

    public void WellasFinish()
    {
        if (disableInput) return;
        soundEffect.Stop();
        
        if (lastDecision != 1)
        {
            wallasImage.SetActive(false);
            dionObject.SetActive(true);
            wellasObject.SetActive(false);
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
                dionText.text = "Yuh uh!";
                dionText.gameObject.SetActive(true);
                options.SetActive(false);
                soundEffect.clip = new Random().Next(0, 1) == 1 ? neutralEnding : neutralEnding2;
                soundEffect.Play();
                disableInput = true;
                neutralEndingObject.SetActive(true);
                Invoke(nameof(KillYourself), 5);
            } else if (lastDecision == 1)
            {
                Image image = wallasImageObject.GetComponent<Image>();
                image.material = wallasBadMaterial;
                image.sprite = wallasBad;
                soundEffect.clip = badEnding;
                soundEffect.Play();
                isDissolving = true;
                disableInput = true;
                badEndingObject.SetActive(true);
                Invoke(nameof(KillYourself), 10);
                PlayerPrefs.SetInt("wellasEnding", 1);
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
