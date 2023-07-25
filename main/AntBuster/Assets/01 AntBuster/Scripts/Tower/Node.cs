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

    void OnMouseDown()  // 노드 클릭 시
    {

        Audio audio = FindObjectOfType<Audio>();
        audio.ClickSound();

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
    // 타워 건설
    void BuildTower(TowerBlueprint blueprint)
    {

        GameObject _tower;

        if (BuildManager.instance.isTower)  // 일반 타워일 떄
        {
            if (GameInfo.money < GameInfo.towerCost)
            {
                buildManager.DeselectTowerToBuild();
                return;
            }

            GameManager.instance.BuyTower(GameInfo.towerCost);

            // (GameObject) 캐스트해서 타워 생성
            _tower = (GameObject)Instantiate(blueprint.prefab,
                    GetBuildPosition(), Quaternion.identity);
            tower = _tower;

            TowerController towerComponent = tower.GetComponent<TowerController>(); // 생성된 타워 스탯 설정
            if (towerComponent != null)
            {
                towerComponent.bulletRate = 3f;
                towerComponent.bulletSpeed = 4;
                towerComponent.bulletDamage = 2;
            }
          
        }


        else if (!BuildManager.instance.isTower) // 마법 타워일 떄
        {
            if (GameInfo.money < GameInfo.magicTowerCost)
            {
                buildManager.DeselectTowerToBuild();
                return;
            }

            GameManager.instance.BuyMagicTower(GameInfo.magicTowerCost);

            // (GameObject) 캐스트해서 타워 생성
            _tower = (GameObject)Instantiate(blueprint.prefab,
                    GetBuildPosition(), Quaternion.identity);
            tower = _tower;

            TowerController towerComponent = tower.GetComponent<TowerController>(); // 생성된 타워 스탯 설정
            if (towerComponent != null)
            {
                towerComponent.bulletRate = 3f;
                towerComponent.bulletSpeed = 2; 
                towerComponent.bulletDamage = 8;
                towerComponent.isMagictower = true;
            }
           
        }

        

            towerBlueprint = blueprint;


            Debug.Log("구매 완료");

        Audio audio = FindObjectOfType<Audio>();
        audio.PaySound();
    }

    public void UpgradeTower()
    {
        if (GameInfo.money < GameInfo.towerUpgradeCost)
        {
            Debug.Log("돈이 부족합니다.");
            return;
        }

        GameManager.instance.UpgradeTower();

        // 이전 타워 제거
        Destroy(tower);

        // 업그레이드 타워 생성
        GameObject _tower = (GameObject)Instantiate(towerBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        tower = _tower;

        if (BuildManager.instance.isTower)  // 일반타워 업그레이드 스탯
        {
            TowerController towerComponent = tower.GetComponent<TowerController>(); // 생성된 타워 스탯 설정
            if (towerComponent != null)
            {
                towerComponent.bulletRate = 1.5f;
                towerComponent.bulletSpeed = 8;
                towerComponent.bulletDamage = 4;
                towerComponent.isUpgraded = true;

            }
           
        }
        else if (!BuildManager.instance.isTower)    // 마법타워 업그레이드 스탯
        {
            TowerController towerComponent = tower.GetComponent<TowerController>(); // 생성된 타워 스탯 설정
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

        Debug.Log("타워 업그레이드");
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
                buildOn.SetActive(true);    // 마우스가 노드 위에 있을 때 하이라이트 
            }
            else
                buildOff.SetActive(true);    // 마우스가 노드 위에 있을 때 하이라이트 
        }

        if (!BuildManager.instance.isTower)
        {
            if (GameInfo.money >= GameInfo.magicTowerCost && tower == null)
            {
                buildMagicOn.SetActive(true);    // 마우스가 노드 위에 있을 때 하이라이트 
            }
            else
                buildMagicOff.SetActive(true);    // 마우스가 노드 위에 있을 때 하이라이트 
        }
    }


    private void OnMouseExit()
    {
        buildOn.SetActive(false);  // 마우스가 노드 에서 나가면 복귀 
        buildOff.SetActive(false);  // 마우스가 노드 에서 나가면 복귀 
        buildMagicOn.SetActive(false);
        buildMagicOff.SetActive(false);
    }
}
