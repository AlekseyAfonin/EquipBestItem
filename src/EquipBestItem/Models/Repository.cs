using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TaleWorlds.Engine;
using TaleWorlds.Library;


namespace EquipBestItem.Models;

public sealed class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly Dictionary<string, T> _repository;

    public Repository(Dictionary<string, T> repository)
    {
        _repository = repository;
    }

    public IEnumerable<T> GetAll()
    {
        return _repository.Values.AsEnumerable();
    }

    public T GetByKey(string key)
    {
        return _repository[key];
    }

    public void Insert(T entity)
    {
        _repository.Add(entity.Key, entity);
    }

    public void Delete(T entity)
    {
        _repository.Remove(entity.Key);
    }

    public void Update(T entity)
    {
        _repository[entity.Key] = entity;
    }

    public void Save()
    {
        PlatformFilePath platformFilePath =
            new(new PlatformDirectoryPath(EngineFilePaths.ConfigsPath.Type,
                    $"{EngineFilePaths.ConfigsPath.Path}/ModSettings/EquipBestItem/"), 
                $"{typeof(T).Name}.json");
        var jsonObj = JsonConvert.SerializeObject(_repository, Formatting.Indented);
        FileHelper.SaveFileString(platformFilePath, jsonObj);
    }
}