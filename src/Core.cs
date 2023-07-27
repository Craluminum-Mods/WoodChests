using Vintagestory.API.Common;

[assembly: ModInfo("Wood Chests")]

namespace WoodChests;

public class Core : ModSystem
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