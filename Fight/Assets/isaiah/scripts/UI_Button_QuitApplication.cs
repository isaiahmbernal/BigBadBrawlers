using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Button_QuitApplication : MonoBehaviour
{
    public Button quitButton;
    
    private void Awake() {
        quitButton = gameObject.GetComponent<Button>();
        quitButton.onClick.AddListener(QuitGame);
    }

    private void QuitGame() {
        Application.Quit();
    }
}
