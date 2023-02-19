using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack_Collider : MonoBehaviour
{
  public List<GameObject> enemies;
  
  public Player_Attack Player_Attack;
  public SpriteRenderer SpriteRenderer;

  public float damage;
  public float xAttackForce;
  public float yAttackForce;

  void Awake()
  {
    enemies = new List<GameObject>();

    Player_Attack = gameObject.GetComponentInParent<Player_Attack>();
    SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    SpriteRenderer.color = new Color(0f, 0f, 0f, .1f);

    damage = 5f;
    xAttackForce = damage / 1.5f;
    yAttackForce = damage * 1.5f;
  }

  public void HurtEnemy()
  {
    // Debug.Log("HurtEnemy()");
    if (enemies.Count != 0)
    {
      // Debug.Log("Enemies Found");
      foreach (GameObject enemy in enemies)
      {
        // Debug.Log("Hitting " + enemy.name);
        float currentDamage = 0f;

        Player_Health Player_Health = enemy.GetComponent<Player_Health>();
        Player_Health.damage += damage;
        currentDamage = Player_Health.damage;
        Player_Health.wasHit = true;

        Rigidbody2D Enemy_Rigidbody2D = enemy.GetComponent<Rigidbody2D>();

        // KNOCKBACK
        switch (gameObject.name)
        {
          case "R-Attack":
            Enemy_Rigidbody2D.velocity = new Vector3 (xAttackForce + (currentDamage / 50), Enemy_Rigidbody2D.velocity.y, 0f);
            Debug.Log("Hit For " + (xAttackForce + (currentDamage / 50)));
            break;
          case "L-Attack":
            Enemy_Rigidbody2D.velocity = new Vector3 (-xAttackForce - (currentDamage / 50), Enemy_Rigidbody2D.velocity.y, 0f);
            Debug.Log("Hit For " + (xAttackForce + (currentDamage / 50)));
            break;
          case "U-Attack":
            Enemy_Rigidbody2D.velocity = new Vector3 (Enemy_Rigidbody2D.velocity.x, yAttackForce, 0f);
            Debug.Log("Hit For " + (xAttackForce + (currentDamage / 50)));
            break;
          case "D-Attack":
            Enemy_Rigidbody2D.velocity = new Vector3 (Enemy_Rigidbody2D.velocity.x, -yAttackForce, 0f);
            Debug.Log("Hit For " + (xAttackForce + (currentDamage / 50)));
            break;
        }
      }
    }
  }

  public void OnTriggerEnter2D(Collider2D other)
  {
    // Debug.Log(gameObject.name + ": Something Entered");
    if (other.gameObject.tag == "Player")
    {
      // Debug.Log(gameObject.name + ": Touching A Player");
      if (other.gameObject.name != gameObject.name)
      {
        enemies.Add(other.gameObject);
        // Debug.Log("Dummy Added " + other.gameObject.name + " to enemy list");

        SpriteRenderer.color = new Color(0f, 255f, 0f, .3f);
        // Debug.Log(gameObject.name + ": Touching Enemy");
      }
    }
  }

  public void OnTriggerExit2D(Collider2D other)
  {
    SpriteRenderer.color = new Color(0f, 0f, 0f, .1f);
    enemies.Remove(other.gameObject);
    // Debug.Log("Dummy Removed " + other.gameObject.name + " from enemy list");
  }
}
