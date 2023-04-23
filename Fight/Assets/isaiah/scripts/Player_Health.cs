using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour
{
  public Animator anim;
  public SpriteRenderer sprite;
  public bool isHurt;
  public bool startHurtTimer;
  public float hurtTimerMax;
  public float hurtTimer;

  void Awake()
  {
    anim = gameObject.GetComponent<Animator>();
    sprite = gameObject.GetComponent<SpriteRenderer>();
    isHurt = false;
    startHurtTimer = false;
    // hurtTimerMax = .25f;
    hurtTimer = hurtTimerMax;
  }

  void FixedUpdate()
  {
    // Start the hurt func if we were hurt
    if (anim.GetBool("wasHit"))
    {
      StartHurt();
    }

    // End the flinch if the recovery time is over
    if (!anim.GetBool("wasHit") && !isHurt && startHurtTimer && hurtTimer <= 0)
    {
      EndHurt();
    }

    // Times the recovery to end the flinch
    if (startHurtTimer)
    {
      hurtTimer -= Time.deltaTime;
      if (hurtTimer <= 0)
      {
        isHurt = false;
      }
    }
    
  }

  // Immobilizes us if we were hit
  void StartHurt()
  {
    anim.SetBool("wasHit", false);
    isHurt = true;
    anim.SetBool("canMove", false);
    anim.SetBool("canTurn", false);
    anim.SetBool("isHurt", true);
    startHurtTimer = true;
    Debug.Log(gameObject.name + ": I was hit!");
    sprite.color = Color.red;
  }

  // Allows us to move again after recovering
  void EndHurt()
  {
    startHurtTimer = false;
    hurtTimer = hurtTimerMax;
    anim.SetBool("isHurt", false);
    anim.SetBool("canMove", true);
    anim.SetBool("canTurn", true);
    sprite.color = new Color(255, 255, 255);
  }
}
