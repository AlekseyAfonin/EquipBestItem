using System.Collections.Generic;
using EquipBestItem.Models;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace EquipBestItem.ViewModels;

internal sealed partial class ModSPInventoryVM : ViewModel
{
    private SPInventoryVM _vm;

    private CharacterFilterWeightsRepository _characterFilterWeightsRepository;
    
    public ModSPInventoryVM(SPInventoryVM vm)
    {
        Dictionary<string, FilterWeights> dictionary = new Dictionary<string, FilterWeights>
        {
            {
                "default", new FilterWeights {Weight = 0, ArmArmor = 0, BodyArmor = 0, HeadArmor = 0}
            },
            {
                "default_civil", new FilterWeights {Weight = 1, ArmArmor = 1, BodyArmor = 1, HeadArmor = 1}
            }
        };
        
        _characterFilterWeightsRepository = new CharacterFilterWeightsRepository(dictionary);
        _vm = vm;
    }
    
    public void ExecuteShowFilterSettings(string equipmentIndexName)
    {
        var equipmentIndex = Helper.ParseEnum<EquipmentIndex>(equipmentIndexName);
        
        ShowSettings(equipmentIndex);
    }
    
    private void ShowSettings(EquipmentIndex equipmentIndex)
    {
        switch (equipmentIndex)
        {
            case EquipmentIndex.Head :
                InformationManager.DisplayMessage(new InformationMessage($"ShowSettings EquipmentIndex.Head "));
                break;
            case EquipmentIndex.Cape :
                InformationManager.DisplayMessage(new InformationMessage($"ShowSettings EquipmentIndex.Cape"));
                break;
            default:
                InformationManager.DisplayMessage(new InformationMessage($"ShowSettings wrong EquipmentIndex"));
                break;
        }
    }
}