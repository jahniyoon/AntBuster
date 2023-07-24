using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isGameOver = false;

    public TMP_Text moneyText;
    public TMP_Text levelText;
    public TMP_Text scoreText;

    [Header("Debug instance")]
    public TMP_Text lostCakeText;
    public TMP_Text levelExp;
    public TMP_Text costText;

    public GameObject gameoverUI;

    // 초기화
    public void Initialization()
    {
        isGameOver = false;
        GameInfo.score = 0;
        GameInfo.money = 300;
        GameInfo.level = 1;
    }   // } Initialization

    private void Awake()
    {
        if (instance == null)
        { instance = this; }

        else
        {
            GlobalFunc.LogWarning("씬에 두 개 이상의 게임 매니저가 존재합니다.");
        }
    }   // } Awake


    void Start()
    {
        Initialization();   // 초기화

        moneyText.text = string.Format("MONEY : {0}", GameInfo.money);
        levelText.text = string.Format("LEVEL : {0}", GameInfo.level);
        scoreText.text = string.Format("SCORE : {0}", GameInfo.score);

        lostCakeText.text = string.Format("LOST CAKE : {0}", GameInfo.lostCake);
        levelExp.text = string.Format("LEVEL : {0} ( {1} / {2} )", GameInfo.level, GameInfo.score, GameInfo.exp);
        costText.text = string.Format("TOWER COST : {0}", GameInfo.towerCost);

    }    // } Start()


    void Update()
    {
       
        if (isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Initialization();   // 초기화
                GlobalFunc.LoadScene("PlayScene");
            }
        }
    }
    public void LostCake()
    {
        GameInfo.lostCake -= 1;
        lostCakeText.text = string.Format("LOST CAKE : {0}", GameInfo.lostCake);
    }

    // 점수를 추가한다.
    public void AddScore(int newScore)
    {
        if (!isGameOver)
        {
            GameInfo.score += newScore;
            scoreText.text = string.Format("SCORE : {0}", GameInfo.score);
            levelExp.text = string.Format("LEVEL : {0} ( {1} / {2} )", GameInfo.level, GameInfo.score, GameInfo.exp);

        }
    }   // } AddScore

    // 돈을 추가한다.
    public void AddMoney(int getMoney)
    {
        if (!isGameOver)
        {
            GameInfo.money += getMoney;
            moneyText.text = string.Format("MONEY : {0}", GameInfo.money);
        }
    }   // } AddScore

    public void BuyTower(int cost)
    {
        if (!isGameOver)
        {
            GameInfo.money -= cost;
            GameInfo.towerCost = GameInfo.towerCost + (GameInfo.towerCost / 2);
            moneyText.text = string.Format("MONEY : {0}", GameInfo.money);

            costText.text = string.Format("TOWER COST : {0}", GameInfo.towerCost);
        }
    }
    public void UpgradeTower()
    {
        GameInfo.money -= GameInfo.towerUpgradeCost;
        moneyText.text = string.Format("MONEY : {0}", GameInfo.money);
    }

    public void OnGameOver()
    {
        isGameOver = true;
        gameoverUI.SetActive(true);

    }

    // 레벨 밸런스
    public void LevelUp()
    {
        GameInfo.level ++;
        GameInfo.exp *= 2;
        levelText.text = string.Format("LEVEL : {0}", GameInfo.level);

        GameInfo.antMaxHealth = 4 + Mathf.Floor(GameInfo.level * 0.4f);
        
        GameInfo.antHealth = GameInfo.antMaxHealth;

        if (GameInfo.level % 10 == 0)
        {
            GameInfo.antSpeed += 0.1f;
        }
        levelExp.text = string.Format("LEVEL : {0} ( {1} / {2} )", GameInfo.level, GameInfo.score, GameInfo.exp);

    }

}
