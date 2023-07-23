using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    public static GameInfo istance;

    public static int level = 1;
    public static int money = 300;
    public static int score = 0;

    void Start()
    {
        level = 1;
        money = 300;
        score = 0;

    }
}
