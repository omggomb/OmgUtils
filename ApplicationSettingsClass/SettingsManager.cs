using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

using OmgUtils.Logging;

namespace OmgUtils.ApplicationSettingsManagement
{
    /// <summary>
    /// Provides a generic application settings management class.
    /// XML Structure expected:
    /// Settings
    ///     Setting identificationName humanReadableName description category value typeAsString
    ///
    /// </summary>
    public class SettingsManager
    {
        /// <summary>
        /// Holds all the loaded settings
        /// Settings are stored using their IdentificationName
        /// </summary>
        public Dictionary<string, Setting> ApplicationSettings { get; set; }

        /// <summary>
        /// If a file is loaded, stores the path to that file
        /// </summary>
        private string sFilePath;

        /// <summary>
        /// Used to log messages if set
        /// </summary>
        private Logger loggerInstance;

        /// <summary>
        /// Convenience indicator if logger is present
        /// </summary>
        private bool bHasLogger;

        /// <summary>
        /// If loggerInstance is not null, the manager will use logger to communicate
        /// errors and warnings, else errors will cause exceptions
        /// </summary>
        /// <param name="loggerInstance"></param>
        public SettingsManager(Logger loggerInstance)
        {
            sFilePath = "";
            this.loggerInstance = loggerInstance;
            bHasLogger = (loggerInstance == null) ? false : true;

            ApplicationSettings = new Dictionary<string, Setting>();
        }

        /// <summary>
        /// Loads all settings from the specified xml file
        /// </summary>
        /// <param name="sPathToFile"></param>
        public bool LoadSettingsFromFile(string sPathToFile)
        {
            ApplicationSettings.Clear();
            XmlReader reader = null;
            try
            {
                reader = XmlReader.Create(sPathToFile);

                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            {
                                if (reader.LocalName == "Setting")
                                {
                                    // XML Structure expected:
                                    // Settings
                                    //     Setting identificationName humanReadableName description category value typeAsString

                                    string typeAsString = reader.GetAttribute("typeAsString");
                                    string identificationName = reader.GetAttribute("identificationName");
                                    string humanReadableName = reader.GetAttribute("humanReadableName");
                                    string description = reader.GetAttribute("description");
                                    string category = reader.GetAttribute("category");
                                    string val = reader.GetAttribute("value");

                                    Setting set = null;

                                    // A little ugly 
                                    switch (typeAsString)
                                    {
                                        case "int":
                                            set = new IntSetting();
                                            break;
                                        case "bool":
                                            set = new BoolSetting();
                                            break;
                                        case "float":
                                            set = new FloatSetting();
                                            break;
                                        case "string":
                                            set = new StringSetting();
                                            break;
                                        default:
                                            {
                                                if (bHasLogger)
                                                {
                                                    loggerInstance.LogWarning("[SettingsManager] Unrecognised type while loading settings: " + typeAsString);
                                                }
                                            }
                                            break;
                                    }

                                    if (set != null)
                                    {
                                        set.IdentificationName = identificationName;
                                        set.HumanReadableName = humanReadableName;
                                        set.Category = category;
                                        set.Description = description;
                                        set.SetFromString(val);
                                        ApplicationSettings.Add(identificationName, set);
                                    }
                                }
                            } break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                if (reader != null)
                    reader.Close();
                if (bHasLogger)
                {
                    loggerInstance.LogError("[SettingsManager:107] " + e.Message);
                    return false;
                }
                else
                {
                    throw;
                }

            }

            this.sFilePath = sPathToFile;
            return true;
        }

        /// <summary>
        /// Saves the settings to the same file they have been loaded from
        /// </summary>
        /// <returns></returns>
        public bool SaveSettings()
        {
            return SaveSettings(sFilePath);
        }

        /// <summary>
        /// Save settings to specified file, override if file already exists
        /// </summary>
        /// <param name="sPathToFile"></param>
        /// <param name="bOverride"></param>
        /// <returns></returns>
        public bool SaveSettings(string sPathToFile, bool bOverride = true)
        {
            if (File.Exists(sPathToFile) && bOverride)
                File.Delete(sPathToFile);
            else if (File.Exists(sPathToFile) && !bOverride)
            {
                if (bHasLogger)
                {
                    loggerInstance.LogError("[SettingsManager:142] Tried saving settings, but file exists and bOverride is false");
                    return false;
                }
                else
                {
                    throw new InvalidOperationException("Tried saving settings, but file exists and bOverride is false");
                }
            }

            XmlWriter writer = null;

            try
            {
                writer = XmlWriter.Create(sPathToFile, new XmlWriterSettings() { Indent = true, NewLineOnAttributes = true });


                writer.WriteStartDocument();
                writer.WriteStartElement("Settings");

                foreach (var set in ApplicationSettings.Values)
                {
                    // XML Structure expected:
                    // Settings
                    //     Setting identificationName humanReadableName description category value typeAsString
                    writer.WriteStartElement("Setting");

                    writer.WriteAttributeString("identificationName", set.IdentificationName);
                    writer.WriteAttributeString("humanReadableName", set.HumanReadableName);
                    writer.WriteAttributeString("description", set.Description);
                    writer.WriteAttributeString("category", set.Category);
                    writer.WriteAttributeString("typeAsString", set.GetTypeAsString());
                    writer.WriteAttributeString("value", set.GetValueAsString());

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();

                writer.Close();
            }
            catch (Exception e)
            {
                if (writer != null)
                    writer.Close();

                if (bHasLogger)
                {
                    loggerInstance.LogError("[SettingsManager:189] " + e.Message);
                    return false;
                }
                else
                    throw;
            }

            return true;
        }

        /// <summary>
        /// Tries to get the wanted setting, returns null if not found
        /// </summary>
        /// <param name="sIdentName"></param>
        /// <returns></returns>
        public Setting GetSetting(string sIdentName)
        {
            Setting output = null;

            if (ApplicationSettings.TryGetValue(sIdentName, out output))
                return output;
            else
            {
                if (bHasLogger)
                {
                    loggerInstance.LogWarning("[SettingsManager:218] Trying to get setting: '" + sIdentName + "' which doesn't exist");
                }
                return null;
            }
        }

        /// <summary>
        /// Add a new setting to the collection
        /// </summary>
        /// <param name="setting">The setting to be added</param>
        /// <param name="bOverride">If setting exists, override?</param>
        /// <returns>True on success else false</returns>
        public bool AddSetting(Setting setting, bool bOverride = true)
        {
            if (ApplicationSettings.ContainsKey(setting.IdentificationName))
            {
                if (bOverride)
                {
                    ApplicationSettings.Remove(setting.IdentificationName);
                }
                else
                {
                    if (bHasLogger)
                    {
                        loggerInstance.LogWarning("[SettingsManager:230] Tryíng to add setting that already exists!");
                        return false;
                    }
                }
            }

            ApplicationSettings.Add(setting.IdentificationName, setting);
            return true;
        }

        

    }
}
