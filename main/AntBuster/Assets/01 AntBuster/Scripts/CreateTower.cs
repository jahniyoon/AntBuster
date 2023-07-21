using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTower : MonoBehaviour
{

    public GameObject towerPrefab = default;   // tower Prefab ��������
    RaycastHit hitLayerMask;
    Vector3 distance;

    private void OnMouseUp()
    {
        //Debug.Log("Ÿ���� �����Ѵ�.");
        //Vector3 towerPosition = spawnerRigid.position; // Ant�� �ʱ� ��ġ�� ����
        //towerPosition.y = 0.1f; // �������� �տ� ����

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



            //float y = this.transform.position.y;    // ���� ����
            //this.transform.position = 
            //    new Vector3(hitLayerMask.point.x, y, hitLayerMask.point.z);
        }
    }

    private void Start()
    {
        distance = Vector3.zero;
    }

}
