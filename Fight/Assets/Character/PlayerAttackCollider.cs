using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCollider : MonoBehaviour
{
  public GameObject otherPlayer;
  public PlayerHealth otherHealth;

  public PlayerAttack myPlayer;
  public SpriteRenderer attackSprite;

  public float attackDirection;
  public float damage;
  public float attackForce;

  void Awake()
  {
    myPlayer = gameObject.GetComponentInParent<PlayerAttack>();
    attackSprite = gameObject.GetComponent<SpriteRenderer>();
    attackSprite.color = new Color(0f, 0f, 0f, .1f);
    
    attackDirection = 0;
    damage = 10f;
    attackForce = damage / 2;
  }

  public void HurtEnemy()
  {
    if (otherPlayer)
    {
      otherHealth.health -= damage;
      otherHealth.wasHit = true;
      attackDirection = otherPlayer.transform.position.x - transform.position.x;
      Debug.Log("Attack Direction: " + attackDirection);
      // Debug.Log(gameObject.name + ": Hit Enemy for " + damage + " with force of " + (attackForce / attackDirection));
      Debug.Log(gameObject.name + ": Hit Enemy for " + damage + " with force of " + attackForce);
      otherPlayer.GetComponent<Rigidbody2D>().AddForce(new Vector2(attackForce * attackDirection, 0), ForceMode2D.Impulse);
      // rb2d.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }
  }

  public void OnTriggerEnter2D(Collider2D other)
  {
    // Debug.Log(gameObject.name + ": Something Entered");
    if (other.gameObject.tag == "Player")
    {
      // Debug.Log(gameObject.name + ": Touching A Player");
      if (other.gameObject.GetComponent<PlayerAttack>() != myPlayer)
      {
        otherPlayer = other.gameObject;
        otherHealth = other.gameObject.GetComponent<PlayerHealth>();
        attackSprite.color = new Color(0f, 255f, 0f, .3f);
        // Debug.Log(gameObject.name + ": Touching Enemy");
      }
    }
  }

  public void OnTriggerExit2D(Collider2D other)
  {
    attackSprite.color = new Color(0f, 0f, 0f, .1f);
    otherPlayer = null;
    otherHealth = null;
    attackDirection = 0;
  }
}
