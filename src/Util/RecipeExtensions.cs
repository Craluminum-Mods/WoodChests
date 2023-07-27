using Vintagestory.API.Common;

namespace WoodChests;

public static class RecipeExtensions
{
    public static char FindAvailableLetter(this GridRecipe recipe)
    {
        char availableLetter = 'A';
        for (; availableLetter <= 'Z'; availableLetter++)
        {
            if (!recipe.IngredientPattern.Contains(availableLetter.ToString())) break;
        }

        return availableLetter;
    }

    public static void AddIngredientAndResolve(this GridRecipe recipe, IWorldAccessor world, char letter, CraftingRecipeIngredient ingred)
    {
        recipe.Ingredients.Add(letter.ToString(), ingred);
        recipe.ResolveIngredients(world);
    }

    public static CraftingRecipeIngredient CreateIngredient(this IWorldAccessor world, ItemStack stack)
    {
        stack.ResolveBlockOrItem(world);

        return new()
        {
            Type = stack.Class,
            ResolvedItemstack = stack,
            Code = stack.Collectible.Code,
        };
    }

    public static bool HasIngredientWithCode(this CraftingRecipeIngredient ingredient, AssetLocation code) => ingredient?.Code == code;
}