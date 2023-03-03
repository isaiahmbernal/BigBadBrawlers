using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player_HUD : MonoBehaviour
{
  public int maxLives;

  public TMP_Text nameText;
  public TMP_Text healthText;
  public GameObject playerLives;
  public GameObject lifeSprite;

  public List<GameObject> livesList;

  public GameObject player;
  public Animator anim;

  void Awake()
  {
    maxLives = 3;

    nameText = gameObject.transform.Find("Player_Name").GetComponent<TMP_Text>();
    healthText = gameObject.transform.Find("Player_Health").GetComponent<TMP_Text>();
    playerLives = gameObject.transform.Find("Player_Lives").gameObject;
    livesList = new List<GameObject>();
  }

  void Start()
  {
    if (gameObject.name == "Player_One_HUD")
    {
      nameText.text = "Player One";
      player = GameObject.Find("Player_One");
    }
    else if (gameObject.name == "Player_Two_HUD")
    {
      nameText.text = "Player Two";
      player = GameObject.Find("Player_Two");
    }
    else if (gameObject.name == "Player_Three_HUD")
    {
      nameText.text = "Player Three";
      player = GameObject.Find("Player_Three");
    }
    else if (gameObject.name == "Player_Four_HUD")
    {
      nameText.text = "Player Four";
      player = GameObject.Find("Player_Four");
    }
    anim = player.GetComponent<Animator>();
    healthText.text = anim.GetFloat("Health").ToString("0.00") + "%";

    for (int i = 0; i < maxLives; i++)
    {
      GameObject life = Instantiate(lifeSprite, playerLives.transform);
      livesList.Add(life);
      life.transform.SetParent(playerLives.transform);

      if (i == 0)
      {
        life.transform.localPosition = new Vector3(-30, -30);
      }
      else if (i == 1)
      {
        life.transform.localPosition = new Vector3(0, -30);
      }
      else if (i == 2)
      {
        life.transform.localPosition = new Vector3(30, -30);
      }
    }
  }

  void FixedUpdate()
  {
    if (healthText.text != anim.GetFloat("Health").ToString("0.00") + "%")
    {
      healthText.text = anim.GetFloat("Health").ToString("0.00") + "%";
    }

    if (livesList.Count != anim.GetInteger("Lives") && livesList.Count != 0)
    {
      Destroy(livesList[livesList.Count - 1]);
      livesList.RemoveAt(livesList.Count - 1);
      anim.SetFloat("Health", 0f);
    }
  }
}
