using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2DController : MonoBehaviour
{
    public float maxSpeed;
    private Rigidbody2D selfRigidBody;
    private Animator selfAnimator;
    private bool facingRight;
    private bool grounded = false;
    //overlap circle to check if on ground
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpForce;
    private Collider2D selfCollider;

    private bool doubleJump = false;


    // Use this for initialization
    void Start()
    {
        selfRigidBody = GetComponent<Rigidbody2D>();
        selfAnimator = GetComponent<Animator>();
        selfCollider = GetComponent<Collider2D>();
        facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    bool hitOther(RaycastHit2D[] hits)
    {
        if (hits.Length == 0 || hits.Length == 1 && hits[0].collider == selfCollider)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    //always be called after specific amount of time
    //physics engine works after each fixed update
    void FixedUpdate()
    {
        //check if grounded
        var hits = Physics2D.BoxCastAll(selfCollider.bounds.center, selfCollider.bounds.size, 0, Vector2.down, 0.1F);
        if (!hitOther(hits))
        {
            grounded = false;
        }
        else
        {
            grounded = true;
            doubleJump = false;
        }

        float move = Input.GetAxis("Horizontal");

        var faceRight = move > 0;
        //flip directions
        if (faceRight ^ facingRight)
            flip();

        var dir = faceRight ? Vector2.right : Vector2.left;

        hits = Physics2D.BoxCastAll(selfCollider.bounds.center, selfCollider.bounds.size, 0, dir, 0.1F);
        if (!hitOther(hits))
        {
            //move
            selfRigidBody.velocity = new Vector2(move * maxSpeed, selfRigidBody.velocity.y);
        }


        if (Input.GetKeyDown(KeyCode.Space)) // this should be generic jump key later
        {
            if (grounded)
            {
                // selfAnimator.SetBool("isGrounded", false);
                selfRigidBody.AddForce(new Vector2(0, jumpForce));
                grounded = false;
            }
            else if (!doubleJump)
            {
                doubleJump = true;
                selfRigidBody.AddForce(new Vector2(0, jumpForce));
                grounded = false;
            }
        }

        //animation
        selfAnimator.SetBool("moving", move != 0F);
        selfAnimator.SetFloat("speed", Mathf.Abs(move));
    }

    void flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;//obj's scale
        scale.x *= -1;
        transform.localScale = scale;
    }
}
