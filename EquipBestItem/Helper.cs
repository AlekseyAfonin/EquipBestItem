using System;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace EquipBestItem
{
    public static class Helper
    {
        internal static object Call(this object o, string methodName, params object[] args)
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
                    //Debug.WriteLine(e.Message);
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
                    //Debug.WriteLine(e.Message);
                }
            }
            return null;
        }

        public static void Serialize<T>(string filename, T data)
        {
            TextWriter writer = null;
            try
            {
                writer = new StreamWriter(filename);

                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(writer, data);
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }


        public static T Deserialize<T>(string filename)
        {
            TextReader reader = null;
            T data = default(T);
            try
            {
                reader = new StreamReader(filename);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                data = (T)serializer.Deserialize(reader);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return data;
        }
    }
}
