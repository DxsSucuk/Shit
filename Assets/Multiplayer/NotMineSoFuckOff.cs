using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class NotMineSoFuckOff : MonoBehaviourPun
{

    public GameObject[] fuckOff;
    private void Awake()
    {
        if (!photonView.IsMine)
        {
            if (TryGetComponent(out Rigidbody2D rigidbody2D))
            {
                rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
            }
            
            foreach (GameObject fuck in fuckOff)
            {
                fuck.SetActive(false);
            }
        }
    }
}
