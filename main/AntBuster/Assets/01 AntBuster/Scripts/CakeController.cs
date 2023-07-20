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

    private void OnEnabel()
    {
        for (int i = 0; i < cakes.Length; i++)
        {
            cakes[i].SetActive(true);
        }
    }
    // 개미가 케이크를 가져갈 때마다 케이크 비활성화
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

    void Update()
    {


    }
}
