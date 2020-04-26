using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;

namespace EquipBestItem
{
    public class FilterWeaponSettings
    {
        public float MaxDataValue { get; set; } = 1f;
        public float ThrustSpeed { get; set; } = 1f;
        public float SwingSpeed { get; set; } = 1f;
        public float MissileSpeed { get; set; } = 1f;
        public float WeaponLength { get; set; } = 1f;
        public float ThrustDamage { get; set; } = 1f;
        public float SwingDamage { get; set; } = 1f;
        public float Accuracy { get; set; } = 1f;
        public float Handling { get; set; } = 1f;
        public float WeaponWeight { get; set; } = 0;
        public float WeaponBodyArmor { get; set; } = 1f;

        //public DamageTypes SwingDamageType { get; set; } = 0;
        //public DamageTypes ThrustDamageType { get; set; } = 0;
        //public int MissileDamage { get; set; }
        //public float WeaponBalance { get; set; }


        //public WeaponClass? WeaponClass { get; set; }
        //public string ItemUsage { get; set; }


        //public WeaponFlags? WeaponFlags { get; set; }

        public FilterWeaponSettings()
        {
            
        }

        //public WeaponFlags GetNextWeaponFlag(WeaponFlags flag)
        //{
        //    switch (flag)
        //    {
        //        case WeaponFlags.MeleeWeapon:
        //            return WeaponFlags.RangedWeapon;
        //        case WeaponFlags.RangedWeapon:
        //            return WeaponFlags.WeaponMask;
        //        case WeaponFlags.FirearmAmmo:
        //            return WeaponFlags.NotUsableWithOneHand;
        //        case WeaponFlags.NotUsableWithOneHand:
        //            return WeaponFlags.NotUsableWithTwoHand;
        //        case WeaponFlags.NotUsableWithTwoHand:
        //            return WeaponFlags.HandUsageMask;
        //        case WeaponFlags.HandUsageMask:
        //            return WeaponFlags.WideGrip;
        //        case WeaponFlags.WideGrip:
        //            return WeaponFlags.AttachAmmoToVisual;
        //        case WeaponFlags.AttachAmmoToVisual:
        //            return WeaponFlags.Consumable;
        //        case WeaponFlags.Consumable:
        //            return WeaponFlags.HasHitPoints;
        //        case WeaponFlags.HasHitPoints:
        //            return WeaponFlags.DataValueMask;
        //        case WeaponFlags.DataValueMask:
        //            return WeaponFlags.HasString;
        //        case WeaponFlags.HasString:
        //            return WeaponFlags.StringHeldByHand;
        //        case WeaponFlags.StringHeldByHand:
        //            return WeaponFlags.UnloadWhenSheathed;
        //        case WeaponFlags.UnloadWhenSheathed:
        //            return WeaponFlags.AffectsArea;
        //        case WeaponFlags.AffectsArea:
        //            return WeaponFlags.Burning;
        //        case WeaponFlags.Burning:
        //            return WeaponFlags.BonusAgainstShield;
        //        case WeaponFlags.BonusAgainstShield:
        //            return WeaponFlags.CanPenetrateShield;
        //        case WeaponFlags.CanPenetrateShield:
        //            return WeaponFlags.CantReloadOnHorseback;
        //        case WeaponFlags.CantReloadOnHorseback:
        //            return WeaponFlags.AutoReload;
        //        case WeaponFlags.AutoReload:
        //            return WeaponFlags.CrushThrough;
        //        case WeaponFlags.CrushThrough:
        //            return WeaponFlags.TwoHandIdleOnMount;
        //        case WeaponFlags.TwoHandIdleOnMount:
        //            return WeaponFlags.NoBlood;
        //        case WeaponFlags.NoBlood:
        //            return WeaponFlags.PenaltyWithShield;
        //        case WeaponFlags.PenaltyWithShield:
        //            return WeaponFlags.MissileWithPhysics;
        //        case WeaponFlags.MissileWithPhysics:
        //            return WeaponFlags.MultiplePenetration;
        //        case WeaponFlags.MultiplePenetration:
        //            return WeaponFlags.CanKnockDown;
        //        case WeaponFlags.CanKnockDown:
        //            return WeaponFlags.CanBlockRanged;
        //        case WeaponFlags.CanBlockRanged:
        //            return WeaponFlags.LeavesTrail;
        //        case WeaponFlags.LeavesTrail:
        //            return WeaponFlags.UseHandAsThrowBase;
        //        case WeaponFlags.UseHandAsThrowBase:
        //            return WeaponFlags.AmmoBreaksOnBounceBack;
        //        case WeaponFlags.AmmoBreaksOnBounceBack:
        //            return WeaponFlags.AmmoCanBreakOnBounceBack;
        //        case WeaponFlags.AmmoCanBreakOnBounceBack:
        //            return WeaponFlags.AmmoBreakOnBounceBackMask;
        //        case WeaponFlags.AmmoBreakOnBounceBackMask:
        //            return WeaponFlags.AmmoSticksWhenShot;
        //            case WeaponFlags.A


        //    }
        //}
    }
}
