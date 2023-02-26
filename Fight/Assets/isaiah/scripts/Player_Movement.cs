using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
  public Rigidbody2D rb2d;
  private Animator anim;
  private SpriteRenderer sprite;
  // public Player_Attack Player_Attack;

  // public bool canMove;
  public float moveSpeed;

  // public bool isGrounded;
  // public bool isFalling;
  // public bool isAscending;
  public float gravityScale;
  // public float bounceForce;
  // public int jumps;
  public float jumpForce;
  private float jumpTimer;
  public float jumpTimerMax;
  private bool pressedJump;
  private bool startJumpTimer;
  private bool releasedJump;

  // public string playerDirection;
  // public string playerLook;

  // public string secretMoves;
  // public string secretCode;

  // private void Start()
  // {
  //   secretMoves = "";
  //   secretCode = "UUDDLRLR";
  // }

  private void Awake()
  {
    rb2d = gameObject.GetComponent<Rigidbody2D>();
    anim = gameObject.GetComponent<Animator>();
    sprite = gameObject.GetComponent<SpriteRenderer>();
    // Player_Attack = gameObject.GetComponent<Player_Attack>();

    // canMove = true;
    moveSpeed = 3f;

    // isGrounded = true;
    // isFalling = false;
    // isAscending = false;
    gravityScale = 1.5f;
    // bounceForce = 3f;
    // jumps = 2;
    jumpForce = 4f;
    jumpTimerMax = .01f;
    jumpTimer = jumpTimerMax;
    pressedJump = false;
    startJumpTimer = false;
    releasedJump = false;

    // playerDirection = "R";
    // playerLook = "";

    rb2d.gravityScale = gravityScale;
    rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    anim.SetBool("isRunning", false);
    // anim.SetInteger("Jumps", jumps);
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
      // if (Input.GetButtonDown("Player_One_Jump") && jumps > 0 && canMove)
      if (Input.GetButtonDown("Player_One_Jump") && anim.GetInteger("Jumps") > 0 && anim.GetBool("canMove"))
      {
        pressedJump = true;
      }
      if (Input.GetButtonUp("Player_One_Jump"))
      {
        releasedJump = true;
      }

      // LOOK UP AND DOWN
      // if (Input.GetAxis("Player_One_Vertical") > 0 && playerLook != "U" && !anim.GetBool("isAttacking"))
      if (Input.GetAxis("Player_One_Vertical") > 0 && anim.GetInteger("Look") <= 0 && !anim.GetBool("isAttacking"))
      {
        // playerLook = "U";
        anim.SetInteger("Look", 1);
      }
      // else if (Input.GetAxis("Player_One_Vertical") == 0 && playerLook != "" && !anim.GetBool("isAttacking"))
      else if (Input.GetAxis("Player_One_Vertical") == 0 && anim.GetInteger("Look") != 0 && !anim.GetBool("isAttacking"))
      {
        // playerLook = "";
        anim.SetInteger("Look", 0);
        // Debug.Log("Looking Neutral");
      }
      // else if (Input.GetAxis("Player_One_Vertical") < 0 && playerLook != "D" && !anim.GetBool("isAttacking"))
      else if (Input.GetAxis("Player_One_Vertical") < 0 && anim.GetInteger("Look") >= 0 && !anim.GetBool("isAttacking"))
      {
        // playerLook = "D";
        anim.SetInteger("Look", -1);
      }

      // HORIZONTAL MOVEMENT
      // if (canMove)
      if (anim.GetBool("canMove"))
      {
        movement = new Vector3(Input.GetAxis("Player_One_Horizontal"), 0f, 0f);
      }

    }

    else if (gameObject.name == "Player_Two")
    {

      // JUMP
      // if (Input.GetButtonDown("Player_Two_Jump") && jumps > 0 && canMove)
      if (Input.GetButtonDown("Player_Two_Jump") && anim.GetInteger("Jumps") > 0 && anim.GetBool("canMove"))
      {
        pressedJump = true;
      }
      if (Input.GetButtonUp("Player_Two_Jump"))
      {
        releasedJump = true;
      }

      // LOOK UP AND DOWN
      // if (Input.GetAxis("Player_Two_Vertical") > 0 && playerLook != "U" && !anim.GetBool("isAttacking"))
      if (Input.GetAxis("Player_Two_Vertical") > 0 && anim.GetInteger("Look") <= 0 && !anim.GetBool("isAttacking"))
      {
        // playerLook = "U";
        anim.SetInteger("Look", 1);
      }
      // else if (Input.GetAxis("Player_Two_Vertical") == 0 && playerLook != "" && !anim.GetBool("isAttacking"))
      else if (Input.GetAxis("Player_Two_Vertical") == 0 && anim.GetInteger("Look") != 0 && !anim.GetBool("isAttacking"))
      {
        // playerLook = "";
        anim.SetInteger("Look", 0);
        // Debug.Log("Looking Neutral");
      }
      // else if (Input.GetAxis("Player_Two_Vertical") < 0 && playerLook != "D" && !anim.GetBool("isAttacking"))
      else if (Input.GetAxis("Player_Two_Vertical") < 0 && anim.GetInteger("Look") >= 0 && !anim.GetBool("isAttacking"))
      {
        // playerLook = "D";
        anim.SetInteger("Look", -1);
      }

      // HORIZONTAL MOVEMENT
      // if (canMove)
      if (anim.GetBool("canMove"))
      {
        movement = new Vector3(Input.GetAxis("Player_Two_Horizontal"), 0f, 0f);
      }

    }

    else if (gameObject.name == "Player_Three")
    {

      // JUMP
      // if (Input.GetButtonDown("Player_Three_Jump") && jumps > 0 && canMove)
      if (Input.GetButtonDown("Player_Three_Jump") && anim.GetInteger("Jumps") > 0 && anim.GetBool("canMove"))
      {
        pressedJump = true;
      }
      if (Input.GetButtonUp("Player_Three_Jump"))
      {
        releasedJump = true;
      }

      // LOOK UP AND DOWN
      // if (Input.GetAxis("Player_Three_Vertical") > 0 && playerLook != "U" && !anim.GetBool("isAttacking"))
      if (Input.GetAxis("Player_Three_Vertical") > 0 && anim.GetInteger("Look") <= 0 && !anim.GetBool("isAttacking"))
      {
        // playerLook = "U";
        anim.SetInteger("Look", 1);
      }
      // else if (Input.GetAxis("Player_Three_Vertical") == 0 && playerLook != "" && !anim.GetBool("isAttacking"))
      else if (Input.GetAxis("Player_Three_Vertical") == 0 && anim.GetInteger("Look") != 0 && !anim.GetBool("isAttacking"))
      {
        // playerLook = "";
        anim.SetInteger("Look", 0);
        // Debug.Log("Looking Neutral");
      }
      // else if (Input.GetAxis("Player_Three_Vertical") < 0 && playerLook != "D" && !anim.GetBool("isAttacking"))
      else if (Input.GetAxis("Player_Three_Vertical") < 0 && anim.GetInteger("Look") >= 0 && !anim.GetBool("isAttacking"))
      {
        // playerLook = "D";
        anim.SetInteger("Look", -1);
      }

      // HORIZONTAL MOVEMENT
      // if (canMove)
      if (anim.GetBool("canMove"))
      {
        movement = new Vector3(Input.GetAxis("Player_Three_Horizontal"), 0f, 0f);
      }

    }

    else if (gameObject.name == "Player_Four")
    {

      // JUMP
      // if (Input.GetButtonDown("Player_Four_Jump") && jumps > 0 && canMove)
      if (Input.GetButtonDown("Player_Four_Jump") && anim.GetInteger("Jumps") > 0 && anim.GetBool("canMove"))
      {
        pressedJump = true;
      }
      if (Input.GetButtonUp("Player_Four_Jump"))
      {
        releasedJump = true;
      }

      // LOOK UP AND DOWN
      // if (Input.GetAxis("Player_Four_Vertical") > 0 && playerLook != "U" && !anim.GetBool("isAttacking"))
      if (Input.GetAxis("Player_Four_Vertical") > 0 && anim.GetInteger("Look") <= 0 && !anim.GetBool("isAttacking"))
      {
        // playerLook = "U";
        anim.SetInteger("Look", 1);
      }
      // else if (Input.GetAxis("Player_Four_Vertical") == 0 && playerLook != "" && !anim.GetBool("isAttacking"))
      else if (Input.GetAxis("Player_Four_Vertical") == 0 && anim.GetInteger("Look") != 0 && !anim.GetBool("isAttacking"))
      {
        // playerLook = "";
        anim.SetInteger("Look", 0);
        // Debug.Log("Looking Neutral");
      }
      // else if (Input.GetAxis("Player_Four_Vertical") < 0 && playerLook != "D" && !anim.GetBool("isAttacking"))
      else if (Input.GetAxis("Player_Four_Vertical") < 0 && anim.GetInteger("Look") >= 0 && !anim.GetBool("isAttacking"))
      {
        // playerLook = "D";
        anim.SetInteger("Look", -1);
      }

      // HORIZONTAL MOVEMENT
      // if (canMove)
      if (anim.GetBool("canMove"))
      {
        movement = new Vector3(Input.GetAxis("Player_Four_Horizontal"), 0f, 0f);
      }

    }

    if (movement.x == 0)
    {
      anim.SetBool("isRunning", false);
    }
    else if ((movement.x > 0 || movement.x < 0) && rb2d.velocity.y == 0)
    {
      anim.SetBool("isRunning", true);
    }

    if (rb2d.velocity.y < 0)
    {
      // isFalling = true;
      anim.SetBool("isFalling", true);
      // isAscending = false;
      anim.SetBool("isAscending", false);
    }
    else if (rb2d.velocity.y == 0)
    {
      // isFalling = false;
      anim.SetBool("isFalling", false);
      // isAscending = false;
      anim.SetBool("isAscending", false);
    }
    else if (rb2d.velocity.y > 0)
    {
      // isFalling = false;
      anim.SetBool("isFalling", false);
      // isAscending = true;
      anim.SetBool("isAscending", true);
    }

    // if (movement.x > 0 && playerDirection != "R" && canMove)
    if (movement.x > 0 && anim.GetInteger("Direction") != 1 && anim.GetBool("canMove"))
    {
      // playerDirection = "R";
      anim.SetInteger("Direction", 1);
      sprite.flipX = false;
    }
    // else if (movement.x < 0 && playerDirection != "L" && canMove)
    else if (movement.x < 0 && anim.GetInteger("Direction") != -1 && anim.GetBool("canMove"))
    {
      // playerDirection = "L";
      anim.SetInteger("Direction", -1);
      sprite.flipX = true;
    }
    transform.position += movement * Time.deltaTime * moveSpeed;
  }

  private void StartJump()
  {
    // Resetting Y velocity to 0 to remove downward force

    // jumps -= 1;
    rb2d.velocity = new Vector3(rb2d.velocity.x, 0f, 0f);
    anim.SetBool("isJumping", true);
    anim.SetInteger("Jumps", anim.GetInteger("Jumps") - 1);
    rb2d.gravityScale = 0f;
    rb2d.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    pressedJump = false;
    startJumpTimer = true;
    // anim.SetInteger("Jumps", jumps);
  }

  private void StopJump()
  {
    rb2d.gravityScale = gravityScale;
    releasedJump = false;
    jumpTimer = jumpTimerMax;
    startJumpTimer = false;
    anim.SetBool("isJumping", false);
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.transform.tag == "Floor")
    {
      // Debug.Log("Collided with FLOOR");
      rb2d.velocity = new Vector3(rb2d.velocity.x, 0f, 0f);
      // jumps = 2;
      anim.SetInteger("Jumps", 2);
      // Player_Attack.maxLights = 3;
      anim.SetInteger("Lights", 3);

      // Debug.Log("Collided with FLOOR");
      // isGrounded = true;
      anim.SetBool("isGrounded", true);
    }
  }

  private void OnCollisionExit2D(Collision2D other)
  {
    if (other.transform.tag == "Floor")
    {
      // Debug.Log("No longer touching FLOOR");
      // isGrounded = false;
      anim.SetBool("isGrounded", false);
    }
  }
}
