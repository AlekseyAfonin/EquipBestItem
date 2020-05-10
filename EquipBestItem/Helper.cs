using System.IO;
using System.Xml;
using System.Xml.Serialization;
using TaleWorlds.Core;

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

        public static void Serialize<T>(string fileName, T data)
        {
            TextWriter writer = null;
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            try
            {
                writer = new StreamWriter(fileName);

                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(writer, data, ns);
            }
            catch
            {
                throw new MBException(fileName + " serialize error");
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }


        public static T Deserialize<T>(string fileName)
        {
            XmlReader xmlReader = null;
            StreamReader streamReader = null;
            T data = default(T);
            try
            {
                using (streamReader = new StreamReader(fileName))
                {
                    xmlReader = XmlReader.Create(streamReader);
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    data = (T)serializer.Deserialize(xmlReader);
                }
            }
            catch
            {
                throw new MBException(fileName + " deserialize error");
            }
            finally
            {
                if (xmlReader != null)
                {
                    xmlReader.Close();
                }
            }
            return data;
        }
    }
}
