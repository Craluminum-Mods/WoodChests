using Vintagestory.API.Common;

namespace WoodChests;

public static class RecipeExtensions
{
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
}