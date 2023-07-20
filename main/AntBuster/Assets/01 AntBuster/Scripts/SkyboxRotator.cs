using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRotator : MonoBehaviour
{
    public float rotationSpeed = 1.0f; // 스카이박스의 회전 속도

    private Material skyboxMaterial;

    private void Start()
    {
        // 현재 스카이박스에 사용된 Material 가져오기
        skyboxMaterial = RenderSettings.skybox;
    }

    private void Update()
    {
        // X축 기준으로 스카이박스 회전
        float rotationAmount = rotationSpeed * Time.deltaTime;
        Vector3 currentRotation = skyboxMaterial.GetVector("_Rotation");
        skyboxMaterial.SetVector("_Rotation", new Vector3(currentRotation.x + rotationAmount, currentRotation.y, currentRotation.z));
    }
}
