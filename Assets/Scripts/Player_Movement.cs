using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [Header("Basics")]
    public int MidAirJumps=1;//initialized mid-air jumps when grounded
    public float JumpSpeed=15,MaxRunSpeed=20, MaxWalkSpeed=10, RunForce=40;

    [Header("Advanced")]
    public float DragFactor=2f;//used to slow down to max speed rather than instantaneous
    public float WallJumpVerticalSpeed=10, WallJumpHorizontalSpeed=20;
    
    private int JumpCount; //used to count remaining mid-air jumps
    private bool IsGrounded,IsWalled,IsMoving; //booleans for jump logic
    private Rigidbody2D MyRB; //player rigidbody
    private Animator myAnimator;
    private Transform GroundCheck,WallCheck; //child transforms used for booleans
    private Vector3 WallCheckDist; //allows one wallcheck to check BOTH sides

    private bool PreviousFaceRight,FacingRight;
    
    // Start is called before the first frame update
    void Start()
    {
        MyRB = gameObject.GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        GroundCheck = gameObject.transform.GetChild(0); //first and second child should correspond to ground and wall check
        WallCheck = gameObject.transform.GetChild(1);
        JumpCount = 0; //intialize jumps to 0
        WallCheckDist = WallCheck.position - gameObject.transform.position; //initial distance of wallcheck maintained
        FacingRight = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (FacingRight && Mathf.Sign(MyRB.velocity.x)<0f)
        {
            FacingRight = false;
            gameObject.transform.localScale = new Vector3(-1f, 1f, 0f);
        }
        else if (!FacingRight && Mathf.Sign(MyRB.velocity.x) > 0f)
        {
            FacingRight = true;
            gameObject.transform.localScale = new Vector3(1f, 1f, 0f);
        }
        //JUMP LOGIC
        IsGrounded = Physics2D.Linecast(gameObject.transform.position, GroundCheck.position, LayerMask.GetMask("Ground")).normal == Vector2.up;//Check to see if ground  is directly below player for groundcheck
        IsWalled =Mathf.Abs( Vector2.Dot(Physics2D.Linecast(gameObject.transform.position, WallCheck.position, LayerMask.GetMask("Wall")).normal, Vector2.left) )== 1f; //Normal is horizontal
        IsMoving = MyRB.velocity.x != 0f || !IsGrounded;
        myAnimator.SetBool("isRunning", IsMoving);
        if (IsGrounded)
        {
            JumpCount = MidAirJumps;//restore jumps
        }
        if (Input.GetButtonDown("Jump") && (IsGrounded || JumpCount > 0)) //Either grounded or has remaining mid-air jumps
        {
            
            if (!IsGrounded)
            {

                JumpCount -= 1;//mid air jump used
            }
            MyRB.velocity = new Vector2(MyRB.velocity.x, JumpSpeed); //If jumpforce is used, force may be applied multiple frames

        }
        if(IsWalled && Input.GetButtonDown("Jump"))
        {
            Debug.Log("YES");
            MyRB.velocity = new Vector2(-WallJumpHorizontalSpeed* Input.GetAxisRaw("Horizontal"), WallJumpVerticalSpeed);
        }

        // HORIZONTAL MOVEMENT LOGIC
        MyRB.AddForce(Vector2.right * RunForce * Input.GetAxisRaw("Horizontal"));
        
        
        if (Input.GetButton("Dash")) //sprinting
        {
            if (Mathf.Abs(MyRB.velocity.x) > MaxRunSpeed)//clamp run speed to max
            {
                MyRB.velocity = new Vector2(Mathf.Clamp(MyRB.velocity.x, -MaxRunSpeed, MaxRunSpeed), MyRB.velocity.y);
            }
        }
        else
        {

            if (Mathf.Abs(MyRB.velocity.x) > 1.5f * MaxWalkSpeed) //if high over regular limit, decrease speed gradually rather than instantly
            {
                MyRB.AddForce(new Vector2(-DragFactor * RunForce * MyRB.velocity.x / MaxWalkSpeed, 0f));//decelerate
            }
            else
            {
                MyRB.velocity = new Vector2(Mathf.Clamp(MyRB.velocity.x, -MaxWalkSpeed, MaxWalkSpeed), MyRB.velocity.y);
            }

        }


        WallCheck.position = gameObject.transform.position + WallCheckDist * Input.GetAxisRaw("Horizontal");
    }
}