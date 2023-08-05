using Newtonsoft.Json.Linq;
using Vintagestory.API.Datastructures;

namespace WoodChests;

public static class TextExtensions
{
    public static string ReplacePlaceholder(this string text, string with)
    {
        return text.Replace("placeholder", with);
    }

    public static JsonObject ParseAndReplacePlaceholder(this string text, string with)
    {
        return text.ReplacePlaceholder(with).Parse();
    }

    public static JsonObject Parse(this string text)
    {
        return new(JToken.Parse(text));
    }
}