using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
  private Animator Animator;
  public Player_Movement Player_Movement;
  public Player_Attack_Collider rAttack;
  public Player_Attack_Collider lAttack;
  public Player_Attack_Collider uAttack;
  public Player_Attack_Collider dAttack;

  public bool isAttacking;

  public int maxLights;
  private bool pressedLight;
  private bool releaseLight;
  private bool startLightTimer;
  private float lightTimerMax;
  private float lightTimer;

  private void Awake()
  {
    Animator = gameObject.GetComponent<Animator>();
    Player_Movement = gameObject.GetComponent<Player_Movement>();
    rAttack = transform.Find("R-Attack").GetComponent<Player_Attack_Collider>();
    lAttack = transform.Find("L-Attack").GetComponent<Player_Attack_Collider>();
    uAttack = transform.Find("U-Attack").GetComponent<Player_Attack_Collider>();
    dAttack = transform.Find("D-Attack").GetComponent<Player_Attack_Collider>();

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
    if (gameObject.name == "Player_One")
    {
      if (Input.GetButtonDown("Player_One_Light") && Player_Movement.isGrounded == true)
      {
        pressedLight = true;
      }
      if (
          Input.GetButtonDown("Player_One_Light")
          && Player_Movement.isGrounded == false
          && maxLights > 0
          && !isAttacking
          )
      {
        maxLights -= 1;
        Debug.Log(maxLights);
        pressedLight = true;
      }
    }
    else if (gameObject.name == "Player_Two")
    {
      if (Input.GetButtonDown("Player_Two_Light") && Player_Movement.isGrounded == true)
      {
        pressedLight = true;
      }
      if (
          Input.GetButtonDown("Player_Two_Light")
          && Player_Movement.isGrounded == false
          && maxLights > 0
          && !isAttacking
          )
      {
        maxLights -= 1;
        Debug.Log(maxLights);
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
  }

  void StartLight()
  {
    pressedLight = false;
    isAttacking = true;
    Player_Movement.canMove = false;
    Animator.SetBool("isAttacking", true);
    // Player_Movement.rb2d.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    if (Player_Movement.playerLook == "")
    {
      switch (Player_Movement.playerDirection)
      {
        case "R":
          rAttack.SpriteRenderer.color = new Color(255f, 0f, 0f, .3f);
          rAttack.HurtEnemy();
          break;
        case "L":
          lAttack.SpriteRenderer.color = new Color(255f, 0f, 0f, .3f);
          lAttack.HurtEnemy();
          break;
      }
    }
    else
    {
      switch (Player_Movement.playerLook)
      {
        case "U":
          uAttack.SpriteRenderer.color = new Color(255f, 0f, 0f, .3f);
          uAttack.HurtEnemy();
          break;
        case "D":
          dAttack.SpriteRenderer.color = new Color(255f, 0f, 0f, .3f);
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
    // Player_Movement.rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    // Player_Movement.rb2d.velocity = new Vector3(Player_Movement.rb2d.velocity.x, -1f, 0f);
    if (Player_Movement.playerLook == "")
    {
      switch (Player_Movement.playerDirection)
      {
        case "R":
          rAttack.SpriteRenderer.color = new Color(0f, 0, 0f, .1f);
          // rAttack.HurtEnemy();
          break;
        case "L":
          lAttack.SpriteRenderer.color = new Color(0f, 0, 0f, .1f);
          // lAttack.HurtEnemy();
          break;
      }
    }
    else
    {
      switch (Player_Movement.playerLook)
      {
        case "U":
          uAttack.SpriteRenderer.color = new Color(0f, 0, 0f, .1f);
          // uAttack.HurtEnemy();
          break;
        case "D":
          dAttack.SpriteRenderer.color = new Color(0f, 0, 0f, .1f);
          // dAttack.HurtEnemy();
          break;
      }
    }
    isAttacking = false;
    Animator.SetBool("isAttacking", false);
    Player_Movement.canMove = true;
  }
}
