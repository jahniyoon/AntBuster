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

    void BuildTower(TowerBlueprint blueprint)
    {
        if (GameInfo.money < GameInfo.towerCost)
        {
            buildManager.DeselectTowerToBuild();
            return;
        }

        GameManager.instance.BuyTower(GameInfo.towerCost);

        // (GameObject) ĳ��Ʈ�ؼ� Ÿ�� ����
        GameObject _tower = (GameObject)Instantiate(blueprint.prefab,
            GetBuildPosition(), Quaternion.identity);
        tower = _tower;

        towerBlueprint = blueprint;

        Debug.Log("���� �Ϸ�");

    }

    public void UpgradeTurret()
    {
        if (GameInfo.money < GameInfo.towerUpgradeCost)
        {
            Debug.Log("���� �����մϴ�.");
            return;
        }

        GameManager.instance.UpgradeTower();

        //Get rid of the old turret
        Destroy(tower);

        //Build a new one
        GameObject _tower = (GameObject)Instantiate(towerBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        tower = _tower;

        isUpgraded = true;

        Debug.Log("Ÿ�� ���׷��̵�");
    }
    public void SellTurret()
    {
        GameManager.instance.AddMoney(GameInfo.towerCost / 2);

        Destroy(tower);
        towerBlueprint = null;
    }


    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;

        if (GameInfo.money > GameInfo.towerCost)
        {
            buildOn.SetActive(true);    // ���콺�� ��� ���� ���� �� ���̶���Ʈ 
        }
        else
        buildOff.SetActive(true);    // ���콺�� ��� ���� ���� �� ���̶���Ʈ 

    }


    private void OnMouseExit()
    {
        buildOn.SetActive(false);  // ���콺�� ��� ���� ������ ���� 
        buildOff.SetActive(false);  // ���콺�� ��� ���� ������ ���� 
    }
}
