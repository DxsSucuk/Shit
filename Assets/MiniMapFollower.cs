using Unity.VisualScripting;
using UnityEngine;

public class MiniMapFollower : MonoBehaviour
{
    public Transform target;

    private void FixedUpdate()
    {
        if (target.IsUnityNull()) return;
        
        if (transform.position != target.position)
        {
            transform.position = target.position;
        }
    }
}