using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using CourseManager;

namespace CourseManagerTests
{
    [TestClass]
    public class CourseTests
    {
        // === ТЕСТЫ СОЗДАНИЯ ОБЪЕКТА ===

        [TestMethod]
        public void Course_Constructor_WithValidData_CreatesObject()
        {
            string name = "C# Programming";
            string description = "Learn C# from scratch";
            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now.AddMonths(3);

            var course = new Course(name, description, startTime, endTime);

            Assert.IsNotNull(course);
            Assert.AreEqual(name, course.Name);
            Assert.AreEqual(description, course.Description);
        }

        [TestMethod]
        public void Course_Constructor_WithNullName_CreatesObject()
        {
            string name = null;
            string description = "Test";
            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now.AddMonths(1);

            var course = new Course(name, description, startTime, endTime);

            Assert.IsNotNull(course);
            Assert.IsNull(course.Name);
        }

        [TestMethod]
        public void Course_Constructor_InitializesEmptyModulesList()
        {
            var course = new Course("Test", "Desc", DateTime.Now, DateTime.Now.AddMonths(1));

            Assert.IsNotNull(course.Modules);
            Assert.AreEqual(0, course.Modules.Count);
        }

        [TestMethod]
        public void Course_AddModule_WithValidModule_AddsSuccessfully()
        {
            var course = new Course("Test", "Desc", DateTime.Now, DateTime.Now.AddMonths(1));
            var module = new Module("Module 1");

            course.AddModule(module);

            Assert.AreEqual(1, course.Modules.Count);
        }

        [TestMethod]
        public void Course_AddModule_MultipleModules_AddsAll()
        {
            var course = new Course("Test", "Desc", DateTime.Now, DateTime.Now.AddMonths(1));
            var module1 = new Module("Module 1");
            var module2 = new Module("Module 2");
            var module3 = new Module("Module 3");

            course.AddModule(module1);
            course.AddModule(module2);
            course.AddModule(module3);

            Assert.AreEqual(3, course.Modules.Count);
        }

        [TestMethod]
        public void Course_RemoveModule_ExistingModule_RemovesSuccessfully()
        {
             
            var course = new Course("Test", "Desc", DateTime.Now, DateTime.Now.AddMonths(1));
            var module = new Module("Module 1");
            course.AddModule(module);
            Assert.AreEqual(1, course.Modules.Count);

            course.RemoveModule(module);

            Assert.AreEqual(0, course.Modules.Count);
        }


        [TestMethod]
        public void Course_RemoveModule_NonExistingModule_DoesNotCrash()
        {
             
            var course = new Course("Test", "Desc", DateTime.Now, DateTime.Now.AddMonths(1));
            var module = new Module("Module 1");

            course.RemoveModule(module);
            Assert.AreEqual(0, course.Modules.Count);
        }

        
        [TestMethod]
        public void Course_Constructor_StartTimeEqualToEndTime_CreatesObject()
        {
             
            DateTime sameTime = DateTime.Now;

            var course = new Course("Test", "Desc", sameTime, sameTime);

            Assert.IsNotNull(course);
            Assert.AreEqual(sameTime, course.StartTime);
        }

        [TestMethod]
        public void Course_Constructor_EndTimeBeforeStartTime_CreatesObject()
        {
             
            DateTime startTime = DateTime.Now.AddDays(10);
            DateTime endTime = DateTime.Now;

            var course = new Course("Test", "Desc", startTime, endTime);

            Assert.IsNotNull(course);
        }

        [TestMethod]
        public void Course_ModulesList_IsAccessible_AfterCreation()
        {
             
            var course = new Course("Test", "Desc", DateTime.Now, DateTime.Now.AddMonths(1));

            var modules = course.Modules;

            Assert.IsNotNull(modules);
            Assert.IsInstanceOfType(modules, typeof(List<Module>));
        }

        [TestMethod]
        public void Course_DisplayCourseInfo_DoesNotCrash()
        {
             
            var course = new Course("Test", "Desc", DateTime.Now, DateTime.Now.AddMonths(1));

            course.DisplayCourseInfo();
        }
    }
}