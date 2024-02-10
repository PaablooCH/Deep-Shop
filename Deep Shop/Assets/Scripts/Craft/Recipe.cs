public class Recipe
{
    private RecipeInfoSO _recipeInfo;

    public RecipeInfoSO RecipeInfo { get => _recipeInfo; }

    public Recipe(RecipeInfoSO recipeInfo)
    {
        _recipeInfo = recipeInfo;
    }

    public string GetRecipeId()
    {
        return _recipeInfo.IdRecipe;
    }

    public string GetResultItemId()
    {
        return _recipeInfo.ItemResult.itemInfo.IdItem;
    }

    public int GetResultQuantity()
    {
        return _recipeInfo.ItemResult.quantity;
    }
}