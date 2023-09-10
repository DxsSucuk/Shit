using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public GameObject devilObject;
    public AudioSource soundEffect;
    public AudioSource bethovenEffect;

    public GameObject dionObject;
    public GameObject scrambleObject;

    public TMP_Text dionText;
    public TMP_Text scrambleText;

    public string bibleSmth = "1 The Beginning In the beginning God created the heavens and the earth.\n" +
                              "2 Now the earth was formless and empty, darkness was over the surface of the deep, and the Spirit of God was hovering over the waters.\n" +
                              "3 And God said, “Let there be light,” and there was light.\n" +
                              "4 God saw that the light was good, and he separated the light from the darkness.\n" +
                              "5 God called the light “day,” and the darkness he called “night.” And there was evening, and there was morning—the first day.\n" +
                              "6 And God said, “Let there be a vault between the waters to separate water from water.”\n" +
                              "7 So God made the vault and separated the water under the vault from the water above it. And it was so.\n" +
                              "8 God called the vault “sky.” And there was evening, and there was morning—the second day.\n" +
                              "9 And God said, “Let the water under the sky be gathered to one place, and let dry ground appear.” And it was so.\n" +
                              "10 God called the dry ground “land,” and the gathered waters he called “seas.” And God saw that it was good.\n" +
                              "11 Then God said, “Let the land produce vegetation: seed-bearing plants and trees on the land that bear fruit with seed in it, according to their various kinds.” And it was so.\n" +
                              "12 The land produced vegetation: plants bearing seed according to their kinds and trees bearing fruit with seed in it according to their kinds. And God saw that it was good.\n" +
                              "13 And there was evening, and there was morning—the third day.\n" +
                              "14 And God said, “Let there be lights in the vault of the sky to separate the day from the night, and let them serve as signs to mark sacred times, and days and years,\n" +
                              "15 and let them be lights in the vault of the sky to give light on the earth.” And it was so.\n" +
                              "16 God made two great lights—the greater light to govern the day and the lesser light to govern the night. He also made the stars.\n" +
                              "17 God set them in the vault of the sky to give light on the earth,\n" +
                              "18 to govern the day and the night, and to separate light from darkness. And God saw that it was good.\n" +
                              "19 And there was evening, and there was morning—the fourth day.\n" + "" +
                              "20 And God said, “Let the water teem with living creatures, and let birds fly above the earth across the vault of the sky.”\n" +
                              "21 So God created the great creatures of the sea and every living thing with which the water teems and that moves about in it, according to their kinds, and every winged bird according to its kind. And God saw that it was good.\n" +
                              "22 God blessed them and said, “Be fruitful and increase in number and fill the water in the seas, and let the birds increase on the earth.”\n" +
                              "23 And there was evening, and there was morning—the fifth day.\n" +
                              "24 And God said, “Let the land produce living creatures according to their kinds: the livestock, the creatures that move along the ground, and the wild animals, each according to its kind.” And it was so.\n" +
                              "25 God made the wild animals according to their kinds, the livestock according to their kinds, and all the creatures that move along the ground according to their kinds. And God saw that it was good.\n" +
                              "26 Then God said, “Let us make mankind in our image, in our likeness, so that they may rule over the fish in the sea and the birds in the sky, over the livestock and all the wild animals,#1:26 Probable reading of the original Hebrew text (see Syriac); Masoretic Text the earth and over all the creatures that move along the ground.”\n" +
                              "27 So God created mankind in his own image, in the image of God he created them; male and female he created them.\n" +
                              "28 God blessed them and said to them, “Be fruitful and increase in number; fill the earth and subdue it. Rule over the fish in the sea and the birds in the sky and over every living creature that moves on the ground.”\n" +
                              "29 Then God said, “I give you every seed-bearing plant on the face of the whole earth and every tree that has fruit with seed in it. They will be yours for food.\n" +
                              "30 And to all the beasts of the earth and all the birds in the sky and all the creatures that move along the ground—everything that has the breath of life in it—I give every green plant for food.” And it was so.\n" +
                              "31 God saw all that he had made, and it was very good. And there was evening, and there was morning—the sixth day.";

    private int step = 1;

    public void Awake()
    {
        devilObject.SetActive(false);
        dionObject.SetActive(false);
        scrambleObject.SetActive(true);

        dionText.text = "No";
        scrambleText.text = "They Weather is shit ain't it?";
        soundEffect.Play();
    }

    public void nextDION()
    {
        if (step == 4) return;
        soundEffect.Stop();
        dionObject.SetActive(false);
        scrambleObject.SetActive(true);
        soundEffect.Play();
        if (step == 2)
        {
            bethovenEffect.Play();
            scrambleText.text = bibleSmth;
            scrambleText.fontSize = 16;
            step = 4;
            devilObject.SetActive(true);
        }
    }

    public void nextScramble()
    {
        if (step == 4) return;
        soundEffect.Stop();
        dionObject.SetActive(true);
        scrambleObject.SetActive(false);
        soundEffect.Play();
        if (step == 1)
        {
            dionText.text = "Yeah hopefully it gets better soon!";
            step = 2;
        }
    }
}