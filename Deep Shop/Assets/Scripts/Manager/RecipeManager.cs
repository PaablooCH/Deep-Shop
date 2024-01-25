using System.Collections;
using UnityEngine;

[System.Serializable]
public class RecipeArray
{
    public Recipe[] recipes;
}

public class RecipeManager : MonoBehaviour
{
    #region Singleton
    public static RecipeManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Singleton fails.");
            return;
        }
        instance = this;
        StartCoroutine(LoadRecipesAsync());
    }
    #endregion

    [SerializeField]
    private Recipe[] _recipes;

    private const string RECIPES_PATH = "JSONs/recipes";

    public Recipe[] Recipes { get => _recipes; set => _recipes = value; }

    public Recipe SearchRecipeByID(int id)
    {
        foreach (Recipe recipe in _recipes)
        {
            if (recipe.id == id)
            {
                return recipe;
            }
        }
        return null;
    }

    public Recipe SearchRecipeByIDResult(int idResult)
    {
        foreach (Recipe recipe in _recipes)
        {
            if (recipe.productResult.idProduct == idResult)
            {
                return recipe;
            }
        }
        return null;
    }

    private IEnumerator LoadRecipesAsync()
    {
        ResourceRequest request = Resources.LoadAsync<TextAsset>(RECIPES_PATH);

        while (!request.isDone)
        {
            float progress = request.progress;
            Debug.Log("Recipes load progress: " + progress * 100f + "%");
            yield return null;
        }
        Debug.Log("Recipes load progress: 100%");

        TextAsset jsonAsset = request.asset as TextAsset;
        if (jsonAsset != null)
        {
            RecipeArray dataPrefab = JsonUtility.FromJson<RecipeArray>(jsonAsset.text);
            _recipes = dataPrefab.recipes;
        }
    }
}
