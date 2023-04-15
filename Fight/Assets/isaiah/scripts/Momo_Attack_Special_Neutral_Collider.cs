using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Momo_Attack_Special_Neutral_Collider : MonoBehaviour
{

  public float damage;
  public float xAttackForce;
  public float yAttackForce;
  public string parentName;
  public int chargeStage;

  public float lifeTime;
  public float currTime;

  private void Awake() {
    lifeTime = 3f;
    currTime = 0f;
  }

  private void FixedUpdate() {
    if (currTime < lifeTime) {
      currTime += Time.deltaTime;
      if (currTime > lifeTime) {
        Destroy(gameObject);
      }
    }
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.tag == "Player" && other.transform.parent.name != parentName)
    {
      Debug.Log("other.gameObject.name: " + other.gameObject.name);
      Debug.Log("parentName: " + parentName);
      if (chargeStage == 0) chargeStage = 1;
      damage = 8 * chargeStage;
      Animator otherAnim = other.gameObject.GetComponentInParent<Animator>();

      if (otherAnim.GetBool("isDodging"))
      {
        return;
      }

      if (otherAnim.GetBool("isBlocking"))
      {
        otherAnim.SetFloat("ShieldHealth", otherAnim.GetFloat("ShieldHealth") - (damage * 5));
        Destroy(gameObject);
        return;
      }

      otherAnim.SetFloat("Health", otherAnim.GetFloat("Health") + damage);
      otherAnim.SetBool("wasHit", true);

      xAttackForce = damage * .15f;
      yAttackForce = damage * .15f;

      float directionX = gameObject.transform.position.x - other.gameObject.transform.position.x;
      float directionY = gameObject.transform.position.y - other.gameObject.transform.position.y;

      if (directionX > 0 && directionY > 0)
      {
        other.gameObject.GetComponentInParent<Rigidbody2D>().velocity = new Vector3(-xAttackForce, -yAttackForce, 0f); ;
        
      }
      else if (directionX < 0 && directionY > 0)
      {
        other.gameObject.GetComponentInParent<Rigidbody2D>().velocity = new Vector3(xAttackForce, -yAttackForce, 0f); ;
      }
      else if (directionX > 0 && directionY < 0)
      {
        other.gameObject.GetComponentInParent<Rigidbody2D>().velocity = new Vector3(-xAttackForce, yAttackForce, 0f); ;
      }
      else if (directionX < 0 && directionY < 0)
      {
        other.gameObject.GetComponentInParent<Rigidbody2D>().velocity = new Vector3(xAttackForce, yAttackForce, 0f); ;
      }

        Destroy(gameObject);
    }
  }

}