using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    #region Singleton
    public static RecipeManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Recipe Manager singleton already exists."); 
            return;
        }
        instance = this;
        _recipeMap = CreateRecipeMap();
    }
    #endregion

    private Dictionary<string, Recipe> _recipeMap;

    public Dictionary<string, Recipe> RecipeMap { get => _recipeMap; set => _recipeMap = value; }

    public Recipe SearchRecipeByID(string id)
    {
        // Search by recipe id
        foreach (Recipe recipe in _recipeMap.Values)
        {
            if (recipe.GetRecipeId() == id)
            {
                return recipe;
            }
        }
        // If not found, display error and return null
        Debug.LogWarning("Doesn`t exist any Recipe with id: " + id + ".");
        return null;
    }

    public Recipe SearchRecipeByIDResult(string idResult)
    {
        // Search by result item id
        foreach (Recipe recipe in _recipeMap.Values)
        {
            if (recipe.GetResultItemId() == idResult)
            {
                return recipe;
            }
        }
        // If not found, display error and return null
        Debug.LogWarning("Doesn`t exist any Recipe which result in an item with id: " + idResult + "."); 
        return null;
    }

    private Dictionary<string, Recipe> CreateRecipeMap()
    {
        // Get all the Scriptable Objects from the quests folder
        RecipeInfoSO[] allRecipes = Resources.LoadAll<RecipeInfoSO>("Recipes");

        // Insert all the quests into a dictionary
        Dictionary<string, Recipe> auxDictionary = new();
        foreach (RecipeInfoSO info in allRecipes)
        {
            if (auxDictionary.ContainsKey(info.IdRecipe))
            {
                Debug.LogWarning("The IdRecipe: " + info.IdRecipe + " has been found repeated, while creating the Item Map.");
                continue;
            }
            auxDictionary.Add(info.IdRecipe, new Recipe(info));
        }

        //return the dictionary
        return auxDictionary;
    }
}
