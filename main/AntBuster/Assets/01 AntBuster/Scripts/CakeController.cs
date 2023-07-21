using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeController : MonoBehaviour
{
    private Rigidbody cakeRigid;

    public GameObject[] cakes;

    public int leftCake = 8;

    void Start()
    {
        cakeRigid = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        for (int i = 0; i < cakes.Length; i++)
        {
            cakes[i].SetActive(true);
        }
    }
    // ���̰� ����ũ�� ������ ������ ����ũ ��Ȱ��ȭ
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Ant"))
        {
            if (leftCake > 0)
            {
                cakes[leftCake - 1].SetActive(false);
                leftCake--;
            }
        }
    }

    public void CakeBack()
    {
        Debug.Log("����ũ ��ȯ");
        cakes[leftCake].SetActive(true);
        leftCake++;

    }
}
