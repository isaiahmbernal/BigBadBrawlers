using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angie_Attack_Special_Neutral_Collider : MonoBehaviour
{
  [SerializeField] private List<GameObject> enemies;
  // [SerializeField] private SpriteRenderer sprite;
  [SerializeField] private Animator anim;
  [SerializeField] private float timeBetweenHits;
  [SerializeField] private float damage;
  [SerializeField] private float xAttackForce;
  [SerializeField] private float yAttackForce;

  private void Awake()
  {
    enemies = new List<GameObject>();
    anim = gameObject.GetComponentInParent<Animator>();
    timeBetweenHits = .05f;
    // sprite = gameObject.GetComponent<SpriteRenderer>();
    // sprite.enabled = false;
    damage = 1.8f;
    xAttackForce = damage * .01f;
    yAttackForce = damage * .0f;
  }

  public void StartFlurry() {
    StartCoroutine(Flurries());
  }

  private IEnumerator Flurries() {
    FlurryHit();

    for (int i = 0; i < 7; i++) {
      yield return new WaitForSeconds(timeBetweenHits);
      FlurryHit();
    }

    yield return new WaitForSeconds(timeBetweenHits);
    FinalHit();
  }

  public void FlurryHit()
  {
    // sprite.enabled = true;

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
              case "R-Flurry":
                enemyRB.velocity = new Vector3 (xAttackForce, 0, 0f);
                // Debug.Log("Hit For " + (xAttackForce + (currentDamage / 30)));
                break;
              case "L-Flurry":
                enemyRB.velocity = new Vector3 (-xAttackForce, 0, 0f);
                // Debug.Log("Hit For " + (xAttackForce + (currentDamage / 30)));
                break;
            }
          }
          
        }
        
      }
    }
  }

  public void FinalHit()
  {
    // sprite.enabled = true;

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
              case "R-Flurry":
                enemyRB.velocity = new Vector3 (xAttackForce * 3 + (currentDamage / 23), enemyRB.velocity.y + yAttackForce + (currentDamage / 30), 0f);
                // Debug.Log("Hit For " + (xAttackForce + (currentDamage / 30)));
                break;
              case "L-Flurry":
                enemyRB.velocity = new Vector3 (-xAttackForce * 3 - (currentDamage / 23), enemyRB.velocity.y + yAttackForce + (currentDamage / 30), 0f);
                // Debug.Log("Hit For " + (xAttackForce + (currentDamage / 30)));
                break;
            }
          }
          
        }
        
      }
    }
  }

  public void StopFlurry() {
    // sprite.enabled = false;
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

        // sprite.color = new Color(0f, 255f, 0f, .3f);
        // Debug.Log(gameObject.name + ": Touching Enemy");
      }
    }
  }

  public void OnTriggerExit2D(Collider2D other)
  {
    // sprite.color = new Color(0f, 0f, 0f, .1f);
    enemies.Remove(other.gameObject);
    // Debug.Log("Dummy Removed " + other.gameObject.name + " from enemy list");
  }
} 