using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Momo_Attack_Heavy_Collider : MonoBehaviour
{

  public float damage;
  public float xAttackForce;
  public float yAttackForce;
  public string parentName;
  public int chargeStage;

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.tag == "Player" && other.gameObject.name != parentName)
    {
      damage = 4 * chargeStage;
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

      xAttackForce = damage * .4f;
      yAttackForce = damage * .4f;

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