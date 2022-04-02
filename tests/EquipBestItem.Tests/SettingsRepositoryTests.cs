using System.Collections.Generic;
using System.Linq;
using EquipBestItem.Models;
using NUnit.Framework;
using Moq;

namespace EquipBestItem.Tests;

public class SettingsRepositoryTests
{
    private Dictionary<string, Settings> _fakeSettings;

    [SetUp]
    public void Setup()
    {
        _fakeSettings = new Dictionary<string, Settings>()
        {
            {"test1", new Settings() {Key = "test1", Value = true}},
            {"test2", new Settings() {Key = "test2", Value = false}}
        };
    }

    [Test]
    public void GetAll_Returns_Equal()
    {
        var fakeSettingsRepository = new Repository<Settings>(_fakeSettings);
        var result = fakeSettingsRepository.GetAll();
        Assert.AreEqual(result, _fakeSettings.Values);
    }
    
    [Test]
    public void Insert_AddedNewEntity()
    {
        var fakeSettingsRepository = new Repository<Settings>(_fakeSettings);
        fakeSettingsRepository.Insert(new Settings() {Key = "test3", Value = true});
        Assert.AreEqual(true, fakeSettingsRepository.GetAll().First(kv => kv.Key == "test3").Value);
    }
    
    [Test]
    public void Update_EntityChanged()
    {
        var fakeSettingsRepository = new Repository<Settings>(_fakeSettings);
        var result = fakeSettingsRepository.GetAll().FirstOrDefault(kv => kv.Key == "test2")?.Value;
        Assert.AreEqual(false, result);
        var newEntityValue = new Settings() {Key = "test2", Value = true};
        fakeSettingsRepository.Update(newEntityValue);
        result = fakeSettingsRepository.GetAll().FirstOrDefault(kv => kv.Key == "test2")?.Value;
        Assert.AreEqual(true, result);
    }
    
    [Test]
    public void Delete_EntityDeleted()
    {
        var fakeSettingsRepository = new Repository<Settings>(_fakeSettings);
        var result = fakeSettingsRepository.GetAll().FirstOrDefault(kv => kv.Key == "test2")?.Value;
        Assert.AreEqual(false, result);
        var newEntityValue = new Settings() {Key = "test2", Value = true};
        fakeSettingsRepository.Delete(newEntityValue);
        result = fakeSettingsRepository.GetAll().FirstOrDefault(kv => kv.Key == "test2")?.Value;
        Assert.IsNull(result);
    }
}