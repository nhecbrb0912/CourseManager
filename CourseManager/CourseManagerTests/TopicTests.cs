using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CourseManager;

namespace CourseManagerTests
{
    [TestClass]
    public class TopicTests
    {
        [TestMethod]
        public void Topic_Constructor_WithValidName_CreatesObject()
        {
            string name = "Variables and Data Types";

            var topic = new Topic(name);

            Assert.IsNotNull(topic);
            Assert.AreEqual(name, topic.Name);
        }

        [TestMethod]
        public void Topic_Constructor_WithEmptyName_CreatesObject()
        {
            string name = string.Empty;

            var topic = new Topic(name);

            Assert.IsNotNull(topic);
            Assert.AreEqual(string.Empty, topic.Name);
        }

        [TestMethod]
        public void Topic_Constructor_WithNullName_CreatesObject()
        {
            string name = null;

            var topic = new Topic(name);

            Assert.IsNotNull(topic);
            Assert.IsNull(topic.Name);
        }


        [TestMethod]
        public void Topic_Name_Getter_ReturnsCorrectValue()
        {
            var topic = new Topic("Test Topic");

            var name = topic.Name;

            Assert.AreEqual("Test Topic", name);
        }

        [TestMethod]
        public void Topic_Name_Setter_UpdatesValue()
        {
            var topic = new Topic("Old Name");

            topic.Name = "New Name";

            Assert.AreEqual("New Name", topic.Name);
        }


        [TestMethod]
        public void Topic_Constructor_WithLongName_CreatesObject()
        {
            string longName = new string('A', 1000);

            var topic = new Topic(longName);

            Assert.IsNotNull(topic);
            Assert.AreEqual(1000, topic.Name.Length);
        }

        [TestMethod]
        public void Topic_Constructor_WithSpecialCharacters_CreatesObject()
        {
            string name = "Topic @#$%^&*()";

            var topic = new Topic(name);

            Assert.IsNotNull(topic);
            Assert.AreEqual(name, topic.Name);
        }

        [TestMethod]
        public void Topic_Constructor_WithUnicodeCharacters_CreatesObject()
        {
            string name = "Тема на русском 中文 🎓";

            var topic = new Topic(name);

            Assert.IsNotNull(topic);
            Assert.AreEqual(name, topic.Name);
        }

        [TestMethod]
        public void Topic_TwoTopicsWithSameName_AreNotSameReference()
        {
            var topic1 = new Topic("Same Name");
            var topic2 = new Topic("Same Name");

            Assert.AreNotSame(topic1, topic2);
        }

        [TestMethod]
        public void Topic_Name_CaseSensitive_Comparison()
        {
            var topic1 = new Topic("Test");
            var topic2 = new Topic("test");

            Assert.AreNotEqual(topic1.Name, topic2.Name);
        }

        [TestMethod]
        public void Topic_Name_ChangeMultipleTimes_UpdatesCorrectly()
        {
            var topic = new Topic("First");

            topic.Name = "Second";
            topic.Name = "Third";

            Assert.AreEqual("Third", topic.Name);
        }

    }
}