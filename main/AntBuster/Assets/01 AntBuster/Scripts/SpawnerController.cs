using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//using static UnityEditorInternal.ReorderableList;

public class SpawnerController : MonoBehaviour
{
    private Rigidbody spawnerRigid;
    public GameObject antPrefab = default;   // ant Prefab 가져오기
    public string antTag = "Ant";

    public int antSpawnValue = 6;
    public int antSpawnCount = 0;

    public float antSpawn = default;
    public float spawnRate = 0.5f;

    public float countDown = 5.0f;
    public TMP_Text timeText;
    public GameObject timer;

    void Start()
    {
        antSpawn = 0;
        spawnerRigid = GetComponent<Rigidbody>();

        //StartCoroutine(StartSpawning());

        timeText.text = countDown.ToString();

    }
    //// 10초 뒤에 스폰 시작을 위한 코루틴
    //private IEnumerator StartSpawning()
    //{
    //    yield return new WaitForSeconds(10f);
    //    shouldSpawn = true;

    //    antSpawn = 0;

    //}

    private void Update()
    {
        if (countDown > 0)
        { 
            countDown -= Time.deltaTime;
            timeText.text = countDown.ToString();
            timeText.text = Mathf.Round(countDown).ToString();
        }



        if (countDown <= 0)
        {
            timer.SetActive(false);

            antSpawn += Time.deltaTime;

            if (antSpawnCount < antSpawnValue && antSpawn > spawnRate)
            {
                antSpawn = 0f;

                //Debug.Log("개미를 생성한다.");
                Vector3 antPosition = spawnerRigid.position; // Ant의 초기 위치를 설정
                antPosition.x = -4f; // 스포너의 앞에 생성
                antPosition.z = 4f; // 스포너의 앞에 생성

                GameObject ant =
                    Instantiate(antPrefab, antPosition, spawnerRigid.rotation);
                ant.tag = antTag; // bullet에게 태그 지정

                AntController antComponent = ant.GetComponent<AntController>(); // 생성된 총알의 Bullet 컴포넌트 가져오기
                if (antComponent != null)
                {
                    antComponent.antLevel = GameInfo.level;
                    antComponent.antHealth = GameInfo.antHealth; // 생성한 ant의 속도와 데미지설정
                    antComponent.antMaxHealth = GameInfo.antMaxHealth; // 생성한 ant의 속도와 데미지설정
                    antComponent.antSpeed = GameInfo.antSpeed;
                }

                antSpawnCount++;
            }
        }
    }


}
