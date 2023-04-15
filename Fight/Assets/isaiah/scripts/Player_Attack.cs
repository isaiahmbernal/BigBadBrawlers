using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    private Animator anim;
    public Rigidbody2D rb;
    public Player_Attack_Collider rAttack;
    public Player_Attack_Collider lAttack;
    public Player_Attack_Collider uAttack;

    // public Player_Attack_Collider dAttack;

    private bool pressedLight;
    private bool releaseLight;
    private bool startLightTimer;
    public float lightTimerMax;
    public float lightTimer;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        rAttack = transform.Find("R-Attack").GetComponent<Player_Attack_Collider>();
        lAttack = transform.Find("L-Attack").GetComponent<Player_Attack_Collider>();
        uAttack = transform.Find("U-Attack").GetComponent<Player_Attack_Collider>();
        // dAttack = transform.Find("D-Attack").GetComponent<Player_Attack_Collider>();

        pressedLight = false;
        releaseLight = false;
        startLightTimer = false;
        // lightTimerMax = .3f;
        lightTimer = lightTimerMax;
    }

    void Update()
    {
        if (gameObject.name == "Player_One")
        {
            if (
                Input.GetButtonDown("Player_One_Light")
                && anim.GetBool("isGrounded")
                && !anim.GetBool("isHurt")
                && !anim.GetBool("isAttacking")
                && !anim.GetBool("isBlocking")
                && !anim.GetBool("isDodging")
            )
            {
                pressedLight = true;
            }
            if (
                Input.GetButtonDown("Player_One_Light")
                && !anim.GetBool("isGrounded")
                && anim.GetInteger("Lights") > 0
                && !anim.GetBool("isAttacking")
                && !anim.GetBool("isHurt")
                && !anim.GetBool("isBlocking")
                && !anim.GetBool("isDodging")
            )
            {
                anim.SetInteger("Lights", anim.GetInteger("Lights") - 1);
                pressedLight = true;
            }
        }
        else if (gameObject.name == "Player_Two")
        {
            if (
                Input.GetButtonDown("Player_Two_Light")
                && anim.GetBool("isGrounded")
                && !anim.GetBool("isHurt")
                && !anim.GetBool("isAttacking")
                && !anim.GetBool("isBlocking")
                && !anim.GetBool("isDodging")
            )
            {
                pressedLight = true;
            }
            if (
                Input.GetButtonDown("Player_Two_Light")
                && !anim.GetBool("isGrounded")
                && anim.GetInteger("Lights") > 0
                && !anim.GetBool("isAttacking")
                && !anim.GetBool("isHurt")
                && !anim.GetBool("isBlocking")
                && !anim.GetBool("isDodging")
            )
            {
                anim.SetInteger("Lights", anim.GetInteger("Lights") - 1);
                pressedLight = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (pressedLight)
        {
            StartLight();
        }

        if (releaseLight)
        {
            StopLight();
        }

        if (startLightTimer)
        {
            lightTimer -= Time.deltaTime;
            if (lightTimer <= 0)
            {
                releaseLight = true;
            }
        }

        if (anim.GetBool("isHurt")) {
            cancelLight();
        }
    }

    void StartLight()
    {
        pressedLight = false;

        if (anim.GetBool("isGrounded"))
        {
            anim.SetBool("canMove", false);
        }
        anim.SetBool("canTurn", false);
        anim.SetBool("isAttacking", true);
        // Player_Movement.rb2d.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        if (anim.GetInteger("Look") <= 0)
        {
            if (anim.GetInteger("Direction") > 0)
            {
                rAttack.sprite.color = new Color(255f, 0f, 0f, .3f);
                rAttack.StartHurt();
            }
            else if (anim.GetInteger("Direction") < 0)
            {
                lAttack.sprite.color = new Color(255f, 0f, 0f, .3f);
                lAttack.StartHurt();
            }
        }
        else
        {
            if (anim.GetInteger("Look") == 1)
            {
                uAttack.sprite.color = new Color(255f, 0f, 0f, .3f);
                uAttack.StartHurt();
            }
            // else if (anim.GetInteger("Look") == -1)
            // {
            //   dAttack.sprite.color = new Color(255f, 0f, 0f, .3f);
            //   dAttack.StartHurt();
            // }
        }
        startLightTimer = true;
    }

    void cancelLight() {
        releaseLight = false;
        startLightTimer = false;
        lightTimer = lightTimerMax;
        anim.SetBool("isAttacking", false);
    }

    void StopLight()
    {
        releaseLight = false;
        startLightTimer = false;
        lightTimer = lightTimerMax;

        if (anim.GetInteger("Look") == 0)
        {
            if (anim.GetInteger("Direction") > 0)
            {
                rAttack.sprite.color = new Color(0f, 0, 0f, .1f);
            }
            else if (anim.GetInteger("Direction") < 0)
            {
                lAttack.sprite.color = new Color(0f, 0, 0f, .1f);
            }
        }
        else
        {
            if (anim.GetInteger("Look") == 1)
            {
                uAttack.sprite.color = new Color(0f, 0, 0f, .1f);
            }
            // else if (anim.GetInteger("Look") == -1)
            // {
            //   dAttack.sprite.color = new Color(0f, 0, 0f, .1f);
            // }
        }

        anim.SetBool("isAttacking", false);
        anim.SetBool("canMove", true);
        anim.SetBool("canTurn", true);
    }
}
