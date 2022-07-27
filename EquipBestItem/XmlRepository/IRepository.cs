using System.Collections.Generic;
using EquipBestItem.Models.Entities;

namespace EquipBestItem.XmlRepository;

internal interface IRepository<T> where T : BaseEntity
{
    void Create(T entity);

    T Read(string key);

    void Update(T entity);

    void Delete(string key);

    IEnumerable<T> ReadAll();

    void Create(IEnumerable<T> entities);

    bool Exists(string key);
}