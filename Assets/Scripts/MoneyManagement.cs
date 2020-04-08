using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManagement : MonoBehaviour
{

    public int moneyPerSecond;
    public int currentMoney;
    public int delayAmount = 2; // Seconds counted
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
        }
        
    }
    public void BuyRange()
    {
        Debug.Log("Range Bought");
        if (currentMoney >= 200)
        {
            currentMoney -= 200;
        }
    }
    public void BuyTank()
    {
        Debug.Log("Tank Bought");
        if (currentMoney >= 300)
        {
            currentMoney -= 300;
        }
    }
}
