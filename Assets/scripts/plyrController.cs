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
	private bool jmp2 = false;
	//overlap circle to check if on ground
	private float groundCheckRadius = 0.2f;
	public LayerMask groundLayer;
	public Transform groundCheck;
	public float jmpHeight;
	

	// Use this for initialization
	void Start ()
	{
		myRB = GetComponent<Rigidbody2D>();
		myAnim = GetComponent<Animator>();
		facingRight = true;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (grounded && Input.GetAxis("Jump") > 0)
		{
			myAnim.SetBool("isGrounded",false);
			myRB.AddForce(new Vector2(0,jmpHeight));
			jmp2 = true;
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
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
		myAnim.SetBool("isGrounded",grounded);
		
		myAnim.SetFloat("verticalSpeed",myRB.velocity.y);
		
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
		myAnim.SetFloat("speed",Mathf.Abs(move));
	}

	void flip()
	{
		facingRight = !facingRight;
		Vector3 scale = transform.localScale;//obj's scale
		scale.x *= -1;
		transform.localScale = scale;
	}
}
