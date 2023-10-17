using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int jumpPower = 18;
    [SerializeField] private float moveSpeed = 15f;
    public float dirX = 0f;
    [SerializeField] private GhostTrail ghostTrail;

    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D coll;
    private TimeManager timemanager;

    [SerializeField] private LayerMask jumpableGround;

    private enum MovementState { idle, running, jumping, falling }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        timemanager = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>();
    }

    // Update is called once per frame
    void Update()
    {

        dirX = Input.GetAxisRaw("Horizontal");

        rb.velocity = new UnityEngine.Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new UnityEngine.Vector2(rb.velocity.x, jumpPower);
        }

        UpdateAnimation();

        if (Input.GetKeyDown(KeyCode.Q) && !(timemanager.TimeIsStopped)) //Stop Time when Q is pressed
        {
            timemanager.StopTime();
        }
        else if (Input.GetKeyDown(KeyCode.Q) && timemanager.TimeIsStopped)  //Continue Time when E is pressed
        {
            timemanager.ContinueTime();
        }

    }

    private void UpdateAnimation()
    {
        MovementState state;
        if (dirX > 0f)
        {
            ghostTrail.makeGhost = true;
            state = MovementState.running;
            transform.localScale = new UnityEngine.Vector3(1.48734f, 1.48734f, 1.48734f);
        }
        else if (dirX < 0)
        {
            ghostTrail.makeGhost = true;
            state = MovementState.running;
            transform.localScale = new UnityEngine.Vector3(-1.48734f, 1.48734f, 1.48734f);
        }
        else
        {
            ghostTrail.makeGhost = false;
            state = MovementState.idle;
        }

        if (rb.velocity.y > 0.1f)
        {
            ghostTrail.makeGhost = true;
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -0.1f)
        {
            ghostTrail.makeGhost = true;
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, UnityEngine.Vector2.down, 0.1f, jumpableGround);
    }
}