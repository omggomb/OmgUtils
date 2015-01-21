using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OmgUtils.ApplicationSettingsManagement
{
    /// <summary>
    /// Represents a single setting inside an application
    /// </summary>
    public class Setting
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

        /// <summary>
        /// String determining the type of the setting's value
        /// </summary>
        public TypeCode ValueType { get; set; }

        /// <summary>
        /// The value of the setting stored as a object
        /// </summary>
        public object Value { get; set; }

     

        /// <summary>
        /// Converts the given type string to a typecode
        /// </summary>
        /// <param name="sTypeString">The string describing the type</param>
        /// <returns>The matching typecode</returns>
        public static TypeCode GetTypeFromTypeString(string sTypeString)
        {
            switch (sTypeString)
            {
                case ("bool"):
                    return TypeCode.Boolean;
                   
                case("int"):
                    return TypeCode.Int32;

                case ("double"):
                    return TypeCode.Double;

                case ("string"):
                    return TypeCode.String;
                    
                default:
                    return TypeCode.Empty;
                    
            }
        }

        /// <summary>
        /// Converts a typecode to a typestring
        /// </summary>
        /// <param name="code">The typecode to be converted</param>
        /// <returns>The matching string</returns>
        public static string GetTypeStringFromCode(TypeCode code)
        {
            switch (code)
            {
                case TypeCode.Boolean:
                    return "bool";
                    
               
                case TypeCode.Double:
                    return "double";
                    
                
                case TypeCode.Int32:
                    return "int";
             
                case TypeCode.String:
                    return "string";
            
                default:
                    return "empty";
                  
            }
        }

        /// <summary>
        /// Checks if the given setting is exactly equal to this one
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is Setting)
            {
                var o = obj as Setting;

                if (IdentificationName == o.IdentificationName &&
                    HumanReadableName == o.HumanReadableName &&
                    ValueType == o.ValueType &&
                    Value == o.Value &&
                    Category == o.Category)
                    return true;
                   
            }

            return false;
        }

     

        /// <summary>
        /// If the valutypes are the same, returns the value of the type T, else returns default(T)
        /// </summary>
        /// <typeparam name="T">Desired type of the settings value</typeparam>
        /// <returns></returns>
        public T GetValue<T>(out bool bSucces)
        {
            var t = typeof(T);
            if (Type.GetTypeCode(t) == ValueType)
            {
                bSucces = true;
                return (T)Value;
            }
            else
            {
                bSucces = false;
                return default(T);
            }
        }

        // <summary>
        /// If the valutypes are the same, returns the value of the type T, else returns default(T)
        /// </summary>
        /// <typeparam name="T">Desired type of the settings value</typeparam>
        /// <returns></returns>
        public T GetValue<T>()
        {
            bool bDummy = false;
            return GetValue<T>(out bDummy);
        }

        /// <summary>
        /// If valuetypes are the same, sets new value, else returns false
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetValue<T>(T value)
        {
            if (Type.GetTypeCode(typeof(T)) != ValueType)
                return false;
            else
            {
                Value = (object)value;
                return true;
            }
        }

        /// <summary>
        /// Feeds the string into the appropriate parse function
        /// </summary>
        /// <param name="sValueAsString"></param>
        public void SetValueFromString(string sValueAsString)
        {
            if (ValueType == TypeCode.Boolean)
                Value = Boolean.Parse(sValueAsString);
            else if (ValueType == TypeCode.Int32)
                Value = Int32.Parse(sValueAsString);
            else if (ValueType == TypeCode.Double)
                Value = Double.Parse(sValueAsString);
            else if (ValueType == TypeCode.String)
                Value = sValueAsString;
        }

        
    }

   
}
