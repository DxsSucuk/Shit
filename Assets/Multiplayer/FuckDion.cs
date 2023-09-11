using System;
using Photon.Pun;
using UnityEngine;

public class FuckDion : MonoBehaviourPun
{
    public GameObject normalUI;
    public GameObject jumpscare;

    private bool killyourself;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!photonView.IsMine) return;
        
        if (other.gameObject.CompareTag("Devil"))
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