using UnityEngine;
using UnityEngine.UI;

public class KarmaSliders : MonoBehaviour
{
    [SerializeField]
    private Slider _evilSlider;
    [SerializeField]
    private Slider _goodSlider;

    void Start()
    {
        PlayerStats.instance.onKarmaChanged += UpdateKarmaSliders;
    }

    private void UpdateKarmaSliders(float newKarma, float oldKarma)
    {
        if (_evilSlider != null && _goodSlider != null)
        {
            if (newKarma == 0)
            {
                _evilSlider.value = 0;
                _goodSlider.value = 0;
            }
            if (newKarma > 0)
            {
                if (oldKarma < 0)
                {
                    _evilSlider.value = 0; // Reset old predominant Slider
                }
                _goodSlider.value = newKarma;
            }
            else
            {
                if (oldKarma > 0)
                {
                    _goodSlider.value = 0; // Reset old predominant Slider
                }
                _evilSlider.value = -newKarma;
            }
        }
    }
}
