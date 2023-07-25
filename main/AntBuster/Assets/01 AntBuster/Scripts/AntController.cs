using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
//using static UnityEngine.GraphicsBuffer;

public class AntController : MonoBehaviour
{
    
    private SpawnerController spawner;
    private CakeController cake;
    private Transform targetPos = default; // 목표의 포지션
    private Transform cakePos = default; // 케이크의 포지션
    private Transform spawnerPos = default; // 스포너의 포지션

    // 데미지 UI 관련
    public TMP_Text damageText;
    public GameObject damageUI;

    public float damageTime = 1f;
    private float damageDisplayDuration = 1f;

    // 점수 UI 관련
    public TMP_Text scoreText;
    public GameObject scoreUI;
   
    private bool getCake = false;
    private bool isDead = false;

    private Vector3 moveDirection = Vector3.forward; // 초기 이동 방향 설정
    private Animator animator = default;

    [Header("Ant Status")]  // 인스펙터 창에 보일 헤더 이름
    public int antLevel = default;
    public float antHealth = default;
    public float antMaxHealth = default;
    public float antSpeed = default;
    public GameObject cakeObj; // 개별 개미의 케이크 오브젝트

    // 개미 움직임
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
        {
            scoreUI.SetActive(true);
            scoreText.text = string.Format("+ {0}", antLevel);
            Debug.Log("점수가 잘 보이나?" + antLevel);
            Die();
        }

        // 개미가 항상 이동 방향을 보도록 회전시킵니다.
        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }

        // damageTime이 일정 시간 이상 지났을 때, damageUI를 비활성화
        if (damageTime < damageDisplayDuration)
        {
            damageTime += Time.deltaTime;
        }
        else
        {
            damageUI.SetActive(false);
        }

      

        //if (0 < antHealth)
        //{
        //    if (!getCake)
        //    {
        //        //Debug.Log("케이크로 가자");
        //        targetPos = cakePos;
        //    }
        //    else
        //    {
        //        //Debug.Log("스포너로 가자");
        //        targetPos = spawnerPos;
        //    }

        //    // 개미가 타겟 쫓아가는 스크립트
        //    if (targetPos != null)
        //    {
        //        // 케이크로 가는 로직
        //        Vector3 targetPosition = new Vector3(targetPos.position.x, transform.position.y, targetPos.position.z);
        //        transform.position = Vector3.MoveTowards(transform.position, targetPosition, antSpeed * Time.deltaTime);

        //        // 개미의 이동 방향 계산
        //        moveDirection = targetPos.position - transform.position;
        //        moveDirection.y = 0f; // 개미가 수직 방향으로 회전하지 않도록 y값을 0으로 설정
        //        moveDirection.Normalize(); // 이동 방향 벡터를 정규화하여 길이를 1로 만듦

        //        // 개미가 이동하는 방향을 바라보도록 설정
        //        if (moveDirection != Vector3.zero)
        //            transform.rotation = Quaternion.LookRotation(moveDirection);
        //    }
        //}

        //if (antHealth <= 0 && !isDead) { Die(); } // 개미 사망
    }
    private void DefaultMove()
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
            // 랜덤 움직임 상태에서 일정 시간이 지났을 때, 다시 목표로 이동하는 상태로 전환
            randomMoveTime = randomMoveDuration;
            currentState = AntState.MoveToCake;
        }
        else
        {
            // 랜덤 움직임 로직 추가 (일정 거리를 이동하거나 일정 시간이 지나면 방향을 변경하도록 구현)
            float moveDistance = antSpeed * Time.deltaTime;
            if (randomMoveDistance > 0f && moveDistance >= randomMoveDistance)
            {
                // 일정 거리를 이동하면 방향을 변경
                float randomAngle = Random.Range(-135f, 135f);
                moveDirection = Quaternion.Euler(0f, randomAngle, 0f) * moveDirection;

                randomMoveDistance = Random.Range(1f, 5f); // 다음 랜덤 이동 거리 설정

            }
            else
            {
                // 일정 시간이 지날 때까지 이동
                transform.position += moveDirection * moveDistance;
                randomMoveDistance -= moveDistance;
            }

        }
    }

    // 개미와 무언가 부딧쳤다.
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("개미 무언가와 충돌");

        if (other.tag.Equals("Cake") && !getCake)
        {
            //Debug.Log("개미 케이크를 주웠다.");
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
            //Debug.Log("케이크 가져오기 임무 완수");
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
            //Debug.Log("개미 Hit");
            animator.SetTrigger("Hit");

            Audio audio = FindObjectOfType<Audio>();
            audio.HitSound();
        }

    }

    public void Hit(float damage)
    {
        damageTime = 0f; // Hit 메서드가 호출될 때마다 damageTime 초기화
        damageUI.SetActive(true);
        damageText.text = string.Format("{0}", damage);
    }
   

    private void Die()
    {
        isDead = true;
        //Debug.Log("개미 사망");
        animator.SetTrigger("Die");
        spawner.antSpawnCount -= 1;

        GameManager.instance.AddScore(antLevel);
        GameManager.instance.AddMoney(antLevel);

        if (GameInfo.score >= GameInfo.exp)
        {
            GameManager.instance.LevelUp();
        }
        Audio audio = FindObjectOfType<Audio>();
        audio.DieSound();

        Destroy(gameObject, 1.5f);
        cakeObj.SetActive(false);


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
            cake.GetCake();
        }
    }
}


