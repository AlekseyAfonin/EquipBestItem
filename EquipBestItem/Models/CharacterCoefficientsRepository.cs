using EquipBestItem.Models.Entities;
using SharpRepository.Repository;
using System.Collections;
using System.Collections.Generic;

namespace EquipBestItem.Models;

internal class CharacterCoefficientsRepository
{
    private readonly IRepository<CharacterCoefficients, string> _repository;

    internal CharacterCoefficientsRepository(IRepository<CharacterCoefficients, string> repository)
    {
        _repository = repository;
    }

    internal void Create(CharacterCoefficients entity)
    {
        _repository.Add(entity);
    }

    internal CharacterCoefficients Read(string name)
    {
        return _repository.Get(name);
    }

    internal void Update(CharacterCoefficients entity)
    {
        _repository.Update(entity);
    }

    internal void Delete(string name)
    {
        _repository.Delete(name);
    }
    
    internal IEnumerable<CharacterCoefficients> ReadAll()
    {
        return _repository.GetAll();
    }
}