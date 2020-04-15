using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManagement : MonoBehaviour
{
    public GameObject prefab;
    public DuckType melee;
    public DuckType range;
    public DuckType tank;

    public Vector2 spawnPoint;

    public int moneyPerSecond;
    public int currentMoney;
    public float delayAmount = 2; // Seconds counted
    Text currentMoneyText;

    float Timer;

    void Start()
    {
        currentMoneyText = GetComponent<Text>();
    }

    public void Update()
    {
        Timer += Time.deltaTime;

        if (Timer >= delayAmount)
        {
            Timer = 0f;
            currentMoney += moneyPerSecond; 
        }
        currentMoneyText.text = "muniey " + currentMoney;
    }

    public void BuyMelee()
    {
        Debug.Log("Melee Bought");

        if (currentMoney >= 100)
        {
            currentMoney -= 100;
            prefab.GetComponent<UnitController>().type = melee;

            GameObject temp = Instantiate(prefab, spawnPoint, Quaternion.identity);

        }
        
    }
    public void BuyRange()
    {
        Debug.Log("Range Bought");
        if (currentMoney >= 200)
        {
            currentMoney -= 200;
            prefab.GetComponent<UnitController>().type = range;

            GameObject temp = Instantiate(prefab, spawnPoint, Quaternion.identity);
        }
    }
    public void BuyTank()
    {
        Debug.Log("Tank Bought");
        if (currentMoney >= 300)
        {
            currentMoney -= 300;
            prefab.GetComponent<UnitController>().type = tank;

            GameObject temp = Instantiate(prefab, spawnPoint, Quaternion.identity);
        }
    }
}
