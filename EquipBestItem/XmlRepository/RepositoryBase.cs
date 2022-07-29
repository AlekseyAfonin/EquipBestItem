using System;
using System.Collections.Generic;
using System.Linq;
using EquipBestItem.Models.Entities;

namespace EquipBestItem.XmlRepository;

internal abstract class RepositoryBase<T> : IRepository<T> where T : BaseEntity
{
    internal RepositoryBase()
    {
        Entities = new List<T>();
        var type = typeof(T);
        TypeName = type.Name;
    }

    protected List<T> Entities { get; set; }
    protected string TypeName { get; }

    public virtual void Create(T entity)
    {
        Entities.Add(entity);
        SaveChanges();
    }

    public virtual T Read(string key)
    {
        return Entities.First(e => e.Key == key);
    }

    public virtual void Update(T entity)
    {
        var index = Entities.FindIndex(x => x.Key == entity.Key);
        
        if (index >= 0) Entities[index] = entity;

        SaveChanges();
    }

    public virtual void Delete(string key)
    {
        var index = Entities.FindIndex(x => x.Key == key);
        
        if (index < 0) return;
        
        Entities.RemoveAt(index);
        SaveChanges();
    }

    public virtual IEnumerable<T> ReadAll()
    {
        return Entities;
    }

    public virtual void Create(IEnumerable<T> entities)
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

    public virtual bool Exists(string key)
    {
        return Entities.Exists(x => x.Key == key);
    }

    protected abstract void LoadItems();

    protected abstract void SaveChanges();
}