using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTower : MonoBehaviour
{
    public TowerBlueprint Tower;

    BuildManager buildManager;  

    
    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectStandardTurret()    // Ÿ�� ��ư�� ������ Ÿ�� ���� ���� ȣ�� 
    {
        Debug.Log("Ÿ�� ����");
        buildManager.SelectTowerToBuild(Tower); // Ÿ�� ������ ����
    }




}
