using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private void Awake()
    {
        instance = this;
        //https://www.youtube.com/watch?v=t7GuWvP_IEQ&list=PLPV2KyIb3jR4u5jX8za5iU1cqnQPmbzG0&index=6
    }

    private GameObject towerToBuild;

    public GameObject GetTowerBuild ()
    {
        return towerToBuild;
    }


}
