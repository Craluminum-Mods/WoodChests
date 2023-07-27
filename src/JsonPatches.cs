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
        var woodTypesCombined = woodTypes.CombineTypes();

        var woodChest = new AssetLocation("woodchests:blocktypes/chests.json");
        var woodDoubleChest = new AssetLocation("woodchests:blocktypes/trunks.json");
        var woodLabeledChest = new AssetLocation("woodchests:blocktypes/labeledchests.json");

        List<JsonPatch> patches = new();

        var TickCount1 = Environment.TickCount;
        for (int i = 0; i < woodTypes.Count; i++)
        {
            try
            {
                patches.Add(new JsonPatch()
                {
                    Op = EnumJsonPatchOp.Add,
                    Value = @"{ ""base"": ""game:block/wood/debarked/placeholder1"", ""overlays"": [""block/chest/sides""] }".ParseAndReplacePlaceholder(woodTypes[i]),
                    Path = "/textures/placeholder1-sides2".ReplacePlaceholder(woodTypes[i]),
                    File = woodChest
                });
                patches.Add(new JsonPatch()
                {
                    Op = EnumJsonPatchOp.Add,
                    Value = @"{ ""base"": ""game:block/wood/debarked/placeholder1"" }".ParseAndReplacePlaceholder(woodTypes[i]),
                    Path = "/textures/placeholder1-lid".ReplacePlaceholder(woodTypes[i]),
                    File = woodChest
                });
                patches.Add(new JsonPatch()
                {
                    Op = EnumJsonPatchOp.Add,
                    Value = @"{ ""base"": ""game:block/wood/debarked/placeholder1"", ""overlays"": [""block/chest/accessories""] }".ParseAndReplacePlaceholder(woodTypes[i]),
                    Path = "/textures/placeholder1-accessories".ReplacePlaceholder(woodTypes[i]),
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
                    Value = @"{ ""base"": ""game:block/wood/debarked/placeholder1"", ""overlays"": [""block/trunk/right-side""] }".ParseAndReplacePlaceholder(woodTypes[i]),
                    Path = "/textures/placeholder1-right-side".ReplacePlaceholder(woodTypes[i]),
                    File = woodDoubleChest
                });
                patches.Add(new JsonPatch()
                {
                    Op = EnumJsonPatchOp.Add,
                    Value = @"{ ""base"": ""game:block/wood/debarked/placeholder1"", ""overlays"": [""block/chest/sides""] }".ParseAndReplacePlaceholder(woodTypes[i]),
                    Path = "/textures/placeholder1-sides2".ReplacePlaceholder(woodTypes[i]),
                    File = woodDoubleChest
                });
                patches.Add(new JsonPatch()
                {
                    Op = EnumJsonPatchOp.Add,
                    Value = @"{ ""base"": ""game:block/wood/debarked/placeholder1"" }".ParseAndReplacePlaceholder(woodTypes[i]),
                    Path = "/textures/placeholder1-lid".ReplacePlaceholder(woodTypes[i]),
                    File = woodDoubleChest
                });
                patches.Add(new JsonPatch()
                {
                    Op = EnumJsonPatchOp.Add,
                    Value = @"{ ""base"": ""game:block/wood/debarked/placeholder1"", ""overlays"": [""block/chest/accessories""] }".ParseAndReplacePlaceholder(woodTypes[i]),
                    Path = "/textures/placeholder1-accessories".ReplacePlaceholder(woodTypes[i]),
                    File = woodDoubleChest
                });
                patches.Add(new JsonPatch()
                {
                    Op = EnumJsonPatchOp.Add,
                    Value = @"{ ""base"": ""game:block/wood/debarked/placeholder1"", ""overlays"": [""block/trunk/left-side""] }".ParseAndReplacePlaceholder(woodTypes[i]),
                    Path = "/textures/placeholder1-left-side".ReplacePlaceholder(woodTypes[i]),
                    File = woodDoubleChest
                });
            }
            catch (Exception e)
            {
                api.Logger.Error("WoodChests: Failed to patch file {0}: {1}", woodDoubleChest, e);
            }
        }
        api.Logger.Debug("WoodChests: {0} took {1} ms", nameof(CreatePatches) + " for chest and doublechest", Environment.TickCount - TickCount1);

        var TickCount2 = Environment.TickCount;
        for (int i = 0; i < woodTypesCombined.Count; i++)
        {
            var twoTypes = woodTypesCombined[i].Split('-');

            try
            {
                patches.Add(new JsonPatch()
                {
                    Op = EnumJsonPatchOp.Add,
                    Value = @"{ ""base"": ""game:block/wood/debarked/placeholder1"", ""overlays"": [""block/chest/sides""] }".ParseAndReplaceTwoPlaceholders(twoTypes),
                    Path = "/textures/placeholder1-placeholder2-sides2".ReplaceTwoPlaceholders(twoTypes),
                    File = woodLabeledChest
                });
                patches.Add(new JsonPatch()
                {
                    Op = EnumJsonPatchOp.Add,
                    Value = @"{ ""base"": ""game:block/wood/debarked/placeholder1"" }".ParseAndReplaceTwoPlaceholders(twoTypes),
                    Path = "/textures/placeholder1-placeholder2-lid".ReplaceTwoPlaceholders(twoTypes),
                    File = woodLabeledChest
                });
                patches.Add(new JsonPatch()
                {
                    Op = EnumJsonPatchOp.Add,
                    Value = @"{ ""base"": ""game:block/wood/debarked/placeholder1"", ""overlays"": [""block/chest/accessories""] }".ParseAndReplaceTwoPlaceholders(twoTypes),
                    Path = "/textures/placeholder1-placeholder2-accessories".ReplaceTwoPlaceholders(twoTypes),
                    File = woodLabeledChest
                });
                patches.Add(new JsonPatch()
                {
                    Op = EnumJsonPatchOp.Add,
                    Value = @"{ ""base"": ""game:block/wood/debarked/placeholder2"" }".ParseAndReplaceTwoPlaceholders(twoTypes),
                    Path = "/textures/placeholder1-placeholder2-label".ReplaceTwoPlaceholders(twoTypes),
                    File = woodLabeledChest
                });
            }
            catch (Exception e)
            {
                api.Logger.Error("WoodChests: Failed to patch file {0}: {1}", woodLabeledChest, e);
            }
        }
        api.Logger.Debug("WoodChests: {0} took {1} ms", nameof(CreatePatches) + " for labeledchest", Environment.TickCount - TickCount2);

        return patches;
    }
}