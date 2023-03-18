using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

  public int playerNum;
  public Object[] playerPrefabs;
  public List<GameObject> playerList;
  public GameObject hudPrefab;
  public GameObject canvas;
  public Sprite[] indicatorList;

  void Awake()
  {
    canvas = GameObject.Find("Canvas");
    playerList = new List<GameObject>();
    // playerPrefabs = Resources.LoadAll("characters", typeof(GameObject));
    // indicatorList = Resources.LoadAll("indicators", typeof(Sprite));
    indicatorList = Resources.LoadAll<Sprite>("indicators");

    for (int i = 0; i < playerNum; i += 1)
    {
      // Debug.Log("i: " + i);
      // GameObject player = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/isaiah/prefabs/Proto", typeof(GameObject));
      // GameObject player = Resources.Load("Proto.prefab");
      Object prefab = playerPrefabs[i];
      GameObject player;
      GameObject HUD;
      switch (i)
      {
        case 0:
          player = (GameObject)Instantiate(prefab, new Vector3(-1, 0, 1), Quaternion.identity);
          player.name = "Player_One";
          player.transform.Find("Indicator").GetComponent<SpriteRenderer>().sprite = indicatorList[i];
          playerList.Add(player);
          Debug.Log("Player One Spawned");

          HUD = Instantiate(hudPrefab);
          HUD.name = "Player_One_HUD";
          HUD.transform.SetParent(canvas.transform);
          HUD.GetComponent<RectTransform>().anchoredPosition = new Vector3(-100f, 100f, 0f);
          Debug.Log("Player One HUD Created");
          break;
        case 1:
          player = (GameObject)Instantiate(prefab, new Vector3(1, 0, 1), Quaternion.identity);
          player.name = "Player_Two";
          player.transform.Find("Indicator").GetComponent<SpriteRenderer>().sprite = indicatorList[i];
          playerList.Add(player);
          Debug.Log("Player Two Spawned");

          HUD = Instantiate(hudPrefab);
          HUD.name = "Player_Two_HUD";
          HUD.transform.SetParent(canvas.transform);
          HUD.GetComponent<RectTransform>().anchoredPosition = new Vector3(100f, 100f, 0f);
          Debug.Log("Player Two HUD Created");
          break;
        case 2:
          player = (GameObject)Instantiate(prefab, new Vector3(-1, 1, 1), Quaternion.identity);
          player.name = "Player_Three";
          player.transform.Find("Indicator").GetComponent<SpriteRenderer>().sprite = indicatorList[i];
          playerList.Add(player);
          Debug.Log("Player Three Spawned");

          HUD = Instantiate(hudPrefab);
          HUD.name = "Player_Three_HUD";
          HUD.transform.SetParent(canvas.transform);
          HUD.GetComponent<RectTransform>().anchoredPosition = new Vector3(-300f, 100f, 0f);
          Debug.Log("Player Three HUD Created");
          break;
        case 3:
          player = (GameObject)Instantiate(prefab, new Vector3(1, 1, 1), Quaternion.identity);
          player.name = "Player_Four";
          player.transform.Find("Indicator").GetComponent<SpriteRenderer>().sprite = indicatorList[i];
          playerList.Add(player);
          Debug.Log("Player Four Spawned");
          
          HUD = Instantiate(hudPrefab);
          HUD.name = "Player_Four_HUD";
          HUD.transform.SetParent(canvas.transform);
          HUD.GetComponent<RectTransform>().anchoredPosition = new Vector3(300f, 100f, 0f);
          Debug.Log("Player Four HUD Created");
          break;
      }
    }
  }

  void Start()
  {
    Application.targetFrameRate = 60;
  }

  void Update()
  {
    if (Input.GetKeyDown("`"))
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
  }

  void OnTriggerExit2D(Collider2D other)
  {
    if (other.gameObject.tag == "Player")
    {
      Animator otherAnim = other.gameObject.GetComponent<Animator>();
      otherAnim.SetInteger("Lives", otherAnim.GetInteger("Lives") - 1);
      otherAnim.transform.position = new Vector3(0, 0, 1);
      Debug.Log("Brruh");
    }
  }
}
