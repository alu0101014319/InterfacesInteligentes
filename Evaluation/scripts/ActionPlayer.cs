using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class ActionPlayer : MonoBehaviour
{
    public static float power = 2;
    public static float money = 1;
    public Text texto;
    public Text texto2;
    void Start()
    {

    }

    void Update()
    {
        SetPower();
    }
    public void Comprar()
    {
        if (money > 0)
        {
            power = power + 1;
            money = money - 1;
        }
    }

    void SetPower()
    {
        texto.text = "Power: " + power.ToString();
        texto2.text = "Money: " + money.ToString();
    }
}
