using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_VictoryResults : MonoBehaviour
{
  [SerializeField] private GameObject p1Win;
  [SerializeField] private GameObject p2Win;
  [SerializeField] private GameObject momoObject;
  [SerializeField] private GameObject angieObject;
  [SerializeField] private GameObject stickmanObject;

  // Handles the victory screen, sets the proper
  // canvas objects to active based on who won
  private void Awake() {
    if (PlayerPrefs.GetString("Winner") == "Player_One") {
      p1Win.SetActive(true);
      switch (PlayerPrefs.GetString("Player_One_Character")) {
        case "Momo":
          momoObject.SetActive(true);
          break;
        case "Angie":
          angieObject.SetActive(true);
          break;
        case "Stickman":
          stickmanObject.SetActive(true);
          break;
      }
    } else {
      p2Win.SetActive(true);
      switch (PlayerPrefs.GetString("Player_Two_Character")) {
        case "Momo":
          momoObject.SetActive(true);
          break;
        case "Angie":
          angieObject.SetActive(true);
          break;
        case "Stickman":
          stickmanObject.SetActive(true);
          break;
      }
    }
  }
}
