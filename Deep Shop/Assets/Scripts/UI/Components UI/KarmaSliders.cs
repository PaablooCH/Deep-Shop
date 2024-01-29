using System;
using UnityEngine;
using UnityEngine.UI;

public class KarmaSliders : MonoBehaviour
{
    [SerializeField] private Slider _evilSlider;
    [SerializeField] private Slider _goodSlider;

    private float _karma;

    void Start()
    {
        GameEventsManager.instance.inventoryEvent.onKarmaChanged += UpdateKarmaSliders;
        _karma = InventoryManager.instance.Karma;
    }

    private void UpdateKarmaSliders(float newKarma)
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
                if (_karma < 0)
                {
                    _evilSlider.value = 0; // Reset old predominant Slider
                }
                _goodSlider.value = newKarma;
            }
            else
            {
                if (_karma > 0)
                {
                    _goodSlider.value = 0; // Reset old predominant Slider
                }
                _evilSlider.value = -newKarma;
            }
            _karma = newKarma;
        }
    }

    private void OnDestroy()
    {
        GameEventsManager.instance.inventoryEvent.onKarmaChanged -= UpdateKarmaSliders;
    }
}
