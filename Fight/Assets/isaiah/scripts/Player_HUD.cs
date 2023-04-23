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
  public Transform playerLives;
  public GameObject lifeSprite;
  public Image characterIcon;
  public Sprite momoIcon;
  public Sprite angieIcon;
  public Sprite stickmanIcon;

  public GameObject player;
  public Animator anim;

  void Awake()
  {
    maxLives = 3;

    nameText = gameObject.transform.Find("Player_Name").GetComponent<TMP_Text>();
    healthText = gameObject.transform.Find("Player_Health").GetComponent<TMP_Text>();
    playerLives = gameObject.transform.Find("Player_Lives");
  }

  // Check which player we're tracking and
  // updates assets accordingly
  void Start()
  {
    if (gameObject.name == "Player_One_HUD")
    {
      nameText.text = PlayerPrefs.GetString("Player_One_Character");
      if (PlayerPrefs.GetString("Player_One_Character") == "Momo") {
        characterIcon.sprite = momoIcon;
      } else if (PlayerPrefs.GetString("Player_One_Character") == "Angie") {
        characterIcon.sprite = angieIcon;
      } else if (PlayerPrefs.GetString("Player_One_Character") == "Stickman") {
        characterIcon.sprite = stickmanIcon;
      }
      player = GameObject.Find("Player_One");
    }
    else if (gameObject.name == "Player_Two_HUD")
    {
      nameText.text = PlayerPrefs.GetString("Player_Two_Character");
      if (PlayerPrefs.GetString("Player_Two_Character") == "Momo") {
        characterIcon.sprite = momoIcon;
      } else if (PlayerPrefs.GetString("Player_Two_Character") == "Angie") {
        characterIcon.sprite = angieIcon;
      } else if (PlayerPrefs.GetString("Player_Two_Character") == "Stickman") {
        characterIcon.sprite = stickmanIcon;
      }
      player = GameObject.Find("Player_Two");
    }
    anim = player.GetComponent<Animator>();
    healthText.text = anim.GetFloat("Health").ToString("0.00") + "%";
  }

  // If the damage text doesn't match the health
  // of the player, update it
  void FixedUpdate()
  {
    if (healthText.text != anim.GetFloat("Health").ToString("0.00") + "%")
    {
      healthText.text = anim.GetFloat("Health").ToString("0.00") + "%";
    }
  }
}
