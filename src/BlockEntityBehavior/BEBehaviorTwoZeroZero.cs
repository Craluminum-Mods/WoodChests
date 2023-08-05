using Vintagestory.API.Common;
using Vintagestory.API.Datastructures;
using Vintagestory.GameContent;

namespace WoodChests;

public class BEBehaviorTwoZeroZero : BlockEntityBehavior
{
    public BEBehaviorTwoZeroZero(BlockEntity blockentity) : base(blockentity) { }

    public override void Initialize(ICoreAPI api, JsonObject properties)
    {
        base.Initialize(api, properties);

        if (Blockentity is not BlockEntityLabeledChest beChest)
        {
            return;
        }

        if (!beChest.type.Contains("-"))
        {
            return;
        }

        int index = beChest.type.IndexOf('-');
        if (index >= 0)
        {
            beChest.type = beChest.type.Substring(0, index);
        }
    }
}