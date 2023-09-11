using System;
using UnityEngine;

public class FuckDion : MonoBehaviour
{
    public GameObject normalUI;
    public GameObject jumpscare;

    private bool killyourself;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
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