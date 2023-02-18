using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proto_Health : MonoBehaviour
{
  public Animator anim;
  public Proto_Movement playerMove;
  public float damage;
  public bool wasHit;
  public bool isHurt;
  public bool startHurtTimer;
  public float hurtTimerMax;
  public float hurtTimer;

  void Awake()
  {
    anim = gameObject.GetComponent<Animator>();
    playerMove = gameObject.GetComponent<Proto_Movement>();
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
    playerMove.canMove = false;
    anim.SetBool("isHurt", true);
    startHurtTimer = true;
    Debug.Log(gameObject.name + ": I was hit!");
  }

  void EndHurt()
  {
    startHurtTimer = false;
    hurtTimer = hurtTimerMax;
    anim.SetBool("isHurt", false);
    playerMove.canMove = true;
  }
}
