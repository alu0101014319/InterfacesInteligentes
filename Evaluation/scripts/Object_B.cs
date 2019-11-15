using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class Object_B : MonoBehaviour
{
    public float pow = 0;
    public float mny = 0;
    private bool check = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        pow = ActionPlayer.power;
        mny = ActionPlayer.money;
    }

    void OnEnable()
    {
        EventManager.CollisionedB += IncreaseM;
        EventManager.CollisionedA += Scale;
        EventManager.CollisionedB += Change_color;
        EventManager.CollisionedA += DecreaseP;
    }

    void OnDisable()
    {
        EventManager.CollisionedB -= IncreaseM;
        EventManager.CollisionedA -= Scale;
        EventManager.CollisionedB -= Change_color;
        EventManager.CollisionedA -= DecreaseP;
    }

    public void IncreaseM()
    {
        mny = mny + 1;
        ActionPlayer.money = mny;
    }

    public void DecreaseP()
    {
        if (pow > 0)
        {
            pow = pow - 1;
            ActionPlayer.power = pow;
        }
    }

    void Scale()
    {
        Vector3 escala = new Vector3(pow, pow, pow);
        transform.localScale -= escala;
    }

    void Change_color()
    {
        if (check == false)
        {
            GetComponent<Renderer>().material.color = Color.black;
            check = true;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.red;
            check = false;
        }
    }
}
