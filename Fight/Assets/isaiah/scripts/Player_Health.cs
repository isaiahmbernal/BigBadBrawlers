using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour
{
  public Animator anim;
  // public Player_Movement Player_Movement;
  // public float damage;
  // public bool wasHit;
  public bool isHurt;
  public bool startHurtTimer;
  public float hurtTimerMax;
  public float hurtTimer;

  void Awake()
  {
    anim = gameObject.GetComponent<Animator>();
    // Player_Movement = gameObject.GetComponent<Player_Movement>();
    // damage = 0f;
    // wasHit = false;
    isHurt = false;
    startHurtTimer = false;
    hurtTimerMax = .25f;
    hurtTimer = hurtTimerMax;
  }

  void FixedUpdate()
  {
    // if (wasHit)
    if (anim.GetBool("wasHit"))
    {
      StartHurt();
    }

    // if (!wasHit && !isHurt && startHurtTimer && hurtTimer <= 0)
    if (!anim.GetBool("wasHit") && !isHurt && startHurtTimer && hurtTimer <= 0)
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
    // wasHit = false;
    anim.SetBool("wasHit", false);
    isHurt = true;
    anim.SetBool("canMove", false);
    // Player_Movement.canMove = false;
    anim.SetBool("isHurt", true);
    startHurtTimer = true;
    Debug.Log(gameObject.name + ": I was hit!");
  }

  void EndHurt()
  {
    startHurtTimer = false;
    hurtTimer = hurtTimerMax;
    anim.SetBool("isHurt", false);
    anim.SetBool("canMove", true);
    // Player_Movement.canMove = true;
  }
}
