using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

namespace WoodChests
{
    class BlockWoodChest : BlockGenericTypedContainer
    {
        public override string GetHeldItemName(ItemStack itemStack)
        {
            return GetName(itemStack.Attributes.GetString("type"));
        }

        public override string GetPlacedBlockName(IWorldAccessor world, BlockPos pos)
        {
            if (api is ICoreClientAPI capi && capi.World.BlockAccessor.GetBlockEntity(pos) is BlockEntityGenericTypedContainer be)
            {
                return GetName(be.type);
            }
            return base.GetPlacedBlockName(world, pos);
        }

        public string GetName(string type)
        {
            string part = Lang.Get($"material-{type}");
            string chest = Lang.GetMatching("game:block-normal-generic-chest-*");
            return string.Format($"{chest} ({part})");
        }
    }
}