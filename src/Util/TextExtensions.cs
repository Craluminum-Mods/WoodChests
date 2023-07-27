using Newtonsoft.Json.Linq;
using Vintagestory.API.Datastructures;

namespace WoodChests;

public static class TextExtensions
{
    public static string ReplacePlaceholder(this string text, string with)
    {
        return text.Replace("placeholder1", with);
    }

    public static string ReplaceTwoPlaceholders(this string text, params string[] with)
    {
        return text.Replace("placeholder1", with[0]).Replace("placeholder2", with[1]);
    }

    public static JsonObject ParseAndReplacePlaceholder(this string text, string with)
    {
        return new(JToken.Parse(text.ReplacePlaceholder(with)));
    }

    public static JsonObject ParseAndReplaceTwoPlaceholders(this string text, params string[] with)
    {
        return new(JToken.Parse(text.ReplaceTwoPlaceholders(with)));
    }

    public static JsonObject Parse(this string text)
    {
        return new(JToken.Parse(text));
    }

    public static string Q(string text) => '"' + text + '"';
    public static string C(string text) => '{' + text + '}';
    public static string R(string text) => '(' + text + ')';
    public static string A(string text) => '[' + text + ']';
}