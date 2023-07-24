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
            Debug.LogError("���� �� �� �̻��� ���� �Ŵ����� �����մϴ�.");
            return;
        }
        instance = this;
    }

    private TowerBlueprint towerToBuild; // �������Ʈ �� ��������
    private Node selectedNode;
    
    public NodeUI nodeUI;

    public bool CanBuild { get { return towerToBuild != null; } } // �Ǽ� �������� üũ

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

    //    // (GameObject) ĳ��Ʈ�ؼ� Ÿ�� ����
    //    GameObject tower = (GameObject)Instantiate(towerToBuild.prefab, 
    //        node.GetBuildPosition(), Quaternion.identity);
    //    node.tower = tower;

    //Debug.Log("���� �Ϸ�");
    //}

}
