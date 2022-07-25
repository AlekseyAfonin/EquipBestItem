namespace EquipBestItem.Models.Entities;

public class CharacterCoefficients : BaseEntity
{
    public const string Default = "default";
    public Coefficients[] WarCoefficients { get; set; }
    public Coefficients[] CivilCoefficients { get; set; }
}