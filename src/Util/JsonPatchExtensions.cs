using System.Collections.Generic;
using System.Text;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.ServerMods.NoObf;

namespace WoodChests;

public static class JsonPatchExtensions
{
    public static void ApplyJsonPatches(this ICoreAPI api, List<JsonPatch> patches)
    {
        var loader = api.ModLoader.GetModSystem<ModJsonPatchLoader>();

        int appliedCount = 0;
        int notfoundCount = 0;
        int errorCount = 0;
        int totalCount = 0;

        for (int i = 0; patches != null && i < patches.Count; i++)
        {
            JsonPatch patch = patches[i];

            totalCount++;
            loader.ApplyPatch(i, new AssetLocation(nameof(JsonPatches)), patch, ref appliedCount, ref notfoundCount, ref errorCount);
        }

        StringBuilder sb = new();
        sb.Append("WoodChests: JsonPatch Loader: ");
        if (appliedCount > 0)
        {
            sb.Append(Lang.Get(", successfully applied {0} patches", appliedCount));
        }

        api.Logger.Notification(sb.ToString());
        api.Logger.VerboseDebug("WoodChests: Patchloader finished");
    }
}