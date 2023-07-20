using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRotator : MonoBehaviour
{
    public float rotationSpeed = 1.0f; // ��ī�̹ڽ��� ȸ�� �ӵ�

    private Material skyboxMaterial;

    private void Start()
    {
        // ���� ��ī�̹ڽ��� ���� Material ��������
        skyboxMaterial = RenderSettings.skybox;
    }

    private void Update()
    {
        // X�� �������� ��ī�̹ڽ� ȸ��
        float rotationAmount = rotationSpeed * Time.deltaTime;
        Vector3 currentRotation = skyboxMaterial.GetVector("_Rotation");
        skyboxMaterial.SetVector("_Rotation", new Vector3(currentRotation.x + rotationAmount, currentRotation.y, currentRotation.z));
    }
}
