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

        /// <summary>
        /// Creates a new instance of the cfg parser.
        /// </summary>
        /// <param name="loggingInstance">If not null, will be used to report errors, else errors cause exceptions</param>
        public CFGFileParser(Logging.Logger loggingInstance)
        {
            Results = new Dictionary<string, string>();
            m_loggerInstance = loggingInstance;
            m_sFileName = "";
        }

        /// <summary>
        /// Parses the given file and stores the results inside the "Results" property
        /// </summary>
        /// <param name="sFileName">Full path to the file</param>
        /// <returns>True if successfully parsed, else false (will succeed as long as the file could be openened and read)</returns>
        public bool Parse(string sFileName)
        {
            StreamReader reader = null;
            bool bSuccess = false;

            m_sFileName = sFileName;

            try
            {
                reader = new StreamReader(File.OpenRead(sFileName));

                while (reader.EndOfStream == false)
                {
                    ParseLine(reader.ReadLine());
                }
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

            return bSuccess;
        }

        void ParseLine(string sLine)
        {
            sLine = sLine.Trim();

            // Simply ignore comments
            if (sLine.StartsWith("//"))
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
