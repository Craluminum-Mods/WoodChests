using System.Collections.Generic;
using System.Linq;
using Vintagestory.API.Client;
using Vintagestory.API.Common;

namespace WoodChests;

public static class TextureExtensions
{
    /// <summary>
    /// Call in Block.OnCollectTextures only
    /// </summary>
    /// <param name="textures">Block.Textures</param>
    /// <param name="types">from Attributes</param>
    public static void GenerateTextures(ref IDictionary<string, CompositeTexture> textures, List<string> types)
    {
        var textureList = textures.ToDictionary(x => x.Key, y => y.Value).ToList();

        foreach (var type in types)
        {
            for (int i = 0; i < textureList.Count; i++)
            {
                var key = textureList[i].Key;
                var value = textureList[i].Value.Clone();

                value.Base = new AssetLocation(value.Base.ToString().Replace("placeholder", type));

                textures.Add(key.Replace("placeholder", type), value);
            }
        }
    }

    /// <summary>
    /// Call in Block.OnCollectTextures only
    /// </summary>
    /// <param name="textures">Block.Textures</param>
    /// <param name="types">from Attributes</param>
    public static void GenerateTexturesForLabeledChest(ref IDictionary<string, CompositeTexture> textures, List<string> types)
    {
        var textureList = textures.ToDictionary(x => x.Key, y => y.Value).ToList();

        for (int i = 0; i < types.Count; i++)
        {
            var twoTypes = types[i].Split('-');
            for (int j = 0; j < textureList.Count; j++)
            {
                var key = textureList[j].Key;
                var value = textureList[j].Value.Clone();

                value.Base.Path = value.Base.Path.Replace("placeholder1", twoTypes[0]);
                value.Base.Path = value.Base.Path.Replace("placeholder2", twoTypes[1]);
                key = key.Replace("placeholder1", twoTypes[0]).Replace("placeholder2", twoTypes[1]);

                textures.Add(key, value);
            }
        }
    }
}