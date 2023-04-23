using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Button_LoadMap : MonoBehaviour
{
    public Button startButton;

    void Awake() {
      startButton = gameObject.GetComponent<Button>();
    }

    void Start() {
      startButton.onClick.AddListener(NextScene);
    }

    // Gets the name of the map to load
    // from PlayerPrefs and loads it
    public void NextScene() {
      SceneManager.LoadScene(PlayerPrefs.GetString("Map"));
    }
}
