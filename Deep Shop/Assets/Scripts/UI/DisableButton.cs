using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisableButton : MonoBehaviour
{
    [SerializeField]
    private Button button;
    [SerializeField]
    private TMP_InputField inputText;

    // Update is called once per frame
    void Update()
    {
        if (inputText)
        {
            if (button.interactable && float.Parse(inputText.text) == 0)
            {
                button.interactable = false;
            }
            else if (!button.interactable && float.Parse(inputText.text) > 0)
            {
                button.interactable = true;
            }
        }
    }
}
