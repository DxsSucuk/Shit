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
    public AudioClip hostageEnding;
    public AudioClip nuclearEnding;
    public AudioClip policeEnding;

    public GameObject hostageEndingObject;
    public GameObject nuclearEndingObject;
    public GameObject policeEndingObject;

    public GameObject nuclearBombGroup;
    
    public GameObject eternityImage;
    
    public GameObject dionObject;
    public GameObject eternityObject;

    public TMP_Text dionText;
    public TMP_Text eternityText;

    public GameObject eternityImageObject;
    public Sprite eternityBad;
    public Sprite eternityHostage;

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
        eternityText.text = "There is an idea of an Alma, some kind of abstraction, but there is no real me, only an entity, something illusory, and though I can hide my cold gaze and you can shake my hand and feel flesh gripping yours and maybe you can even sense our lifestyles are probably comparable: I simply am not there.";
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
                eternityText.text = "Fuck you";
                eternityImage.SetActive(true);
            }
            else if (lastDecision == 2)
            {
                eternityText.text = "Well, we have to end apartheid for one. And slow down the nuclear arms race, stop terrorism and world hunger. " +
                                    "We have to provide food and shelter for the homeless, and oppose racial discrimination and promote civil rights, " +
                                    "while also promoting equal rights for women. We have to encourage a return to traditional moral values." +
                                    "Most importantly, we have to promote general social concern, and less materialism in young people.";
            }
            else
            {
                eternityText.text = "[Dies]";
            }
            
            step = 2;
        }
    }

    public void EternityFinish()
    {
        if (disableInput) return;
        soundEffect.Stop();
        
        if (lastDecision == -1)
        {
            eternityImage.SetActive(false);
            dionObject.SetActive(true);
            eternityObject.SetActive(false);
            soundEffect.Play();
        }

        if (step == 1)
        {
            optionA.text = "Wud?";
            optionB.text = "[Fourth Amendment]";
            optionC.text = "Yo Mama";
        } else if (step == 2)
        {
            if (lastDecision == 2)
            {
                dionText.gameObject.SetActive(false);
                options.SetActive(false);
                Image image = eternityImageObject.GetComponent<Image>();
                image.sprite = eternityHostage;
                soundEffect.clip = hostageEnding;
                soundEffect.Play();
                disableInput = true;
                hostageEndingObject.SetActive(true);
                Invoke(nameof(KillYourself), 5);
                PlayerPrefs.SetInt("eternityEnding", 2);
                PlayerPrefs.Save();
            } else if (lastDecision == 1)
            {
                DionUtil.setBlocker(true);
                eternityImageObject.transform.Rotate(0, -180, 0);
                Image image = eternityImageObject.GetComponent<Image>();
                image.sprite = eternityBad;
                soundEffect.clip = policeEnding;
                soundEffect.Play();
                disableInput = true;
                policeEndingObject.SetActive(true);
                Invoke(nameof(KillYourself), 10);
                PlayerPrefs.SetInt("eternityEnding", 1);
                PlayerPrefs.Save();
            }
            else
            {
                dionText.gameObject.SetActive(false);
                options.SetActive(false);
                soundEffect.clip = nuclearEnding;
                soundEffect.Play();
                disableInput = true;
                nuclearEndingObject.SetActive(true);
                Invoke(nameof(Nuke), 2);
                PlayerPrefs.SetInt("eternityEnding", 3);
                PlayerPrefs.Save();
            }
        }
    }

    public void Nuke()
    {
        nuclearBombGroup.SetActive(true);
    }
    
    public void KillYourself()
    {
        Application.Quit();
    }
}
