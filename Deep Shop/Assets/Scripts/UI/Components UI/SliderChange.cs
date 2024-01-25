using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderChange : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    private void Start()
    {
        _slider.onValueChanged.AddListener((v) =>
        {
            _textMeshPro.text = _slider.value.ToString("0.0") + " G";
        });
    }
}
