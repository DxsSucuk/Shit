using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NuclearBombScript : MonoBehaviour
{
    private bool nuked;

    public GameObject videoPlayer;
    
    public float speed = 3;
    
    public Image Image;
    
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -364)
        {
            if (!nuked)
            {
                DionUtil.setBlocker(true);
                videoPlayer.SetActive(true);
                Image.enabled = false;
                nuked = true;
            }
        }
        else
        {
            if (nuked) return;
            
            Vector3 position = transform.position;

            position.y -= speed + Time.deltaTime;

            transform.position = position;
        }
    }
}
