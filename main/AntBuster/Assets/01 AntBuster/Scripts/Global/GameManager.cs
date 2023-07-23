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

    public GameObject gameoverUI;

    // �ʱ�ȭ
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
            GlobalFunc.LogWarning("���� �� �� �̻��� ���� �Ŵ����� �����մϴ�.");
        }
    }   // } Awake


    void Start()
    {
        Initialization();   // �ʱ�ȭ

        moneyText.text = string.Format("MONEY : {0}", GameInfo.money);
        levelText.text = string.Format("LEVEL : {0}", GameInfo.level);
        scoreText.text = string.Format("SCORE : {0}", GameInfo.score);

    }    // } Start()


    void Update()
    {
       
        if (isGameOver == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Initialization();   // �ʱ�ȭ
                GlobalFunc.LoadScene("PlayScene");
            }
        }
    }

    // ������ �߰��Ѵ�.
    public void AddScore(int newScore)
    {
        if (isGameOver == false)
        {
            GameInfo.score += newScore;
            scoreText.text = string.Format("SCORE : {0}", GameInfo.score);
        }
    }   // } AddScore

    // ������ �߰��Ѵ�.
    public void AddMoney(int getMoney)
    {
        if (isGameOver == false)
        {
            GameInfo.money += getMoney;
            moneyText.text = string.Format("MONEY : {0}", GameInfo.money);
        }
    }   // } AddScore

    public void OnGameOver()
    {
        isGameOver = true;
        gameoverUI.SetActive(true);

    }

}
