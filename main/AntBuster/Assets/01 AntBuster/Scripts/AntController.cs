using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class AntController : MonoBehaviour
{
    
    private Transform targetPos = default; // ��ǥ�� ������
    private Transform cakePos = default; // ����ũ�� ������
    private Transform spawnerPos = default; // �������� ������
    private bool getCake = false;

    private Vector3 moveDirection = Vector3.forward; // �ʱ� �̵� ���� ����
    private Animator animator = default;


    [Header("Ant Status")]  // �ν����� â�� ���� ��� �̸�
    public int antLevel = 1;
    public float antHealth = 4;
    public float antSpeed = 0.5f;


    void Start()
    {
        //CakeController�� transform�� ��������
        cakePos = FindObjectOfType<CakeController>().transform;
        spawnerPos = FindObjectOfType<SpawnerController>().transform;

        animator = GetComponent<Animator>();


    }

    void Update()
    {
        if (!getCake) {
            //Debug.Log("����ũ�� ����");
            targetPos = cakePos; }
        else {
            //Debug.Log("�����ʷ� ����");
            targetPos = spawnerPos;  }

        // ���̰� Ÿ�� �Ѿư��� ��ũ��Ʈ
        if (targetPos != null)
        {
            // ����ũ�� ���� ����
            Vector3 targetPosition = new Vector3(targetPos.position.x, transform.position.y, targetPos.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, antSpeed * Time.deltaTime);
        
            // ������ �̵� ���� ���
            moveDirection = targetPos.position - transform.position;
            moveDirection.y = 0f; // ���̰� ���� �������� ȸ������ �ʵ��� y���� 0���� ����
            moveDirection.Normalize(); // �̵� ���� ���͸� ����ȭ�Ͽ� ���̸� 1�� ����

            // ���̰� �̵��ϴ� ������ �ٶ󺸵��� ����
            if (moveDirection != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(moveDirection);
        }


        if (antHealth <= 0) { Die(); } // ���� ���
    }

    // ���̿� ���� �ε��ƴ�.
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("���� ���𰡿� �浹");

        if (other.tag.Equals("Cake"))
        {
            Debug.Log("���� ����ũ�� �ֿ���.");
            getCake = true;

        }
        if (other.tag.Equals("Spawner"))
        {
            Debug.Log("����ũ �������� �ӹ� �ϼ�");
            Destroy(gameObject);
        }
        if (other.tag.Equals("Bullet"))
        {
            Debug.Log("���� Hit");
            animator.SetTrigger("Hit");
        }

    }

    private void Die()
    {
        Debug.Log("���� ���");
        Destroy(gameObject);
        animator.SetTrigger("Die");
        Destroy(gameObject, 2);
    }


}


