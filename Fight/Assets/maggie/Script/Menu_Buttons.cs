using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Buttons : MonoBehaviour
{
    public bool reload;
    public void PlayGame()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        if (!reload)
        {
            index += 1;
            if (index == SceneManager.sceneCountInBuildSettings) { index = 0; }
        }
        SceneManager.LoadScene(index);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
