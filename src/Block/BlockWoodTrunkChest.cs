using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;
using static WoodChests.TextureExtensions;

namespace WoodChests
{
    class BlockWoodTrunkChest : BlockGenericTypedContainerTrunk
    {
        public override void OnCollectTextures(ICoreAPI api, ITextureLocationDictionary textureDict)
        {
            var types = this.GetTypes();
            if (types == null)
            {
                base.OnCollectTextures(api, textureDict);
                return;
            }

            GenerateTextures(ref Textures, types);
            base.OnCollectTextures(api, textureDict);
        }

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
            var part = Lang.Get($"material-{type}");
            var chest = Lang.GetMatching("game:block-normal-generic-trunk-*");
            return string.Format($"{chest} ({part})");
        }
    }
}