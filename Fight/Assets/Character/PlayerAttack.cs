using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    public PlayerMove playerMove;
    public PlayerAttackCollider rAttack;
    public PlayerAttackCollider lAttack;
    public PlayerAttackCollider uAttack;
    public PlayerAttackCollider dAttack;
    
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
        playerMove = gameObject.GetComponent<PlayerMove>();
        rAttack = transform.Find("R-Attack").GetComponent<PlayerAttackCollider>();
        lAttack = transform.Find("L-Attack").GetComponent<PlayerAttackCollider>();
        uAttack = transform.Find("U-Attack").GetComponent<PlayerAttackCollider>();
        dAttack = transform.Find("D-Attack").GetComponent<PlayerAttackCollider>();

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
        if (Input.GetKeyDown(KeyCode.E) && playerMove.isGrounded == true)
        {
            pressedLight = true;
        }
        if (
            Input.GetKeyDown(KeyCode.E)
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
                    rAttack.attackSprite.color = new Color(0f, 0f, 0f, .1f);
                    // rAttack.HurtEnemy();
                    break;
                case "L":
                    lAttack.attackSprite.color = new Color(0f, 0f, 0f, .1f);
                    // lAttack.HurtEnemy();
                    break;
            }
        }
        else
        {
            switch (playerMove.playerLook)
            {
                case "U":
                    uAttack.attackSprite.color = new Color(0f, 0f, 0f, .1f);
                    // uAttack.HurtEnemy();
                    break;
                case "D":
                    dAttack.attackSprite.color = new Color(0f, 0f, 0f, .1f);
                    // dAttack.HurtEnemy();
                    break;
            }
        }
        isAttacking = false;
        anim.SetBool("isAttacking", false);
        playerMove.canMove = true;
    }
}
