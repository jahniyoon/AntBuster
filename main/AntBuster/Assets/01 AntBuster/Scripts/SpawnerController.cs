using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    private Rigidbody spawnerRigid;
    public GameObject antPrefab = default;   // ant Prefab 가져오기
    public string antTag = "Ant";

    public int antSpawnValue = 6;
    public int antSpawnCount = 0;

    public float antSpawn = default;
    public float spawnRate = 0.5f;

    [Header("Spawn Ant Status")]  // 인스펙터 창에 보일 헤더 이름
    public int Level = 1;
    public float MaxHealth = 4;
    public float Health = 4;
    public float Speed = 0.5f;

    void Start()
    {
        antSpawn = 0;
        spawnerRigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
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
                    antComponent.antHealth = Health; // 생성한 ant의 속도와 데미지설정
                    antComponent.antMaxHealth = MaxHealth; // 생성한 ant의 속도와 데미지설정
                    antComponent.antSpeed = Speed;
                }

            antSpawnCount++;
        }
    }
}
