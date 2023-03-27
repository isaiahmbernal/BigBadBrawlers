using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Dodge : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D rb2d;
    public SpriteRenderer sprite;
    public SpriteRenderer shieldSprite;
    public Collider2D playerCollider;

    public int dodgeDirection;
    public bool pressedDodge;
    public float maxDodgeTimer;
    public float currDodgeTimer;
    public float dodgeForce;

    float myGravity;

    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        shieldSprite = transform.Find("Shield").GetComponent<SpriteRenderer>();
        playerCollider = transform.Find("Player-Collider").GetComponent<Collider2D>();

        dodgeDirection = 0;
        pressedDodge = false;
        // maxDodgeTimer = .5f;
        currDodgeTimer = maxDodgeTimer;
        // dodgeForce = 5f;

        myGravity = rb2d.gravityScale;
    }

    void Update()
    {
        if (gameObject.name == "Player_One")
        {
            if (
                Input.GetAxis("Player_One_Horizontal") != 0
                && anim.GetBool("isBlocking")
                && !anim.GetBool("isDodging")
                && anim.GetBool("isGrounded")
                && !anim.GetBool("isAttacking")
            )
            {
                if (Input.GetAxis("Player_One_Horizontal") > 0f)
                {
                    dodgeDirection = 1;
                }
                else
                {
                    dodgeDirection = -1;
                }
                pressedDodge = true;
            }
            
            else if (
                // Input.GetButtonDown("Player_One_Horizontal")
                Input.GetButtonDown("Player_One_Block")
                && !anim.GetBool("isBlocking")
                && !anim.GetBool("isDodging")
                && !anim.GetBool("isGrounded")
                && (anim.GetInteger("Jumps") != -1)
            )
            {
                anim.SetInteger("Jumps", -1);
                dodgeDirection = 0;
                pressedDodge = true;
            }
        }
        else if (gameObject.name == "Player_Two")
        {
            if (
                Input.GetAxis("Player_Two_Horizontal") != 0
                && anim.GetBool("isBlocking")
                && !anim.GetBool("isDodging")
                && anim.GetBool("isGrounded")
                && !anim.GetBool("isAttacking")
            )
            {
                if (Input.GetAxis("Player_Two_Horizontal") > 0f)
                {
                    dodgeDirection = 1;
                }
                else
                {
                    dodgeDirection = -1;
                }
                pressedDodge = true;
            }
            
            else if (
                // Input.GetButtonDown("Player_Two_Horizontal")
                Input.GetButtonDown("Player_Two_Block")
                && !anim.GetBool("isBlocking")
                && !anim.GetBool("isDodging")
                && !anim.GetBool("isGrounded")
                && (anim.GetInteger("Jumps") != -1)
            )
            {
                anim.SetInteger("Jumps", -1);
                dodgeDirection = 0;
                pressedDodge = true;
            }
        }
        else if (gameObject.name == "Player_Three")
        {
            if (
                Input.GetAxis("Player_Three_Horizontal") != 0
                && anim.GetBool("isBlocking")
                && !anim.GetBool("isDodging")
                && anim.GetBool("isGrounded")
                && !anim.GetBool("isAttacking")
            )
            {
                if (Input.GetAxis("Player_Three_Horizontal") > 0f)
                {
                    dodgeDirection = 1;
                }
                else
                {
                    dodgeDirection = -1;
                }
                pressedDodge = true;
            }
            
            else if (
                // Input.GetButtonDown("Player_Three_Horizontal")
                Input.GetButtonDown("Player_Three_Block")
                && !anim.GetBool("isBlocking")
                && !anim.GetBool("isDodging")
                && !anim.GetBool("isGrounded")
                && (anim.GetInteger("Jumps") != -1)
            )
            {
                anim.SetInteger("Jumps", -1);
                dodgeDirection = 0;
                pressedDodge = true;
            }
        }
        else if (gameObject.name == "Player_Four")
        {
            if (
                Input.GetAxis("Player_Four_Horizontal") != 0
                && anim.GetBool("isBlocking")
                && !anim.GetBool("isDodging")
                && anim.GetBool("isGrounded")
                && !anim.GetBool("isAttacking")
            )
            {
                if (Input.GetAxis("Player_Four_Horizontal") > 0f)
                {
                    dodgeDirection = 1;
                }
                else
                {
                    dodgeDirection = -1;
                }
                pressedDodge = true;
            }
            
            else if (
                // Input.GetButtonDown("Player_Four_Horizontal")
                Input.GetButtonDown("Player_Four_Block")
                && !anim.GetBool("isBlocking")
                && !anim.GetBool("isDodging")
                && !anim.GetBool("isGrounded")
                && (anim.GetInteger("Jumps") != -1)
            )
            {
                anim.SetInteger("Jumps", -1);
                dodgeDirection = 0;
                pressedDodge = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (pressedDodge)
        {
            Debug.Log("Pressed Dodge");
            pressedDodge = false;
            StartDodge();
        }

        if (anim.GetBool("isDodging"))
        {
            currDodgeTimer -= Time.deltaTime;

            if (currDodgeTimer < 0f)
            {
                EndDodge();
            }
        }
    }

    void StartDodge()
    {
        Debug.Log("Start Dodge");
        anim.SetBool("isDodging", true);
        anim.SetBool("isBlocking", false);
        anim.SetBool("canMove", false);
        sprite.color = new Color(255f, 255f, 255f, .3f);
        shieldSprite.enabled = false;
        playerCollider.enabled = false;

        rb2d.gravityScale = myGravity / 2;
        rb2d.velocity = new Vector2(0f, 0f);
        if (dodgeDirection > 0)
        {
            rb2d.AddForce(new Vector2(dodgeForce, 0f), ForceMode2D.Impulse);
            // Debug.Log("Dodge Right");
        }
        else if (dodgeDirection < 0)
        {
            rb2d.AddForce(new Vector2(-dodgeForce, 0f), ForceMode2D.Impulse);
            // Debug.Log("Dodge Left");
        }
    }

    void EndDodge()
    {
        Debug.Log("End Dodge");
        anim.SetBool("isDodging", false);
        if (gameObject.name == "Player_One")
        {
            if (Input.GetButton("Player_One_Block") && anim.GetBool("isGrounded"))
            {
                anim.SetBool("isBlocking", true);
                anim.SetBool("canMove", false);
                shieldSprite.enabled = true;
            }
            else
            {
                anim.SetBool("isBlocking", false);
                anim.SetBool("canMove", true);
                shieldSprite.enabled = false;
            }
        }
        else if (gameObject.name == "Player_Two")
        {
            if (Input.GetButton("Player_Two_Block") && anim.GetBool("isGrounded"))
            {
                anim.SetBool("isBlocking", true);
                anim.SetBool("canMove", false);
                shieldSprite.enabled = true;
            }
            else
            {
                anim.SetBool("isBlocking", false);
                anim.SetBool("canMove", true);
                shieldSprite.enabled = false;
            }
        }
        else if (gameObject.name == "Player_Three")
        {
            if (Input.GetButton("Player_Three_Block") && anim.GetBool("isGrounded"))
            {
                anim.SetBool("isBlocking", true);
                anim.SetBool("canMove", false);
                shieldSprite.enabled = true;
            }
            else
            {
                anim.SetBool("isBlocking", false);
                anim.SetBool("canMove", true);
                shieldSprite.enabled = false;
            }
        }
        else if (gameObject.name == "Player_Four")
        {
            if (Input.GetButton("Player_Four_Block") && anim.GetBool("isGrounded"))
            {
                anim.SetBool("isBlocking", true);
                anim.SetBool("canMove", false);
                shieldSprite.enabled = true;
            }
            else
            {
                anim.SetBool("isBlocking", false);
                anim.SetBool("canMove", true);
                shieldSprite.enabled = false;
            }
        }

        sprite.color = new Color(255f, 255f, 255f, 1f);
        currDodgeTimer = maxDodgeTimer;
        rb2d.velocity = new Vector2(0f, 0f);
        rb2d.gravityScale = myGravity;
        playerCollider.enabled = true;
    }
}
