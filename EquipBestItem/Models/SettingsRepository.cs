using EquipBestItem.Models.Entities;
using SharpRepository.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipBestItem.Models
{
    internal class SettingsRepository
    {
        private readonly IRepository<Settings, string> _repository;

        internal SettingsRepository(IRepository<Settings, string> repository)
        {
            _repository = repository;
        }

        internal void Create(Settings entity)
        {
            _repository.Add(entity);
        }

        internal Settings Read(string name)
        {
            return _repository.Get(name);
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
            return _repository.GetAll();
        }
    }
}
