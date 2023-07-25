using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class HpBarController : MonoBehaviour
{
    public Slider hpSlider;
    public TMP_Text hpText;
    public AntController ant;

    private Transform cam;

    void Start()
    {
        cam = Camera.main.transform;
        ant = FindObjectOfType<AntController>();

        hpSlider = GetComponent<Slider>();
        if (hpSlider != null && hpSlider.gameObject.activeSelf) // Hpslider가 있는지 확인 및 게임 오브젝트 활성화 여부 확인
        {
            hpSlider.minValue = 0;
        }
    }

    void Update()
    {
        if (hpSlider != null && hpSlider.gameObject.activeSelf) // Hpslider가 있는지 확인 및 게임 오브젝트 활성화 여부 확인
        {
            hpSlider.transform.LookAt(hpSlider.transform.position +
            cam.rotation * Vector3.forward,
            cam.rotation * Vector3.up);

            hpSlider.maxValue = ant.antMaxHealth;
            hpSlider.value = ant.antHealth;
            hpText.text = (ant.antHealth.ToString() + "/" + ant.antMaxHealth.ToString());

            if (hpSlider.value == 0)
            {
                //gameObject.SetActive(false);

            }
        }

    }
}
