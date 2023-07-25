using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = default;
    public float damage = default;
    private Rigidbody rigid = default;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.velocity = transform.forward * speed;

        Destroy(gameObject, 3.0f);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Ant"))
        {
            //Debug.Log("�Ѿ��� ���̿� �ε��ƴ�.");

            AntController antController = other.GetComponent<AntController>();

            if (antController != null)
            {
                //Debug.Log("���̿��� ������ :"+ damage);
                antController.antHealth -= damage;
                antController.Hit(damage);
            }
            Destroy(gameObject);

        }

    }

}
