using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    private Transform headTransform;    // ���̸� ���� Tower�� �߸���Ÿ
    public GameObject bulletPrefab = default;   // bullet Prefab ��������
    public string bulletTag = "Bullet";


    [Header("Tower Status")]  // �ν����� â�� ���� ��� �̸�
    public int towerLevel = 1;
    public float range = 100;
    public float bulletRate = 3;
    public float bulletSpeed = 4;
    public float bulletDamage = 2;

    private float bulletSpawn = default;
    private bool bulletFire = false;

    void Start()
    {
        bulletSpawn = 0f;

        // ArcherTowerBody �ڽ� ������Ʈ�� Head �ڽ� ������Ʈ�� ã�Ƽ� �ش� transform ���� �����´�.
        Transform archerTowerBody = transform.Find("ArcherTower");
        headTransform = archerTowerBody.Find("Head");
        
    }
    void Update()
    {
        // �߸���Ÿ�� ���� �Ĵٺ��� �ϱ�
        if (headTransform != null)
        {
            // �±װ� "ant"�� ��� ������Ʈ���� ã���ϴ�.
            GameObject[] ants = GameObject.FindGameObjectsWithTag("Ant");

            if (ants.Length > 0)
            {
                // ���� ����� "ant" ������Ʈ�� ã���ϴ�.
                Transform closestAnt = FindClosestAnt(ants);

                // ���� ����� "ant" ������Ʈ�� �ٶ󺸵��� Head�� ȸ����ŵ�ϴ�.
                headTransform.LookAt(closestAnt);
            }
        }


        // ȭ�� �߻�
        if (bulletFire)
        {
            bulletSpawn += Time.deltaTime;

            if (bulletSpawn > bulletRate)
            {
                bulletSpawn = 0f;

                Vector3 bulletPosition = headTransform.position; // bullet�� �ʱ� ��ġ�� Head ������Ʈ�� ��ġ�� ����
                bulletPosition.y = 1f; // bullet�� ���̸� 1.4�� ����

                GameObject bullet =
                    Instantiate(bulletPrefab, bulletPosition, headTransform.rotation);
                bullet.tag = bulletTag; // bullet���� �±� ����

                Bullet bulletComponent = bullet.GetComponent<Bullet>(); // ������ �Ѿ��� Bullet ������Ʈ ��������
                if (bulletComponent != null)
                {
                    bulletComponent.speed = 5f; // bulletSpeed ���� 5�� ����
                }

                //bullet.transform.LookAt(closestAnt);

            }
        }
    }

    // ������ ��ġ�� ã�´�.
    Transform FindClosestAnt(GameObject[] ants)
    {
        Transform closestAnt = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject ant in ants)
        {
            float distance = Vector3.Distance(headTransform.position, ant.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestAnt = ant.transform;
            }
        }

        return closestAnt;
    }   // } FindClosestAnt

    // Ÿ���� ���� �����ߴ�.
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Ant"))
        {
            Debug.Log("Ÿ���� ���� ����");
            bulletFire = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Ant"))
        {
            Debug.Log("Ÿ���� ���� ��ħ");
            bulletFire = false;
        }
    }
}
