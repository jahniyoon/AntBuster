using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class AntController : MonoBehaviour
{
    
    private SpawnerController spawner;
    private CakeController cake;
    private Transform targetPos = default; // ��ǥ�� ������
    private Transform cakePos = default; // ����ũ�� ������
    private Transform spawnerPos = default; // �������� ������

    private bool getCake = false;
    private bool isDead = false;

    private Vector3 moveDirection = Vector3.forward; // �ʱ� �̵� ���� ����
    private Animator animator = default;

    [Header("Ant Status")]  // �ν����� â�� ���� ��� �̸�
    public float antHealth = default;
    public float antSpeed = default;
    public GameObject cakeObj; // ���� ������ ����ũ ������Ʈ


  

    void Start()
    {
       

        // ���� �� üũ�� ���� ������ ��Ʈ�ѷ� ��������
        spawner = FindObjectOfType<SpawnerController>();
        cake = FindObjectOfType<CakeController>();

        //CakeController�� transform�� ��������
        cakePos = FindObjectOfType<CakeController>().transform;
        spawnerPos = FindObjectOfType<SpawnerController>().transform;

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (0 < antHealth)
        {
            if (!getCake)
            {
                //Debug.Log("����ũ�� ����");
                targetPos = cakePos;
            }
            else
            {
                //Debug.Log("�����ʷ� ����");
                targetPos = spawnerPos;
            }

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
        }

        if (antHealth <= 0 && !isDead) { Die(); } // ���� ���
    }

    // ���̿� ���� �ε��ƴ�.
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("���� ���𰡿� �浹");

        if (other.tag.Equals("Cake"))
        {
            Debug.Log("���� ����ũ�� �ֿ���.");
            //if (cake.leftCake > 0)
            //{
                getCake = true;
                GetCake();
            //}
        }
        if (other.tag.Equals("Spawner"))
        {
            Debug.Log("����ũ �������� �ӹ� �ϼ�");
            spawner.antSpawnCount -= 1;
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
        isDead = true;
        Debug.Log("���� ���");
        animator.SetTrigger("Die");
        spawner.antSpawnCount -= 1;
        cakeObj.SetActive(false);

        Destroy(gameObject, 1.5f);

        // ����ũ�� �������־����� ����ũ ���� +1
        if (getCake)
        { cake.CakeBack(); }


    }
    private void GetCake()
    {

        if (getCake && cake.leftCake > 0)
        {
            // ���̿� �޷��ִ� ���� ������ ������Ʈ ��������

            Debug.Log("���� ����ũ ���.");
            cakeObj.SetActive(true);
        }
    }
}


