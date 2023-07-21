using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cake : MonoBehaviour
{
    
    // 움직이는 특정 오브젝트를 저장할 변수
    public GameObject movingObject;

    void Start()
    {
    

        // 움직이는 특정 오브젝트를 케이크 오브젝트의 자식으로 설정
        movingObject.transform.SetParent(transform);
    }

    void Update()
    {
        // 케이크 오브젝트의 애니메이션을 따라가도록 움직이는 특정 오브젝트의 위치와 회전값을 설정
        movingObject.transform.localPosition = Vector3.zero;
        movingObject.transform.localRotation = Quaternion.identity;

        // 케이크 오브젝트의 애니메이션을 제어하는 코드 추가
    }

}
