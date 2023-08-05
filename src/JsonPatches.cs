using System.Collections.Generic;
using Vintagestory.API.Common;
using Vintagestory.ServerMods.NoObf;
using System;

namespace WoodChests;

public class JsonPatches : ModSystem
{
    public override double ExecuteOrder() => 0.05;

    public override void AssetsLoaded(ICoreAPI api)
    {
        var TickCount = Environment.TickCount;
        var patches = CreatePatches(api);
        api.ApplyJsonPatches(patches);
        api.Logger.Debug("WoodChests: {0} took {1} ms", ToString(), Environment.TickCount - TickCount);
    }

    private List<JsonPatch> CreatePatches(ICoreAPI api)
    {
        var woodTypes = api.GetTypesFromWorldProperties("worldproperties/block/wood.json", "aged");

        var woodChest = new AssetLocation("woodchests:blocktypes/chest.json");
        var woodDoubleChest = new AssetLocation("woodchests:blocktypes/chest-trunk.json");
        var woodLabeledChest = new AssetLocation("woodchests:blocktypes/chest-labeled.json");

        List<JsonPatch> patches = new();

        var TickCount1 = Environment.TickCount;
        for (int i = 0; i < woodTypes.Count; i++)
        {
            try
            {
                patches.Add(new JsonPatch()
                {
                    Op = EnumJsonPatchOp.Add,
                    Value = @"{ ""base"": ""game:block/wood/debarked/placeholder"", ""overlays"": [""block/chest/sides""] }".ParseAndReplacePlaceholder(woodTypes[i]),
                    Path = "/textures/placeholder-sides2".ReplacePlaceholder(woodTypes[i]),
                    File = woodChest
                });
                patches.Add(new JsonPatch()
                {
                    Op = EnumJsonPatchOp.Add,
                    Value = @"{ ""base"": ""game:block/wood/debarked/placeholder"" }".ParseAndReplacePlaceholder(woodTypes[i]),
                    Path = "/textures/placeholder-lid".ReplacePlaceholder(woodTypes[i]),
                    File = woodChest
                });
                patches.Add(new JsonPatch()
                {
                    Op = EnumJsonPatchOp.Add,
                    Value = @"{ ""base"": ""game:block/wood/debarked/placeholder"", ""overlays"": [""block/chest/accessories""] }".ParseAndReplacePlaceholder(woodTypes[i]),
                    Path = "/textures/placeholder-accessories".ReplacePlaceholder(woodTypes[i]),
                    File = woodChest
                });
            }
            catch (Exception e)
            {
                api.Logger.Error("WoodChests: Failed to patch file {0}: {1}", woodChest, e);
            }

            try
            {
                patches.Add(new JsonPatch()
                {
                    Op = EnumJsonPatchOp.Add,
                    Value = @"{ ""base"": ""game:block/wood/debarked/placeholder"", ""overlays"": [""block/trunk/right-side""] }".ParseAndReplacePlaceholder(woodTypes[i]),
                    Path = "/textures/placeholder-right-side".ReplacePlaceholder(woodTypes[i]),
                    File = woodDoubleChest
                });
                patches.Add(new JsonPatch()
                {
                    Op = EnumJsonPatchOp.Add,
                    Value = @"{ ""base"": ""game:block/wood/debarked/placeholder"", ""overlays"": [""block/chest/sides""] }".ParseAndReplacePlaceholder(woodTypes[i]),
                    Path = "/textures/placeholder-sides2".ReplacePlaceholder(woodTypes[i]),
                    File = woodDoubleChest
                });
                patches.Add(new JsonPatch()
                {
                    Op = EnumJsonPatchOp.Add,
                    Value = @"{ ""base"": ""game:block/wood/debarked/placeholder"" }".ParseAndReplacePlaceholder(woodTypes[i]),
                    Path = "/textures/placeholder-lid".ReplacePlaceholder(woodTypes[i]),
                    File = woodDoubleChest
                });
                patches.Add(new JsonPatch()
                {
                    Op = EnumJsonPatchOp.Add,
                    Value = @"{ ""base"": ""game:block/wood/debarked/placeholder"", ""overlays"": [""block/chest/accessories""] }".ParseAndReplacePlaceholder(woodTypes[i]),
                    Path = "/textures/placeholder-accessories".ReplacePlaceholder(woodTypes[i]),
                    File = woodDoubleChest
                });
                patches.Add(new JsonPatch()
                {
                    Op = EnumJsonPatchOp.Add,
                    Value = @"{ ""base"": ""game:block/wood/debarked/placeholder"", ""overlays"": [""block/trunk/left-side""] }".ParseAndReplacePlaceholder(woodTypes[i]),
                    Path = "/textures/placeholder-left-side".ReplacePlaceholder(woodTypes[i]),
                    File = woodDoubleChest
                });
            }
            catch (Exception e)
            {
                api.Logger.Error("WoodChests: Failed to patch file {0}: {1}", woodDoubleChest, e);
            }

            try
            {
            patches.Add(new JsonPatch()
                {
                    Op = EnumJsonPatchOp.Add,
                    Value = @"{ ""base"": ""game:block/wood/debarked/placeholder"", ""overlays"": [""block/chest/sides""] }".ParseAndReplacePlaceholder(woodTypes[i]),
                    Path = "/textures/placeholder-sides2".ReplacePlaceholder(woodTypes[i]),
                    File = woodLabeledChest
                });
                patches.Add(new JsonPatch()
                {
                    Op = EnumJsonPatchOp.Add,
                    Value = @"{ ""base"": ""game:block/wood/debarked/placeholder"" }".ParseAndReplacePlaceholder(woodTypes[i]),
                    Path = "/textures/placeholder-lid".ReplacePlaceholder(woodTypes[i]),
                    File = woodLabeledChest
                });
                patches.Add(new JsonPatch()
                {
                    Op = EnumJsonPatchOp.Add,
                    Value = @"{ ""base"": ""game:block/wood/debarked/placeholder"", ""overlays"": [""block/chest/accessories""] }".ParseAndReplacePlaceholder(woodTypes[i]),
                    Path = "/textures/placeholder-accessories".ReplacePlaceholder(woodTypes[i]),
                    File = woodLabeledChest
                });
                patches.Add(new JsonPatch()
                {
                    Op = EnumJsonPatchOp.Add,
                    Value = @"{ ""base"": ""game:block/wood/chest/label"" }".Parse(),
                    Path = "/textures/placeholder-label".ReplacePlaceholder(woodTypes[i]),
                    File = woodLabeledChest
                });
            }
            catch (Exception e)
            {
                api.Logger.Error("WoodChests: Failed to patch file {0}: {1}", woodLabeledChest, e);
            }
        }
        api.Logger.Debug("WoodChests: {0} took {1} ms", nameof(CreatePatches) + " for chest, doublechest and labeled chest", Environment.TickCount - TickCount1);
        return patches;
    }
}