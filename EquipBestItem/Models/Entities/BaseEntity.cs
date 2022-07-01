using SharpRepository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipBestItem.Models.Entities
{
    public class BaseEntity
    {
        [RepositoryPrimaryKey]
        public string Name { get; init; } = default!;
    }
}
