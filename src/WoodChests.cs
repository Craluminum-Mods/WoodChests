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
            api.World.Logger.Event("started 'Wood Chests' mod");
        }
    }
}