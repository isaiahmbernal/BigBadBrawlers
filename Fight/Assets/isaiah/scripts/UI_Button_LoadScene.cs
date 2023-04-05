using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Button_LoadScene : MonoBehaviour
{
    public Button startButton;
    public string sceneName;

    void Awake() {
      startButton = gameObject.GetComponent<Button>();
    }

    void Start() {
      startButton.onClick.AddListener(NextScene);
    }

    public void NextScene() {
      SceneManager.LoadScene(sceneName);
    }
}
