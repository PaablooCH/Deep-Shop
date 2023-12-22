using UnityEngine;
using UnityEngine.UI;

public class KarmaSliders : MonoBehaviour
{
    [SerializeField]
    private Slider evilSlider;
    [SerializeField]
    private Slider goodSlider;

    void Start()
    {
        PlayerStats.instance.onKarmaChanged += UpdateKarmaSliders;
    }

    private void UpdateKarmaSliders(float newKarma, float oldKarma)
    {
        if (evilSlider != null && goodSlider != null)
        {
            if (newKarma == 0)
            {
                evilSlider.value = 0;
                goodSlider.value = 0;
            }
            if (newKarma > 0)
            {
                if (oldKarma < 0)
                {
                    evilSlider.value = 0; // Reset old predominant Slider
                }
                goodSlider.value = newKarma;
            }
            else
            {
                if (oldKarma > 0)
                {
                    goodSlider.value = 0; // Reset old predominant Slider
                }
                evilSlider.value = -newKarma;
            }
        }
    }
}
