using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace EquipBestItem.Models.Entities;

public class CalculatorContext
{
    public EquipmentIndex EquipmentIndex;
    public CharacterObject Character = null!;
    public bool IsInWarSet;
}