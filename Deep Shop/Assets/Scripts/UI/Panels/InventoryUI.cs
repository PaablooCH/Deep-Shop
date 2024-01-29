using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private bool _childsInitialized = false;

    // Update is called once per frame
    void Update()
    {
        if (!_childsInitialized)
        {
            transform.parent?.gameObject.SetActive(false);
            gameObject.SetActive(false);
            _childsInitialized = true;

            enabled = false;
        }
    }
}
