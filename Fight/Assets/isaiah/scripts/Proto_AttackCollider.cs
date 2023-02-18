using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proto_AttackCollider : MonoBehaviour
{
  public List<GameObject> enemies;
  
  public Proto_Attack myPlayer;
  public SpriteRenderer attackSprite;

  public float damage;
  public float xAttackForce;
  public float yAttackForce;

  void Awake()
  {
    enemies = new List<GameObject>();

    myPlayer = gameObject.GetComponentInParent<Proto_Attack>();
    attackSprite = gameObject.GetComponent<SpriteRenderer>();
    attackSprite.color = new Color(0f, 0f, 0f, .1f);

    damage = 10f;
    xAttackForce = damage / 1.5f;
    yAttackForce = damage * 1.5f;
  }

  public void HurtEnemy()
  {
    Debug.Log("HurtEnemy()");
    if (enemies.Count != 0)
    {
      Debug.Log("Enemies Found");
      foreach (GameObject enemy in enemies)
      {
        Debug.Log("Hitting " + enemy.name);
        switch (enemy.name)
        {
          case "Proto":
            Proto_Health proto_Health = enemy.GetComponent<Proto_Health>();
            proto_Health.damage += damage;
            proto_Health.wasHit = true;
            break;
          case "Dummy":
            Dummy_Health dummy_Health = enemy.GetComponent<Dummy_Health>();
            Debug.Log(dummy_Health);
            dummy_Health.damage += damage;
            Debug.Log(dummy_Health.damage);
            dummy_Health.wasHit = true;
            break;
        }

        Rigidbody2D otherRB2D = enemy.GetComponent<Rigidbody2D>();
        switch (gameObject.name)
        {
          case "R-Attack":
            otherRB2D.velocity = new Vector3 (xAttackForce, otherRB2D.velocity.y, 0f);
            break;
          case "L-Attack":
            otherRB2D.velocity = new Vector3 (-xAttackForce, otherRB2D.velocity.y, 0f);
            break;
          case "U-Attack":
            otherRB2D.velocity = new Vector3 (otherRB2D.velocity.x, yAttackForce, 0f);
            break;
          case "D-Attack":
            otherRB2D.velocity = new Vector3 (otherRB2D.velocity.x, -yAttackForce, 0f);
            break;
        }
      }
    }
  }

  public void OnTriggerEnter2D(Collider2D other)
  {
    // Debug.Log(gameObject.name + ": Something Entered");
    if (other.gameObject.tag == "Player" && other.gameObject.tag != "Attack_Collider")
    {
      // Debug.Log(gameObject.name + ": Touching A Player");
      if (other.gameObject.name != gameObject.name)
      {
        enemies.Add(other.gameObject);
        // Debug.Log("Dummy Added " + other.gameObject.name + " to enemy list");

        attackSprite.color = new Color(0f, 255f, 0f, .3f);
        // Debug.Log(gameObject.name + ": Touching Enemy");
      }
    }
  }

  public void OnTriggerExit2D(Collider2D other)
  {
    attackSprite.color = new Color(0f, 0f, 0f, .1f);
    enemies.Remove(other.gameObject);
    // Debug.Log("Dummy Removed " + other.gameObject.name + " from enemy list");
  }
}
