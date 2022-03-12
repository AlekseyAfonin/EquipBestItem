using System;
using System.Collections.Generic;
using System.Linq;

namespace EquipBestItem.Models;

internal sealed class CharacterFilterWeightsRepository : IRepository
{
    private IDictionary<string, FilterWeights> CharacterFilterWeights { get; }

    public CharacterFilterWeightsRepository(IDictionary<string, FilterWeights> characterFilterWeights)
    {
        CharacterFilterWeights = characterFilterWeights;
    }

    public IDictionary<string, FilterWeights> GetCharacterFilterWeights()
    {
        return CharacterFilterWeights;
    }

    public FilterWeights GetFilterWeightsByCharacterName(string key)
    {
        return CharacterFilterWeights[key];
    }

    public void Insert(string key, FilterWeights value)
    {
        CharacterFilterWeights.Add(key, value);
    }

    public void Delete(string key)
    {
        CharacterFilterWeights.Remove(key);
    }

    public void Update(string key, FilterWeights value)
    {
        CharacterFilterWeights[key] = value;
    }

    public void Save()
    {
        //throw new NotImplementedException();
    }
}