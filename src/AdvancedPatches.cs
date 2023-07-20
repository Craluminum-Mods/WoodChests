using System.Linq;
using Vintagestory.API.Common;
using Vintagestory.API.Util;

namespace WoodChests;

public class AdvancedPatches : ModSystem
{
    public int IteratedTimes { get; set; }
    public ChestPatch CChestPatch { get; set; }
    public DoubleChestPatch DChestPatch { get; set; }
    public LabeledChestPatch LChestPatch { get; set; }

    // public GridRecipeLoader GridRecipeLoader { get; set; }

    public override void AssetsLoaded(ICoreAPI api)
    {
        var woodTypes = api.Assets
            .Get<StandardWorldProperty>(new AssetLocation("worldproperties/block/wood.json")).Variants
            .Select(x => x.Code.Path)
            .ToArray()
            .Append("aged")
            .ToList();

        var woodTypesCombined = woodTypes.SelectMany(_ => woodTypes, (first, second) => $"{first}-{second}").ToList();

        CChestPatch = new ChestPatch(woodTypes);
        DChestPatch = new DoubleChestPatch(woodTypes);
        LChestPatch = new LabeledChestPatch(woodTypesCombined);
    }

    public override void AssetsFinalize(ICoreAPI api)
    {
        // if (api.Side == EnumAppSide.Server)
        // {
        //     GridRecipeLoader = api.ModLoader.GetModSystem<GridRecipeLoader>();
        //     // GridRecipeLoader.LoadRecipe(null, null);
        // }

        for (int i = 0; i < api.World.Blocks.Count; i++)
        {
            switch (api.World.Blocks[i])
            {
                case BlockWoodChest:
                    api.World.Blocks[i].ChangeAttribute(CChestPatch.Types, "types");
                    api.World.Blocks[i].ChangeAttribute(CChestPatch.DefaultType, "defaultType");
                    api.World.Blocks[i].ChangeAttribute(CChestPatch.RotatatableInterval, "rotatatableInterval");
                    api.World.Blocks[i].ChangeAttribute(CChestPatch.Drop, "drop");
                    api.World.Blocks[i].ChangeAttribute(CChestPatch.QuantitySlots, "quantitySlots");
                    api.World.Blocks[i].ChangeAttribute(CChestPatch.DialogTitleLangCode, "dialogTitleLangCode");
                    api.World.Blocks[i].ChangeAttribute(CChestPatch.StorageType, "storageType");
                    api.World.Blocks[i].ChangeAttribute(CChestPatch.RetrieveOnly, "retrieveOnly");
                    api.World.Blocks[i].ChangeAttribute(CChestPatch.Shape, "shape");
                    api.World.Blocks[i].ChangeAttribute(CChestPatch.TypedOpenSound, "typedOpenSound");
                    api.World.Blocks[i].ChangeAttribute(CChestPatch.TypedCloseSound, "typedCloseSound");

                    if (api.World.Blocks[i].Variant["side"] == "east")
                    {
                        api.World.Blocks[i].AddToCreativeInventory(api.World, CChestPatch.Types);
                    }
                    break;
                case BlockWoodLabeledChest:
                    api.World.Blocks[i].ChangeAttribute(LChestPatch.Types, "types");
                    api.World.Blocks[i].ChangeAttribute(LChestPatch.DefaultType, "defaultType");
                    api.World.Blocks[i].ChangeAttribute(LChestPatch.RotatatableInterval, "rotatatableInterval");
                    api.World.Blocks[i].ChangeAttribute(LChestPatch.Drop, "drop");
                    api.World.Blocks[i].ChangeAttribute(LChestPatch.QuantitySlots, "quantitySlots");
                    api.World.Blocks[i].ChangeAttribute(LChestPatch.DialogTitleLangCode, "dialogTitleLangCode");
                    api.World.Blocks[i].ChangeAttribute(LChestPatch.StorageType, "storageType");
                    api.World.Blocks[i].ChangeAttribute(LChestPatch.RetrieveOnly, "retrieveOnly");
                    api.World.Blocks[i].ChangeAttribute(LChestPatch.Shape, "shape");
                    api.World.Blocks[i].ChangeAttribute(LChestPatch.TypedOpenSound, "typedOpenSound");
                    api.World.Blocks[i].ChangeAttribute(LChestPatch.TypedCloseSound, "typedCloseSound");

                    if (api.World.Blocks[i].Variant["side"] == "east")
                    {
                        api.World.Blocks[i].AddToCreativeInventory(api.World, LChestPatch.Types);
                    }
                    break;
                case BlockWoodTrunkChest:
                    api.World.Blocks[i].ChangeAttribute(DChestPatch.Types, "types");
                    api.World.Blocks[i].ChangeAttribute(DChestPatch.DefaultType, "defaultType");
                    api.World.Blocks[i].ChangeAttribute(DChestPatch.RotatatableInterval, "rotatatableInterval");
                    api.World.Blocks[i].ChangeAttribute(DChestPatch.Drop, "drop");
                    api.World.Blocks[i].ChangeAttribute(DChestPatch.QuantitySlots, "quantitySlots");
                    api.World.Blocks[i].ChangeAttribute(DChestPatch.DialogTitleLangCode, "dialogTitleLangCode");
                    api.World.Blocks[i].ChangeAttribute(DChestPatch.StorageType, "storageType");
                    api.World.Blocks[i].ChangeAttribute(DChestPatch.RetrieveOnly, "retrieveOnly");
                    api.World.Blocks[i].ChangeAttribute(DChestPatch.Shape, "shape");
                    api.World.Blocks[i].ChangeAttribute(DChestPatch.TypedOpenSound, "typedOpenSound");
                    api.World.Blocks[i].ChangeAttribute(DChestPatch.TypedCloseSound, "typedCloseSound");

                    api.World.Blocks[i].ChangeAttribute(DChestPatch.QuantityColumns, "quantityColumns");

                    if (api.World.Blocks[i].Variant["side"] == "east")
                    {
                        api.World.Blocks[i].AddToCreativeInventory(api.World, DChestPatch.Types);
                    }
                    break;
            }
        }
    }
}