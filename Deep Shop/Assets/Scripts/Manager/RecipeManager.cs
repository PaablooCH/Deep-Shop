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
        ReadJSON();
    }
    #endregion

    [SerializeField]
    private Recipe[] _recipes;

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

    private void ReadJSON()
    {
        TextAsset jsonAsset = Resources.Load<TextAsset>("JSONs/recipes");
        if (jsonAsset != null)
        {
            RecipeArray dataPrefab = JsonUtility.FromJson<RecipeArray>(jsonAsset.text);
            _recipes = dataPrefab.recipes;
        }
    }
}
