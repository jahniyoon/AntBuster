using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    private Rigidbody spawnerRigid;
    public GameObject antPrefab = default;   // ant Prefab ��������
    public string antTag = "Ant";

    public int antSpawnValue = 6;
    public int antSpawnCount = 0;

    public float antSpawn = default;
    public float spawnRate = 0.5f;

    [Header("Spawn Ant Status")]  // �ν����� â�� ���� ��� �̸�
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
                    antComponent.antHealth = Health; // ������ ant�� �ӵ��� ����������
                    antComponent.antMaxHealth = MaxHealth; // ������ ant�� �ӵ��� ����������
                    antComponent.antSpeed = Speed;
                }

            antSpawnCount++;
        }
    }
}
