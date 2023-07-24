using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    public GameObject selectFloor;
    public GameObject tower;
    //public BuildManager;

    private bool isMouseOver = false;

    private void Start()
    {
    }
    private void Update()
    {
        // 매 프레임마다 마우스가 올라가 있는지 확인하고 실행
        if (isMouseOver)
        {
            selectFloor.SetActive(true);
            Debug.Log("마우스 올라간다");
        }
    }
    private void OnMouseDown()
    {
        if (tower != null)
        {
            Debug.Log("여기에 지을 수 없습니다!");
            return;
        }

        // build a tower

    }
    private void OnMouseEnter()
    {
        isMouseOver = true;
    }



    private void OnMouseExit()
    {
        isMouseOver = false;
        selectFloor.SetActive(false);
    }
}
