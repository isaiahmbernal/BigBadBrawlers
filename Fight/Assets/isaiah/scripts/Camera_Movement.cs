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

  // Calculate every frame
  void FixedUpdate()
  {
    Distance();
    Position();
  }

  // Get the positions of all players and set
  // the position of the camera to the avg point
  // of all of them
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

  // Get the greatest distance from the
  // distance between each player, and use
  // it to set how far the camera should be
  // from the stage (so it can see all players
  // on the screen at once)
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
        // Debug.Log("Distance " + currDist + " between " + GameManager.playerList[i].name + " and " + GameManager.playerList[j].name);
        if (currDist > currGreatest)
        {
          currGreatest = currDist;
          // Debug.Log("New Distance: " + currGreatest);
        }
      }
    }
    mainCamera.fieldOfView = currGreatest * 60f;
    if (mainCamera.fieldOfView < 70f)
    {
      mainCamera.fieldOfView = 70f;
    }
    else if (mainCamera.fieldOfView > 90f)
    {
      mainCamera.fieldOfView = 90f;
    }
  }
}
