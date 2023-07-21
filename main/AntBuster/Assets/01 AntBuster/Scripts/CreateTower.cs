using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTower : MonoBehaviour
{

    public GameObject towerPrefab = default;   // tower Prefab 가져오기
    RaycastHit hitLayerMask;
    Vector3 distance;

    private void OnMouseUp()
    {
        //Debug.Log("타워를 생성한다.");
        //Vector3 towerPosition = spawnerRigid.position; // Ant의 초기 위치를 설정
        //towerPosition.y = 0.1f; // 스포너의 앞에 생성

        //GameObject ant =
        //    Instantiate(towerPrefab, towerPosition, spawnerRigid.rotation);
    }

    private void OnMouseDown()
    {
        distance = Vector3.zero;
    }
    private void OnMouseDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.green);

        int layerMask = 1 << LayerMask.NameToLayer("Stage");
        if (Physics.Raycast(ray, out hitLayerMask, Mathf.Infinity, layerMask))
        {
            if (distance == Vector3.zero) distance = this.transform.position - hitLayerMask.point;

            this.transform.position = hitLayerMask.point + distance;



            //float y = this.transform.position.y;    // 높이 저장
            //this.transform.position = 
            //    new Vector3(hitLayerMask.point.x, y, hitLayerMask.point.z);
        }
    }

    private void Start()
    {
        distance = Vector3.zero;
    }

}
