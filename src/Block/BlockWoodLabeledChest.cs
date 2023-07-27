using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

namespace WoodChests
{
    class BlockWoodLabeledChest : BlockGenericTypedContainer
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
            var types = type.Split('-');
            var part1 = Lang.Get($"material-{types[0]}");
            var part2 = types.Length == 1 ? null : Lang.Get($"material-{types[1]}");
            var chest = Lang.GetMatching("game:block-normal-labeled-labeledchest-*");
            return string.Format($"{chest} ({part1}) ({part2})");
        }
    }
}