using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plyrController : MonoBehaviour
{
	public float maxSpeed;
	private Rigidbody2D myRB;
	private Animator myAnim;
	private bool facingRight;
	private bool grounded = false;
	//overlap circle to check if on ground
	public float groundCheckRadius = 0.2f;
	public LayerMask groundLayer;
	public Transform groundCheck;
	public float jmpHeight;
    private Collider2D selfCollider;
	

	// Use this for initialization
	void Start ()
	{
		myRB = GetComponent<Rigidbody2D>();
		myAnim = GetComponent<Animator>();
        selfCollider = GetComponent<Collider2D>();
        facingRight = true;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (grounded && Input.GetAxis("Jump") > 0)
		{
            Debug.Log("JUMP");
			//myAnim.SetBool("isGrounded",false);
			myRB.AddForce(new Vector2(0,jmpHeight));
			grounded = false;
		}
		/*else if (!grounded && jmp2 && Input.GetAxis("Jump") > 0)
		{
			myAnim.SetBool("isGrounded",false);
			myRB.AddForce(new Vector2(0,jmpHeight));
			jmp2 = false;
		}*/
	}
	//always be called after specific amount of time 
	//physics engine works after each fixed update
	void FixedUpdate ()
	{
        //check if grounded
        var hit = Physics2D.Raycast(selfCollider.bounds.min, Vector2.down, 0.1F);
        grounded = hit.collider != null;
		//myAnim.SetBool("isGrounded",grounded);
		
		//myAnim.SetFloat("verticalSpeed",myRB.velocity.y);
		
		float move = Input.GetAxis("Horizontal");
		
		//move
		myRB.velocity = new Vector2(move*maxSpeed,myRB.velocity.y);
		//flip directions
		if (move > 0 && !facingRight)
		{
			flip();
		}else if (move < 0 && facingRight)
		{
			flip();
		}
		
		//animation
		//myAnim.SetFloat("speed",Mathf.Abs(move));
	}

	void flip()
	{
		facingRight = !facingRight;
		Vector3 scale = transform.localScale;//obj's scale
		scale.x *= -1;
		transform.localScale = scale;
	}
}
