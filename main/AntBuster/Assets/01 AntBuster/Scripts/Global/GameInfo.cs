using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    public static GameInfo istance;

    public static int level = 1;
    public static int exp = 1;
    public static int money = 300;
    public static int score = 0;
    public static int lostCake = 8;

    public static float antMaxHealth = 4;
    public static float antHealth = 4;
    public static float antSpeed = 0.5f;

    void Start()
    {
        level = 1;
        money = 300;
        score = 0;

    }

    private void Update()
    {
      
    }

    

}
