using System;
using Photon.Pun;
using UnityEngine;

public class FuckDion : MonoBehaviourPun
{
    public GameObject normalUI;
    public GameObject jumpscare;

    private bool killyourself;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger");
        if (!photonView.IsMine) return;
        
        if (other.CompareTag("Devil"))
        {
            if (killyourself) return;
            
            normalUI.SetActive(false);
            jumpscare.SetActive(true);

            killyourself = true;
            Invoke(nameof(KYS), 5);
        }
    }

    public void KYS()
    {
        Application.Quit();
    }
}