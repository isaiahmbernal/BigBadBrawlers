using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_CharacterSelection : MonoBehaviour
{
    public Button prevChar;
    public Button nextChar;
    public Transform charactersTransform;
    public List<Transform> listOfCharacters;
    public int currCharIdx;
    public string charName;
    public TMP_Text nameText;
    public string playerName;

    // Create a list of characters, enable the
    // first character gameObject so it can be
    // seen in-game, and globally set that character
    // to the selected character in PlayerPrefs
    private void Start() {
        currCharIdx = 0;
        playerName = gameObject.transform.name;

        prevChar.onClick.AddListener(PreviousCharacter);
        nextChar.onClick.AddListener(NextCharacter);
        
        charactersTransform = transform.Find("Player_Characters");
        foreach (Transform child in charactersTransform) {
            listOfCharacters.Add(child);
        }

        nameText = transform.Find("Name").GetComponent<TMP_Text>();

        listOfCharacters[currCharIdx].gameObject.SetActive(true);
        charName = listOfCharacters[currCharIdx].gameObject.name;
        nameText.text = charName;
        PlayerPrefs.SetString(playerName + "_Character", charName);
    }

    // Go to previous pos in list and globally
    // save that selected character in PlayerPrefs
    private void PreviousCharacter() {
        Debug.Log("Previous");
        listOfCharacters[currCharIdx].gameObject.SetActive(false);
        if (currCharIdx == 0) {
            currCharIdx = listOfCharacters.Count - 1;
        } else {
            currCharIdx -= 1;
        }
        listOfCharacters[currCharIdx].gameObject.SetActive(true);
        charName = listOfCharacters[currCharIdx].gameObject.name;
        Debug.Log(charName);
        nameText.text = charName;
        PlayerPrefs.SetString(playerName + "_Character", charName);
    }

    // Go to next pos in list and globally
    // save that selected character in PlayerPrefs
    private void NextCharacter() {
        Debug.Log("Next");
        listOfCharacters[currCharIdx].gameObject.SetActive(false);
        if (currCharIdx >= listOfCharacters.Count - 1) {
            currCharIdx = 0;
        } else {
            currCharIdx += 1;
        }
        listOfCharacters[currCharIdx].gameObject.SetActive(true);
        charName = listOfCharacters[currCharIdx].gameObject.name;
        Debug.Log(charName);
        nameText.text = charName;
        PlayerPrefs.SetString(playerName + "_Character", charName);
    }
}
