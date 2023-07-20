using System.Collections.Generic;
using System.Linq;
using Vintagestory.API.Common;
using Vintagestory.API.Util;

[assembly: ModInfo("Wood Chests")]

namespace WoodChests;

public class Core : ModSystem
{
    public List<string> WoodTypes { get; private set; }
    public List<string> WoodTypesCombined { get; private set; }

    public override void Start(ICoreAPI api)
    {
        base.Start(api);
        api.RegisterBlockClass("WoodChests_BlockWoodChest", typeof(BlockWoodChest));
        api.RegisterBlockClass("WoodChests_BlockWoodLabeledChest", typeof(BlockWoodLabeledChest));
        api.RegisterBlockClass("WoodChests_BlockWoodTrunkChest", typeof(BlockWoodTrunkChest));
        api.World.Logger.Event("started 'Wood Chests' mod");
    }

    public override void AssetsLoaded(ICoreAPI api)
    {
        WoodTypes = api.Assets
            .Get<StandardWorldProperty>(new AssetLocation("worldproperties/block/wood.json")).Variants
            .Select(x => x.Code.Path)
            .ToArray()
            .Append("aged")
            .ToList();

        WoodTypesCombined = WoodTypes.SelectMany(_ => WoodTypes, (first, second) => $"{first}-{second}").ToList();
    }
}