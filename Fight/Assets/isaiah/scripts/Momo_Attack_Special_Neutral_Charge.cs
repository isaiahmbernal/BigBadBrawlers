using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Momo_Attack_Special_Neutral_Charge : MonoBehaviour
{

  public Animator anim;
  public GameObject eBall;

  public bool isCharging;
  public bool pressedCharge;
  public bool releaseCharge;
  public float chargeTimer;
  public bool startChargeTimer;
  public int chargeStage;
  public float timeSinceLastCharge;
  public bool runLastChargeTimer;
  public float speed1;
  public float speed2;
  public float speed3;
  public float chargeRecoveryTime;
  public Transform rEnergyBlastPoint;
  public Transform lEnergyBlastPoint;

  void Awake()
  {
    anim = gameObject.GetComponent<Animator>();

    isCharging = false;
    pressedCharge = false;
    releaseCharge = false;
    chargeTimer = 0f;
    startChargeTimer = false;
    timeSinceLastCharge = 0f;
    runLastChargeTimer = false;
    speed1 = 100f;
    speed2 = 200f;
    speed3 = 300f;

    chargeRecoveryTime = 0.5f;

    rEnergyBlastPoint = gameObject.transform.Find("R-EnergyBlastPoint");
    lEnergyBlastPoint = gameObject.transform.Find("L-EnergyBlastPoint");
  }

  void Update()
  {
    if (gameObject.name == "Player_One")
    {
      if (Input.GetButtonDown("Player_One_Heavy")
        && !anim.GetBool("isHurt")
        && !anim.GetBool("isAttacking")
        && !anim.GetBool("isCharging")
        && !anim.GetBool("fireSpecial")
        && !anim.GetBool("isBlocking")
        && !anim.GetBool("isDodging")
        && !runLastChargeTimer)
      {
        pressedCharge = true;
      }

      if (Input.GetButtonUp("Player_One_Heavy")
        && !anim.GetBool("isHurt")
        && !anim.GetBool("isAttacking")
        && anim.GetBool("isCharging")
        && !anim.GetBool("fireSpecial")
        && !anim.GetBool("isBlocking")
        && !anim.GetBool("isDodging")
        && !runLastChargeTimer)
      {
        releaseCharge = true;
      }
    }

    if (gameObject.name == "Player_Two")
    {
      if (Input.GetButtonDown("Player_Two_Heavy")
        && !anim.GetBool("isHurt")
        && !anim.GetBool("isAttacking")
        && !anim.GetBool("isCharging")
        && !anim.GetBool("fireSpecial")
        && !anim.GetBool("isBlocking")
        && !anim.GetBool("isDodging")
        && !runLastChargeTimer)
      {
        pressedCharge = true;
      }

      if (Input.GetButtonUp("Player_Two_Heavy")
        && !anim.GetBool("isHurt")
        && !anim.GetBool("isAttacking")
        && anim.GetBool("isCharging")
        && !anim.GetBool("fireSpecial")
        && !anim.GetBool("isBlocking")
        && !anim.GetBool("isDodging")
        && !runLastChargeTimer)
      {
        releaseCharge = true;
      }
    }
  }

  void FixedUpdate()
  {

    if (runLastChargeTimer)
    {
      timeSinceLastCharge += Time.deltaTime;
      // Debug.Log("Last Charge Timer: " + timeSinceLastCharge);
      if (timeSinceLastCharge > 2)
      {
        runLastChargeTimer = false;
      }
    }

    if (pressedCharge)
    {
      StartCharge();
    }

    if (releaseCharge || anim.GetBool("isHurt"))
    {
      StopCharge();
    }

    if (startChargeTimer)
    {
      chargeTimer += Time.deltaTime;

      if (chargeTimer > .1f && chargeTimer < 1.5f && anim.GetInteger("chargeLevel") != 1)
      {
        // chargeStage = 1;
        anim.SetInteger("chargeLevel", 1);
        // Debug.Log("Charge Stage: " + chargeStage);
      }
      else if (chargeTimer > 1.5f && chargeTimer < 2.75f && anim.GetInteger("chargeLevel") != 2)
      {
        // chargeStage = 2;
        anim.SetInteger("chargeLevel", 2);
        // Debug.Log("Charge Stage: " + chargeStage);
      }
      else if (chargeTimer > 2.75f && anim.GetInteger("chargeLevel") != 3)
      {
        // chargeStage = 3;
        anim.SetInteger("chargeLevel", 3);
        // Debug.Log("Charge Stage: " + chargeStage);
      }
    }
  }

  void StartCharge()
  {
    pressedCharge = false;
    // isCharging = true;
    anim.SetBool("isCharging", true);
    startChargeTimer = true;
    // chargeStage = 0;
    anim.SetInteger("chargeLevel", 0);
    if (anim.GetBool("isGrounded"))
    {
      anim.SetBool("canMove", false);
    }
    anim.SetBool("canTurn", false);
    // anim.SetInteger("chargeLevel", 1);
    // Debug.Log("Start Charge");
  }

  void StopCharge()
  {
    if (anim.GetBool("isHurt")) {
      anim.SetBool("isCharging", false);
      releaseCharge = false;
      // isCharging = false;
      startChargeTimer = false;
      chargeTimer = 0f;
      // chargeStage = 0;
      anim.SetInteger("chargeLevel", 0);
      anim.SetBool("fireSpecial", false);
      timeSinceLastCharge = 0f;
      runLastChargeTimer = true;
      // Debug.Log("Stop Charge");
      return;
    }

    float xPos;
    if (anim.GetInteger("Direction") > 0) {
      xPos = rEnergyBlastPoint.position.x;
      Debug.Log("Blast Point: " + xPos);
    } else {
      xPos = lEnergyBlastPoint.position.x;
      Debug.Log("Blast Point: " + xPos);
    }
    GameObject chargeBall = (GameObject)Instantiate(eBall, new Vector3(xPos, rEnergyBlastPoint.position.y, 1), Quaternion.identity);
    chargeBall.GetComponent<Momo_Attack_Special_Neutral_Collider>().parentName = transform.name;
    chargeBall.GetComponent<Momo_Attack_Special_Neutral_Collider>().chargeStage = anim.GetInteger("chargeLevel");
    
    
    switch (anim.GetInteger("chargeLevel"))
    {
      case 0:

        if (anim.GetInteger("Direction") > 0)
        {
          chargeBall.transform.localScale = new Vector3(.50f, .50f, .50f);
          chargeBall.GetComponent<Rigidbody2D>().AddForce(new Vector2(speed1, 0f));
        }
        else if (anim.GetInteger("Direction") < 0)
        {
          chargeBall.transform.localScale = new Vector3(.50f, .50f, .50f);
          chargeBall.GetComponent<Rigidbody2D>().AddForce(new Vector2(-speed1, 0f));
        }

        break;

      case 1:

        if (anim.GetInteger("Direction") > 0)
        {
          chargeBall.transform.localScale = new Vector3(.50f, .50f, .50f);
          chargeBall.GetComponent<Rigidbody2D>().AddForce(new Vector2(speed1, 0f));
        }
        else if (anim.GetInteger("Direction") < 0)
        {
          chargeBall.transform.localScale = new Vector3(.50f, .50f, .50f);
          chargeBall.GetComponent<Rigidbody2D>().AddForce(new Vector2(-speed1, 0f));
        }

        break;

      case 2:

        if (anim.GetInteger("Direction") > 0)
        {
          chargeBall.transform.localScale = new Vector3(.75f, .75f, .75f);
          chargeBall.GetComponent<Rigidbody2D>().AddForce(new Vector2(speed2, 0f));
        }
        else if (anim.GetInteger("Direction") < 0)
        {
          chargeBall.transform.localScale = new Vector3(.75f, .75f, .75f);
          chargeBall.GetComponent<Rigidbody2D>().AddForce(new Vector2(-speed2, 0f));
        }

        break;

      case 3:

        if (anim.GetInteger("Direction") > 0)
        {
          chargeBall.transform.localScale = new Vector3(1f, 1f, 1f);
          chargeBall.GetComponent<Rigidbody2D>().AddForce(new Vector2(speed3, 0f));
        }
        else if (anim.GetInteger("Direction") < 0)
        {
          chargeBall.transform.localScale = new Vector3(1f, 1f, 1f);
          chargeBall.GetComponent<Rigidbody2D>().AddForce(new Vector2(-speed3, 0f));
        }

        break;
    }

    anim.SetBool("isCharging", false);
    anim.SetBool("fireSpecial", true);
    releaseCharge = false;
    // isCharging = false;
    startChargeTimer = false;
    chargeTimer = 0f;
    // chargeStage = 0;
    anim.SetInteger("chargeLevel", 0);
    timeSinceLastCharge = 0f;
    runLastChargeTimer = true;
    StartCoroutine(chargeRecover());

    // releaseCharge = false;
    // isCharging = false;
    // startChargeTimer = false;
    // chargeTimer = 0f;
    // chargeStage = 0;
    // anim.SetBool("canMove", true);
    // anim.SetBool("canTurn", true);
    // anim.SetBool("isCharging", false);
    // anim.SetInteger("chargeLevel", 0);
    // timeSinceLastCharge = 0f;
    // runLastChargeTimer = true;
    // // Debug.Log("Stop Charge");
  }

  private IEnumerator chargeRecover() {
    yield return new WaitForSeconds(chargeRecoveryTime);

    anim.SetBool("fireSpecial", false);
    anim.SetBool("canMove", true);
    anim.SetBool("canTurn", true);
    // anim.SetInteger("chargeLevel", 0);
    // Debug.Log("Stop Charge");
  }
}
