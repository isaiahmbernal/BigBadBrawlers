using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Momo_Attack_Heavy_Charge : MonoBehaviour
{

  public Animator anim;
  // public Player_Attack Player_Attack;
  // public Player_Movement Player_Movement;
  // public int playerDirection;
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

  void Awake()
  {
    anim = gameObject.GetComponent<Animator>();
    // // Player_Attack = gameObject.GetComponent<Player_Attack>();
    // Player_Movement = gameObject.GetComponent<Player_Movement>();
    // playerDirection = Player_Movement.playerDirection;
    // playerDirection = anim.GetInteger("Direction");

    isCharging = false;
    pressedCharge = false;
    releaseCharge = false;
    chargeTimer = 0f;
    startChargeTimer = false;
    chargeStage = 1;
    timeSinceLastCharge = 0f;
    runLastChargeTimer = false;
    speed1 = 100f;
    speed2 = 200f;
    speed3 = 300f;
  }

  void Update()
  {
    if (gameObject.name == "Player_One")
    {
      if (Input.GetButtonDown("Player_One_Heavy") && !anim.GetBool("isAttacking") && !runLastChargeTimer)
      {
        pressedCharge = true;
      }

      if (Input.GetButtonUp("Player_One_Heavy") && !anim.GetBool("isAttacking") && !runLastChargeTimer && isCharging == true)
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
      Debug.Log("Last Charge Timer: " + timeSinceLastCharge);
      if (timeSinceLastCharge > 1)
      {
        runLastChargeTimer = false;
      }
    }

    if (pressedCharge)
    {
      StartCharge();
    }

    if (releaseCharge)
    {
      StopCharge();
    }

    if (startChargeTimer)
    {
      chargeTimer += Time.deltaTime;

      if (chargeTimer > 1 && chargeTimer < 2 && chargeStage != 2)
      {
        chargeStage = 2;
        Debug.Log("Charge Stage: " + chargeStage);
      }
      if (chargeTimer > 2 && chargeStage != 3)
      {
        chargeStage = 3;
        Debug.Log("Charge Stage: " + chargeStage);
      }
    }
  }

  void StartCharge()
  {
    pressedCharge = false;
    isCharging = true;
    startChargeTimer = true;
    chargeStage = 1;
    anim.SetBool("canMove", false);
    // Player_Movement.canMove = false;
    anim.SetBool("isCharging", true);
    // Debug.Log("Start Charge");
    // playerDirection = anim.GetInteger("Direction");
    // playerDirection = Player_Movement.playerDirection;
  }

  void StopCharge()
  {
    GameObject chargeBall = (GameObject)Instantiate(eBall, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.1f, 0), Quaternion.identity);
    chargeBall.GetComponent<Momo_Attack_Heavy_Collider>().parentName = gameObject.name;
    chargeBall.GetComponent<Momo_Attack_Heavy_Collider>().chargeStage = chargeStage;
    switch (chargeStage)
    {
      case 1:

        if (anim.GetInteger("Direction") > 0)
        {
          chargeBall.transform.localScale = new Vector3(.15f, .15f, .15f);
          chargeBall.GetComponent<Rigidbody2D>().AddForce(new Vector2(speed1, 0f));
        }
        else if (anim.GetInteger("Direction") < 0)
        {
          chargeBall.transform.localScale = new Vector3(.15f, .15f, .15f);
          chargeBall.GetComponent<Rigidbody2D>().AddForce(new Vector2(-speed1, 0f));
        }

        // switch (playerDirection)
        // {
        //   case "R":
        //     chargeBall.transform.localScale = new Vector3(.15f, .15f, .15f);
        //     chargeBall.GetComponent<Rigidbody2D>().AddForce(new Vector2(speed1, 0f));
        //     break;

        //   case "L":
        //     chargeBall.transform.localScale = new Vector3(.15f, .15f, .15f);
        //     chargeBall.GetComponent<Rigidbody2D>().AddForce(new Vector2(-speed1, 0f));
        //     break;
        // }

        break;

      case 2:

        if (anim.GetInteger("Direction") > 0)
        {
          chargeBall.transform.localScale = new Vector3(.25f, .25f, .25f);
          chargeBall.GetComponent<Rigidbody2D>().AddForce(new Vector2(speed2, 0f));
        }
        else if (anim.GetInteger("Direction") < 0)
        {
          chargeBall.transform.localScale = new Vector3(.25f, .25f, .25f);
          chargeBall.GetComponent<Rigidbody2D>().AddForce(new Vector2(-speed2, 0f));
        }

        // switch (playerDirection)
        // {
        //   case "R":
        //     chargeBall.transform.localScale = new Vector3(.25f, .25f, .25f);
        //     chargeBall.GetComponent<Rigidbody2D>().AddForce(new Vector2(speed2, 0f));
        //     break;

        //   case "L":
        //     chargeBall.transform.localScale = new Vector3(.25f, .25f, .25f);
        //     chargeBall.GetComponent<Rigidbody2D>().AddForce(new Vector2(-speed2, 0f));
        //     break;
        // }

        break;

      case 3:

        if (anim.GetInteger("Direction") > 0)
        {
          chargeBall.transform.localScale = new Vector3(.4f, .4f, .4f);
          chargeBall.GetComponent<Rigidbody2D>().AddForce(new Vector2(speed3, 0f));
        }
        else if (anim.GetInteger("Direction") < 0)
        {
          chargeBall.transform.localScale = new Vector3(.4f, .4f, .4f);
          chargeBall.GetComponent<Rigidbody2D>().AddForce(new Vector2(-speed3, 0f));
        }

        // switch (playerDirection)
        // {
        //   case "R":
        //     chargeBall.transform.localScale = new Vector3(.4f, .4f, .4f);
        //     chargeBall.GetComponent<Rigidbody2D>().AddForce(new Vector2(speed3, 0f));
        //     break;

        //   case "L":
        //     chargeBall.transform.localScale = new Vector3(.4f, .4f, .4f);
        //     chargeBall.GetComponent<Rigidbody2D>().AddForce(new Vector2(-speed3, 0f));
        //     break;
        // }

        break;
    }

    releaseCharge = false;
    isCharging = false;
    startChargeTimer = false;
    chargeTimer = 0f;
    chargeStage = 1;
    anim.SetBool("canMove", true);
    // Player_Movement.canMove = true;
    anim.SetBool("isCharging", false);
    timeSinceLastCharge = 0f;
    runLastChargeTimer = true;
    Debug.Log("Stop Charge");
  }
}