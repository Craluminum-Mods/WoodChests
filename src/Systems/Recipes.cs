using System.Collections.Generic;
using Vintagestory.API.Common;
using Vintagestory.API.Util;
using Vintagestory.ServerMods;
using Vintagestory.API.Datastructures;
using Newtonsoft.Json.Linq;
using System;

namespace WoodChests;

public class Recipes : ModSystem
{
    public GridRecipeLoader GridRecipeLoader { get; set; }

    public override bool ShouldLoad(EnumAppSide forSide) => forSide == EnumAppSide.Server;

    public override double ExecuteOrder() => 1;

    public bool WildCardMatch(AssetLocation Code, string wildCard)
    {
        return Code != null && WildcardUtil.Match(wildCard, Code.Path);
    }

    public override void AssetsLoaded(ICoreAPI api)
    {
        if (api.Side != EnumAppSide.Server)
        {
            return;
        }

        GridRecipeLoader = api.ModLoader.GetModSystem<GridRecipeLoader>();

        List<string> woodTypes = api.GetTypesFromWorldProperties("worldproperties/block/wood.json", "aged");

        List<GridRecipe> chestRecipes = CreateChestRecipes(api, woodTypes, EnumChestType.Normal);
        chestRecipes.AddRange(CreateChestRecipes(api, woodTypes, EnumChestType.Trunk));
        chestRecipes.AddRange(CreateChestRecipes(api, woodTypes, EnumChestType.Labeled));

        foreach (GridRecipe recipe in chestRecipes)
        {
            AssetLocation location = new("woodchests:recipes/" + recipe.GetHashCode().ToString() + Environment.TickCount);
            GridRecipeLoader.LoadRecipe(location, recipe);
        }
    }

    private static List<GridRecipe> CreateChestRecipes(ICoreAPI api, List<string> types, EnumChestType chestType)
    {
        List<GridRecipe> recipes = new();

        foreach (string type in types)
        {
            switch (chestType)
            {
                case EnumChestType.Normal:
                    recipes.Add(CreateChestRecipe(api, type));
                    break;
                case EnumChestType.Trunk:
                    recipes.Add(CreateTrunkRecipe(api, type));
                    break;
                case EnumChestType.Labeled:
                    recipes.Add(CreateChestLabeledRecipe(api, type));

                    if (api.ModLoader.IsModEnabled("vanvar"))
                    {
                        recipes.Add(CreateChestLabeledRecipe2(api, type));
                    }
                    break;
            }

            recipes.Add(CreateTrunkRecycleRecipe(api, type));
            recipes.Add(CreateChestLabeledRecycleRecipe(api, type));
        }
        return recipes;
    }

    private static GridRecipe CreateChestRecipe(ICoreAPI api, string type)
    {
        ItemStack outputStack = new(api.World.GetBlock(new AssetLocation("woodchests:wchest-east")));

        GridRecipe recipe = new()
        {
            IngredientPattern = "_W,WW,NW".Replace(",", ""),
            Width = 2,
            Height = 3,
            Ingredients = new Dictionary<string, CraftingRecipeIngredient>
            {
                ["W"] = new CraftingRecipeIngredient()
                {
                    Type = EnumItemClass.Item,
                    Code = new AssetLocation("game:plank-" + type),
                    Quantity = 2
                },
                ["N"] = new CraftingRecipeIngredient()
                {
                    Type = EnumItemClass.Item,
                    Code = new AssetLocation("game:metalnailsandstrips-*")
                }
            },
            Output = api.World.CreateIngredient(outputStack)
        };

        recipe.Output.Attributes = new(JToken.FromObject(new { type }));
        recipe.Output.Resolve(api.World, "");
        recipe.ResolveIngredients(api.World);
        return recipe;
    }

    private static GridRecipe CreateTrunkRecipe(ICoreAPI api, string type)
    {
        ItemStack outputStack = new(api.World.GetBlock(new AssetLocation("woodchests:wtrunk-east")));

        GridRecipe recipe = new()
        {
            IngredientPattern = "WW",
            Width = 2,
            Height = 1,
            Ingredients = new Dictionary<string, CraftingRecipeIngredient>
            {
                ["W"] = new CraftingRecipeIngredient()
                {
                    Type = EnumItemClass.Block,
                    Code = new AssetLocation("woodchests:wchest-east"),
                    Attributes = new(JToken.FromObject(new { type }))
                }
            },
            Output = api.World.CreateIngredient(outputStack)
        };

        recipe.Output.Attributes = new(JToken.FromObject(new { type }));
        recipe.Output.Resolve(api.World, "");
        recipe.ResolveIngredients(api.World);
        return recipe;
    }

    private static GridRecipe CreateChestLabeledRecipe(ICoreAPI api, string type)
    {
        ItemStack outputStack = new(api.World.GetBlock(new AssetLocation("woodchests:wlabeledchest-east")));

        GridRecipe recipe = new()
        {
            IngredientPattern = "SW",
            Width = 1,
            Height = 2,
            Ingredients = new Dictionary<string, CraftingRecipeIngredient>
            {
                ["W"] = new CraftingRecipeIngredient()
                {
                    Type = EnumItemClass.Block,
                    Code = new AssetLocation("woodchests:wchest-east"),
                    Attributes = new(JToken.FromObject(new { type }))
                },
                ["S"] = new CraftingRecipeIngredient()
                {
                    Type = EnumItemClass.Block,
                    Code = new AssetLocation("game:sign-ground-north")
                }
            },
            Output = api.World.CreateIngredient(outputStack)
        };

        recipe.Output.Attributes = new(JToken.FromObject(new { type }));
        recipe.Output.Resolve(api.World, "");
        recipe.ResolveIngredients(api.World);
        return recipe;
    }

    /// <summary>From Vanilla Variants signs</summary>
    private static GridRecipe CreateChestLabeledRecipe2(ICoreAPI api, string type)
    {
        ItemStack outputStack = new(api.World.GetBlock(new AssetLocation("woodchests:wlabeledchest-east")));

        GridRecipe recipe = new()
        {
            IngredientPattern = "SW",
            Width = 1,
            Height = 2,
            Ingredients = new Dictionary<string, CraftingRecipeIngredient>
            {
                ["W"] = new CraftingRecipeIngredient()
                {
                    Type = EnumItemClass.Block,
                    Code = new AssetLocation("woodchests:wchest-east"),
                    Attributes = new(JToken.FromObject(new { type }))
                },
                ["S"] = new CraftingRecipeIngredient()
                {
                    Type = EnumItemClass.Block,
                    Code = new AssetLocation("vanvar:sign-*-ground-north")
                }
            },
            Output = api.World.CreateIngredient(outputStack)
        };

        recipe.Output.Attributes = new(JToken.FromObject(new { type }));
        recipe.Output.Resolve(api.World, "");
        recipe.ResolveIngredients(api.World);
        return recipe;
    }

    private static GridRecipe CreateTrunkRecycleRecipe(ICoreAPI api, string type)
    {
        ItemStack outputStack = new(api.World.GetBlock(new AssetLocation("woodchests:wchest-east")));

        GridRecipe recipe = new()
        {
            IngredientPattern = "TW",
            Width = 1,
            Height = 2,
            RecipeGroup = 3,
            Shapeless = true,
            Ingredients = new Dictionary<string, CraftingRecipeIngredient>
            {
                ["W"] = new CraftingRecipeIngredient()
                {
                    Type = EnumItemClass.Block,
                    Code = new AssetLocation("woodchests:wtrunk-east"),
                    Attributes = new(JToken.FromObject(new { type }))
                },
                ["T"] = new CraftingRecipeIngredient()
                {
                    Type = EnumItemClass.Item,
                    Code = new AssetLocation("game:saw-*"),
                    IsTool = true
                }
            },
            Output = api.World.CreateIngredient(outputStack)
        };

        recipe.Output.Quantity = 2;
        recipe.Output.Attributes = new(JToken.FromObject(new { type }));
        recipe.Output.Resolve(api.World, "");
        recipe.ResolveIngredients(api.World);
        return recipe;
    }

    private static GridRecipe CreateChestLabeledRecycleRecipe(ICoreAPI api, string type)
    {
        ItemStack outputStack = new(api.World.GetBlock(new AssetLocation("woodchests:wchest-east")));
        JsonItemStack returnedStack = new() { Type = EnumItemClass.Block, Code = new("game:sign-ground-north") };

        GridRecipe recipe = new()
        {
            IngredientPattern = "TW",
            Width = 1,
            Height = 2,
            RecipeGroup = 2,
            Shapeless = true,
            Ingredients = new Dictionary<string, CraftingRecipeIngredient>
            {
                ["W"] = new CraftingRecipeIngredient()
                {
                    Type = EnumItemClass.Block,
                    Code = new AssetLocation("woodchests:wlabeledchest-east"),
                    ReturnedStack = returnedStack,
                    Attributes = new(JToken.FromObject(new { type }))
                },
                ["T"] = new CraftingRecipeIngredient()
                {
                    Type = EnumItemClass.Item,
                    Code = new AssetLocation("game:saw-*"),
                    IsTool = true
                }
            },
            Output = api.World.CreateIngredient(outputStack)
        };

        recipe.Output.Attributes = new(JToken.FromObject(new { type }));
        recipe.Output.Resolve(api.World, "");
        recipe.ResolveIngredients(api.World);
        return recipe;
    }
}