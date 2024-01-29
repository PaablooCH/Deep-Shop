using System.Collections.Generic;
using UnityEngine;

public class PackageWithItems : MonoBehaviour
{
    [SerializeField] private ItemsAcquiredUI _itemsAcquiredUI;

    [SerializeField] private SpriteRenderer _chestOpened;
    [SerializeField] private SpriteRenderer _chestClosed;
    [SerializeField] private SpriteRenderer _chestEmpty;

    private List<ItemQuantity> _package = new();
    private bool _isPlayer = false;

    public List<ItemQuantity> Package { get => _package; set => _package = value; }

    private void Start()
    {
        _chestOpened.enabled = false;
        _chestClosed.enabled = false;
        _chestEmpty.enabled = false;
    }

    private void Update()
    {
        if (_package.Count != 0 && _isPlayer && Input.GetKeyDown(KeyCode.E))
        {
            _itemsAcquiredUI.OpenUI(gameObject);
        }
    }

    public void AddNewPackage(ItemQuantity newProductQuantity)
    {
        _package.Add(newProductQuantity);
        if (_package.Count > 0)
        {
            _chestOpened.enabled = true;
        }
    }

    public List<ItemQuantity> PickPackages()
    {
        List<ItemQuantity> copyPackage = new(_package);
        _package.Clear();
        _chestOpened.enabled = false;
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
}
