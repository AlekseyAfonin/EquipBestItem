using System;
using System.Diagnostics;
using System.IO;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

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
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
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
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
            return null;
        }

        public static string SavePath
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Mount and Blade II Bannerlord\\EquipBestItem\\";
            }
        }
        
        public static string LogPath
        {
            get
            {
                return Helper.SavePath + "EquipBestItem_log.txt";
            }
        }
        
        public static void ClearLog()
        {
            string logPath = Helper.LogPath;
            if (File.Exists(logPath))
            {
                File.Delete(logPath);
            }
        }
        
        public static void Log(string text)
        {
            File.AppendAllText(Helper.LogPath, text + "\n");
        }
        

    }
}
