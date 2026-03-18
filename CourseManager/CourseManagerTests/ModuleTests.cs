using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using CourseManager;

namespace CourseManagerTests
{
    [TestClass]
    public class ModuleTests
    {
        [TestMethod]
        public void Module_Constructor_WithValidName_CreatesObject()
        {
            string name = "Introduction to C#";

            var module = new Module(name);

            Assert.IsNotNull(module);
            Assert.AreEqual(name, module.Name);
        }

        [TestMethod]
        public void Module_Constructor_InitializesProgressToZero()
        {
            var module = new Module("Test Module");

            Assert.AreEqual(0, module.Progress);
        }

        [TestMethod]
        public void Module_Constructor_InitializesEmptyTopicsList()
        {
            var module = new Module("Test Module");

            Assert.IsNotNull(module.Topics);
            Assert.AreEqual(0, module.Topics.Count);
        }

        [TestMethod]
        public void Module_UpdateProgress_ValidValue_UpdatesSuccessfully()
        {
            var module = new Module("Test Module");

            module.UpdateProgress(50);

            Assert.AreEqual(50, module.Progress);
        }

        [TestMethod]
        public void Module_UpdateProgress_ZeroValue_UpdatesSuccessfully()
        {
            var module = new Module("Test Module");

            module.UpdateProgress(0);

            Assert.AreEqual(0, module.Progress);
        }

        [TestMethod]
        public void Module_UpdateProgress_HundredValue_UpdatesSuccessfully()
        {
            var module = new Module("Test Module");

            module.UpdateProgress(100);

            Assert.AreEqual(100, module.Progress);
        }

        [TestMethod]
        public void Module_AddTopic_ValidTopic_AddsSuccessfully()
        {
            var module = new Module("Test Module");
            var topic = new Topic("Topic 1");

            module.AddTopic(topic);

            Assert.AreEqual(1, module.Topics.Count);
        }

        [TestMethod]
        public void Module_UpdateProgress_NegativeValue_DoesNotUpdate()
        {
            var module = new Module("Test Module");
            decimal initialProgress = module.Progress;

            module.UpdateProgress(-10);

            Assert.AreEqual(initialProgress, module.Progress);
        }

        [TestMethod]
        public void Module_UpdateProgress_ValueOver100_DoesNotUpdate()
        {
            var module = new Module("Test Module");
            decimal initialProgress = module.Progress;

            module.UpdateProgress(150);

            Assert.AreEqual(initialProgress, module.Progress);
        }


        [TestMethod]
        public void Module_UpdateProgress_BoundaryValue_MinusOne()
        {
            var module = new Module("Test Module");

            module.UpdateProgress(-1);

            Assert.AreEqual(0, module.Progress);
        }

        [TestMethod]
        public void Module_UpdateProgress_BoundaryValue_OneHundredOne()
        {
            var module = new Module("Test Module");

            module.UpdateProgress(101);

            Assert.AreEqual(0, module.Progress);
        }

        [TestMethod]
        public void Module_DisplayModuleInfo_DoesNotCrash()
        {
            var module = new Module("Test Module");

            module.DisplayModuleInfo();
        }
    }
}