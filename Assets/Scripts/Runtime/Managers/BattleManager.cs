using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour 
{
    public GameObject enemyTower;
    public GameObject duckTower;

    public int maxLife;
    public int currentPlayerLife;

    private void Awake()
    {
        enemyTower.GetComponent<Health>().onDeath.AddListener(DuckBattle);
        duckTower.GetComponent<Health>().onDeath.AddListener(EnemyBattle);
    }

    public void DuckBattle()
    {
        FindObjectOfType<ProgressManager>().CompleteLevel();
        SceneManager.LoadScene("02-MainScreen");
    }

    public void EnemyBattle()
    {
        SceneManager.LoadScene("02-MainScreen");
    }
}
