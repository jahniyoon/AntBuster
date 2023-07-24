using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("씬에 두 개 이상의 빌드 매니저가 존재합니다.");
            return;
        }
        instance = this;
    }

    private TowerBlueprint towerToBuild; // 블루프린트 값 가져오기
    private Node selectedNode;
    
    public NodeUI nodeUI;

    public bool CanBuild { get { return towerToBuild != null; } } // 건설 가능한지 체크

   public void SelectNode (Node node)
    {
        if(selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        towerToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public void SelectTowerToBuild (TowerBlueprint tower)
    {
        towerToBuild = tower;
        DeselectNode();
    }
    public void DeselectTowerToBuild()
    {
        towerToBuild = null;
        DeselectNode();
    }

    public TowerBlueprint GetTowerBuild()
    {
        return towerToBuild;
    }

    //public void BuildTowerOn(Node node)
    //{
    //    if(GameInfo.money < GameInfo.towerCost)
    //    {
    //        return;
    //    }

    //    GameManager.instance.BuyTower(GameInfo.towerCost);

    //    // (GameObject) 캐스트해서 타워 생성
    //    GameObject tower = (GameObject)Instantiate(towerToBuild.prefab, 
    //        node.GetBuildPosition(), Quaternion.identity);
    //    node.tower = tower;

    //Debug.Log("구매 완료");
    //}

}
