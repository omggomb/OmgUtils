using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OmgUtils.ApplicationSettingsClass
{
    /// <summary>
    /// A single application setting that can be used inside the
    /// <see cref="SerializedSettingsManager"/> class
    /// </summary>
    /// <typeparam name="T">A serializable type</typeparam>
    [Serializable]
    public class SerializedSetting<T> 
    {
        /// <summary>
        /// A descriptive name of this setting.
        /// </summary>
        public string HumanReadableName
        {
            get;
            set;
        }

        /// <summary>
        /// A category for this setting
        /// </summary>
        public string Category
        {
            get;
            set;
        }

        /// <summary>
        /// A description for this setting, maybe intervals etc.
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// Ze value
        /// </summary>
        public T Value
        {
            get;
            set;
        }
    }
}
