using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndMove : MonoBehaviour
{
    RaycastHit hitLayerMask;    // Raycast ��� �� ã��
    Vector3 distance;

    private void OnMouseUp()        // ���콺�� ������ ��
    {
        distance = Vector3.zero;
    }


    private void OnMouseDrag()      // ���콺�� �巡������ ��
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.green);

        int layerMask = 1 << LayerMask.NameToLayer("World");    // Stage ���̾ �����ϴ�
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
