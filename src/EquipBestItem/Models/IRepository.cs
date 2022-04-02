using System.Collections.Generic;

namespace EquipBestItem.Models;

public interface IRepository<T> where T : BaseEntity
{  
    IEnumerable<T> GetAll();
    void Insert(T entity);
    void Delete(T entity);        
    void Update(T entity);
    void Save();
}