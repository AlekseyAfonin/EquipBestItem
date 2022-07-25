using System.Collections.Generic;
using EquipBestItem.Models.Entities;

namespace EquipBestItem.XmlRepository;

internal abstract class RepositoryBase<T> : IRepository<T> where T : BaseEntity
{
    internal RepositoryBase()
    {
        var type = typeof(T);
        TypeName = type.Name;
    }

    protected string TypeName { get; }

    public abstract void Create(T entity);

    public abstract T Read(string key);

    public abstract void Update(T entity);

    public abstract void Delete(string key);

    public abstract IEnumerable<T> ReadAll();

    public abstract bool Exists(string key);
}