using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stickman_Attack_Special_Neutral : MonoBehaviour
{
  public Animator anim;
  public bool canFire;
  public float chargeTime;
  public float lifeTime;
  public float recoverTime;
  public bool pressedCharge;
  public Stickman_Attack_Special_Neutral_Collider lLaser;
  public Stickman_Attack_Special_Neutral_Collider rLaser;

  private void Awake()
  {
    anim = gameObject.GetComponent<Animator>();
    canFire = true;
    chargeTime = 1f;
    lifeTime = 1f;
    recoverTime = 3f;
    pressedCharge = false;
    lLaser = gameObject.transform.Find("L-Laser").GetComponent<Stickman_Attack_Special_Neutral_Collider>();
    rLaser = gameObject.transform.Find("R-Laser").GetComponent<Stickman_Attack_Special_Neutral_Collider>();
  }

  private void Update()
  {
    if (gameObject.name == "Player_One")
    {
      if (Input.GetButtonDown("Player_One_Heavy") 
        && !anim.GetBool("isHurt")
        && !anim.GetBool("isAttacking")
        && !anim.GetBool("isBlocking")
        && !anim.GetBool("isDodging")
        && !anim.GetBool("isCharging")
        && !anim.GetBool("fireSpecial")
        && canFire)
      {
        pressedCharge = true;
      }
    }

    if (gameObject.name == "Player_Two")
    {
      if (Input.GetButtonDown("Player_Two_Heavy") 
        && !anim.GetBool("isHurt")
        && !anim.GetBool("isAttacking")
        && !anim.GetBool("isBlocking")
        && !anim.GetBool("isDodging")
        && !anim.GetBool("isCharging")
        && !anim.GetBool("fireSpecial")
        && canFire)
      {
        pressedCharge = true;
      }
    }
  }

  private void FixedUpdate() {
    if (pressedCharge) {
      pressedCharge = false;
      StartCoroutine(Laser());
    }
  }

  private IEnumerator Laser() {
    if (anim.GetBool("isHurt")) {
      anim.SetBool("isCharging", false);
      anim.SetBool("fireSpecial", false);
      yield return new WaitForSeconds(recoverTime);
      canFire = true;
      yield break;
    }

    canFire = false;
    anim.SetBool("isCharging", true);
    anim.SetBool("canMove", false);
    anim.SetBool("canTurn", false);

    yield return new WaitForSeconds(chargeTime);
    if (anim.GetBool("isHurt")) {
      anim.SetBool("isCharging", false);
      anim.SetBool("fireSpecial", false);
      yield return new WaitForSeconds(recoverTime);
      canFire = true;
      yield break;
    }

    anim.SetBool("isCharging", false);

    if (anim.GetInteger("Direction") > 0) {
      rLaser.StartLaser();
    } else {
      lLaser.StartLaser();
    }
    
    anim.SetBool("fireSpecial", true);

    yield return new WaitForSeconds(lifeTime);
    if (anim.GetBool("isHurt")) {
      anim.SetBool("isCharging", false);
      anim.SetBool("fireSpecial", false);
      yield return new WaitForSeconds(recoverTime);
      canFire = true;
      yield break;
    }
    if (anim.GetInteger("Direction") > 0) {
      rLaser.StopLaser();
    } else {
      lLaser.StopLaser();
    }
    
    anim.SetBool("fireSpecial", false);
    anim.SetBool("canMove", true);
    anim.SetBool("canTurn", true);

    yield return new WaitForSeconds(recoverTime);
    canFire = true;
  }
}
