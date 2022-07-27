using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using EquipBestItem.Models.Entities;

namespace EquipBestItem.XmlRepository;

internal sealed class XmlRepository<T> : RepositoryBase<T> where T : BaseEntity, new()
{
    private readonly string _storagePath;

    internal XmlRepository(string storagePath)
    {
        Entities = new List<T>();
        _storagePath = $"{storagePath}{TypeName}.xml";
        LoadItems();
    }

    private List<T> Entities { get; set; }

    public override void Create(T entity)
    {
        Entities.Add(entity);
        SaveChanges();
    }

    public override T Read(string key)
    {
        return Entities.First(e => e.Key == key);
    }

    public override IEnumerable<T> ReadAll()
    {
        return Entities;
    }

    public override void Update(T entity)
    {
        var index = Entities.FindIndex(x => x.Key == entity.Key);
        
        if (index >= 0) Entities[index] = entity;

        SaveChanges();
    }

    public override void Delete(string key)
    {
        var index = Entities.FindIndex(x => x.Key == key);
        
        if (index < 0) return;
        
        Entities.RemoveAt(index);
        SaveChanges();
    }

    public override bool Exists(string key)
    {
        return Entities.Exists(x => x.Key == key);
    }

    public override void Create(IEnumerable<T> entities)
    {
        try
        {
            if (entities == null) throw new ArgumentNullException();

            foreach (var entity in entities)
            {
                Entities.Add(entity);
            }
            
            SaveChanges();
        }
        catch (Exception e)
        {
            Helper.ShowMessage($"Entities create exception: {e.Message}");
            throw;
        }
    }

    private void LoadItems()
    {
        try
        {
            if (!File.Exists(_storagePath)) return;

            using var fileStream = new FileStream(_storagePath, FileMode.Open);
            using var streamReader = new StreamReader(fileStream);
            Entities = (List<T>) new XmlSerializer(typeof(List<T>)).Deserialize(streamReader);
        }
        catch
        {
            // ignored
        }
    }

    private void SaveChanges()
    {
        var writer = new StreamWriter(_storagePath, false);
        var serializer = new XmlSerializer(typeof(List<T>));
        serializer.Serialize(writer, Entities);
        writer.Close();
    }
}