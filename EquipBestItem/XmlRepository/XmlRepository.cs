using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using EquipBestItem.Models.Entities;

namespace EquipBestItem.XmlRepository;

internal sealed class XmlRepository<T> : RepositoryBase<T> where T : BaseEntity
{
    private readonly string _storagePath;
    private readonly string _storageFolder;

    internal XmlRepository(string storageFolder)
    {
        _storageFolder = storageFolder;
        _storagePath = $"{_storageFolder}{TypeName}.xml";
        LoadItems();
    }
    
    protected override void LoadItems()
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

    protected override void SaveChanges()
    {
        if (!Directory.Exists(_storageFolder)) Directory.CreateDirectory(_storageFolder);
    
        var writer = new StreamWriter(_storagePath, false);
        var serializer = new XmlSerializer(typeof(List<T>));
        serializer.Serialize(writer, Entities);
        writer.Close();
    }
}