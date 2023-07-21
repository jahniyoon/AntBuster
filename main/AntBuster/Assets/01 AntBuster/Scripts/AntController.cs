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
    private Transform targetPos = default; // 목표의 포지션
    private Transform cakePos = default; // 케이크의 포지션
    private Transform spawnerPos = default; // 스포너의 포지션

    private bool getCake = false;
    private bool isDead = false;

    private Vector3 moveDirection = Vector3.forward; // 초기 이동 방향 설정
    private Animator animator = default;

    [Header("Ant Status")]  // 인스펙터 창에 보일 헤더 이름
    public float antHealth = default;
    public float antSpeed = default;
    public GameObject cakeObj; // 개별 개미의 케이크 오브젝트


  

    void Start()
    {
       

        // 개미 수 체크를 위한 스포너 컨트롤러 가져오기
        spawner = FindObjectOfType<SpawnerController>();
        cake = FindObjectOfType<CakeController>();

        //CakeController의 transform값 가져오기
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
                //Debug.Log("케이크로 가자");
                targetPos = cakePos;
            }
            else
            {
                //Debug.Log("스포너로 가자");
                targetPos = spawnerPos;
            }

            // 개미가 타겟 쫓아가는 스크립트
            if (targetPos != null)
            {
                // 케이크로 가는 로직
                Vector3 targetPosition = new Vector3(targetPos.position.x, transform.position.y, targetPos.position.z);
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, antSpeed * Time.deltaTime);

                // 개미의 이동 방향 계산
                moveDirection = targetPos.position - transform.position;
                moveDirection.y = 0f; // 개미가 수직 방향으로 회전하지 않도록 y값을 0으로 설정
                moveDirection.Normalize(); // 이동 방향 벡터를 정규화하여 길이를 1로 만듦

                // 개미가 이동하는 방향을 바라보도록 설정
                if (moveDirection != Vector3.zero)
                    transform.rotation = Quaternion.LookRotation(moveDirection);
            }
        }

        if (antHealth <= 0 && !isDead) { Die(); } // 개미 사망
    }

    // 개미와 무언가 부딧쳤다.
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("개미 무언가와 충돌");

        if (other.tag.Equals("Cake"))
        {
            Debug.Log("개미 케이크를 주웠다.");
            //if (cake.leftCake > 0)
            //{
                getCake = true;
                GetCake();
            //}
        }
        if (other.tag.Equals("Spawner"))
        {
            Debug.Log("케이크 가져오기 임무 완수");
            spawner.antSpawnCount -= 1;
            Destroy(gameObject);
        }
        if (other.tag.Equals("Bullet"))
        {
            Debug.Log("개미 Hit");
            animator.SetTrigger("Hit");
        }

    }

    private void Die()
    {
        isDead = true;
        Debug.Log("개미 사망");
        animator.SetTrigger("Die");
        spawner.antSpawnCount -= 1;
        cakeObj.SetActive(false);

        Destroy(gameObject, 1.5f);

        // 케이크를 가지고있었으면 케이크 숫자 +1
        if (getCake)
        { cake.CakeBack(); }


    }
    private void GetCake()
    {

        if (getCake && cake.leftCake > 0)
        {
            // 개미에 달려있는 케익 증손자 오브젝트 가져오기

            Debug.Log("개미 케이크 든다.");
            cakeObj.SetActive(true);
        }
    }
}


