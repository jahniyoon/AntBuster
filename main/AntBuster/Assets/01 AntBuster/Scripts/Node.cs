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

    void OnMouseDown()  // 노드 클릭 시
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;     // UI 오브젝트가 있을 때 다른 동작 없이 해당 이벤트 전달
        }

        if (tower != null)  // 비어있지 않으면 건설 불가
        {
            Debug.Log("여기에 타워가 있나?");
            buildManager.SelectNode(this);  // 타워가 있으면 선택
            return;
        }

        if (!buildManager.CanBuild) // 타워 프리팹이 null인 경우 아무것도 안함
        {
            buildManager.DeselectNode();
            buildManager.DeselectTowerToBuild();
            return;

        }


        BuildTower(buildManager.GetTowerBuild());

        //buildManager.BuildTowerOn(this);

        //// 타워 건설
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

        // (GameObject) 캐스트해서 타워 생성
        GameObject _tower = (GameObject)Instantiate(blueprint.prefab,
            GetBuildPosition(), Quaternion.identity);
        tower = _tower;

        towerBlueprint = blueprint;

        Debug.Log("구매 완료");

    }

    public void UpgradeTurret()
    {
        if (GameInfo.money < GameInfo.towerUpgradeCost)
        {
            Debug.Log("돈이 부족합니다.");
            return;
        }

        GameManager.instance.UpgradeTower();

        //Get rid of the old turret
        Destroy(tower);

        //Build a new one
        GameObject _tower = (GameObject)Instantiate(towerBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        tower = _tower;

        isUpgraded = true;

        Debug.Log("타워 업그레이드");
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
            buildOn.SetActive(true);    // 마우스가 노드 위에 있을 때 하이라이트 
        }
        else
        buildOff.SetActive(true);    // 마우스가 노드 위에 있을 때 하이라이트 

    }


    private void OnMouseExit()
    {
        buildOn.SetActive(false);  // 마우스가 노드 에서 나가면 복귀 
        buildOff.SetActive(false);  // 마우스가 노드 에서 나가면 복귀 
    }
}
