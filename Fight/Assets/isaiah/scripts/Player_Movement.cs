using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
  public Rigidbody2D Rigidbody2D;
  private Animator Animator;
  private SpriteRenderer SpriteRenderer;
  public Player_Attack Player_Attack;

  public bool canMove;
  public float moveSpeed;

  public bool isGrounded;
  public bool isFalling;
  public bool isAscending;
  public float gravityScale;
  public float bounceForce;
  public int jumps;
  public float jumpForce;
  private float jumpTimer;
  public float jumpTimerMax;
  private bool pressedJump;
  private bool startJumpTimer;
  private bool releasedJump;

  public string playerDirection;
  public string playerLook;

  public string secretMoves;
  public string secretCode;

  private void Start()
  {
    secretMoves = "";
    secretCode = "UUDDLRLR";
  }

  private void Awake()
  {
    Rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    Animator = gameObject.GetComponent<Animator>();
    SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    Player_Attack = gameObject.GetComponent<Player_Attack>();

    canMove = true;
    moveSpeed = 10f;

    isGrounded = true;
    isFalling = false;
    isAscending = false;
    gravityScale = 5f;
    bounceForce = 10f;
    jumps = 2;
    jumpForce = 15f;
    jumpTimerMax = .01f;
    jumpTimer = jumpTimerMax;
    pressedJump = false;
    startJumpTimer = false;
    releasedJump = false;

    playerDirection = "R";
    playerLook = "";

    Rigidbody2D.gravityScale = gravityScale;
    Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
    Animator.SetBool("isRunning", false);
    Animator.SetInteger("Jumps", jumps);
  }

  private void Update()
  {
    Movement();
  }

  private void FixedUpdate()
  {
    if (pressedJump)
    {
      StartJump();
    }

    if (releasedJump)
    {
      StopJump();
    }

    if (startJumpTimer)
    {
      jumpTimer -= Time.deltaTime;
      if (jumpTimer <= 0)
      {
        releasedJump = true;
      }
    }
  }

  private void Movement()
  {

    Vector3 movement = new Vector3(0f, 0f, 0f);

    if (gameObject.name == "Player_One")
    {

      // JUMP
      if (Input.GetButtonDown("Player_One_Jump") && jumps > 0 && canMove)
      {
        pressedJump = true;
      }
      if (Input.GetButtonUp("Player_One_Jump"))
      {
        releasedJump = true;
      }

      // LOOK UP AND DOWN
      if (Input.GetAxis("Player_One_Vertical") > 0 && playerLook != "U" && !Player_Attack.isAttacking)
      {
        playerLook = "U";
        Animator.SetInteger("Look", 1);
      }
      else if (Input.GetAxis("Player_One_Vertical") == 0 && playerLook != "" && !Player_Attack.isAttacking)
      {
        playerLook = "";
        Animator.SetInteger("Look", 0);
        // Debug.Log("Looking Neutral");
      }
      else if (Input.GetAxis("Player_One_Vertical") < 0 && playerLook != "D" && !Player_Attack.isAttacking)
      {
        playerLook = "D";
        Animator.SetInteger("Look", -1);
      }

      // HORIZONTAL MOVEMENT
      movement = new Vector3(Input.GetAxis("Player_One_Horizontal"), 0f, 0f);

    }

    else if (gameObject.name == "Player_Two")
    {

      // JUMP
      if (Input.GetButtonDown("Player_Two_Jump") && jumps > 0 && canMove)
      {
        pressedJump = true;
      }
      if (Input.GetButtonUp("Player_Two_Jump"))
      {
        releasedJump = true;
      }

      // LOOK UP AND DOWN
      if (Input.GetAxis("Player_Two_Vertical") > 0 && playerLook != "U" && !Player_Attack.isAttacking)
      {
        playerLook = "U";
        Animator.SetInteger("Look", 1);
      }
      else if (Input.GetAxis("Player_Two_Vertical") == 0 && playerLook != "" && !Player_Attack.isAttacking)
      {
        playerLook = "";
        Animator.SetInteger("Look", 0);
        // Debug.Log("Looking Neutral");
      }
      else if (Input.GetAxis("Player_Two_Vertical") < 0 && playerLook != "D" && !Player_Attack.isAttacking)
      {
        playerLook = "D";
        Animator.SetInteger("Look", -1);
      }

      // HORIZONTAL MOVEMENT
      movement = new Vector3(Input.GetAxis("Player_Two_Horizontal"), 0f, 0f);

    }

    else if (gameObject.name == "Player_Three")
    {

      // JUMP
      if (Input.GetButtonDown("Player_Three_Jump") && jumps > 0 && canMove)
      {
        pressedJump = true;
      }
      if (Input.GetButtonUp("Player_Three_Jump"))
      {
        releasedJump = true;
      }

      // LOOK UP AND DOWN
      if (Input.GetAxis("Player_Three_Vertical") > 0 && playerLook != "U" && !Player_Attack.isAttacking)
      {
        playerLook = "U";
        Animator.SetInteger("Look", 1);
      }
      else if (Input.GetAxis("Player_Three_Vertical") == 0 && playerLook != "" && !Player_Attack.isAttacking)
      {
        playerLook = "";
        Animator.SetInteger("Look", 0);
        // Debug.Log("Looking Neutral");
      }
      else if (Input.GetAxis("Player_Three_Vertical") < 0 && playerLook != "D" && !Player_Attack.isAttacking)
      {
        playerLook = "D";
        Animator.SetInteger("Look", -1);
      }

      // HORIZONTAL MOVEMENT
      movement = new Vector3(Input.GetAxis("Player_Three_Horizontal"), 0f, 0f);

    }

    else if (gameObject.name == "Player_Four")
    {

      // JUMP
      if (Input.GetButtonDown("Player_Four_Jump") && jumps > 0 && canMove)
      {
        pressedJump = true;
      }
      if (Input.GetButtonUp("Player_Four_Jump"))
      {
        releasedJump = true;
      }

      // LOOK UP AND DOWN
      if (Input.GetAxis("Player_Four_Vertical") > 0 && playerLook != "U" && !Player_Attack.isAttacking)
      {
        playerLook = "U";
        Animator.SetInteger("Look", 1);
      }
      else if (Input.GetAxis("Player_Four_Vertical") == 0 && playerLook != "" && !Player_Attack.isAttacking)
      {
        playerLook = "";
        Animator.SetInteger("Look", 0);
        // Debug.Log("Looking Neutral");
      }
      else if (Input.GetAxis("Player_Four_Vertical") < 0 && playerLook != "D" && !Player_Attack.isAttacking)
      {
        playerLook = "D";
        Animator.SetInteger("Look", -1);
      }

      // HORIZONTAL MOVEMENT
      movement = new Vector3(Input.GetAxis("Player_Four_Horizontal"), 0f, 0f);

    }

    if (movement.x == 0)
    {
      Animator.SetBool("isRunning", false);
    }
    else if ((movement.x > 0 || movement.x < 0) && Rigidbody2D.velocity.y == 0)
    {
      Animator.SetBool("isRunning", true);
    }

    if (Rigidbody2D.velocity.y < 0)
    {
      isFalling = true;
      Animator.SetBool("isFalling", isFalling);
      isAscending = false;
      Animator.SetBool("isAscending", isAscending);
    }
    else if (Rigidbody2D.velocity.y == 0)
    {
      isFalling = false;
      Animator.SetBool("isFalling", isFalling);
      isAscending = false;
      Animator.SetBool("isAscending", isAscending);
    }
    else if (Rigidbody2D.velocity.y > 0)
    {
      isFalling = false;
      Animator.SetBool("isFalling", isFalling);
      isAscending = true;
      Animator.SetBool("isAscending", isAscending);
    }

    if (movement.x > 0 && playerDirection != "R" && canMove)
    {
      playerDirection = "R";
      SpriteRenderer.flipX = false;
    }
    else if (movement.x < 0 && playerDirection != "L" && canMove)
    {
      playerDirection = "L";
      SpriteRenderer.flipX = true;
    }
    transform.position += movement * Time.deltaTime * moveSpeed;
  }

  private void StartJump()
  {
    // Resetting Y velocity to 0 to remove downward force
    jumps -= 1;
    Rigidbody2D.velocity = new Vector3(Rigidbody2D.velocity.x, 0f, 0f);
    Animator.SetBool("isJumping", true);
    Rigidbody2D.gravityScale = 0f;
    Rigidbody2D.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    pressedJump = false;
    startJumpTimer = true;
    Animator.SetInteger("Jumps", jumps);
  }

  private void StopJump()
  {
    Rigidbody2D.gravityScale = gravityScale;
    releasedJump = false;
    jumpTimer = jumpTimerMax;
    startJumpTimer = false;
    Animator.SetBool("isJumping", false);
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.transform.tag == "Floor")
    {
      // Debug.Log("Collided with FLOOR");
      Rigidbody2D.velocity = new Vector3(Rigidbody2D.velocity.x, 0f, 0f);
      jumps = 2;
      Animator.SetInteger("Jumps", jumps);
      Player_Attack.maxLights = 3;

      // Debug.Log("Collided with FLOOR");
      isGrounded = true;
      Animator.SetBool("isGrounded", true);
    }
  }

  private void OnCollisionExit2D(Collision2D other)
  {
    if (other.transform.tag == "Floor")
    {
      // Debug.Log("No longer touching FLOOR");
      isGrounded = false;
      Animator.SetBool("isGrounded", false);
    }
  }
}
