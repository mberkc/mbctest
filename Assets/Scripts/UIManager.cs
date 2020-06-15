using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public static UIManager Instance;
    void Awake () {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad (this);
        } else if (Instance != this)
            Destroy (gameObject);
    }
}