using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//using static UnityEditorInternal.ReorderableList;

public class SpawnerController : MonoBehaviour
{
    private Rigidbody spawnerRigid;
    public GameObject antPrefab = default;   // ant Prefab ��������
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
    //// 10�� �ڿ� ���� ������ ���� �ڷ�ƾ
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


}
