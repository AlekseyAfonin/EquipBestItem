using System;
using TaleWorlds.Core;
using SharpRepository.Repository;

namespace EquipBestItem.Models.Entities;

public class CharacterCoefficients : BaseEntity
{
    public Coefficients[] WarCoefficients { get; set; }
    public Coefficients[] CivilCoefficients { get; set; }

    public const string Default = "default";
}