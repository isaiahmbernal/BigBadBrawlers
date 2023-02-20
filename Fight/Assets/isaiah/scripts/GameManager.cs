using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

  public int playerNum;
  public Object[] playerPrefabs;
  public List<GameObject> playerList;

  void Awake()
  {
    playerList = new List<GameObject>();
    playerNum = 4;
    playerPrefabs = Resources.LoadAll("prefabs", typeof(GameObject));
  }

  void Start()
  {
    for (int i = 0; i < playerNum; i += 1)
    {
      // Debug.Log("i: " + i);
      // GameObject player = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/isaiah/prefabs/Proto", typeof(GameObject));
      // GameObject player = Resources.Load("Proto.prefab");
      Object prefab = playerPrefabs[0];
      GameObject player;
      switch (i)
      {
        case 0:
          player = (GameObject)Instantiate(prefab, new Vector3(-4, -1, 0), Quaternion.identity);
          player.name = "Player_One";
          playerList.Add(player);
          Debug.Log("Player One Spawned");
          break;
        case 1:
          player = (GameObject)Instantiate(prefab, new Vector3(4, -1, 0), Quaternion.identity);
          player.name = "Player_Two";
          playerList.Add(player);
          Debug.Log("Player Two Spawned");
          break;
        case 2:
          player = (GameObject)Instantiate(prefab, new Vector3(-4, 0, 0), Quaternion.identity);
          player.name = "Player_Three";
          playerList.Add(player);
          Debug.Log("Player Three Spawned");
          break;
        case 3:
          player = (GameObject)Instantiate(prefab, new Vector3(4, 0, 0), Quaternion.identity);
          player.name = "Player_Four";
          playerList.Add(player);
          Debug.Log("Player Four Spawned");
          break;
      }
    }
    // Debug.Log(playerList[0].name);
  }

  void Update()
  {
    if (Input.GetKeyDown("`"))
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
  }
}
