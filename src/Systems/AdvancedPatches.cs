using Vintagestory.API.Common;
using System.Collections.Generic;
using Vintagestory.API.Util;

namespace WoodChests;

public class AdvancedPatches : ModSystem
{
    public Chest CChest { get; set; }
    public DoubleChest DChest { get; set; }
    public LabeledChest LChest { get; set; }

    public override void AssetsLoaded(ICoreAPI api)
    {
        List<string> woodTypes = api.GetTypesFromWorldProperties("worldproperties/block/wood.json", "aged");

        CChest = new Chest(woodTypes);
        DChest = new DoubleChest(woodTypes);
        LChest = new LabeledChest(woodTypes);
    }

    public override void AssetsFinalize(ICoreAPI api)
    {
        foreach (var block in api.World.Blocks)
        {
            switch (block)
            {
                case BlockWoodChest:
                    block.ChangeAttribute(CChest.Types, "types");
                    block.ChangeAttribute(CChest.DefaultType, "defaultType");
                    block.ChangeAttribute(CChest.RotatatableInterval, "rotatatableInterval");
                    block.ChangeAttribute(CChest.Drop, "drop");
                    block.ChangeAttribute(CChest.QuantitySlots, "quantitySlots");
                    block.ChangeAttribute(CChest.DialogTitleLangCode, "dialogTitleLangCode");
                    block.ChangeAttribute(CChest.StorageType, "storageType");
                    block.ChangeAttribute(CChest.RetrieveOnly, "retrieveOnly");
                    block.ChangeAttribute(CChest.Shape, "shape");
                    block.ChangeAttribute(CChest.TypedOpenSound, "typedOpenSound");
                    block.ChangeAttribute(CChest.TypedCloseSound, "typedCloseSound");

                    if (block.Variant["side"] == "east")
                    {
                        block.AddToCreativeInventory(api.World, CChest.Types);
                    }
                    break;
                case BlockWoodLabeledChest:
                    block.BlockEntityBehaviors = block.BlockEntityBehaviors.Append(new BlockEntityBehaviorType() { Name = "WoodChests:TwoZeroZero", properties = null });

                    block.ChangeAttribute(LChest.Types, "types");
                    block.ChangeAttribute(LChest.DefaultType, "defaultType");
                    block.ChangeAttribute(LChest.RotatatableInterval, "rotatatableInterval");
                    block.ChangeAttribute(LChest.Drop, "drop");
                    block.ChangeAttribute(LChest.QuantitySlots, "quantitySlots");
                    block.ChangeAttribute(LChest.DialogTitleLangCode, "dialogTitleLangCode");
                    block.ChangeAttribute(LChest.StorageType, "storageType");
                    block.ChangeAttribute(LChest.RetrieveOnly, "retrieveOnly");
                    block.ChangeAttribute(LChest.Shape, "shape");
                    block.ChangeAttribute(LChest.TypedOpenSound, "typedOpenSound");
                    block.ChangeAttribute(LChest.TypedCloseSound, "typedCloseSound");

                    if (block.Variant["side"] == "east")
                    {
                        block.AddToCreativeInventory(api.World, LChest.Types);
                    }
                    break;
                case BlockWoodTrunkChest:
                    block.ChangeAttribute(DChest.Types, "types");
                    block.ChangeAttribute(DChest.DefaultType, "defaultType");
                    block.ChangeAttribute(DChest.RotatatableInterval, "rotatatableInterval");
                    block.ChangeAttribute(DChest.Drop, "drop");
                    block.ChangeAttribute(DChest.QuantitySlots, "quantitySlots");
                    block.ChangeAttribute(DChest.DialogTitleLangCode, "dialogTitleLangCode");
                    block.ChangeAttribute(DChest.StorageType, "storageType");
                    block.ChangeAttribute(DChest.RetrieveOnly, "retrieveOnly");
                    block.ChangeAttribute(DChest.Shape, "shape");
                    block.ChangeAttribute(DChest.TypedOpenSound, "typedOpenSound");
                    block.ChangeAttribute(DChest.TypedCloseSound, "typedCloseSound");

                    block.ChangeAttribute(DChest.QuantityColumns, "quantityColumns");

                    if (block.Variant["side"] == "east")
                    {
                        block.AddToCreativeInventory(api.World, DChest.Types);
                    }
                    break;
            }
        }
    }
}