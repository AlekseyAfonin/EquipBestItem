using System;
using System.Xml;
using System.Xml.Serialization;
using TaleWorlds.Core;
using TaleWorlds.Library;
using StringReader = System.IO.StringReader;
using StringWriter = System.IO.StringWriter;

namespace EquipBestItem
{
    public static class Helper
    {

        internal static object GetMethod(this object o, string methodName, params object[] args)
        {
            var mi = o.GetType().GetMethod(methodName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (mi != null)
            {
                try
                {
                    return mi.Invoke(o, args);
                }
                catch
                {
                    throw new MBException(methodName + " GetField() exception");
                }
            }
            return null;
        }

        internal static object GetField(this object o, string fieldName)
        {
            var mi = o.GetType().GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (mi != null)
            {
                try
                {
                    return mi.GetValue(o);
                }
                catch
                {
                    throw new MBException(fieldName + " GetField() exception");
                }
            }
            return null;
        }

        public static void Serialize<T>(PlatformFilePath platformFilePath, T data)
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
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
            string fileString = FileHelper.GetFileContentString(platformFilePath);
            T data = default (T);
            
            try
            {
                StringReader stringReader;
                using (stringReader = new StringReader(fileString))
                {
                    var xmlReader = XmlReader.Create(stringReader);
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    
                    if (serializer.CanDeserialize(xmlReader))
                    {
                        data = (T)serializer.Deserialize(xmlReader);
                    }
                }
            }
            catch (Exception e)
            {
                throw new MBException(platformFilePath.FileName + " " + e.Message);
            }
            
            return data;
        }

    }
}
