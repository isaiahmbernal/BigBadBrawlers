using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_MapSelection : MonoBehaviour
{
    public Button prevMap;
    public Button nextMap;
    public Transform mapsTransform;
    public List<Transform> listOfMaps;
    public int currMapIdx;
    public string mapName;
    public TMP_Text nameText;

    private void Start() {
        currMapIdx = 0;

        prevMap.onClick.AddListener(PreviousCharacter);
        nextMap.onClick.AddListener(NextCharacter);
        
        mapsTransform = transform.Find("Maps");
        foreach (Transform child in mapsTransform) {
            listOfMaps.Add(child);
        }

        nameText = transform.Find("Name").GetComponent<TMP_Text>();

        listOfMaps[currMapIdx].gameObject.SetActive(true);
        mapName = listOfMaps[currMapIdx].gameObject.name;
        nameText.text = mapName;
        PlayerPrefs.SetString("Map", mapName);
    }

    private void PreviousCharacter() {
        Debug.Log("Previous");
        listOfMaps[currMapIdx].gameObject.SetActive(false);
        if (currMapIdx == 0) {
            currMapIdx = listOfMaps.Count - 1;
        } else {
            currMapIdx -= 1;
        }
        listOfMaps[currMapIdx].gameObject.SetActive(true);
        mapName = listOfMaps[currMapIdx].gameObject.name;
        nameText.text = mapName;
        PlayerPrefs.SetString("Map", mapName);
    }

    private void NextCharacter() {
        Debug.Log("Next");
        listOfMaps[currMapIdx].gameObject.SetActive(false);
        if (currMapIdx >= listOfMaps.Count - 1) {
            currMapIdx = 0;
        } else {
            currMapIdx += 1;
        }
        listOfMaps[currMapIdx].gameObject.SetActive(true);
        mapName = listOfMaps[currMapIdx].gameObject.name;
        nameText.text = mapName;
        PlayerPrefs.SetString("Map", mapName);
    }
}
