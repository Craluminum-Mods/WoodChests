using System.Collections.Generic;
using System.Linq;

namespace WoodChests;

public class Chest
{
    public List<string> Types { get; set; }
    public string DefaultType { get; set; }
    public Dictionary<string, string> RotatatableInterval { get; set; }
    public Dictionary<string, bool> Drop { get; set; }
    public Dictionary<string, int> QuantitySlots { get; set; }
    public Dictionary<string, string> DialogTitleLangCode { get; set; }
    public Dictionary<string, int> StorageType { get; set; }
    public Dictionary<string, bool> RetrieveOnly { get; set; }
    public Dictionary<string, string> Shape { get; set; }
    public Dictionary<string, string> TypedOpenSound { get; set; }
    public Dictionary<string, string> TypedCloseSound { get; set; }

    public Chest(List<string> types)
    {
        Types = types;
        DefaultType = types[0];

        Drop = types.ToDictionary(key => key, _ => true);
        RetrieveOnly = types.ToDictionary(key => key, _ => false);
        RotatatableInterval = types.ToDictionary(key => key, _ => "22.5degnot45deg");
        StorageType = types.ToDictionary(key => key, _ => 189);
        TypedCloseSound = types.ToDictionary(key => key, _ => "game:sounds/block/largechestclose");
        TypedOpenSound = types.ToDictionary(key => key, _ => "game:sounds/block/largechestopen");

        DialogTitleLangCode = types.ToDictionary(key => key, _ => "chestcontents");
        QuantitySlots = types.ToDictionary(key => key, _ => 16);
        Shape = types.ToDictionary(key => key, _ => "game:block/wood/chest/normal");
    }
}
