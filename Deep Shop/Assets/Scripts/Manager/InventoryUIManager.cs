using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject inventoryAndKarma;

    private void Start()
    {
        if (inventoryAndKarma != null)
        {
            inventoryAndKarma.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            inventoryAndKarma.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            inventoryAndKarma.SetActive(false);
        }
    }
}
