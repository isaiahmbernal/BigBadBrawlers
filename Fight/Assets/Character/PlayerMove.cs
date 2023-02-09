using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
  public Rigidbody2D rb2d;
  private Animator anim;
  private SpriteRenderer spriteRenderer;
  public PlayerAttack playerAttack;

  public bool canMove;
  public float moveSpeed;

  public bool isGrounded;
  public float gravityScale;
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
    rb2d = gameObject.GetComponent<Rigidbody2D>();
    anim = gameObject.GetComponent<Animator>();
    spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    playerAttack = gameObject.GetComponent<PlayerAttack>();

    canMove = true;
    moveSpeed = 15f;

    isGrounded = true;
    gravityScale = 5f;
    jumps = 2;
    jumpForce = 15f;
    jumpTimerMax = .01f;
    jumpTimer = jumpTimerMax;
    pressedJump = false;
    startJumpTimer = false;
    releasedJump = false;

    playerDirection = "R";
    playerLook = "";
    
    rb2d.gravityScale = gravityScale;
    rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    anim.SetBool("isRunning", false);
    anim.SetInteger("Jumps", jumps);
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
    // gameObject.transform.Rotate(0f, 0f, 0f);

    if (Input.GetButtonDown("Jump") && jumps > 0 && canMove)
    {
      pressedJump = true;
    }

    if (Input.GetButtonUp("Jump"))
    {
      releasedJump = true;
    }

    if (Input.GetAxis("Vertical") > 0 && playerLook != "U" && !playerAttack.isAttacking)
    {
      playerLook = "U";
      anim.SetInteger("Look", 1);
      // if (secretMoves.Length < 8)
      // {
      //   secretMoves = secretMoves + playerLook;
      //   if (secretMoves == secretCode)
      //   {
      //     rb2d.constraints = RigidbodyConstraints2D.None;
      //   }
      // }
      // else
      // {
      //   secretMoves = secretMoves.Remove(0, 1);
      //   secretMoves = secretMoves + playerLook;
      //   if (secretMoves == secretCode)
      //   {
      //     rb2d.constraints = RigidbodyConstraints2D.None;
      //   }
      // }
      // Debug.Log(secretMoves);
      // Debug.Log("Looking Up");
    }
    else if (Input.GetAxis("Vertical") == 0 && playerLook != "" && !playerAttack.isAttacking)
    {
      playerLook = "";
      anim.SetInteger("Look", 0);
      // Debug.Log("Looking Neutral");
    }
    else if (Input.GetAxis("Vertical") < 0 && playerLook != "D" && !playerAttack.isAttacking)
    {
      playerLook = "D";
      anim.SetInteger("Look", -1);
      // if (secretMoves.Length < 8)
      // {
      //   secretMoves = secretMoves + playerLook;
      //   if (secretMoves == secretCode)
      //   {
      //     rb2d.constraints = RigidbodyConstraints2D.None;
      //   }
      // }
      // else
      // {
      //   secretMoves = secretMoves.Remove(0, 1);
      //   secretMoves = secretMoves + playerLook;
      //   if (secretMoves == secretCode)
      //   {
      //     rb2d.constraints = RigidbodyConstraints2D.None;
      //   }
      // }
      // Debug.Log(secretMoves);
      // Debug.Log("Looking Down");
    }

    Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);

    if (movement.x == 0)
    {
      anim.SetBool("isRunning", false);
    }

    if ((movement.x > 0 || movement.x < 0) && rb2d.velocity.y == 0)
    {
      anim.SetBool("isRunning", true);
    }

    if (rb2d.velocity.y < 0)
    {
      anim.SetBool("isFalling", true);
      anim.SetBool("isAscending", false);
    }
    else if (rb2d.velocity.y == 0)
    {
      anim.SetBool("isFalling", false);
      anim.SetBool("isAscending", false);
    }
    else if (rb2d.velocity.y > 0)
    {
      anim.SetBool("isFalling", false);
      anim.SetBool("isAscending", true);
    }

    // Debug.Log(movement);
    if (movement.x > 0 && playerDirection != "R" && canMove)
    {
      playerDirection = "R";
      spriteRenderer.flipX = false;
      // if (secretMoves.Length < 8)
      // {
      //   secretMoves = secretMoves + playerDirection;
      //   if (secretMoves == secretCode)
      //   {
      //     rb2d.constraints = RigidbodyConstraints2D.None;
      //   }
      // }
      // else
      // {
      //   secretMoves = secretMoves.Remove(0, 1);
      //   secretMoves = secretMoves + playerDirection;
      //   if (secretMoves == secretCode)
      //   {
      //     rb2d.constraints = RigidbodyConstraints2D.None;
      //   }
      // }
      // Debug.Log(secretMoves);
      // Debug.Log(playerDirection);
    }
    else if (movement.x < 0 && playerDirection != "L" && canMove)
    {
      playerDirection = "L";
      spriteRenderer.flipX = true;
      // if (secretMoves.Length < 8)
      // {
      //   secretMoves = secretMoves + playerDirection;
      //   if (secretMoves == secretCode)
      //   {
      //     rb2d.constraints = RigidbodyConstraints2D.None;
      //   }
      // }
      // else
      // {
      //   secretMoves = secretMoves.Remove(0, 1);
      //   secretMoves = secretMoves + playerDirection;
      //   if (secretMoves == secretCode)
      //   {
      //     rb2d.constraints = RigidbodyConstraints2D.None;
      //   }
      // }
      // Debug.Log(secretMoves);
      // Debug.Log(playerDirection);
    }
    // if (canMove)
    // {
    //   transform.position += movement * Time.deltaTime * moveSpeed;
    // }
    transform.position += movement * Time.deltaTime * moveSpeed;
  }

  private void StartJump()
  {
    // Resetting Y velocity to 0 to remove downward force
    jumps -= 1;
    rb2d.velocity = new Vector3(rb2d.velocity.x, 0f, 0f);
    anim.SetBool("isJumping", true);
    rb2d.gravityScale = 0f;
    rb2d.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    pressedJump = false;
    startJumpTimer = true;
    anim.SetInteger("Jumps", jumps);
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
      jumps = 2;
      anim.SetInteger("Jumps", jumps);
      playerAttack.maxLights = 3;

      // Debug.Log("Collided with FLOOR");
      isGrounded = true;
      anim.SetBool("isGrounded", true);
    }
  }

  private void OnCollisionExit2D(Collision2D other)
  {
    if (other.transform.tag == "Floor")
    {
      // Debug.Log("No longer touching FLOOR");
      isGrounded = false;
      anim.SetBool("isGrounded", false);
    }
  }
}
