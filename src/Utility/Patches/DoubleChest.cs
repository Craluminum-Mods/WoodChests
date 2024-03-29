using System.Collections.Generic;
using System.Linq;

namespace WoodChests;

public class DoubleChest : Chest
{
    public Dictionary<string, int> QuantityColumns { get; set; }

    public DoubleChest(List<string> types) : base(types)
    {
        DialogTitleLangCode = types.ToDictionary(key => key, _ => "trunkcontents");
        QuantitySlots = types.ToDictionary(key => key, _ => 36);
        Shape = types.ToDictionary(key => key, _ => "game:block/wood/trunk/normal");

        QuantityColumns = types.ToDictionary(key => key, _ => 9);
    }
}