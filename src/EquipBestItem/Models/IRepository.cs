using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;

namespace EquipBestItem.Models;

internal interface IRepository
{
    IDictionary<string, FilterWeights> GetCharacterFilterWeights();        
    FilterWeights GetFilterWeightsByCharacterName(string characterName);        
    void Insert(string key, FilterWeights value);
    void Delete(string key);        
    void Update(string key, FilterWeights value);
    void Save();
}