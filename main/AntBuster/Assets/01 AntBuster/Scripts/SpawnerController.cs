using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditorInternal.ReorderableList;

public class SpawnerController : MonoBehaviour
{
    private Rigidbody spawnerRigid;
    public GameObject antPrefab = default;   // ant Prefab ��������
    public string antTag = "Ant";

    public int antSpawnValue = 6;
    public int antSpawnCount = 0;

    public float antSpawn = default;
    public float spawnRate = 0.5f;



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

            //Debug.Log("���̸� �����Ѵ�.");
                Vector3 antPosition = spawnerRigid.position; // Ant�� �ʱ� ��ġ�� ����
                antPosition.x = -4f; // �������� �տ� ����
                antPosition.z = 4f; // �������� �տ� ����

                GameObject ant =
                    Instantiate(antPrefab, antPosition, spawnerRigid.rotation);
                ant.tag = antTag; // bullet���� �±� ����

                AntController antComponent = ant.GetComponent<AntController>(); // ������ �Ѿ��� Bullet ������Ʈ ��������
                if (antComponent != null)
                {
                    antComponent.antLevel = GameInfo.level;
                    antComponent.antHealth = GameInfo.antHealth; // ������ ant�� �ӵ��� ����������
                    antComponent.antMaxHealth = GameInfo.antMaxHealth; // ������ ant�� �ӵ��� ����������
                    antComponent.antSpeed = GameInfo.antSpeed;
                }

            antSpawnCount++;
        }
    }
}
