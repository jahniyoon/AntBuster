using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTower : MonoBehaviour
{
    public TowerBlueprint Tower;
    public TowerBlueprint magicTower;

    BuildManager buildManager;  

    
    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectStandardTower()    // Ÿ�� ��ư�� ������ Ÿ�� ���� ���� ȣ�� 
    {
        BuildManager.instance.isTower = true;
        buildManager.SelectTowerToBuild(Tower); // Ÿ�� ������ ����
    }


    public void SelectMagicTower()    // Ÿ�� ��ư�� ������ Ÿ�� ���� ���� ȣ�� 
    {
        BuildManager.instance.isTower = false;
        buildManager.SelectTowerToBuild(magicTower); // Ÿ�� ������ ����
    }

}
