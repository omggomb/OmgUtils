using Microsoft.VisualStudio.TestTools.UnitTesting;
using OmgUtils.ApplicationSettingsManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmgUtils.ApplicationSettingsManagement.Tests
{
    [TestClass()]
    public class SettingTests
    {
        #region INT

        [TestMethod()]
        public void SettingTestIntInt()
        {
            const int value = 1234;
            var setting = new IntSetting()
            {
                Category = "none",
                Description = "none",
                HumanReadableName = "Test setting",
                IdentificationName = "mySetting",
                Value = value
            };

            int convSetting = (int)setting;

            Assert.AreEqual(value, convSetting);
        }

        [TestMethod()]
        public void SettingTestIntFloat()
        {
            const float value = 1234.5678f;
            var setting = new IntSetting()
            {
                Category = "none",
                Description = "none",
                HumanReadableName = "Test setting",
                IdentificationName = "mySetting",
                Value = (int)value
            };

            int convSetting = (int)setting;

            Assert.AreEqual((int)value, convSetting);
        }

        [TestMethod()]
        public void SettingTestIntBoolTrue()
        {
            const int value = 44;
            var setting = new IntSetting()
            {
                Category = "none",
                Description = "none",
                HumanReadableName = "Test setting",
                IdentificationName = "mySetting",
                Value = value
            };

            bool convSetting = (bool)setting;

            Assert.AreEqual(true, convSetting);
        }

        [TestMethod()]
        public void SettingTestIntBoolFalse()
        {
            const int value = 0;
            var setting = new IntSetting()
            {
                Category = "none",
                Description = "none",
                HumanReadableName = "Test setting",
                IdentificationName = "mySetting",
                Value = value
            };

            bool convSetting = (bool)setting;

            Assert.AreEqual(false, convSetting);
        }

        [TestMethod()]
        public void SettingTestIntString()
        {
            const int value = 555;
            var setting = new IntSetting()
            {
                Category = "none",
                Description = "none",
                HumanReadableName = "Test setting",
                IdentificationName = "mySetting",
                Value = value
            };

            string convSetting = (string)setting;

            Assert.AreEqual(value.ToString(), convSetting);
        }

        #endregion INT

        #region FLOAT

        [TestMethod()]
        public void SettingTestFloatInt()
        {
            const float value = 1234.567f;
            var setting = new FloatSetting()
            {
                Category = "none",
                Description = "none",
                HumanReadableName = "Test setting",
                IdentificationName = "mySetting",
                Value = value
            };

            int convSetting = (int)setting;

            Assert.AreEqual((int)value, convSetting);
        }

        [TestMethod()]
        public void SettingTestFloatFloat()
        {
            const float value = 1234.56f;
            var setting = new FloatSetting()
            {
                Category = "none",
                Description = "none",
                HumanReadableName = "Test setting",
                IdentificationName = "mySetting",
                Value = value
            };

            float convSetting = (float)setting;

            Assert.AreEqual(value, convSetting);
        }

        [TestMethod()]
        public void SettingTestFloatBoolTrue()
        {
            const float value = 44.56f;
            var setting = new FloatSetting()
            {
                Category = "none",
                Description = "none",
                HumanReadableName = "Test setting",
                IdentificationName = "mySetting",
                Value = value
            };

            bool convSetting = (bool)setting;

            Assert.AreEqual(true, convSetting);
        }

        [TestMethod()]
        public void SettingTestFloatBoolFalse()
        {
            const float value = 0.0f;
            var setting = new FloatSetting()
            {
                Category = "none",
                Description = "none",
                HumanReadableName = "Test setting",
                IdentificationName = "mySetting",
                Value = value
            };

            bool convSetting = (bool)setting;

            Assert.AreEqual(false, convSetting);
        }

        [TestMethod()]
        public void SettingTestFloatString()
        {
            const float value = 555.444f;
            var setting = new FloatSetting()
            {
                Category = "none",
                Description = "none",
                HumanReadableName = "Test setting",
                IdentificationName = "mySetting",
                Value = value
            };

            string convSetting = (string)setting;

            Assert.AreEqual(value.ToString(), convSetting);
        }

        #endregion FLOAT

        #region BOOL

        [TestMethod()]
        public void SettingTestBoolIntTrue()
        {
            const bool value = true;
            var setting = new BoolSetting()
            {
                Category = "none",
                Description = "none",
                HumanReadableName = "Test setting",
                IdentificationName = "mySetting",
                Value = value
            };

            int convSetting = (int)setting;

            Assert.AreEqual(1, convSetting);
        }

        [TestMethod()]
        public void SettingTestBoolIntFalse()
        {
            const bool value = false;
            var setting = new BoolSetting()
            {
                Category = "none",
                Description = "none",
                HumanReadableName = "Test setting",
                IdentificationName = "mySetting",
                Value = value
            };

            int convSetting = (int)setting;

            Assert.AreEqual(0, convSetting);
        }

        [TestMethod()]
        public void SettingTestBoolFloatTrue()
        {
            const bool value = true;
            var setting = new BoolSetting()
            {
                Category = "none",
                Description = "none",
                HumanReadableName = "Test setting",
                IdentificationName = "mySetting",
                Value = value
            };

            int convSetting = (int)setting;

            Assert.AreEqual(1.0f, convSetting);
        }

        [TestMethod()]
        public void SettingTestBoolFloatFalse()
        {
            const bool value = false;
            var setting = new BoolSetting()
            {
                Category = "none",
                Description = "none",
                HumanReadableName = "Test setting",
                IdentificationName = "mySetting",
                Value = value
            };

            int convSetting = (int)setting;

            Assert.AreEqual(0.0f, convSetting);
        }

        [TestMethod()]
        public void SettingTestBoolBoolTrue()
        {
            const bool value = true;
            var setting = new BoolSetting()
            {
                Category = "none",
                Description = "none",
                HumanReadableName = "Test setting",
                IdentificationName = "mySetting",
                Value = value
            };

            bool convSetting = (bool)setting;

            Assert.AreEqual(value, convSetting);
        }

        [TestMethod()]
        public void SettingTestBoolBoolFalse()
        {
            const bool value = false;
            var setting = new BoolSetting()
            {
                Category = "none",
                Description = "none",
                HumanReadableName = "Test setting",
                IdentificationName = "mySetting",
                Value = value
            };

            bool convSetting = (bool)setting;

            Assert.AreEqual(value, convSetting);
        }

        [TestMethod()]
        public void SettingTestBoolStringTrue()
        {
            const bool value = true;
            var setting = new BoolSetting()
            {
                Category = "none",
                Description = "none",
                HumanReadableName = "Test setting",
                IdentificationName = "mySetting",
                Value = value
            };

            string convSetting = (string)setting;

            Assert.AreEqual(value.ToString(), convSetting);
        }

        [TestMethod()]
        public void SettingTestBoolStringFalse()
        {
            const bool value = false;
            var setting = new BoolSetting()
            {
                Category = "none",
                Description = "none",
                HumanReadableName = "Test setting",
                IdentificationName = "mySetting",
                Value = value
            };

            string convSetting = (string)setting;

            Assert.AreEqual(value.ToString(), convSetting);
        }

        #endregion BOOL

        #region STRING

        [TestMethod()]
        public void SettingTestStringInt()
        {
            const string value = "555";
            var setting = new StringSetting()
            {
                Category = "none",
                Description = "none",
                HumanReadableName = "Test setting",
                IdentificationName = "mySetting",
                Value = value
            };

            int convSetting = (int)setting;

            Assert.AreEqual(555, convSetting);
        }

        [TestMethod()]
        public void SettingTestStringFloat()
        {
            const string value = "555,44";
            var setting = new StringSetting()
            {
                Category = "none",
                Description = "none",
                HumanReadableName = "Test setting",
                IdentificationName = "mySetting",
                Value = value
            };

            float convSetting = (float)setting;

            Assert.AreEqual(555.44f, convSetting);
        }

        [TestMethod()]
        public void SettingTestStringBool()
        {
            const string value = "true";
            var setting = new StringSetting()
            {
                Category = "none",
                Description = "none",
                HumanReadableName = "Test setting",
                IdentificationName = "mySetting",
                Value = value
            };

            bool convSetting = (bool)setting;

            Assert.AreEqual(true, convSetting);
        }

        [TestMethod()]
        public void SettingTestStringString()
        {
            const string value = "Hello";
            var setting = new StringSetting()
            {
                Category = "none",
                Description = "none",
                HumanReadableName = "Test setting",
                IdentificationName = "mySetting",
                Value = value
            };

            string convSetting = (string)setting;

            Assert.AreEqual(value, convSetting);
        }

        #endregion STRING

        [TestMethod()]
        public void SettingTestStringToString()
        {
            string expextedString = "mySetting=Hello";
            const string value = "Hello";
            var setting = new StringSetting()
            {
                Category = "none",
                Description = "none",
                HumanReadableName = "Test setting",
                IdentificationName = "mySetting",
                Value = value
            };

            Assert.AreEqual(expextedString, setting.ToString());
        }

        [TestMethod()]
        public void SettingTestBoolToString()
        {
            string expextedString = "mySetting=True";
            const bool value = true;
            var setting = new BoolSetting()
            {
                Category = "none",
                Description = "none",
                HumanReadableName = "Test setting",
                IdentificationName = "mySetting",
                Value = value
            };

            Assert.AreEqual(expextedString, setting.ToString());
        }

        [TestMethod()]
        public void SettingTestFloatToString()
        {
            string expextedString = "mySetting=14,44";
            const float value = 14.44f;
            var setting = new FloatSetting()
            {
                Category = "none",
                Description = "none",
                HumanReadableName = "Test setting",
                IdentificationName = "mySetting",
                Value = value
            };

            Assert.AreEqual(expextedString, setting.ToString());
        }

        [TestMethod()]
        public void SettingTestIntToString()
        {
            string expextedString = "mySetting=345";
            const int value = 345;
            var setting = new FloatSetting()
            {
                Category = "none",
                Description = "none",
                HumanReadableName = "Test setting",
                IdentificationName = "mySetting",
                Value = value
            };

            Assert.AreEqual(expextedString, setting.ToString());
        }
    }
}