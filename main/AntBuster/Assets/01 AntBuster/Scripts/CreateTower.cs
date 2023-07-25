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

    public void SelectStandardTower()    // 타워 버튼을 누르면 타워 생성 세팅 호출 
    {
        BuildManager.instance.isTower = true;
        buildManager.SelectTowerToBuild(Tower); // 타워 프리팹 전달
    }


    public void SelectMagicTower()    // 타워 버튼을 누르면 타워 생성 세팅 호출 
    {
        BuildManager.instance.isTower = false;
        buildManager.SelectTowerToBuild(magicTower); // 타워 프리팹 전달
    }

}
