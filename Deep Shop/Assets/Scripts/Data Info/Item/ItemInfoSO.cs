using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Item/New Item Container")]
public class ItemInfoSO : ScriptableObject
{
    [SerializeField, ReadOnly] private string _id = Guid.NewGuid().ToString();
    [SerializeField] private string _idItem;
    
    [Header("General")]
    [SerializeField] private string _nameItem;
    [SerializeField] private string _description;

    [Header("Characteristics")]
    [SerializeField] private float _buyPrice;
    [SerializeField] private float _maxSoldPrice;
    [SerializeField] private float _karma;
    [SerializeField] private int _weightSpawn;

    [Header("Icon")]
    [SerializeField] private Sprite _sprite;
    [SerializeField] private Color _color;

    public string Id { get => _id; }
    public string IdItem { get => _idItem; }
    public string NameItem { get => _nameItem; }
    public string Description { get => _description; }
    public float BuyPrice { get => _buyPrice; }
    public float MaxSoldPrice { get => _maxSoldPrice; }
    public float Karma { get => _karma; }
    public int WeightSpawn { get => _weightSpawn; }
    public Sprite Sprite { get => _sprite; }
    public Color Color { get => _color; }

    private void OnValidate()
    {
#if UNITY_EDITOR
        _nameItem = name;
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}