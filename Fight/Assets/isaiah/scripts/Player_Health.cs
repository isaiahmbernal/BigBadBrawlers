using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour
{
  public Animator Animator;
  public Player_Movement Player_Movement;
  public float damage;
  public bool wasHit;
  public bool isHurt;
  public bool startHurtTimer;
  public float hurtTimerMax;
  public float hurtTimer;

  void Awake()
  {
    Animator = gameObject.GetComponent<Animator>();
    Player_Movement = gameObject.GetComponent<Player_Movement>();
    damage = 0f;
    wasHit = false;
    isHurt = false;
    startHurtTimer = false;
    hurtTimerMax = .25f;
    hurtTimer = hurtTimerMax;
  }

  void FixedUpdate()
  {
    if (wasHit)
    {
      StartHurt();
    }

    if (!wasHit && !isHurt && startHurtTimer && hurtTimer <= 0)
    {
      EndHurt();
    }

    if (startHurtTimer)
    {
      hurtTimer -= Time.deltaTime;
      if (hurtTimer <= 0)
      {
        isHurt = false;
      }
    }
    
  }

  void StartHurt()
  {
    wasHit = false;
    isHurt = true;
    Player_Movement.canMove = false;
    Animator.SetBool("isHurt", true);
    startHurtTimer = true;
    Debug.Log(gameObject.name + ": I was hit!");
  }

  void EndHurt()
  {
    startHurtTimer = false;
    hurtTimer = hurtTimerMax;
    Animator.SetBool("isHurt", false);
    Player_Movement.canMove = true;
  }
}