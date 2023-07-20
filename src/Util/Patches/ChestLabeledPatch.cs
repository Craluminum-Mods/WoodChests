using System.Collections.Generic;
using System.Linq;

namespace WoodChests;

public class LabeledChestPatch : ChestPatch
{
    public LabeledChestPatch(List<string> types) : base(types)
    {
        DialogTitleLangCode = types.ToDictionary(key => key, _ => "chestcontents");
        QuantitySlots = types.ToDictionary(key => key, _ => 16);
        Shape = types.ToDictionary(key => key, _ => "woodchests:block/chest/normal-labeled");
    }
}
