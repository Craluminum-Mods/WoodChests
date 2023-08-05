using Vintagestory.API.Common;

namespace WoodChests;

public class AdvancedPatches : ModSystem
{
    public Chest CChest { get; set; }
    public DoubleChest DChest { get; set; }
    public LabeledChest LChest { get; set; }

    public override double ExecuteOrder() => 0.11;

    public override void AssetsLoaded(ICoreAPI api)
    {
        var woodTypes = api.GetTypesFromWorldProperties("worldproperties/block/wood.json", "aged");

        CChest = new Chest(woodTypes);
        DChest = new DoubleChest(woodTypes);
        LChest = new LabeledChest(woodTypes);
    }

    public override void AssetsFinalize(ICoreAPI api)
    {
        for (int i = 0; i < api.World.Blocks.Count; i++)
        {
            switch (api.World.Blocks[i])
            {
                case BlockWoodChest:
                    api.World.Blocks[i].ChangeAttribute(CChest.Types, "types");
                    api.World.Blocks[i].ChangeAttribute(CChest.DefaultType, "defaultType");
                    api.World.Blocks[i].ChangeAttribute(CChest.RotatatableInterval, "rotatatableInterval");
                    api.World.Blocks[i].ChangeAttribute(CChest.Drop, "drop");
                    api.World.Blocks[i].ChangeAttribute(CChest.QuantitySlots, "quantitySlots");
                    api.World.Blocks[i].ChangeAttribute(CChest.DialogTitleLangCode, "dialogTitleLangCode");
                    api.World.Blocks[i].ChangeAttribute(CChest.StorageType, "storageType");
                    api.World.Blocks[i].ChangeAttribute(CChest.RetrieveOnly, "retrieveOnly");
                    api.World.Blocks[i].ChangeAttribute(CChest.Shape, "shape");
                    api.World.Blocks[i].ChangeAttribute(CChest.TypedOpenSound, "typedOpenSound");
                    api.World.Blocks[i].ChangeAttribute(CChest.TypedCloseSound, "typedCloseSound");

                    if (api.World.Blocks[i].Variant["side"] == "east")
                    {
                        api.World.Blocks[i].AddToCreativeInventory(api.World, CChest.Types);
                    }
                    break;
                case BlockWoodLabeledChest:
                    api.World.Blocks[i].ChangeAttribute(LChest.Types, "types");
                    api.World.Blocks[i].ChangeAttribute(LChest.DefaultType, "defaultType");
                    api.World.Blocks[i].ChangeAttribute(LChest.RotatatableInterval, "rotatatableInterval");
                    api.World.Blocks[i].ChangeAttribute(LChest.Drop, "drop");
                    api.World.Blocks[i].ChangeAttribute(LChest.QuantitySlots, "quantitySlots");
                    api.World.Blocks[i].ChangeAttribute(LChest.DialogTitleLangCode, "dialogTitleLangCode");
                    api.World.Blocks[i].ChangeAttribute(LChest.StorageType, "storageType");
                    api.World.Blocks[i].ChangeAttribute(LChest.RetrieveOnly, "retrieveOnly");
                    api.World.Blocks[i].ChangeAttribute(LChest.Shape, "shape");
                    api.World.Blocks[i].ChangeAttribute(LChest.TypedOpenSound, "typedOpenSound");
                    api.World.Blocks[i].ChangeAttribute(LChest.TypedCloseSound, "typedCloseSound");

                    if (api.World.Blocks[i].Variant["side"] == "east")
                    {
                        api.World.Blocks[i].AddToCreativeInventory(api.World, LChest.Types);
                    }
                    break;
                case BlockWoodTrunkChest:
                    api.World.Blocks[i].ChangeAttribute(DChest.Types, "types");
                    api.World.Blocks[i].ChangeAttribute(DChest.DefaultType, "defaultType");
                    api.World.Blocks[i].ChangeAttribute(DChest.RotatatableInterval, "rotatatableInterval");
                    api.World.Blocks[i].ChangeAttribute(DChest.Drop, "drop");
                    api.World.Blocks[i].ChangeAttribute(DChest.QuantitySlots, "quantitySlots");
                    api.World.Blocks[i].ChangeAttribute(DChest.DialogTitleLangCode, "dialogTitleLangCode");
                    api.World.Blocks[i].ChangeAttribute(DChest.StorageType, "storageType");
                    api.World.Blocks[i].ChangeAttribute(DChest.RetrieveOnly, "retrieveOnly");
                    api.World.Blocks[i].ChangeAttribute(DChest.Shape, "shape");
                    api.World.Blocks[i].ChangeAttribute(DChest.TypedOpenSound, "typedOpenSound");
                    api.World.Blocks[i].ChangeAttribute(DChest.TypedCloseSound, "typedCloseSound");

                    api.World.Blocks[i].ChangeAttribute(DChest.QuantityColumns, "quantityColumns");

                    if (api.World.Blocks[i].Variant["side"] == "east")
                    {
                        api.World.Blocks[i].AddToCreativeInventory(api.World, DChest.Types);
                    }
                    break;
            }
        }
    }
}