using System.Collections.Generic;
using System.Linq;
using Vintagestory.API.Common;
using Vintagestory.API.Util;

namespace WoodChests;

public static class ListExtensions
{
    public static List<string> GetTypesFromWorldProperties(this ICoreAPI api, string pathToWorldProperties, params string[] extraTypes)
    {
        return api.Assets
            .Get<StandardWorldProperty>(new AssetLocation(pathToWorldProperties)).Variants
            .Select(x => x.Code.Path)
            .ToArray()
            .Append(extraTypes)
            .ToList();
    }

    public static List<string> CombineTypes(this List<string> types)
    {
        return types.SelectMany(x => types, (first, second) => $"{first}-{second}").ToList();
    }
}