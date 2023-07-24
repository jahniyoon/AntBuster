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

    public void SelectStandardTurret()    // 타워 버튼을 누르면 타워 생성 세팅 호출 
    {
        Debug.Log("타워 생성");
        buildManager.SelectTowerToBuild(Tower); // 타워 프리팹 전달
    }




}
