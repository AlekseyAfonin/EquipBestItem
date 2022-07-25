using System.Collections.Generic;
using EquipBestItem.Models.Entities;
using EquipBestItem.XmlRepository;

namespace EquipBestItem.Models;

internal class CharacterCoefficientsRepository
{
    private readonly IRepository<CharacterCoefficients> _repository;

    internal CharacterCoefficientsRepository(IRepository<CharacterCoefficients> repository)
    {
        _repository = repository;
    }

    internal void Create(CharacterCoefficients entity)
    {
        _repository.Create(entity);
    }

    internal CharacterCoefficients Read(string name)
    {
        return _repository.Read(name);
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
        return _repository.ReadAll();
    }
}