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
        // �� �����Ӹ��� ���콺�� �ö� �ִ��� Ȯ���ϰ� ����
        if (isMouseOver)
        {
            selectFloor.SetActive(true);
            Debug.Log("���콺 �ö󰣴�");
        }
    }
    private void OnMouseDown()
    {
        if (tower != null)
        {
            Debug.Log("���⿡ ���� �� �����ϴ�!");
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
