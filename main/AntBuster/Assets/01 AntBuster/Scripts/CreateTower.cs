using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTower : MonoBehaviour
{

    public GameObject towerPrefab = default;   // tower Prefab 가져오기
    private GameObject currentTower;


    RaycastHit hitLayerMask;
    Vector3 distance;
    Vector3 towerPos;

    private void OnMouseUp()
    {
        distance = Vector3.zero;
    }

    public void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.green);

        int layerMask = 1 << LayerMask.NameToLayer("World");
        if (Physics.Raycast(ray, out hitLayerMask, Mathf.Infinity, layerMask))
        {
            if (distance == Vector3.zero) distance = hitLayerMask.point;

            towerPos = hitLayerMask.point /*+ distance*/;

            Debug.Log("타워를 생성하려한다.");
            Create(towerPos);
        }
    }
    
    private void OnMouseDrag()
    {
        if (currentTower == null) return; // currentTower가 null일 경우 바로 return하여 오류 방지

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.green);

        int layerMask = 1 << LayerMask.NameToLayer("World");
        if (Physics.Raycast(ray, out hitLayerMask, Mathf.Infinity, layerMask))
        {
            if (distance == Vector3.zero) distance = currentTower.transform.position - hitLayerMask.point;

            currentTower.transform.position = hitLayerMask.point + distance;


            //float y = this.transform.position.y;    // 높이 저장
            //this.transform.position = 
            //    new Vector3(hitLayerMask.point.x, y, hitLayerMask.point.z);
        }
    }

    private void Create(Vector3 position)
    {
        currentTower = Instantiate(towerPrefab, position, Quaternion.identity);
    

    }

    private void Start()
    {
        distance = Vector3.zero;
    }

}
