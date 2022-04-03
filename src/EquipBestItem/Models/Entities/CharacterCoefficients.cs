using System;
using TaleWorlds.Core;

namespace EquipBestItem.Models.Entities;

[Serializable]
public class CharacterCoefficients : BaseEntity
{
    public Coefficients[] WarCoefficients { get; set; } = new Coefficients[(int)EquipmentIndex.NumEquipmentSetSlots];
    public Coefficients[] CivilCoefficients { get; set; } = new Coefficients[(int)EquipmentIndex.NumEquipmentSetSlots];
}