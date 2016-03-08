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
    public class SettingsManagerTests
    {
        [TestMethod()]
        public void SettingsManagerTestIndexingTest()
        {
            var manager = new SettingsManager(null);

            const string settingKey = "TestSetting";
            const int settingValue = 345;

            var s = new IntSetting()
            {
                Category = "none",
                HumanReadableName = "none",
                Description = "none",
                IdentificationName = settingKey,
                Value = settingValue
            };

            manager.AddSetting(s);

            Assert.AreEqual(settingValue, (int)manager[settingKey]);
        }

        [TestMethod()]
        public void SettingsManagerTestIndexingTestNull()
        {
            var manager = new SettingsManager(null);

            const string settingKey = "TestSetting";


            Assert.AreEqual(null, manager[settingKey]);
        }

    }
}