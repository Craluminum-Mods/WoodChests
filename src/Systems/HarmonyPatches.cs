using System.Reflection;
using HarmonyLib;
using Vintagestory.API.Common;

namespace WoodChests;

public class HarmonyPatches : ModSystem
{
    public const string HarmonyID = "craluminum2413.woochests";

    public override bool AllowRuntimeReload => true;

    public override void Start(ICoreAPI api)
    {
        base.Start(api);
        new Harmony(HarmonyID).PatchAll(Assembly.GetExecutingAssembly());
    }

    public override void Dispose()
    {
        new Harmony(HarmonyID).UnpatchAll();
        base.Dispose();
    }

    public static int heldFrames;

    [HarmonyPatch(typeof(CollectibleObject), nameof(CollectibleObject.OnHeldIdle))]
    public static class CollectibleObject_OnHeldIdle_Patch_2_0_0
    {
        public static void Postfix(ItemSlot slot, EntityAgent byEntity)
        {
            heldFrames++;
            if (heldFrames < 100)
            {
                return;
            }
            heldFrames = 0;

            ItemStack stack = slot?.Itemstack;

            if (stack?.Collectible is not BlockWoodLabeledChest)
            {
                return;
            }

            string type = stack.Attributes.GetString("type");

            if (!type.Contains("-"))
            {
                return;
            }

            int index = type.IndexOf('-');
            if (index >= 0)
            {
                type = type.Substring(0, index);
            }

            stack.Attributes.SetString("type", type);
            slot.MarkDirty();

            byEntity.Api.Logger.Debug("ZZZ: OnHeldIdle");
        }

        private static bool OnInvSlot(ItemSlot slot)
        {
            slot.MarkDirty();
            return true;
        }
    }
}