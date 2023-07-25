using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    BuildManager buildManager;

    public GameObject buildOn;
    public GameObject buildOff;
    public GameObject buildMagicOn;
    public GameObject buildMagicOff;
    public Vector3 positionOffset;


    [Header("Optional")]
    public GameObject tower;
    public TowerBlueprint towerBlueprint;
    public bool isUpgraded = false;



    private void Start()
    {
        buildManager = BuildManager.instance;
    }
  
    public Vector3 GetBuildPosition ()
    {
        return transform.position + positionOffset;
    }

    void OnMouseDown()  // ��� Ŭ�� ��
    {

        Audio audio = FindObjectOfType<Audio>();
        audio.ClickSound();

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;     // UI ������Ʈ�� ���� �� �ٸ� ���� ���� �ش� �̺�Ʈ ����
        }

        if (tower != null)  // ������� ������ �Ǽ� �Ұ�
        {
            Debug.Log("���⿡ Ÿ���� �ֳ�?");
            buildManager.SelectNode(this);  // Ÿ���� ������ ����
            return;
        }

        if (!buildManager.CanBuild) // Ÿ�� �������� null�� ��� �ƹ��͵� ����
        {
            buildManager.DeselectNode();
            buildManager.DeselectTowerToBuild();
            return;

        }


        BuildTower(buildManager.GetTowerBuild());

        //buildManager.BuildTowerOn(this);

        //// Ÿ�� �Ǽ�
        //GameObject towerToBuild = BuildManager.instance.GetTowerToBuild();
        //tower = (GameObject)Instantiate(towerToBuild, transform.position, transform.rotation);

    }
    // Ÿ�� �Ǽ�
    void BuildTower(TowerBlueprint blueprint)
    {

        GameObject _tower;

        if (BuildManager.instance.isTower)  // �Ϲ� Ÿ���� ��
        {
            if (GameInfo.money < GameInfo.towerCost)
            {
                buildManager.DeselectTowerToBuild();
                return;
            }

            GameManager.instance.BuyTower(GameInfo.towerCost);

            // (GameObject) ĳ��Ʈ�ؼ� Ÿ�� ����
            _tower = (GameObject)Instantiate(blueprint.prefab,
                    GetBuildPosition(), Quaternion.identity);
            tower = _tower;

            TowerController towerComponent = tower.GetComponent<TowerController>(); // ������ Ÿ�� ���� ����
            if (towerComponent != null)
            {
                towerComponent.bulletRate = 3f;
                towerComponent.bulletSpeed = 4;
                towerComponent.bulletDamage = 2;
            }
          
        }


        else if (!BuildManager.instance.isTower) // ���� Ÿ���� ��
        {
            if (GameInfo.money < GameInfo.magicTowerCost)
            {
                buildManager.DeselectTowerToBuild();
                return;
            }

            GameManager.instance.BuyMagicTower(GameInfo.magicTowerCost);

            // (GameObject) ĳ��Ʈ�ؼ� Ÿ�� ����
            _tower = (GameObject)Instantiate(blueprint.prefab,
                    GetBuildPosition(), Quaternion.identity);
            tower = _tower;

            TowerController towerComponent = tower.GetComponent<TowerController>(); // ������ Ÿ�� ���� ����
            if (towerComponent != null)
            {
                towerComponent.bulletRate = 3f;
                towerComponent.bulletSpeed = 2; 
                towerComponent.bulletDamage = 8;
                towerComponent.isMagictower = true;
            }
           
        }

        

            towerBlueprint = blueprint;


            Debug.Log("���� �Ϸ�");

        Audio audio = FindObjectOfType<Audio>();
        audio.PaySound();
    }

    public void UpgradeTower()
    {
        if (GameInfo.money < GameInfo.towerUpgradeCost)
        {
            Debug.Log("���� �����մϴ�.");
            return;
        }

        GameManager.instance.UpgradeTower();

        // ���� Ÿ�� ����
        Destroy(tower);

        // ���׷��̵� Ÿ�� ����
        GameObject _tower = (GameObject)Instantiate(towerBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        tower = _tower;

        if (BuildManager.instance.isTower)  // �Ϲ�Ÿ�� ���׷��̵� ����
        {
            TowerController towerComponent = tower.GetComponent<TowerController>(); // ������ Ÿ�� ���� ����
            if (towerComponent != null)
            {
                towerComponent.bulletRate = 1.5f;
                towerComponent.bulletSpeed = 8;
                towerComponent.bulletDamage = 4;
                towerComponent.isUpgraded = true;

            }
           
        }
        else if (!BuildManager.instance.isTower)    // ����Ÿ�� ���׷��̵� ����
        {
            TowerController towerComponent = tower.GetComponent<TowerController>(); // ������ Ÿ�� ���� ����
            if (towerComponent != null)
            {
                towerComponent.bulletRate = 1.5f;
                towerComponent.bulletSpeed = 4;
                towerComponent.bulletDamage = 12;
                towerComponent.isUpgraded = true;

            }
           
        }

        isUpgraded = true;

        Audio audio = FindObjectOfType<Audio>();
        audio.PaySound();

        Debug.Log("Ÿ�� ���׷��̵�");
    }
    public void SellTower()
    {
        GameManager.instance.AddMoney(GameInfo.towerCost / 2);

        Destroy(tower);
        towerBlueprint = null;

        Audio audio = FindObjectOfType<Audio>();
        audio.SellSound();
    }


    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;

        if (BuildManager.instance.isTower)
        {
            if (GameInfo.money >= GameInfo.towerCost && tower == null)
            {
                buildOn.SetActive(true);    // ���콺�� ��� ���� ���� �� ���̶���Ʈ 
            }
            else
                buildOff.SetActive(true);    // ���콺�� ��� ���� ���� �� ���̶���Ʈ 
        }

        if (!BuildManager.instance.isTower)
        {
            if (GameInfo.money >= GameInfo.magicTowerCost && tower == null)
            {
                buildMagicOn.SetActive(true);    // ���콺�� ��� ���� ���� �� ���̶���Ʈ 
            }
            else
                buildMagicOff.SetActive(true);    // ���콺�� ��� ���� ���� �� ���̶���Ʈ 
        }
    }


    private void OnMouseExit()
    {
        buildOn.SetActive(false);  // ���콺�� ��� ���� ������ ���� 
        buildOff.SetActive(false);  // ���콺�� ��� ���� ������ ���� 
        buildMagicOn.SetActive(false);
        buildMagicOff.SetActive(false);
    }
}
