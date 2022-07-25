using System.Collections.Generic;
using EquipBestItem.Models.Entities;
using EquipBestItem.XmlRepository;

namespace EquipBestItem.Models;

internal class SettingsRepository
{
    private readonly IRepository<Settings> _repository;

    internal SettingsRepository(IRepository<Settings> repository)
    {
        _repository = repository;
    }

    internal void Create(Settings entity)
    {
        _repository.Create(entity);
    }

    internal Settings Read(string name)
    {
        return _repository.Read(name);
    }

    internal void Update(Settings entity)
    {
        _repository.Update(entity);
    }

    internal void Delete(string name)
    {
        _repository.Delete(name);
    }

    internal IEnumerable<Settings> ReadAll()
    {
        return _repository.ReadAll();
    }
}