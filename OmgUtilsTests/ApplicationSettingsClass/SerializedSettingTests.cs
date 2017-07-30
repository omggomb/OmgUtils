using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OmgUtils.ApplicationSettingsClass;
using System.Xml.Serialization;
using System.IO;

namespace OmgUtilsTests.ApplicationSettingsClass
{
    [TestClass]
    public class SerializedSettingTests
    {
        [TestMethod]
        public void TestWrite()
        {
            var setting = new SerializedSetting<SerializedSetting<int>>
            {
                Category = "Test category",
                Description = "Some value for testing",
                HumanReadableName = "Test string",
                Value = new SerializedSetting<int>
                {
                    Category = "Test category",
                    Description = "Some value for testing",
                    HumanReadableName = "Test int",
                    Value = 666
                }
            };

            string filePath = @".\testSetting.xml";

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            XmlSerializer writer = new XmlSerializer(setting.GetType());

            FileStream file = File.Create(filePath);

            writer.Serialize(file, setting);
            file.Close();
        }
    }
}
