using System.Diagnostics.CodeAnalysis;

namespace EquipBestItem.Models.Entities;

[SuppressMessage("ReSharper", "PropertyCanBeMadeInitOnly.Global")]
public class CharacterCoefficients : BaseEntity
{
    public const string Default = "default";
    public Coefficients[] WarCoefficients { get; set; } = null!;
    public Coefficients[] CivilCoefficients { get; set; } = null!;
}