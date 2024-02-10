using System.Collections.Generic;
using UnityEngine;

public class PackageWithItems : MonoBehaviour, IInteractable
{
    [Header("Package Sprites")]
    [SerializeField] private SpriteRenderer _spriteOpened;
    [SerializeField] private SpriteRenderer _spriteClosed;
    [SerializeField] private SpriteRenderer _spriteEmpty;

    private List<DeliverObject> _packagesWaiting = new();
    private List<ItemQuantity> _packagesReady = new();
    private bool _isPlayer = false;

    public List<ItemQuantity> Package { get => _packagesReady; set => _packagesReady = value; }
    public List<DeliverObject> PackagesWaiting { get => _packagesWaiting; set => _packagesWaiting = value; }

    private void Start()
    {
        _spriteOpened.enabled = false;
        _spriteClosed.enabled = false;
        _spriteEmpty.enabled = false;
    }

    private void Update()
    {
        // Interact point
        if (_packagesReady.Count != 0 && _isPlayer && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }

        // From waiting to ready
        List<DeliverObject> auxList = new();
        foreach (DeliverObject deliverObject in _packagesWaiting)
        {
            deliverObject.TimeToDeliver -= Time.deltaTime;
            if (deliverObject.TimeToDeliver <= 0f)
            {
                AddNewPackage(deliverObject.ProductQuantity);
            }
            else
            {
                auxList.Add(deliverObject);
            }
        }
        _packagesWaiting = auxList;
    }

    public void AddNewPackage(ItemQuantity newProductQuantity)
    {
        _packagesReady.Add(newProductQuantity);
        if (_packagesReady.Count > 0)
        {
            _spriteOpened.enabled = true;
        }
    }

    public List<ItemQuantity> PickPackages()
    {
        List<ItemQuantity> copyPackage = new(_packagesReady);
        _packagesReady.Clear();
        _spriteOpened.enabled = false;
        return copyPackage;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isPlayer = false;
        }
    }

    public void Interact()
    {
        UIManager.instance.OpenUI(UIs.ITEM_ACQ, gameObject);
    }
}
