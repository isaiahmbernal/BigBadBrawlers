using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy_Attack : MonoBehaviour
{
    private Animator anim;
    public Dummy_Movement playerMove;
    public Dummy_AttackCollider rAttack;
    public Dummy_AttackCollider lAttack;
    public Dummy_AttackCollider uAttack;
    public Dummy_AttackCollider dAttack;
    
    public bool isAttacking;

    public int maxLights;
    private bool pressedLight;
    private bool releaseLight;
    private bool startLightTimer;
    private float lightTimerMax;
    private float lightTimer;

    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        playerMove = gameObject.GetComponent<Dummy_Movement>();
        rAttack = transform.Find("R-Attack").GetComponent<Dummy_AttackCollider>();
        lAttack = transform.Find("L-Attack").GetComponent<Dummy_AttackCollider>();
        uAttack = transform.Find("U-Attack").GetComponent<Dummy_AttackCollider>();
        dAttack = transform.Find("D-Attack").GetComponent<Dummy_AttackCollider>();

        isAttacking = false;
        
        maxLights = 3;
        pressedLight = false;
        releaseLight = false;
        startLightTimer = false;
        lightTimerMax = .3f;
        lightTimer = lightTimerMax;
    }

    void Update()
    {
        if (Input.GetButtonDown("Player_Two_Light") && playerMove.isGrounded == true)
        {
            pressedLight = true;
        }
        if (
            Input.GetButtonDown("Player_Two_Light")
            && playerMove.isGrounded == false
            && maxLights > 0
            && !isAttacking
        )
        {
            maxLights -= 1;
            Debug.Log(maxLights);
            pressedLight = true;
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
    }

    void StartLight()
    {
        pressedLight = false;
        isAttacking = true;
        playerMove.canMove = false;
        anim.SetBool("isAttacking", true);
        // playerMove.rb2d.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        if (playerMove.playerLook == "")
        {
            switch (playerMove.playerDirection)
            {
                case "R":
                    rAttack.attackSprite.color = new Color(255f, 0f, 0f, .3f);
                    rAttack.HurtEnemy();
                    break;
                case "L":
                    lAttack.attackSprite.color = new Color(255f, 0f, 0f, .3f);
                    lAttack.HurtEnemy();
                    break;
            }
        }
        else
        {
            switch (playerMove.playerLook)
            {
                case "U":
                    uAttack.attackSprite.color = new Color(255f, 0f, 0f, .3f);
                    uAttack.HurtEnemy();
                    break;
                case "D":
                    dAttack.attackSprite.color = new Color(255f, 0f, 0f, .3f);
                    dAttack.HurtEnemy();
                    break;
            }
        }
        startLightTimer = true;
    }

    void StopLight()
    {
        releaseLight = false;
        startLightTimer = false;
        lightTimer = lightTimerMax;
        // playerMove.rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        // playerMove.rb2d.velocity = new Vector3(playerMove.rb2d.velocity.x, -1f, 0f);
        if (playerMove.playerLook == "")
        {
            switch (playerMove.playerDirection)
            {
                case "R":
                    rAttack.attackSprite.color = new Color(0f, 0, 0f, .1f);
                    // rAttack.HurtEnemy();
                    break;
                case "L":
                    lAttack.attackSprite.color = new Color(0f, 0, 0f, .1f);
                    // lAttack.HurtEnemy();
                    break;
            }
        }
        else
        {
            switch (playerMove.playerLook)
            {
                case "U":
                    uAttack.attackSprite.color = new Color(0f, 0, 0f, .1f);
                    // uAttack.HurtEnemy();
                    break;
                case "D":
                    dAttack.attackSprite.color = new Color(0f, 0, 0f, .1f);
                    // dAttack.HurtEnemy();
                    break;
            }
        }
        isAttacking = false;
        anim.SetBool("isAttacking", false);
        playerMove.canMove = true;
    }
}
