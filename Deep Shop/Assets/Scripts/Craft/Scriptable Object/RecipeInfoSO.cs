using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Recipe/New Recipe Container")]
public class RecipeInfoSO : ScriptableObject
{
    [SerializeField, ReadOnly] private string _id = Guid.NewGuid().ToString();
    [SerializeField] private string _idRecipe;
    
    [Header("General")]
    [SerializeField] private string _nameRecipe;

    [Header("Result")]
    [SerializeField] private ItemQuantitySerialized _itemResult;

    [Header("Recipe")]
    [SerializeField] private ItemQuantitySerialized[] _itemNeeded;
    [SerializeField] private float _money;

    public string IdRecipe { get => _idRecipe; }
    public string NameRecipe { get => _nameRecipe; }
    public ItemQuantitySerialized ItemResult { get => _itemResult; }
    public ItemQuantitySerialized[] ItemsNeeded { get => _itemNeeded; }
    public float Money { get => _money; }

    private void OnValidate()
    {
#if UNITY_EDITOR
        _nameRecipe = name;
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}