using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OmgUtils.ApplicationSettingsManagement;
using OmgUtils.ApplicationSettingsClass;
using System.Collections.Generic;
using System.IO;

namespace OmgUtilsTests.ApplicationSettingsClass
{
    [TestClass]
    public class SerializedSettingsManagerUnitTests
    {

        private MySerializedSettingsManager settingsManager;

        public class MySerializedSettingsManager : SerializedSettingsManager
        {
            public SerializedSetting<int> intSetting = new SerializedSetting<int>
            {
                Category = "Testing",
                Description = "Some int test setting",
                HumanReadableName = "Integer setting",
                Value = 666
            };

            public SerializedSetting<float> floatSetting = new SerializedSetting<float>
            {
                Category = "Testing",
                Description = "Some float testing setting",
                HumanReadableName = "Float setting",
                Value = 66.6f
            };

            public SerializedSetting<bool> boolSetting = new SerializedSetting<bool>
            {
                Category = "Testing",
                Description = "Some bool testing setting",
                HumanReadableName = "Bool setting",
                Value = true
            };

            public SerializedSetting<string> stringSetting = new SerializedSetting<string>
            {
                Category = "Testing",
                Description = "Some string setting",
                HumanReadableName = "The string setting",
                Value = "Hello"
            };

            public List<SerializedSetting<string>> stringSettingList = new List<SerializedSetting<string>>();

            public MySerializedSettingsManager()
            {
                for (int i = 0; i < 10; ++i)
                {
                    stringSettingList.Add(new SerializedSetting<string>
                    {
                        Category = "Testing list",
                        Description = "Some setting inside a list",
                        HumanReadableName = "List item no: " + i,
                        Value = "Setting no " + i
                    });
                }
            }
        }

        [TestInitialize()]
        public void Before()
        {
            settingsManager = new MySerializedSettingsManager();
        }

        [TestMethod()]
        public void TestSerializedSettingsManagerWriting()
        {
            string filePath = @".\Settings.xml";
            SerializedSettingsManager.SaveSettings(this.settingsManager, filePath);

            Assert.IsTrue(File.Exists(filePath));
        }

        [TestMethod()]
        public void TestSerializedSettingsManagerLoading()
        {
            string filePath = @".\Settings.xml";
            SerializedSettingsManager.SaveSettings(this.settingsManager, filePath);

            var newSettings = SerializedSettingsManager.LoadSettings<MySerializedSettingsManager>(filePath);

            Assert.AreEqual(settingsManager.intSetting.Value, newSettings.intSetting.Value);
            Assert.AreEqual(settingsManager.floatSetting.Value, newSettings.floatSetting.Value);
            Assert.AreEqual(settingsManager.boolSetting.Value, newSettings.boolSetting.Value);
            Assert.AreEqual(settingsManager.stringSetting.Value, newSettings.stringSetting.Value);

            for (int i = 0; i < settingsManager.stringSettingList.Count; ++i)
            {
                Assert.AreEqual(settingsManager.stringSettingList[i].Value, newSettings.stringSettingList[i].Value);
            }
        }

    }



}
