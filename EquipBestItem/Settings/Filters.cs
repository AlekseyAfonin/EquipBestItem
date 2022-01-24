using System;
using TaleWorlds.Core;

namespace EquipBestItem.Settings
{
	public class Filters
	{
		private const float Tolerance = 0.00000000001f;

		private FilterElement[] _filterElements;

		public FilterElement[] FilterElement
		{
			get => _filterElements;
			set => _filterElements = value;
		}

		public Filters()
		{
			_filterElements = new FilterElement[12];

			for (int i = 0; i < 12; i++)
			{
				_filterElements[i] = new FilterElement();
			}
		}

		public Filters(bool isDefault)
		{
			_filterElements = new FilterElement[12];

			for (int i = 0; i < 12; i++)
			{
				_filterElements[i] = new FilterElement();
			}

			for (EquipmentIndex index = EquipmentIndex.WeaponItemBeginSlot; index < EquipmentIndex.NumEquipmentSetSlots; index++)
			{
				if (index >= EquipmentIndex.WeaponItemBeginSlot && index < EquipmentIndex.NumAllWeaponSlots)
				{
					this[index].MaxDataValue = 100f;
					this[index].ThrustSpeed = 100f;
					this[index].SwingSpeed = 100f;
					this[index].MissileSpeed = 100f;
					this[index].WeaponLength = 100f;
					this[index].ThrustDamage = 100f;
					this[index].SwingDamage = 100f;
					this[index].Accuracy = 100f;
					this[index].Handling = 100f;
					this[index].Weight = 100f;
					this[index].WeaponBodyArmor = 100f;
				}
				else if (index == EquipmentIndex.Head)
				{
					this[index].HeadArmor = 100f;
					this[index].Weight = 100f;
				}
				else if (index == EquipmentIndex.Cape)
				{
					this[index].ArmorBodyArmor = 100f;
					this[index].Weight = 100f;
				}
				else if (index == EquipmentIndex.Body)
				{
					this[index].HeadArmor = 100f;
					this[index].ArmorBodyArmor = 100f;
					this[index].ArmArmor = 100f;
					this[index].LegArmor = 100f;
					this[index].Weight = 100f;
				}
				else if (index == EquipmentIndex.Leg)
				{
					this[index].LegArmor = 100f;
					this[index].Weight = 100f;
				}
				else if (index == EquipmentIndex.Gloves)
				{
					this[index].ArmArmor = 100f;
					this[index].Weight = 100f;
				}
				else if (index == EquipmentIndex.Horse)
				{
					this[index].ChargeDamage = 100f;
					this[index].HitPoints = 100f;
					this[index].Maneuver = 100f;
					this[index].Speed = 100f;
				}
				else if (index == EquipmentIndex.HorseHarness)
				{
					this[index].ArmorBodyArmor = 100f;
					this[index].ManeuverBonus = 100f;
					this[index].SpeedBonus = 100f;
					this[index].ChargeBonus = 100f;
					this[index].Weight = 100f;
				}
			}
		}

		public FilterElement this[int index]
		{
			get => _filterElements[index];
			set => _filterElements[index] = value;
		}

		public FilterElement this[EquipmentIndex index]
		{
			get => _filterElements[(int)index];
			set => this[(int)index] = value;
		}

		public bool IsDefault(EquipmentIndex index, bool isInWarSet)
		{
			Filters defaultFilters = SettingsLoader.Instance.GetCharacterSettingsByName("default_equipbestitem", isInWarSet).Filters;

			if (index >= EquipmentIndex.WeaponItemBeginSlot && index < EquipmentIndex.NumAllWeaponSlots)
			{
				if (Math.Abs(this[index].MaxDataValue - defaultFilters[index].MaxDataValue) > Tolerance) return false;
				if (Math.Abs(this[index].ThrustSpeed - defaultFilters[index].ThrustSpeed) > Tolerance) return false;
				if (Math.Abs(this[index].SwingSpeed - defaultFilters[index].SwingSpeed) > Tolerance) return false;
				if (Math.Abs(this[index].MissileSpeed - defaultFilters[index].MissileSpeed) > Tolerance) return false;
				if (Math.Abs(this[index].WeaponLength - defaultFilters[index].WeaponLength) > Tolerance) return false;
				if (Math.Abs(this[index].ThrustDamage - defaultFilters[index].ThrustDamage) > Tolerance) return false;
				if (Math.Abs(this[index].SwingDamage - defaultFilters[index].SwingDamage) > Tolerance) return false;
				if (Math.Abs(this[index].Accuracy - defaultFilters[index].Accuracy) > Tolerance) return false;
				if (Math.Abs(this[index].Handling - defaultFilters[index].Handling) > Tolerance) return false;
				if (Math.Abs(this[index].Weight - defaultFilters[index].Weight) > Tolerance) return false;
				if (Math.Abs(this[index].WeaponBodyArmor - defaultFilters[index].WeaponBodyArmor) > Tolerance) return false;
			}
			else if (index >= EquipmentIndex.ArmorItemBeginSlot && index < EquipmentIndex.ArmorItemEndSlot)
			{
				if (Math.Abs(this[index].HeadArmor - defaultFilters[index].HeadArmor) > Tolerance) return false;
				if (Math.Abs(this[index].ArmorBodyArmor - defaultFilters[index].ArmorBodyArmor) > Tolerance) return false;
				if (Math.Abs(this[index].LegArmor - defaultFilters[index].LegArmor) > Tolerance) return false;
				if (Math.Abs(this[index].ArmArmor - defaultFilters[index].ArmArmor) > Tolerance) return false;
				if (Math.Abs(this[index].Weight - defaultFilters[index].Weight) > Tolerance) return false;

			}
			else if (index == EquipmentIndex.Horse)
			{
				if (Math.Abs(this[index].ChargeDamage - defaultFilters[index].ChargeDamage) > Tolerance) return false;
				if (Math.Abs(this[index].HitPoints - defaultFilters[index].HitPoints) > Tolerance) return false;
				if (Math.Abs(this[index].Maneuver - defaultFilters[index].Maneuver) > Tolerance) return false;
				if (Math.Abs(this[index].Speed - defaultFilters[index].Speed) > Tolerance) return false;
			}
			else if (index == EquipmentIndex.HorseHarness)
			{
				if (Math.Abs(this[index].ArmorBodyArmor - defaultFilters[index].ArmorBodyArmor) > Tolerance) return false;
				if (Math.Abs(this[index].ManeuverBonus - defaultFilters[index].ManeuverBonus) > Tolerance) return false;
				if (Math.Abs(this[index].SpeedBonus - defaultFilters[index].SpeedBonus) > Tolerance) return false;
				if (Math.Abs(this[index].ChargeBonus - defaultFilters[index].ChargeBonus) > Tolerance) return false;
				if (Math.Abs(this[index].Weight - defaultFilters[index].Weight) > Tolerance) return false;
			}
			return true;
		}

		public bool IsLocked(EquipmentIndex index)
		{
			if (index >= EquipmentIndex.WeaponItemBeginSlot && index < EquipmentIndex.NumAllWeaponSlots)
			{
				if (this[index].MaxDataValue != 0) return false;
				if (this[index].ThrustSpeed != 0) return false;
				if (this[index].SwingSpeed != 0) return false;
				if (this[index].MissileSpeed != 0) return false;
				if (this[index].WeaponLength != 0) return false;
				if (this[index].ThrustDamage != 0) return false;
				if (this[index].SwingDamage != 0) return false;
				if (this[index].Accuracy != 0) return false;
				if (this[index].Handling != 0) return false;
				if (this[index].Weight != 0) return false;
				if (this[index].WeaponBodyArmor != 0) return false;
			}
			else if (index >= EquipmentIndex.ArmorItemBeginSlot && index < EquipmentIndex.ArmorItemEndSlot)
			{
				if (this[index].HeadArmor != 0) return false;
				if (this[index].ArmorBodyArmor != 0) return false;
				if (this[index].LegArmor != 0) return false;
				if (this[index].ArmArmor != 0) return false;
				if (this[index].Weight != 0) return false;

			}
			else if (index == EquipmentIndex.Horse)
			{
				if (this[index].ChargeDamage != 0) return false;
				if (this[index].HitPoints != 0) return false;
				if (this[index].Maneuver != 0) return false;
				if (this[index].Speed != 0) return false;
			}
			else if (index == EquipmentIndex.HorseHarness)
			{
				if (this[index].ArmorBodyArmor != 0) return false;
				if (this[index].ManeuverBonus != 0) return false;
				if (this[index].SpeedBonus != 0) return false;
				if (this[index].ChargeBonus != 0) return false;
				if (this[index].Weight != 0) return false;
			}
			return true;
		}

		public void SetDefault(EquipmentIndex index, bool isInWarSet)
		{
			Filters defaultFilters = SettingsLoader.Instance.GetCharacterSettingsByName("default_equipbestitem", isInWarSet).Filters;

			if (index >= EquipmentIndex.WeaponItemBeginSlot && index < EquipmentIndex.NumAllWeaponSlots)
			{
				this[index].MaxDataValue = defaultFilters[index].MaxDataValue;
				this[index].ThrustSpeed = defaultFilters[index].ThrustSpeed;
				this[index].SwingSpeed = defaultFilters[index].SwingSpeed;
				this[index].MissileSpeed = defaultFilters[index].MissileSpeed;
				this[index].WeaponLength = defaultFilters[index].WeaponLength;
				this[index].ThrustDamage = defaultFilters[index].ThrustDamage;
				this[index].SwingDamage = defaultFilters[index].SwingDamage;
				this[index].Accuracy = defaultFilters[index].Accuracy;
				this[index].Handling = defaultFilters[index].Handling;
				this[index].Weight = defaultFilters[index].Weight;
				this[index].WeaponBodyArmor = defaultFilters[index].WeaponBodyArmor;
			}
			else if (index == EquipmentIndex.Head)
			{
				this[index].HeadArmor = defaultFilters[index].HeadArmor;
				this[index].Weight = defaultFilters[index].Weight;
			}
			else if (index == EquipmentIndex.Cape)
			{
				this[index].ArmorBodyArmor = defaultFilters[index].ArmorBodyArmor;
				this[index].Weight = defaultFilters[index].Weight;
			}
			else if (index == EquipmentIndex.Body)
			{
				this[index].HeadArmor = defaultFilters[index].HeadArmor;
				this[index].ArmorBodyArmor = defaultFilters[index].ArmorBodyArmor;
				this[index].LegArmor = defaultFilters[index].LegArmor;
				this[index].ArmArmor = defaultFilters[index].ArmArmor;
				this[index].Weight = defaultFilters[index].Weight;
			}
			else if (index == EquipmentIndex.Leg)
			{
				this[index].LegArmor = defaultFilters[index].LegArmor;
				this[index].Weight = defaultFilters[index].Weight;
			}
			else if (index == EquipmentIndex.Gloves)
			{
				this[index].ArmArmor = defaultFilters[index].ArmArmor;
				this[index].Weight = defaultFilters[index].Weight;
			}
			else if (index == EquipmentIndex.Horse)
			{
				this[index].ChargeDamage = defaultFilters[index].ChargeDamage;
				this[index].HitPoints = defaultFilters[index].HitPoints;
				this[index].Maneuver = defaultFilters[index].Maneuver;
				this[index].Speed = defaultFilters[index].Speed;
			}
			else if (index == EquipmentIndex.HorseHarness)
			{
				this[index].ArmorBodyArmor = defaultFilters[index].ArmorBodyArmor;
				this[index].ManeuverBonus = defaultFilters[index].ManeuverBonus;
				this[index].SpeedBonus = defaultFilters[index].SpeedBonus;
				this[index].ChargeBonus = defaultFilters[index].ChargeBonus;
				this[index].Weight = defaultFilters[index].Weight;
			}
		}

		public void SetLock(EquipmentIndex index)
		{
			if (index >= EquipmentIndex.WeaponItemBeginSlot && index < EquipmentIndex.NumAllWeaponSlots)
			{
				this[index].MaxDataValue = 0f;
				this[index].ThrustSpeed = 0f;
				this[index].SwingSpeed = 0f;
				this[index].MissileSpeed = 0f;
				this[index].WeaponLength = 0f;
				this[index].ThrustDamage = 0f;
				this[index].SwingDamage = 0f;
				this[index].Accuracy = 0f;
				this[index].Handling = 0f;
				this[index].Weight = 0f;
				this[index].WeaponBodyArmor = 0f;
			}
			else if (index == EquipmentIndex.Head)
			{
				this[index].HeadArmor = 0f;
				this[index].Weight = 0f;
			}
			else if (index == EquipmentIndex.Cape)
			{
				this[index].ArmorBodyArmor = 0f;
				this[index].Weight = 0f;
			}
			else if (index == EquipmentIndex.Leg)
			{
				this[index].LegArmor = 0f;
				this[index].Weight = 0f;
			}
			else if (index == EquipmentIndex.Gloves)
			{
				this[index].ArmArmor = 0f;
				this[index].Weight = 0f;
			}
			else if (index == EquipmentIndex.Body)
			{
				this[index].HeadArmor = 0f;
				this[index].ArmorBodyArmor = 0f;
				this[index].LegArmor = 0f;
				this[index].ArmArmor = 0f;
				this[index].Weight = 0f;
			}
			else if (index == EquipmentIndex.Horse)
			{
				this[index].ChargeDamage = 0f;
				this[index].HitPoints = 0f;
				this[index].Maneuver = 0f;
				this[index].Speed = 0f;
			}
			else if (index == EquipmentIndex.HorseHarness)
			{
				this[index].ArmorBodyArmor = 0f;
				this[index].ManeuverBonus = 0f;
				this[index].SpeedBonus = 0f;
				this[index].ChargeBonus = 0f;
				this[index].Weight = 0f;
			}
		}
	}
}
