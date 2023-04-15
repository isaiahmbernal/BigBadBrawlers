using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack_Collider : MonoBehaviour
{
  public List<GameObject> enemies;
  
  // public Player_Attack Player_Attack;
  public SpriteRenderer sprite;
  public Animator anim;

  public float damage;
  public float xAttackForce;
  public float yAttackForce;
  public float timeBeforeAttack;
  public bool isAttacking;

  void Awake()
  {
    enemies = new List<GameObject>();

    anim = gameObject.GetComponentInParent<Animator>();

    sprite = gameObject.GetComponent<SpriteRenderer>();
    sprite.color = new Color(0f, 0f, 0f, .1f);

    // damage = 5f;
    xAttackForce = damage * .60f;
    yAttackForce = damage * .70f;
  }
  
  public void StartHurt() {
    StartCoroutine(HurtEnemy());
  }

  // public void FixedUpdate() {
  //   if (anim.GetBool("isHurt"))
  // }

  public IEnumerator HurtEnemy()
  {
    yield return new WaitForSeconds(timeBeforeAttack);

    if (enemies.Count != 0 && !anim.GetBool("isHurt"))
    {
      // Debug.Log("Enemies Found");
      foreach (GameObject enemy in enemies)
      {
        // Debug.Log("Hitting " + enemy.name);
        float currentDamage = 0f;

        Animator enemyAnim = enemy.GetComponentInParent<Animator>();

        if (!enemyAnim.GetBool("isDodging"))
        {

          if (enemyAnim.GetBool("isBlocking"))
          {
            enemyAnim.SetFloat("ShieldHealth", enemyAnim.GetFloat("ShieldHealth") - damage * 5);
          }

          else
          {
            enemyAnim.SetFloat("Health", enemyAnim.GetFloat("Health") + damage);
            currentDamage = enemyAnim.GetFloat("Health");
            enemyAnim.SetBool("wasHit", true);

            Rigidbody2D enemyRB = enemy.GetComponentInParent<Rigidbody2D>();

            // KNOCKBACK
            switch (gameObject.name)
            {
              case "R-Attack":
                enemyRB.velocity = new Vector3 (xAttackForce + (currentDamage / 30), enemyRB.velocity.y, 0f);
                // Debug.Log("Hit For " + (xAttackForce + (currentDamage / 30)));
                break;
              case "L-Attack":
                enemyRB.velocity = new Vector3 (-xAttackForce - (currentDamage / 30), enemyRB.velocity.y, 0f);
                // Debug.Log("Hit For " + (xAttackForce + (currentDamage / 30)));
                break;
              case "U-Attack":
                enemyRB.velocity = new Vector3 (enemyRB.velocity.x, yAttackForce + (currentDamage / 30), 0f);
                // Debug.Log("Hit For " + (yAttackForce + (currentDamage / 30)));
                break;
              case "D-Attack":
                enemyRB.velocity = new Vector3 (enemyRB.velocity.x, -yAttackForce - (currentDamage / 30), 0f);
                // Debug.Log("Hit For " + (yAttackForce - (currentDamage / 30)));
                break;
            }
          }
          
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
