using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public int playerNum = 2;
  public List<GameObject> playerList;
  public Transform playerOneLives;
  public Transform playerTwoLives;
  public GameObject momoPrefab;
  public GameObject angiePrefab;
  public GameObject stickmanPrefab;
  public GameObject hudPrefab;
  public GameObject canvas;
  public Sprite[] indicatorList;

  // Instantiate all players and track them
  void Awake()
  {
    canvas = GameObject.Find("Canvas");

    playerList = new List<GameObject>();
    // playerPrefabs = Resources.LoadAll("characters", typeof(GameObject));
    // indicatorList = Resources.LoadAll("indicators", typeof(Sprite));
    indicatorList = Resources.LoadAll<Sprite>("indicators");

    for (int i = 0; i < playerNum; i += 1)
    {
      Object charPrefab;
      GameObject player;
      // GameObject HUD;
      switch (i)
      {
        case 0:

          charPrefab = momoPrefab;

          if (PlayerPrefs.GetString("Player_One_Character") == "Momo") {
            charPrefab = momoPrefab;
          } else if (PlayerPrefs.GetString("Player_One_Character") == "Angie") {
            charPrefab = angiePrefab;
          } else if (PlayerPrefs.GetString("Player_One_Character") == "Stickman") {
            charPrefab = stickmanPrefab;
          }

          player = (GameObject)Instantiate(charPrefab, new Vector3(-1, 0, 1), Quaternion.identity);
          player.name = "Player_One";
          player.transform.Find("Platform-Collider").gameObject.layer = LayerMask.NameToLayer("Momo-Platform");
          player.transform.Find("Platform-Trigger").gameObject.layer = LayerMask.NameToLayer("Momo-Platform");
          player.transform.Find("Stage-Collider").gameObject.layer = LayerMask.NameToLayer("Momo-Stage");
          player.transform.Find("Indicator").GetComponent<SpriteRenderer>().sprite = indicatorList[i];
          playerList.Add(player);
          Debug.Log("Player One Spawned");

          break;

        case 1:

          charPrefab = momoPrefab;
          
          if (PlayerPrefs.GetString("Player_Two_Character") == "Momo") {
            charPrefab = momoPrefab;
          } else if (PlayerPrefs.GetString("Player_Two_Character") == "Angie") {
            charPrefab = angiePrefab;
          } else if (PlayerPrefs.GetString("Player_Two_Character") == "Stickman") {
            charPrefab = stickmanPrefab;
          }
          
          player = (GameObject)Instantiate(charPrefab, new Vector3(1, 0, 1), Quaternion.identity);
          player.name = "Player_Two";
          player.transform.Find("Platform-Collider").gameObject.layer = LayerMask.NameToLayer("Boxer-Platform");
          player.transform.Find("Platform-Trigger").gameObject.layer = LayerMask.NameToLayer("Boxer-Platform");
          player.transform.Find("Stage-Collider").gameObject.layer = LayerMask.NameToLayer("Boxer-Stage");
          player.transform.Find("Indicator").GetComponent<SpriteRenderer>().sprite = indicatorList[i];
          playerList.Add(player);
          Debug.Log("Player Two Spawned");

          break;

      }
    }
  }

  void Start()
  {
    Debug.Log("Player One: " + PlayerPrefs.GetString("Player_One_Character"));
    Debug.Log("Player Two: " + PlayerPrefs.GetString("Player_Two_Character"));
    Application.targetFrameRate = 60;
  }

  // Debug button to reset match
  void Update()
  {
    if (Input.GetKeyDown("`"))
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
  }

  // If a player leaves the game area, remove
  // a life, reset their health, and put them
  // back on the stage (also sets game over if
  // someone loses all lives)
  void OnTriggerExit2D(Collider2D other)
  {
    if (other.gameObject.tag == "Player")
    {
      // Debug.Log("Player Died");
      Animator otherAnim = other.gameObject.GetComponent<Animator>();
      int currLives = otherAnim.GetInteger("Lives");
      if (currLives == 1) {
        Debug.Log("Game Over");
        if (other.gameObject.name == "Player_One") {
          PlayerPrefs.SetString("Winner", "Player_Two");
          SceneManager.LoadScene("Victory Screen");
        } else {
          PlayerPrefs.SetString("Winner", "Player_One");
          SceneManager.LoadScene("Victory Screen");
        }
        // otherAnim.SetFloat("Health", 0);
        // otherAnim.transform.position = new Vector3(0, 0, 1);
        return;
      }
      otherAnim.SetInteger("Lives", otherAnim.GetInteger("Lives") - 1);
      otherAnim.transform.position = new Vector3(0, 0, 1);
      other.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
      // Debug.Log(other.gameObject.name);
      if (other.gameObject.name == "Player_One") {
        Debug.Log("Player One Lost Life");
        otherAnim.SetFloat("Health", 0);
        Destroy(playerOneLives.GetChild(0).gameObject);
      } else if (other.gameObject.name == "Player_Two") {
        Debug.Log("Player Two Lost Life");
        otherAnim.SetFloat("Health", 0);
        Destroy(playerTwoLives.GetChild(0).gameObject);
      }
    }
  }
}