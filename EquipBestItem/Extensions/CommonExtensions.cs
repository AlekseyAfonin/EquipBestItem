using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TaleWorlds.Core;
using TaleWorlds.ScreenSystem;

namespace EquipBestItem.Extensions;

public static class CommonExtensions
{
    internal static T? FindLayer<T>(this IEnumerable<ScreenLayer>? screenLayers) where T : ScreenLayer
    {
        if (screenLayers == null) return default;

        foreach (var layer in screenLayers)
            if (layer is T targetLayer)
                return targetLayer;
        return default;
    }

    internal static void GetMethod(this object o, string methodName, params object[] args)
    {
        var mi = o.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
        if (mi == null) return;
        try
        {
            mi.Invoke(o, args);
        }
        catch
        {
            throw new MBException($"{methodName} method retrieval error");
        }
    }

    // internal static IEnumerable<ItemParams> GetFlags(this ItemParams itemType)
    // {
    //     return Enum.GetValues(typeof(ItemParams))
    //         .Cast<ItemParams>()
    //         .Where(p => itemType.HasFlag(p));
    // }

    internal static IEnumerable<T> GetUniqueFlags<T>(this T flags) where T : Enum
    {
        ulong flag = 1;
        foreach (var value in Enum.GetValues(flags.GetType()).Cast<T>())
        {
            var bits = Convert.ToUInt64(value);
            while (flag < bits) flag <<= 1;

            if (flag == bits && flags.HasFlag(value)) yield return value;
        }
    }

    public static object GetPropValue<T>(this T @this, string propertyName)
    {
        var type = @this?.GetType();
        var property = type?.GetProperty(propertyName,
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

        return property?.GetValue(@this, null) ?? throw new InvalidOperationException("Get property value exception");
    }

    public static void SetPropValue<T>(this T @this, string propertyName, object value)
    {
        var type = @this?.GetType();
        var property = type?.GetProperty(propertyName,
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
        property?.SetValue(@this, value);
    }

    public static string GetName(this WeaponClass param)
    {
        return param switch
        {
            WeaponClass.Undefined => "Undefined",
            WeaponClass.Dagger => GameTexts.FindText("str_inventory_weapon.1").ToString(),
            WeaponClass.OneHandedSword => GameTexts.FindText("str_inventory_weapon.2").ToString(),
            WeaponClass.TwoHandedSword => GameTexts.FindText("str_inventory_weapon.3").ToString(),
            WeaponClass.OneHandedAxe => GameTexts.FindText("str_inventory_weapon.4").ToString(),
            WeaponClass.TwoHandedAxe => GameTexts.FindText("str_inventory_weapon.5").ToString(),
            WeaponClass.Mace => GameTexts.FindText("str_inventory_weapon.6").ToString(),
            WeaponClass.Pick => GameTexts.FindText("str_inventory_weapon.7").ToString(),
            WeaponClass.TwoHandedMace => GameTexts.FindText("str_inventory_weapon.8").ToString(),
            WeaponClass.OneHandedPolearm => GameTexts.FindText("str_inventory_weapon.9").ToString(),
            WeaponClass.TwoHandedPolearm => GameTexts.FindText("str_inventory_weapon.10").ToString(),
            WeaponClass.LowGripPolearm => GameTexts.FindText("str_inventory_weapon.11").ToString(),
            WeaponClass.Arrow => GameTexts.FindText("str_inventory_weapon.12").ToString(),
            WeaponClass.Bolt => GameTexts.FindText("str_inventory_weapon.13").ToString(),
            WeaponClass.Cartridge => GameTexts.FindText("str_inventory_weapon.14").ToString(),
            WeaponClass.Bow => GameTexts.FindText("str_inventory_weapon.15").ToString(),
            WeaponClass.Crossbow => GameTexts.FindText("str_inventory_weapon.16").ToString(),
            WeaponClass.Stone => GameTexts.FindText("str_inventory_weapon.17").ToString(),
            WeaponClass.Boulder => GameTexts.FindText("str_inventory_weapon.18").ToString(),
            WeaponClass.ThrowingAxe => GameTexts.FindText("str_inventory_weapon.19").ToString(),
            WeaponClass.ThrowingKnife => GameTexts.FindText("str_inventory_weapon.20").ToString(),
            WeaponClass.Javelin => GameTexts.FindText("str_inventory_weapon.21").ToString(),
            WeaponClass.Pistol => GameTexts.FindText("str_inventory_weapon.22").ToString(),
            WeaponClass.Musket => GameTexts.FindText("str_inventory_weapon.23").ToString(),
            WeaponClass.SmallShield => GameTexts.FindText("str_inventory_weapon.24").ToString(),
            WeaponClass.LargeShield => GameTexts.FindText("str_inventory_weapon.25").ToString(),
            WeaponClass.Banner => GameTexts.FindText("str_inventory_weapon.26").ToString(),
            WeaponClass.NumClasses => GameTexts.FindText("str_inventory_weapon.").ToString(),
            _ => throw new ArgumentOutOfRangeException(nameof(param), param, null)
        };
    }
}