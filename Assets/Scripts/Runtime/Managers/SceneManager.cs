using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DuckBoot
{
    public class SceneManager : Singleton<SceneManager>
    {
        //portable FSM soon to be implemented
        public enum SceneState { Launch, Menu, Game, Combat, Fashion, Credits, Reset }

        //begin at launch
        SceneState m_currState = SceneState.Launch;

        private void Awake()
        {
            
        }
    }
}

