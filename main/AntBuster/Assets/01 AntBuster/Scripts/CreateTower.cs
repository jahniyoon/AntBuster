using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTower : MonoBehaviour
{

    public GameObject towerPrefab = default;   // tower Prefab ��������
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

            Debug.Log("Ÿ���� �����Ϸ��Ѵ�.");
            Create(towerPos);
        }
    }
    
    private void OnMouseDrag()
    {
        if (currentTower == null) return; // currentTower�� null�� ��� �ٷ� return�Ͽ� ���� ����

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.green);

        int layerMask = 1 << LayerMask.NameToLayer("World");
        if (Physics.Raycast(ray, out hitLayerMask, Mathf.Infinity, layerMask))
        {
            if (distance == Vector3.zero) distance = currentTower.transform.position - hitLayerMask.point;

            currentTower.transform.position = hitLayerMask.point + distance;


            //float y = this.transform.position.y;    // ���� ����
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
