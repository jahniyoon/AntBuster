using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndMove : MonoBehaviour
{
    RaycastHit hitLayerMask;    // Raycast 닿는 곳 찾기
    Vector3 distance;

    private void OnMouseUp()        // 마우스를 떼었을 때
    {
        distance = Vector3.zero;
    }


    private void OnMouseDrag()      // 마우스를 드래그중일 때
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.green);

        int layerMask = 1 << LayerMask.NameToLayer("World");    // Stage 레이어만 가능하다
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
