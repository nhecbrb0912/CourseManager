using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CourseManager.Tests
{
    [TestClass]
    public class ModuleStatisticsTests
    {
        // === ТЕСТЫ: IsCompleted() ===

        [TestMethod]
        public void IsCompleted_WhenProgressIs99_ReturnsFalse()
        {
            // Arrange
            var module = new Module("Test");
            module.UpdateProgress(99);

            // Act
            var isCompleted = module.IsCompleted();

            // Assert
            Assert.IsFalse(isCompleted);
        }

        [TestMethod]
        public void IsCompleted_WhenProgressIs100_ReturnsTrue()
        {
            // Arrange
            var module = new Module("Test");
            module.UpdateProgress(100);

            // Act
            var isCompleted = module.IsCompleted();

            // Assert
            Assert.IsTrue(isCompleted);
        }

        // === ТЕСТЫ: GetCompletedTopicsCount() ===

        [TestMethod]
        public void GetCompletedTopicsCount_WhenModuleNotCompleted_ReturnsZero()
        {
            // Arrange
            var module = new Module("Test");
            module.AddTopic(new Topic("Topic 1"));
            module.AddTopic(new Topic("Topic 2"));
            module.UpdateProgress(50);

            // Act
            var count = module.GetCompletedTopicsCount();

            // Assert
            Assert.AreEqual(0, count);
        }

        [TestMethod]
        public void GetCompletedTopicsCount_WhenModuleCompleted_ReturnsTopicsCount()
        {
            // Arrange
            var module = new Module("Test");
            module.AddTopic(new Topic("Topic 1"));
            module.AddTopic(new Topic("Topic 2"));
            module.AddTopic(new Topic("Topic 3"));
            module.UpdateProgress(100);

            // Act
            var count = module.GetCompletedTopicsCount();

            // Assert
            Assert.AreEqual(3, count);
        }
    }
}