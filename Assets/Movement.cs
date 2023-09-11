using Photon.Pun;
using UnityEngine;

public class Movement : MonoBehaviourPun
{
    Rigidbody2D body;

    public float horizontal;
    public float vertical;

    public float runSpeed = 20.0f;

    private Vector2 moveDir;

    private void Awake()
    {
    }

    void Start ()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!photonView.IsMine) return;
        
        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down

        if (!IsMoving())
        {
            body.velocity = Vector2.zero;
        }
        
        moveDir = new Vector2(horizontal, vertical).normalized;
    }

    void FixedUpdate()
    {
        if (!photonView.IsMine) return;
        
        body.velocity = moveDir * runSpeed * Time.deltaTime;
    }

    public bool IsMoving()
    {
        return horizontal != 0 || vertical != 0;
    }
}
