using System;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace EquipBestItem.Models;


public abstract class BaseEntity
{
    [JsonIgnore]
    public string Key { get; init; }
}