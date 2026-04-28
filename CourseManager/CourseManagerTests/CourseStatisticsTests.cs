using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace CourseManager.Tests
{
    [TestClass]
    public class CourseStatisticsTests
    {
        // === ТЕСТЫ: CalculateOverallProgress() ===

        [TestMethod]
        public void CalculateOverallProgress_WhenNoModules_ReturnsZero()
        {
            // Arrange
            var course = new Course("Test", "Desc", DateTime.Now, DateTime.Now.AddMonths(1));

            // Act
            var progress = course.CalculateOverallProgress();

            // Assert
            Assert.AreEqual(0, progress);
        }

        [TestMethod]
        public void CalculateOverallProgress_WithTwoModules_ReturnsAverage()
        {
            // Arrange
            var course = new Course("Test", "Desc", DateTime.Now, DateTime.Now.AddMonths(1));

            var module1 = new Module("Module 1");
            module1.UpdateProgress(50);
            course.AddModule(module1);

            var module2 = new Module("Module 2");
            module2.UpdateProgress(30);
            course.AddModule(module2);

            // Act
            var progress = course.CalculateOverallProgress();

            // Assert
            Assert.AreEqual(40, progress); // (50 + 30) / 2 = 40
        }

        [TestMethod]
        public void CalculateOverallProgress_WhenAllModulesCompleted_Returns100()
        {
            // Arrange
            var course = new Course("Test", "Desc", DateTime.Now, DateTime.Now.AddMonths(1));

            var module1 = new Module("Module 1");
            module1.UpdateProgress(100);
            course.AddModule(module1);

            var module2 = new Module("Module 2");
            module2.UpdateProgress(100);
            course.AddModule(module2);

            // Act
            var progress = course.CalculateOverallProgress();

            // Assert
            Assert.AreEqual(100, progress);
        }

        // === ТЕСТЫ: GetCompletedModulesCount() ===

        [TestMethod]
        public void GetCompletedModulesCount_WhenNoCompletedModules_ReturnsZero()
        {
            // Arrange
            var course = new Course("Test", "Desc", DateTime.Now, DateTime.Now.AddMonths(1));

            var module1 = new Module("Module 1");
            module1.UpdateProgress(50);
            course.AddModule(module1);

            // Act
            var count = course.GetCompletedModulesCount();

            // Assert
            Assert.AreEqual(0, count);
        }

        [TestMethod]
        public void GetCompletedModulesCount_WhenSomeModulesCompleted_ReturnsCorrectCount()
        {
            // Arrange
            var course = new Course("Test", "Desc", DateTime.Now, DateTime.Now.AddMonths(1));

            var module1 = new Module("Module 1");
            module1.UpdateProgress(100);
            course.AddModule(module1);

            var module2 = new Module("Module 2");
            module2.UpdateProgress(50);
            course.AddModule(module2);

            var module3 = new Module("Module 3");
            module3.UpdateProgress(100);
            course.AddModule(module3);

            // Act
            var count = course.GetCompletedModulesCount();

            // Assert
            Assert.AreEqual(2, count);
        }

        // === ТЕСТЫ: IsCourseCompleted() ===

        [TestMethod]
        public void IsCourseCompleted_WhenProgressLessThan100_ReturnsFalse()
        {
            // Arrange
            var course = new Course("Test", "Desc", DateTime.Now, DateTime.Now.AddMonths(1));

            var module1 = new Module("Module 1");
            module1.UpdateProgress(99);
            course.AddModule(module1);

            // Act
            var isCompleted = course.IsCourseCompleted();

            // Assert
            Assert.IsFalse(isCompleted);
        }

        [TestMethod]
        public void IsCourseCompleted_WhenProgressEquals100_ReturnsTrue()
        {
            // Arrange
            var course = new Course("Test", "Desc", DateTime.Now, DateTime.Now.AddMonths(1));

            var module1 = new Module("Module 1");
            module1.UpdateProgress(100);
            course.AddModule(module1);

            // Act
            var isCompleted = course.IsCourseCompleted();

            // Assert
            Assert.IsTrue(isCompleted);
        }
    }
}