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
    [SerializeField] private float rechargeTime;
    [SerializeField] private bool canDodge;

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
        rechargeTime = .75f;
        canDodge = true;

        myGravity = rb2d.gravityScale;
    }

    // Check for player input
    void Update()
    {
        if (gameObject.name == "Player_One")
        {
            if (
                (Input.GetAxis("Player_One_Horizontal_Controller") != 0
                || Input.GetAxis("Player_One_Horizontal_Keyboard") != 0)
                && !anim.GetBool("isHurt")
                && !anim.GetBool("isAttacking")
                && !anim.GetBool("isCharging")
                && !anim.GetBool("fireSpecial")
                && anim.GetBool("isBlocking")
                && !anim.GetBool("isDodging")
                && anim.GetBool("isGrounded")
            )
            {
                if (Input.GetAxis("Player_One_Horizontal_Controller") > 0f || Input.GetAxis("Player_One_Horizontal_Keyboard") > 0f)
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
                // Input.GetButtonDown("Player_One_Horizontal_Controller")
                Input.GetButtonDown("Player_One_Block")
                && !anim.GetBool("isHurt")
                && !anim.GetBool("isAttacking")
                && !anim.GetBool("isCharging")
                && !anim.GetBool("fireSpecial")
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
                (Input.GetAxis("Player_Two_Horizontal_Controller") != 0
                || Input.GetAxis("Player_Two_Horizontal_Keyboard") != 0)
                && !anim.GetBool("isHurt")
                && !anim.GetBool("isAttacking")
                && !anim.GetBool("isCharging")
                && !anim.GetBool("fireSpecial")
                && anim.GetBool("isBlocking")
                && !anim.GetBool("isDodging")
                && anim.GetBool("isGrounded")
            )
            {
                if (Input.GetAxis("Player_Two_Horizontal_Controller") > 0f || Input.GetAxis("Player_Two_Horizontal_Keyboard") > 0f)
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
                // Input.GetButtonDown("Player_Two_Horizontal_Controller")
                Input.GetButtonDown("Player_Two_Block")
                && !anim.GetBool("isHurt")
                && !anim.GetBool("isAttacking")
                && !anim.GetBool("isCharging")
                && !anim.GetBool("fireSpecial")
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
        // Start dodging if we can
        if (pressedDodge && canDodge)
        {
            Debug.Log("Pressed Dodge");
            pressedDodge = false;
            StartDodge();
        }

        // If we're dodging, time it so
        // we know when to stop it
        if (anim.GetBool("isDodging"))
        {
            currDodgeTimer -= Time.deltaTime;

            if (currDodgeTimer < 0f)
            {
                StartCoroutine(EndDodge());
            }
        }
    }

    // Start the dodge, prevents us from
    // being attacked and pushes us in the
    // direction we move
    void StartDodge()
    {
        canDodge = false;
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

    // IEnumerator to end the dodge and wait
    // for seconds to give a recharge time for
    // being able to dodge again
    private IEnumerator EndDodge()
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

        sprite.color = new Color(255f, 255f, 255f, 1f);
        currDodgeTimer = maxDodgeTimer;
        rb2d.velocity = new Vector2(0f, 0f);
        rb2d.gravityScale = myGravity;
        playerCollider.enabled = true;

        yield return new WaitForSeconds(rechargeTime);
        canDodge = true;
    }
}
