using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player_HUD : MonoBehaviour
{
  public TMP_Text nameText;
  public TMP_Text healthText;
  public GameObject player;
  public Animator anim;
  public bool isLoaded;

  void Awake()
  {
    nameText = gameObject.transform.Find("Player_Name").GetComponent<TMP_Text>();
    healthText = gameObject.transform.Find("Player_Health").GetComponent<TMP_Text>();
    // if (gameObject.name == "Player_One_HUD")
    // {
    //   Debug.Log("Bruh");
    //   nameText.text = "Player One";
    //   player = GameObject.Find("Player_One");
    // }
    // anim = player.GetComponent<Animator>();
    // healthText.text = anim.GetFloat("Health").ToString();
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
  }

  void FixedUpdate()
  {
    if (healthText.text != anim.GetFloat("Health").ToString("0.00") + "%")
    {
      healthText.text = anim.GetFloat("Health").ToString("0.00") + "%";
    }
  }
}
