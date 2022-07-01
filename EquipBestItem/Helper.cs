using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace EquipBestItem;

internal static class Helper
{
    /// <summary>
    /// 1
    /// </summary>
    /// <param name="value"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    internal static T ParseEnum<T>(string value)
    {
        return (T) Enum.Parse(typeof(T), value, true);
    }
    
    internal static object? GetMethod(this object o, string methodName, params object[] args)
    {
        var mi = o.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
        if (mi == null) return null;
        try
        {
            return mi.Invoke(o, args);
        }
        catch
        {
            throw new MBException(methodName + " GetField() exception");
        }
    }
    
    internal static object? GetField(this object o, string fieldName)
    {
        var mi = o.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
        if (mi == null) return null;
        
        try
        {
            return mi.GetValue(o);
        }
        catch
        {
            throw new MBException(fieldName + " GetField() exception");
        }
    }
    
    internal static Dictionary<string, T>? Deserialize<T>()
    {
        PlatformFilePath platformFilePath =
            new(new PlatformDirectoryPath(EngineFilePaths.ConfigsPath.Type,
                    $"{EngineFilePaths.ConfigsPath.Path}/ModSettings/EquipBestItem/"),
                $"{typeof(T).Name}.json");
        
        var jsonString = FileHelper.GetFileContentString(platformFilePath);
        if (jsonString is null) return null;
        var repository = JsonConvert.DeserializeObject<Dictionary<string, T>>(jsonString);
        
        return repository;
    }
}