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

        hpSlider =GetComponent<Slider>();
        hpSlider.minValue = 0;
    }

    void Update()
    {
        hpSlider.transform.LookAt(hpSlider.transform.position +
            cam.rotation * Vector3.forward,
            cam.rotation * Vector3.up);

        hpSlider.maxValue = ant.antMaxHealth;
        hpSlider.value = ant.antHealth;
        hpText.text = (ant.antHealth.ToString() + "/" + ant.antMaxHealth.ToString());

        if (hpSlider.value == 0)
        {
            gameObject.SetActive(false);

        }

    }
}
