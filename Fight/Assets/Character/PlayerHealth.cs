using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
  public Animator anim;
  public float health;
  public bool wasHit;
  public bool isHurt;
  public bool startHurtTimer;
  public float hurtTimerMax;
  public float hurtTimer;

  void Awake()
  {
    anim = gameObject.GetComponent<Animator>();
    health = 100.0f;
    wasHit = false;
    isHurt = false;
    startHurtTimer = false;
    hurtTimerMax = .5f;
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
    anim.SetBool("isHurt", true);
    startHurtTimer = true;
    Debug.Log(gameObject.name + ": I was hit!");
  }

  void EndHurt()
  {
    startHurtTimer = false;
    hurtTimer = hurtTimerMax;
    anim.SetBool("isHurt", false);
  }
}
