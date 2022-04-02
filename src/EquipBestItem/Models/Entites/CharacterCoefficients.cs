using System;

namespace EquipBestItem.Models;

[Serializable]
public class CharacterCoefficients : BaseEntity
{
    public Coefficients[] WarCoefficients { get; set; }
    public Coefficients[] CivilCoefficients { get; set; }
}