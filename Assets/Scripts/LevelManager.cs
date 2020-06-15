using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Write a level manager that loads the created level objects and initiates the level
// Make the system testable in the scene LevelTest

public class LevelManager : MonoBehaviour {
    public static LevelManager Instance;

    public LevelObject[] levels;
    public int level, highestLevel;

    public Transform levelElements;
    LevelObject currentLevelScriptableObject;
    WinCondition winCondition;
    GameObject player;
    Camera cam;

    Vector3 spawnPos = Vector3.zero;

    void Awake () {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad (this);
        } else if (Instance != this)
            Destroy (gameObject);

        cam = Camera.main;
        level = 0;
        LoadLevel ();
    }

    void LoadLevel () {
        currentLevelScriptableObject = levels[level];
        cam.backgroundColor = currentLevelScriptableObject.cameraBgColor;
        GameObject player = Instantiate (currentLevelScriptableObject.levelPrefab, spawnPos, Quaternion.identity);
        player.transform.parent = levelElements.transform;
        winCondition = currentLevelScriptableObject.winCondition;
    }

    public void CheckWinCondition () {
        if (winCondition == WinCondition.ClearAllArea)
            ClearLevelElements ();
        else if (winCondition == WinCondition.ClearSpecificArea)
            ClearSpecificLevelElements ();
        AdvanceLevel ();
    }

    void AdvanceLevel () {
        if (level < highestLevel) {
            level++;
            LoadLevel ();
        } else
            print ("Last Level Completed!");
    }

    void ClearLevelElements () {
        foreach (Transform child in levelElements) {
            GameObject.Destroy (child.gameObject);
        }
    }

    void ClearSpecificLevelElements () {
        foreach (Transform child in levelElements) {
            GameObject.Destroy (child.gameObject);
        }
    }

}