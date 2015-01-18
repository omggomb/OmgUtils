using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Controls;
using System.IO;
using System.Windows.Documents;

namespace OmgUtils.Logging
{
    /// <summary>
    /// Provides a set of default severity levels for the logger
    /// </summary>
    public class DefaultSeverityLevels
    {
        /// <summary>
        /// Normal severity
        /// </summary>
        public const string SL_Info = "info";

        /// <summary>
        /// Message is a warning
        /// </summary>
        public const string SL_Warning = "warning";

        /// <summary>
        /// Message is an error
        /// </summary>
        public const string SL_Error = "error";
    }

    /// <summary>
    /// A generic logging class for logging into a file as well as
    /// a TextBlock or RichTextBox if desired
    /// </summary>
    public class Logger
    {
        #region Attributes
        /// <summary>
        /// Should a timestamp be included for each message?
        /// </summary>
        public bool IncludeTimeStamp { get; set; }

        /// <summary>
        /// If a text box or block is set disable logging to that control
        /// </summary>
        public bool UseVisualLogging { get; set; }

        /// <summary>
        /// Should the severity name be include in the message
        /// [timestamp] [severity] message
        /// </summary>
        public bool IncludeSeverityNameInLog { get; set; }

        /// <summary>
        /// Matches severity levels to colors for colored output inside a rich text box
        /// </summary>
        public Dictionary<string, SolidColorBrush> SeverityColors { get; set; }

        /// <summary>
        /// List containing all the severity levels to be logged
        /// If a level is not inside this list it will be ignored (verbosity control)
        /// </summary>
        public List<string> AllowedSeverityLevels { get; set; }

        /// <summary>
        /// If viusal logging is enabled and set to a rich text box, disable color coding
        /// </summary>
        public bool UseColorCoding { get; set; }

        /// <summary>
        /// Defines wether logging should occur to a file
        /// </summary>
        public bool UseFileLogging { get; set; }

        /// <summary>
        /// TextBlock instance to be logged to, if any
        /// </summary>
        private TextBlock textBlock;

        /// <summary>
        /// RichTextBox to be logged to, if any
        /// </summary>
        private RichTextBox richTextBox;

        /// <summary>
        /// FileInfo object created after initialisation
        /// </summary>
        private FileInfo fileInfo;

        #endregion

        #region Methods

        public Logger()
        {
            IncludeTimeStamp = true;
            UseVisualLogging = true;

            SeverityColors = new Dictionary<string, SolidColorBrush>();
            SeverityColors.Add(DefaultSeverityLevels.SL_Info, Brushes.Black);
            SeverityColors.Add(DefaultSeverityLevels.SL_Warning, Brushes.DarkGoldenrod);
            SeverityColors.Add(DefaultSeverityLevels.SL_Error, Brushes.DarkRed);

            AllowedSeverityLevels = new List<string>();
            AllowedSeverityLevels.Add(DefaultSeverityLevels.SL_Error);
            AllowedSeverityLevels.Add(DefaultSeverityLevels.SL_Info);
            AllowedSeverityLevels.Add(DefaultSeverityLevels.SL_Warning);

            UseColorCoding = true;
            IncludeSeverityNameInLog = true;
            UseFileLogging = true;

            textBlock = null;
            richTextBox = null;
            fileInfo = null;
        }

        /// <summary>
        /// Initialze the logger without using a visual output control
        /// </summary>
        /// <param name="sFilePath">Path to the file to be logged to</param>
        /// <returns>True on succes</returns>
        public bool Init(string sFilePath)
        {
            return InitInternal(sFilePath);
        }

        /// <summary>
        /// Initialze the logger and use a TextBlock instance as visual output
        /// </summary>
        /// <param name="sFilePath">Path to the file to be logged to</param>
        /// <param name="textBlock">Instance of a TextBlock control</param>
        /// <returns>True on succes</returns>
        public bool Init(string sFilePath, TextBlock textBlock)
        {
            if (textBlock == null)
                throw new ArgumentNullException("textBlock");
            else
                this.textBlock = textBlock;

            return InitInternal(sFilePath);
        }

        /// <summary>
        /// Initialze the logger and use a RichTextBox instance as visual output
        /// </summary>
        /// <param name="sFilePath">Path to the file to be logged to</param>
        /// <param name="richTextBox">Instance of a RichTextBox control</param>
        /// <returns>True on succes</returns>
        public bool Init(string sFilePath, RichTextBox richTextBox)
        {
            if (richTextBox == null)
                throw new ArgumentNullException("richTextBox");
            else
                this.richTextBox = richTextBox;

            return InitInternal(sFilePath);
        }

        /// <summary>
        /// Initialze the logger and use a RichTextBox instance as visual output, but don't use any file logging
        /// </summary>
        /// <param name="rtb">Instance of a RichTextBox control</param>
        /// <returns>True on succes</returns>
        public bool Init(RichTextBox rtb)
        {
            if (rtb != null)
                richTextBox = rtb;
            else
                throw new ArgumentNullException("rtb");

            UseFileLogging = false;

            return true;
        }

        /// <summary>
        /// Initialze the logger and use a TextBlock instance as visual output, but don't use any file logging
        /// </summary>
        /// <param name="tb">Instance of a TextBlock control</param>
        /// <returns>True on succes</returns>
        public bool Init(TextBlock tb)
        {
            if (tb != null)
                textBlock = tb;
            else
                throw new ArgumentNullException("tb");

            UseFileLogging = false;

            return true;
        }

        /// <summary>
        /// Writes a message to the log with the given severity
        /// </summary>
        /// <param name="sMessage">The message to be written</param>
        /// <param name="sSeverity">The severity level</param>
        public void LogMessage(string sMessage, string sSeverity)
        {
            if (!AllowedSeverityLevels.Contains(sSeverity))
                return;

            string sFinalMessage = ConstructFinalMessage(sMessage, sSeverity);

            if (UseVisualLogging)
            {
                LogVisualTB(sFinalMessage, sSeverity);
                LogVisualRTB(sFinalMessage, sSeverity);
            }
            if (UseFileLogging)
                LogToFile(sFinalMessage, sSeverity);
        }

        /// <summary>
        /// Log a message with severity "info" to the log (black color)
        /// </summary>
        /// <param name="sMessage"></param>
        public void LogInfo(string sMessage)
        {
            LogMessage(sMessage, DefaultSeverityLevels.SL_Info);
        }

        /// <summary>
        /// Log a message with severity "warning" to the log (darkgoldenrod color)
        /// </summary>
        /// <param name="sMessage"></param>
        public void LogWarning(string sMessage)
        {
            LogMessage(sMessage, DefaultSeverityLevels.SL_Warning);
        }

        /// <summary>
        /// Log a message with severity "error" to the log (darkred color)
        /// </summary>
        /// <param name="sMessage"></param>
        public void LogError(string sMessage)
        {
            LogMessage(sMessage, DefaultSeverityLevels.SL_Error);
        }

        private bool InitInternal(string sPathToFile)
        {

            try
            {
                fileInfo = new FileInfo(sPathToFile);

                if (!fileInfo.Exists)
                    fileInfo.Create();

            }
            catch (Exception)
            {
                
                throw;
            }

            return true;
        }

        private void LogVisualTB(string sMessage, string sSeverity)
        {
            if (textBlock != null)
            {
                var range = new TextRange(textBlock.ContentStart, textBlock.ContentEnd);
                range.Text = sMessage + "\n";

                if (UseColorCoding)
                {
                    SolidColorBrush brush = null;

                    if (!SeverityColors.TryGetValue(sSeverity, out brush))
                        brush = Brushes.Black;

                    range.ApplyPropertyValue(TextElement.ForegroundProperty, brush);
                }
   
                    
            }
        }

        private void LogVisualRTB(string sMessage, string sSeverity)
        {
            if (richTextBox != null)
            {
                var range = new TextRange(richTextBox.Document.ContentEnd, richTextBox.Document.ContentEnd);
                range.Text = "\n" + sMessage;

                if (UseColorCoding)
                {
                    SolidColorBrush brush = null;

                    if (!SeverityColors.TryGetValue(sSeverity, out brush))
                        brush = Brushes.Black;

                    range.ApplyPropertyValue(TextElement.ForegroundProperty, brush);
                }

                richTextBox.ScrollToEnd();

            }
        }

        private void LogToFile(string sMessage, string sSeverity)
        {
            if (fileInfo != null)
            {
                try
                {
                    var strWriter = fileInfo.AppendText();

                    strWriter.WriteLine(sMessage);

                    strWriter.Close();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        private string ConstructFinalMessage(string sMessage, string sSeverity)
        {
            string sFinalMessage = sMessage;

            if (IncludeSeverityNameInLog)
                sFinalMessage = "[" + sSeverity + "] " + sFinalMessage;

            if (IncludeTimeStamp)
                sFinalMessage = "[" + DateTime.Now.ToLongTimeString() + "] " + sFinalMessage;

            return sFinalMessage;
        }
        #endregion
    }
}
