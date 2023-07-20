using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Vintagestory.API.Common;
using Vintagestory.API.Datastructures;
using Vintagestory.API.Util;

namespace WoodChests;

public static class CollectibleExtensions
{
    public static void ChangeAttribute(this CollectibleObject obj, object val, params string[] path)
    {
        obj.Attributes ??= new JsonObject(new JObject());

        switch (path.Length)
        {
            case 1:
                obj.Attributes.Token[path[0]] = JToken.FromObject(val);
                break;
            case 2:
                obj.Attributes.Token[path[0]][path[1]] = JToken.FromObject(val);
                break;
        }
    }

    public static void AddToCreativeInventory(this CollectibleObject obj, IWorldAccessor world, List<string> types)
    {
        var stacks = ObjectCacheUtil.GetOrCreate(world.Api, "creativeStacks-" + obj.Code, delegate
        {
            return types.ConvertAll(type => obj.GenJstack(world, $"{{ type: \"{type}\"}}")).ToArray();
        });

        obj.CreativeInventoryStacks = new[]
        {
            new CreativeTabAndStackList() { Stacks = stacks, Tabs = new string[] { "general", "decorative", "cralwv" } }
        };
    }

    public static JsonItemStack GenJstack(this CollectibleObject obj, IWorldAccessor world, string jsonAttributes)
    {
        var jsonItemStack = new JsonItemStack
        {
            Code = obj.Code,
            Type = obj.ItemClass,
            Attributes = new JsonObject(JToken.Parse(jsonAttributes))
        };
        jsonItemStack.Resolve(world, "");
        return jsonItemStack;
    }

    public static List<string> GetTypes(this CollectibleObject obj) => obj.Attributes["types"].AsObject<List<string>>();
}