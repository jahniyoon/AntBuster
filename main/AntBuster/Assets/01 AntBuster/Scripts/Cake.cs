using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cake : MonoBehaviour
{
    
    // �����̴� Ư�� ������Ʈ�� ������ ����
    public GameObject movingObject;

    void Start()
    {
    

        // �����̴� Ư�� ������Ʈ�� ����ũ ������Ʈ�� �ڽ����� ����
        movingObject.transform.SetParent(transform);
    }

    void Update()
    {
        // ����ũ ������Ʈ�� �ִϸ��̼��� ���󰡵��� �����̴� Ư�� ������Ʈ�� ��ġ�� ȸ������ ����
        movingObject.transform.localPosition = Vector3.zero;
        movingObject.transform.localRotation = Quaternion.identity;

        // ����ũ ������Ʈ�� �ִϸ��̼��� �����ϴ� �ڵ� �߰�
    }

}
