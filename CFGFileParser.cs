using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OmgUtils
{
    /// <summary>
    /// Parses a given .cfg file and stores the results in a dictionary
    /// Does not support commands, only assignments
    /// </summary>
    public class CFGFileParser
    {
        /// <summary>
        /// Stores the parsed values
        /// </summary>
        public Dictionary<string, string> Results { get; set; }

        Logging.Logger m_loggerInstance;
        string m_sFileName;
        bool m_bInitialized;

        /// <summary>
        /// Creates a new instance of the cfg parser.
        /// </summary>
        /// <param name="loggingInstance">If not null, will be used to report errors, else errors cause exceptions</param>
        public CFGFileParser(Logging.Logger loggingInstance)
        {
            Results = new Dictionary<string, string>();
            m_loggerInstance = loggingInstance;
            m_sFileName = "";
            m_bInitialized = false;
        }

        /// <summary>
        /// Parses the given file and stores the results inside the "Results" property
        /// </summary>
        /// <param name="sFileName">Full path to the file</param>
        /// <returns>True if successfully parsed, else false (will succeed as long as the file could be openened and read)</returns>
        public bool Parse(string sFileName, bool bCreateNew = true)
        {
            StreamReader reader = null;
            bool bSuccess = false;

            m_sFileName = sFileName;

            try
            {
                if (!File.Exists(sFileName) && bCreateNew)
                {
                    File.Create(sFileName).Close();
                }
                else if (!File.Exists(sFileName))
                {
                    Log(string.Format("[CFGParser] Tried parsing file '{0}', but it does not exist and bCreateNew is false", sFileName),
                        Logging.DefaultSeverityLevels.SL_Error);
                    return false;
                }
                reader = new StreamReader(File.OpenRead(sFileName));

                while (reader.EndOfStream == false)
                {
                    ParseLine(reader.ReadLine());
                }

                reader.Close();
                bSuccess = true;
            }
            catch (Exception e)
            {
                if (m_loggerInstance != null)
                {
                    m_loggerInstance.LogError(e.Message);
                    return false;
                }
                else
                    throw;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return m_bInitialized = bSuccess;
        }

        public bool SetValue(string sKey, string sValue, bool bCreate = true)
        {
            if (!m_bInitialized)
            {
                Log(string.Format("[CFGParser] Tried setting key '{0}' before file was parsed", sKey),
                    Logging.DefaultSeverityLevels.SL_Error);
                return false;
            }

            if (Results.ContainsKey(sKey))
            {
                Results[sKey] = sValue;
            }
            else
            {
                if (bCreate)
                {
                    Results.Add(sKey, sValue);
                }
                else
                {
                    Log(String.Format("[CFGParser] Tried setting value {0} for key {1}, but key does not exist and bCreate is false", sValue, sKey),
                        Logging.DefaultSeverityLevels.SL_Warning);
                    return false;
                }
            }

            WriteCFGFile();

            return true;
        }

        void WriteCFGFile()
        {
            StreamReader reader = null;
            StreamWriter writer = null;
            try
            {
                reader = new StreamReader(File.OpenRead(m_sFileName));

                string sFinalFile = "";

                while (reader.EndOfStream == false)
                {
                    string sLine = reader.ReadLine();

                    foreach (var key in Results.Keys)
                    {
                        if (sLine.Contains(key))
                        {
                            sFinalFile += key + "=" + Results[key] + Environment.NewLine;
                        }
                        else
                            sFinalFile += sLine + Environment.NewLine;
                    }
                }

                reader.Close();

                writer = new StreamWriter(File.OpenWrite(m_sFileName));

                writer.Write(sFinalFile);

                writer.Close();
            }
            catch (Exception e)
            {
                if (m_loggerInstance != null)
                {
                    m_loggerInstance.LogError(e.Message);
                }
                else
                    throw;
            }
            finally
            {
                if (reader != null)
                    reader.Close();

                if (writer != null)
                    writer.Close();
            }
        }

        void ParseLine(string sLine)
        {
            sLine = sLine.Trim();

            // Simply ignore comments and emtpy lines
            if (sLine.StartsWith("//") || string.IsNullOrWhiteSpace(sLine))
            {
                return;
            }
            else if (!sLine.Contains('='))
            {
                string sMessage = "[CFGParser] Cannot parse line '" + sLine + "' in file '" + m_sFileName + "'";
                Log(sMessage, Logging.DefaultSeverityLevels.SL_Warning);
            }
            else
            {
                var splitString = sLine.Split('=');

                if (splitString.Count() == 2)
                {
                    string sVar = splitString[0].Trim();
                    string sVal = splitString[1].Trim();

                    Results.Add(sVar, sVal);
                }
                else
                {
                    Log(String.Format("[CFGParser] Found line ({0}) in file : '{1}' with more than one equals sign.", sLine, m_sFileName),
                        Logging.DefaultSeverityLevels.SL_Warning);
                }
            }
        }

        void Log(string sMessage, string sSeverity)
        {
            if (m_loggerInstance != null)
                m_loggerInstance.LogMessage(sMessage, sSeverity);
        }
    }
}
