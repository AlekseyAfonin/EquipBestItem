using System;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using TaleWorlds.Core;
using TaleWorlds.Library;
using StringReader = System.IO.StringReader;
using StringWriter = System.IO.StringWriter;

namespace EquipBestItem;

public static class Helper
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T ParseEnum<T>(string value)
    {
        return (T) Enum.Parse(typeof(T), value, true);
    }
    
    public static void Serialize<T>(PlatformFilePath platformFilePath, T data)
    {
        var ns = new XmlSerializerNamespaces();
        ns.Add("", "");

        try
        {
            var serializer = new XmlSerializer(typeof(T));
            var stringWriter = new StringWriter();
            serializer.Serialize(stringWriter, data);

            FileHelper.SaveFileString(platformFilePath, stringWriter.ToString());
        }
        catch
        {
            throw new MBException(platformFilePath.FileName + " serialize error");
        }
    }


    public static T Deserialize<T>(PlatformFilePath platformFilePath)
    {
        var fileString = FileHelper.GetFileContentString(platformFilePath);
        var data = default(T);

        try
        {
            StringReader stringReader;
            using (stringReader = new StringReader(fileString))
            {
                var xmlReader = XmlReader.Create(stringReader);
                var serializer = new XmlSerializer(typeof(T));

                if (serializer.CanDeserialize(xmlReader)) data = (T) serializer.Deserialize(xmlReader);
            }
        }
        catch (Exception e)
        {
            throw new MBException(platformFilePath.FileName + " " + e.Message);
        }

        return data;
    }
}