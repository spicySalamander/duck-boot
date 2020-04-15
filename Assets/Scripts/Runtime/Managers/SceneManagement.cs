using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManagement : MonoBehaviour
{
    //portable FSM soon to be implemented
    public enum SceneState { Launch, Menu, Game, Combat, Fashion, Credits, Reset }

    //begin at launch
    SceneState m_currState = SceneState.Launch;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene(1);
            DontDestroyOnLoad(this);
        }
        else
        {
            Debug.LogError("Scene manager not instantiated at launch scene.");
        }
    }
}


