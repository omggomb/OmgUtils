using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OmgUtils.ApplicationSettingsManagement
{
    /// <summary>
    /// Represents a single setting inside an application
    /// </summary>
    public abstract class Setting
    {
        /// <summary>
        /// The string used to identify the setting
        /// </summary>
        public string IdentificationName { get; set; }

        /// <summary>
        /// A human readable name for the setting
        /// </summary>
        public string HumanReadableName { get; set; }

        /// <summary>
        /// The category string, delimited by dots (".")
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// A readable description of the setting's meaning
        /// </summary>
        public string Description { get; set; }

    
       

        public Setting()
        {
            IdentificationName = "";
            HumanReadableName = "";
            Category = "";
            Description =  "";
        }

        /// <summary>
        /// Returns the type of the setting as string
        /// </summary>
        /// <returns></returns>
        public abstract string GetTypeAsString();

        /// <summary>
        /// Sets the settings value by trying to parse the given string
        /// </summary>
        /// <param name="sValue"></param>
        /// <returns></returns>
        public virtual bool SetFromString(string sValue)
        {
            if (sValue == null)
                return false;

            return true;
        }

        /// <summary>
        /// Returns the value as a string
        /// </summary>
        /// <returns></returns>
        public abstract string GetValueAsString();
    }

    /// <summary>
    /// A setting containing an integer
    /// </summary>
    public class IntSetting : Setting
    {
        /// <summary>
        /// The value of the setting
        /// </summary>
        public int  Value { get; set; }

        public override string GetTypeAsString()
        {
            return "int";
        }

        public override bool SetFromString(string sValue)
        {
            if (!base.SetFromString(sValue))
                return false;
            int res = 0;
            if (int.TryParse(sValue, out res))
            {
                Value = res;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string GetValueAsString()
        {
            return Value.ToString();
        }
    }

    /// <summary>
    /// A setting containing a float
    /// </summary>
    public class FloatSetting : Setting
    {
        /// <summary>
        /// The value of the setting
        /// </summary>
        public float Value { get; set; }

        public override string GetTypeAsString()
        {
            return "float";
        }

        public override bool SetFromString(string sValue)
        {
            if (!base.SetFromString(sValue))
                return false;
            float res = 0;
            if (float.TryParse(sValue, out res))
            {
                Value = res;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string GetValueAsString()
        {
            return Value.ToString();
        }
    }

    /// <summary>
    /// A setting containing a bool
    /// </summary>
    public class BoolSetting : Setting
    {
        /// <summary>
        /// The value of the setting  
        /// </summary>
        public bool Value { get; set; }

        public override string GetTypeAsString()
        {
            return "bool";
        }

        public override bool SetFromString(string sValue)
        {
            if (!base.SetFromString(sValue))
                return false;
            bool res = false;
            if (bool.TryParse(sValue, out res))
            {
                Value = res;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string GetValueAsString()
        {
            return Value.ToString();
        }
    }

    /// <summary>
    /// A setting containing a string
    /// </summary>
    public class StringSetting : Setting
    {
        /// <summary>
        /// The value of the setting  
        /// </summary>
        /// 
        public string Value { get; set; }
        public override string GetTypeAsString()
        {
            return "string";
        }

        public override bool SetFromString(string sValue)
        {
            if (!base.SetFromString(sValue))
                return false;

            Value = sValue;
            return true;
        }

        public override string GetValueAsString()
        {
            return Value;
        }
    }




     

}
