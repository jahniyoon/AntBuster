using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    private Transform headTransform;    // 개미를 향할 Tower의 발리스타
    public GameObject bulletPrefab = default;   // bullet Prefab 가져오기
    public string bulletTag = "Bullet";


    [Header("Tower Status")]  // 인스펙터 창에 보일 헤더 이름
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

        // ArcherTowerBody 자식 오브젝트의 Head 자식 오브젝트를 찾아서 해당 transform 값을 가져온다.
        Transform archerTowerBody = transform.Find("ArcherTower");
        headTransform = archerTowerBody.Find("Head");
        
    }
    void Update()
    {
        // 발리스타가 개미 쳐다보게 하기
        if (headTransform != null)
        {
            // 태그가 "ant"인 모든 오브젝트들을 찾습니다.
            GameObject[] ants = GameObject.FindGameObjectsWithTag("Ant");

            if (ants.Length > 0)
            {
                // 가장 가까운 "ant" 오브젝트를 찾습니다.
                Transform closestAnt = FindClosestAnt(ants);

                // 가장 가까운 "ant" 오브젝트를 바라보도록 Head를 회전시킵니다.
                headTransform.LookAt(closestAnt);
            }
        }


        // 화살 발사
        if (bulletFire)
        {
            bulletSpawn += Time.deltaTime;

            if (bulletSpawn > bulletRate)
            {
                bulletSpawn = 0f;

                Vector3 bulletPosition = headTransform.position; // bullet의 초기 위치를 Head 오브젝트의 위치로 설정
                bulletPosition.y = 1f; // bullet의 높이를 1.4로 설정

                GameObject bullet =
                    Instantiate(bulletPrefab, bulletPosition, headTransform.rotation);
                bullet.tag = bulletTag; // bullet에게 태그 지정

                Bullet bulletComponent = bullet.GetComponent<Bullet>(); // 생성된 총알의 Bullet 컴포넌트 가져오기
                if (bulletComponent != null)
                {
                    bulletComponent.speed = 5f; // bulletSpeed 값을 5로 설정
                }

                //bullet.transform.LookAt(closestAnt);

            }
        }
    }

    // 개미의 위치를 찾는다.
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

    // 타워가 무언가 감지했다.
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Ant"))
        {
            Debug.Log("타워가 개미 감지");
            bulletFire = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Ant"))
        {
            Debug.Log("타워가 개미 놓침");
            bulletFire = false;
        }
    }
}
