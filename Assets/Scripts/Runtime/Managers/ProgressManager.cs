using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    public int levelProgress = 1;

    private void Awake()
    {
        levelProgress = 1;
        DontDestroyOnLoad(gameObject);
    }

    public void CompleteLevel()
    {
        levelProgress++;
    }

    public void ResetLevels()
    {
        levelProgress = 1;
    }
}
