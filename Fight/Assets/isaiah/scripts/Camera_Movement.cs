using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Movement : MonoBehaviour
{

  public GameManager GameManager;
  public Camera mainCamera;
  public float maxXPos;

  void Awake()
  {
    mainCamera = Camera.main;
    GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
  }

  void FixedUpdate()
  {
    // Distance();
    Position();
  }

  void Position()
  {
    float totalX = 0f;
    float totalY = 0f;
    foreach (GameObject player in GameManager.playerList)
    {
      totalX += player.transform.position.x;
      totalY += player.transform.position.y;
    }
    float centerX = totalX / GameManager.playerList.Count;
    float centerY = totalY / GameManager.playerList.Count;

    if (centerX < -4f)
    {
      centerX = -4f;
    }
    else if (centerX > 4f)
    {
      centerX = 4f;
    }

    if (centerY < -3f)
    {
      centerY = -3f;
    }
    else if (centerY > 2f)
    {
      centerY = 2f;
    }

    mainCamera.transform.position = new Vector3(centerX, centerY, transform.position.z);
  }

  void Distance()
  {
    float currGreatest = 0;
    for (int i = 0; i < GameManager.playerList.Count - 1; i += 1)
    {
      // Debug.Log("i: " + i);
      for (int j = i + 1; j < GameManager.playerList.Count; j += 1)
      {
        // Debug.Log("j: " + j);
        float currDist = Vector3.Distance(GameManager.playerList[i].transform.position, GameManager.playerList[j].transform.position);
        // Debug.Log("i: " + i + " | j: " + j);
        Debug.Log("Distance " + currDist + " between " + GameManager.playerList[i].name + " and " + GameManager.playerList[j].name);
        if (currDist > currGreatest)
        {
          currGreatest = currDist;
          Debug.Log("New Distance: " + currGreatest);
        }
      }
    }
    if (currGreatest > 5)
    {
      mainCamera.orthographicSize = 5;
    }
    else if (currGreatest < 4)
    {
      mainCamera.orthographicSize = 4;
    }
    else
    {
      mainCamera.orthographicSize = currGreatest;
    }
  }
}
