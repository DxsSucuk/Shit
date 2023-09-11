using System;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

public class CameraSex : MonoBehaviourPun
{
    public Transform target;
    public float smoothing;

    public Vector2 maxPos, minPos;

    private void FixedUpdate()
    {
        if (target.IsUnityNull()) return;
        
        if (transform.position != target.position)
        {
            Vector3 targetPos = new Vector3(target.position.x, target.position.y, target.position.z);

            targetPos.x = Mathf.Clamp(targetPos.x, minPos.x, maxPos.x);
            targetPos.y = Mathf.Clamp(targetPos.y, minPos.y, maxPos.y);

            transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
        }
    }
}