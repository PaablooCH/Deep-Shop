using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderChange : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private TextMeshProUGUI textMeshPro;

    private void Start()
    {
        slider.onValueChanged.AddListener((v) =>
        {
            textMeshPro.text = slider.value.ToString("0.0") + " G";
        });
    }
}
