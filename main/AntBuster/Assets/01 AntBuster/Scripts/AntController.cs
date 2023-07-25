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
    public int antLevel = default;
    public float antHealth = default;
    public float antMaxHealth = default;
    public float antSpeed = default;
    public GameObject cakeObj; // ���� ������ ����ũ ������Ʈ

    // ���� ������
    public enum AntState
    {
        MoveToCake,
        MoveToSpawner,
        RandomMove,
    }
    private AntState currentState = AntState.MoveToCake;
    private float randomMoveTime = 2f;
    private float randomMoveDuration = 5f;
    private float randomMoveDistance = 5f;



    void Start()
    {
        antMaxHealth = antHealth;

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
            switch (currentState)
            {
                case AntState.MoveToCake:
                    DefaultMove();
                    break;

               

                case AntState.RandomMove:
                    RandomMove();
                    break;
            }
        }

        if (antHealth <= 0 && !isDead)
        { Die(); }


        // ���̰� �׻� �̵� ������ ������ ȸ����ŵ�ϴ�.
        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }
        //if (0 < antHealth)
        //{
        //    if (!getCake)
        //    {
        //        //Debug.Log("����ũ�� ����");
        //        targetPos = cakePos;
        //    }
        //    else
        //    {
        //        //Debug.Log("�����ʷ� ����");
        //        targetPos = spawnerPos;
        //    }

        //    // ���̰� Ÿ�� �Ѿư��� ��ũ��Ʈ
        //    if (targetPos != null)
        //    {
        //        // ����ũ�� ���� ����
        //        Vector3 targetPosition = new Vector3(targetPos.position.x, transform.position.y, targetPos.position.z);
        //        transform.position = Vector3.MoveTowards(transform.position, targetPosition, antSpeed * Time.deltaTime);

        //        // ������ �̵� ���� ���
        //        moveDirection = targetPos.position - transform.position;
        //        moveDirection.y = 0f; // ���̰� ���� �������� ȸ������ �ʵ��� y���� 0���� ����
        //        moveDirection.Normalize(); // �̵� ���� ���͸� ����ȭ�Ͽ� ���̸� 1�� ����

        //        // ���̰� �̵��ϴ� ������ �ٶ󺸵��� ����
        //        if (moveDirection != Vector3.zero)
        //            transform.rotation = Quaternion.LookRotation(moveDirection);
        //    }
        //}

        //if (antHealth <= 0 && !isDead) { Die(); } // ���� ���
    }
    private void DefaultMove()
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
        if (Random.value < 0.9f)
        {
            currentState = AntState.RandomMove;
        }
    }

    private void RandomMove()
    {
        randomMoveTime -= Time.deltaTime;

        if (randomMoveTime <= 0f)
        {
            // ���� ������ ���¿��� ���� �ð��� ������ ��, �ٽ� ��ǥ�� �̵��ϴ� ���·� ��ȯ
            randomMoveTime = randomMoveDuration;
            currentState = AntState.MoveToCake;
        }
        else
        {
            // ���� ������ ���� �߰� (���� �Ÿ��� �̵��ϰų� ���� �ð��� ������ ������ �����ϵ��� ����)
            float moveDistance = antSpeed * Time.deltaTime;
            if (randomMoveDistance > 0f && moveDistance >= randomMoveDistance)
            {
                // ���� �Ÿ��� �̵��ϸ� ������ ����
                float randomAngle = Random.Range(-135f, 135f);
                moveDirection = Quaternion.Euler(0f, randomAngle, 0f) * moveDirection;

                randomMoveDistance = Random.Range(1f, 5f); // ���� ���� �̵� �Ÿ� ����

            }
            else
            {
                // ���� �ð��� ���� ������ �̵�
                transform.position += moveDirection * moveDistance;
                randomMoveDistance -= moveDistance;
            }

        }
    }

    // ���̿� ���� �ε��ƴ�.
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("���� ���𰡿� �浹");

        if (other.tag.Equals("Cake") && !getCake)
        {
            //Debug.Log("���� ����ũ�� �ֿ���.");
            if (cake.leftCake > 0)
            {
                getCake = true;
                GetCake();

                Audio audio = FindObjectOfType<Audio>();
                audio.LaughSound();
            }
        }
        if (other.tag.Equals("Spawner") && getCake)
        {
            //Debug.Log("����ũ �������� �ӹ� �ϼ�");
            spawner.antSpawnCount -= 1;
            GameManager.instance.LostCake();
            Destroy(gameObject);

            Audio audio = FindObjectOfType<Audio>();
            audio.LaughSound();

            if (GameInfo.lostCake <= 0)
            {
                GameManager.instance.OnGameOver();
            }
        }
        if (other.tag.Equals("Bullet"))
        {
            //Debug.Log("���� Hit");
            animator.SetTrigger("Hit");

            Audio audio = FindObjectOfType<Audio>();
            audio.HitSound();
        }

    }

    private void Die()
    {
        isDead = true;
        //Debug.Log("���� ���");
        animator.SetTrigger("Die");
        spawner.antSpawnCount -= 1;
        cakeObj.SetActive(false);

        GameManager.instance.AddScore(antLevel);
        GameManager.instance.AddMoney(antLevel);

        if (GameInfo.score >= GameInfo.exp)
        {
            GameManager.instance.LevelUp();
        }
        Audio audio = FindObjectOfType<Audio>();
        audio.DieSound();

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
            cake.GetCake();
        }
    }
}


