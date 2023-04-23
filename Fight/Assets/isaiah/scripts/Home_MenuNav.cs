using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Home_MenuNav : MonoBehaviour
{
  [SerializeField] private GameObject currScreen;
  [SerializeField] private GameObject nextScreen;
  [SerializeField] private Button btn;

  private void Awake() {
    btn = gameObject.GetComponent<Button>();
    btn.onClick.AddListener(LoadScreen);
  }

  // Set the current canvas object to
  // inactive (so it's not visible) and
  // set the pointed canvas object to
  // active to be seen
  private void LoadScreen() {
    nextScreen.SetActive(true);
    currScreen.SetActive(false);
  }
}
