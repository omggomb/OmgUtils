using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OmgUtils.ApplicationSettingsClass
{
    /// <summary>
    /// Settings management using serialization. Can be used with the *Setting classes.
    /// </summary>
    [Serializable]
    public abstract class SerializedSettingsManager
    {
        public static void SaveSettings<T>(T settingsManager, string filePath) where T : SerializedSettingsManager
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            XmlSerializer writer = new XmlSerializer(typeof(T));

            FileStream file = File.Create(filePath);

            writer.Serialize(file, settingsManager);
            file.Close();
        }

        public static T LoadSettings<T>(string filePath) where T : SerializedSettingsManager
        {
            XmlSerializer reader  = new XmlSerializer(typeof(T));

            StreamReader fileReader = new StreamReader(filePath);
            T settingsManager = (T)reader.Deserialize(fileReader);
            fileReader.Close();

            return settingsManager;
        }
    }
}
