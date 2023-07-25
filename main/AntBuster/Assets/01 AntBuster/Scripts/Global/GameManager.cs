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

    public TMP_Text costText;
    public TMP_Text magicCostText;


    [Header("Debug instance")]
    public TMP_Text lostCakeText;
    public TMP_Text levelExp;

    public GameObject gameoverUI;

    // 초기화
    public void Initialization()
    {
        isGameOver = false;
        GameInfo.level = 1;
        GameInfo.score = 0;
        GameInfo.exp = 1;
        GameInfo.money = 1000;
        GameInfo.lostCake = 8;
        GameInfo.towerCost = 30;
        GameInfo.magicTowerCost = 300;

        GameInfo.antMaxHealth = 4;
        GameInfo.antHealth = 4;
        GameInfo.antSpeed = 0.5f;
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

        moneyText.text = string.Format("GOLD : {0}", GameInfo.money);
        levelText.text = string.Format("LEVEL : {0}", GameInfo.level);
        scoreText.text = string.Format("SCORE : {0}", GameInfo.score);

        lostCakeText.text = string.Format("LOST CAKE : {0}", GameInfo.lostCake);
        levelExp.text = string.Format("LEVEL : {0} ( {1} / {2} )", GameInfo.level, GameInfo.score, GameInfo.exp);
        costText.text = string.Format("$ {0}", GameInfo.towerCost);
        magicCostText.text = string.Format("$ {0}", GameInfo.magicTowerCost);

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
            moneyText.text = string.Format("GOLD : {0}", GameInfo.money);
        }
    }   // } AddScore

    public void BuyTower(int cost)
    {
        if (!isGameOver)
        {
            GameInfo.money -= cost;
            GameInfo.towerCost = GameInfo.towerCost + (GameInfo.towerCost / 2);
            moneyText.text = string.Format("GOLD : {0}", GameInfo.money);

            costText.text = string.Format("$ {0}", GameInfo.towerCost);
        }
    }
   

    public void BuyMagicTower(int cost)
    {
        if (!isGameOver)
        {
            GameInfo.money -= cost;
            GameInfo.magicTowerCost = GameInfo.magicTowerCost + (GameInfo.magicTowerCost / 2);
            moneyText.text = string.Format("GOLD : {0}", GameInfo.money);

            magicCostText.text = string.Format("$ {0}", GameInfo.magicTowerCost);
        }
    }
    public void UpgradeTower()
    {
        GameInfo.money -= GameInfo.towerUpgradeCost;
        moneyText.text = string.Format("GOLD : {0}", GameInfo.money);
    }

    public void OnGameOver()
    {
        isGameOver = true;
        gameoverUI.SetActive(true);

        Audio audio = FindObjectOfType<Audio>();
        audio.RetrySound();

    }

    // 레벨 밸런스
    public void LevelUp()
    {
        GameInfo.level ++;  // 레벨업
        GameInfo.exp = GameInfo.exp + GameInfo.level * 2;  // 필요 경험치
        levelText.text = string.Format("LEVEL : {0}", GameInfo.level);

        GameInfo.antMaxHealth = 4 + Mathf.Floor(GameInfo.level * 0.5f);
        
        GameInfo.antHealth = GameInfo.antMaxHealth;

        // 개미 속도 업
        if (GameInfo.level % 3 == 0)
        {
            GameInfo.antSpeed += 0.1f;
        }
        // 필요 경험치
        levelExp.text = string.Format("LEVEL : {0} ( {1} / {2} )", GameInfo.level, GameInfo.score, GameInfo.exp);

    }

}
