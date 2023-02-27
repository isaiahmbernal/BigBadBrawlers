using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack_Collider : MonoBehaviour
{
  public List<GameObject> enemies;
  
  // public Player_Attack Player_Attack;
  public SpriteRenderer sprite;

  public float damage;
  public float xAttackForce;
  public float yAttackForce;

  void Awake()
  {
    enemies = new List<GameObject>();

    // Player_Attack = gameObject.GetComponentInParent<Player_Attack>();
    sprite = gameObject.GetComponent<SpriteRenderer>();
    sprite.color = new Color(0f, 0f, 0f, .1f);

    damage = 5f;
    xAttackForce = damage * 0.30f;
    yAttackForce = damage * 0.70f;
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

        // Player_Health health = enemy.GetComponent<Player_Health>();
        Animator enemyAnim = enemy.GetComponent<Animator>();
        enemyAnim.SetFloat("Health", enemyAnim.GetFloat("Health") + damage);
        // health.damage += damage;
        currentDamage = enemyAnim.GetFloat("Health");
        // currentDamage = health.damage;
        // health.wasHit = true;
        enemyAnim.SetBool("wasHit", true);

        Rigidbody2D enemyRB = enemy.GetComponent<Rigidbody2D>();

        // KNOCKBACK
        switch (gameObject.name)
        {
          case "R-Attack":
            enemyRB.velocity = new Vector3 (xAttackForce + (currentDamage / 45), enemyRB.velocity.y, 0f);
            Debug.Log("Hit For " + (xAttackForce + (currentDamage / 45)));
            break;
          case "L-Attack":
            enemyRB.velocity = new Vector3 (-xAttackForce - (currentDamage / 45), enemyRB.velocity.y, 0f);
            Debug.Log("Hit For " + (xAttackForce + (currentDamage / 45)));
            break;
          case "U-Attack":
            enemyRB.velocity = new Vector3 (enemyRB.velocity.x, yAttackForce + (currentDamage / 45), 0f);
            Debug.Log("Hit For " + (yAttackForce + (currentDamage / 45)));
            break;
          case "D-Attack":
            enemyRB.velocity = new Vector3 (enemyRB.velocity.x, -yAttackForce - (currentDamage / 45), 0f);
            Debug.Log("Hit For " + (yAttackForce - (currentDamage / 45)));
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

        sprite.color = new Color(0f, 255f, 0f, .3f);
        // Debug.Log(gameObject.name + ": Touching Enemy");
      }
    }
  }

  public void OnTriggerExit2D(Collider2D other)
  {
    sprite.color = new Color(0f, 0f, 0f, .1f);
    enemies.Remove(other.gameObject);
    // Debug.Log("Dummy Removed " + other.gameObject.name + " from enemy list");
  }
}
