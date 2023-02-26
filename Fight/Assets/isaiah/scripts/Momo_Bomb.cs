using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Momo_Bomb : MonoBehaviour
{
  public Animator anim;
  // public string parentName;

  public List<GameObject> enemies;

  public bool isExploding;
  public float timer;

  void Awake()
  {
    enemies = new List<GameObject>();
    anim = gameObject.GetComponent<Animator>();
    timer = 1f;
  }

  void FixedUpdate()
  {
    if (isExploding)
    {
      timer -= Time.deltaTime;
      if (timer < 0)
      {
        Destroy(gameObject);
      }
    }
  }

  void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.tag == "Player")
    {
      isExploding = true;
      anim.SetBool("isExploding", true);
      gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
      Destroy(transform.Find("Collider").gameObject);
    }
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.tag == "Player")
    {
      enemies.Add(other.gameObject);
    }
  }

  public void OnTriggerExit2D(Collider2D other)
  {
    if (other.gameObject.tag == "Player")
    {
      enemies.Remove(other.gameObject);
    }
  }
}
