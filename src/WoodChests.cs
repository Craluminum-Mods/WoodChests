using Vintagestory.API.Common;

[assembly: ModInfo("Wood Chests",
    Authors = new[] { "Craluminum2413" })]

namespace WoodChests
{
    class WoodChests : ModSystem
    {
        public override void Start(ICoreAPI api)
        {
            base.Start(api);
            api.RegisterBlockClass("WoodChests_BlockWoodChest", typeof(BlockWoodChest));
            api.RegisterBlockClass("WoodChests_BlockWoodLabeledChest", typeof(BlockWoodLabeledChest));
            api.RegisterBlockClass("WoodChests_BlockWoodTrunkChest", typeof(BlockWoodTrunkChest));
            api.World.Logger.Event("started 'Wood Chests' mod");
        }
    }
}