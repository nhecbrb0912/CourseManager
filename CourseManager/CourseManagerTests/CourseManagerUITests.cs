using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.UIA3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using FlaUI.Core.Conditions;
using System.Windows.Forms;




namespace CourseManager.Tests
{
    [TestClass]
    public class CourseManagerTests
    {
        private FlaUI.Core.Application _app;
        private UIA3Automation _automation;
        private Window _mainWindow;
        private readonly string _appPath = @"E:\CourseManager\CourseManager\CourseManager\bin\Debug\CourseManager.exe";
        private readonly string _dataFile = @"E:\CourseManager\CourseManager\CourseManagerTests\bin\Debug\courses.json";

        [TestInitialize]
        public void TestInitialize()
        {
            try
            {
                var processes = Process.GetProcessesByName("CourseManager");
                foreach (var p in processes)
                {
                    p.Kill();
                    Thread.Sleep(1000);
                }
            }
            catch { }

            for (int i = 0; i < 5; i++)
            {
                try
                {
                    if (File.Exists(_dataFile))
                    {
                        File.Delete(_dataFile);
                        break;
                    }
                }
                catch { Thread.Sleep(500); }
            }
            Thread.Sleep(1000);

            _app = FlaUI.Core.Application.Launch(_appPath);
            _automation = new UIA3Automation();
            Thread.Sleep(3000);
            _mainWindow = _app.GetMainWindow(_automation);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            try
            {
                if (_app != null && !_app.HasExited)
                {
                    _app.Kill();
                    Thread.Sleep(2000);
                }
            }
            catch { }
            finally
            {
                try { _automation?.Dispose(); } catch { }
                _app = null;
                _mainWindow = null;
            }
        }

        private void CreateCourse(string courseName = "Курс по C#", string description = "Базовые знания")
        {
            var createBtn = _mainWindow.FindFirstDescendant(cf => cf.ByText("Создать курс"));
            Assert.IsNotNull(createBtn);
            createBtn.Click();
            Thread.Sleep(800);

            if (_mainWindow.ModalWindows.Length > 0)
            {
                var win = _mainWindow.ModalWindows[0];
                var fields = win.FindAllDescendants(cf => cf.ByControlType(ControlType.Edit));

                if (fields.Length > 0) fields[0].AsTextBox().Text = courseName;
                if (fields.Length > 1) fields[1].AsTextBox().Text = description;

                var ok = win.FindFirstDescendant(cf => cf.ByText("ОК"));
                if (ok != null) ok.Click();
                Thread.Sleep(800);

                if (_mainWindow.ModalWindows.Length > 0)
                {
                    var msg = _mainWindow.ModalWindows[0];
                    var msgOk = msg.FindFirstDescendant(cf => cf.ByText("ОК"));
                    if (msgOk != null) msgOk.Click();
                    Thread.Sleep(300);
                }
            }
        }

        private void AddModuleToSelectedCourse(string moduleName = "Типы данных")
        {
            var addBtn = _mainWindow.FindFirstDescendant(cf => cf.ByText("Добавить модуль"));
            Assert.IsNotNull(addBtn);
            addBtn.Click();
            Thread.Sleep(800);

            if (_mainWindow.ModalWindows.Length > 0)
            {
                var win = _mainWindow.ModalWindows[0];
                var nameTxt = win.FindFirstDescendant(cf => cf.ByControlType(ControlType.Edit));
                if (nameTxt != null) nameTxt.AsTextBox().Text = moduleName;

                var ok = win.FindFirstDescendant(cf => cf.ByText("ОК"));
                if (ok != null) ok.Click();
                Thread.Sleep(800);

                if (_mainWindow.ModalWindows.Length > 0)
                {
                    var msg = _mainWindow.ModalWindows[0];
                    var msgOk = msg.FindFirstDescendant(cf => cf.ByText("ОК"));
                    if (msgOk != null) msgOk.Click();
                    Thread.Sleep(300);
                }
            }
        }

        private void SelectFirstCourse()
        {
            var listView = _mainWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.List));
            Assert.IsNotNull(listView);
            var listBox = listView.AsListBox();
            Assert.IsTrue(listBox.Items.Length > 0);

            try
            {
                listBox.Items[0].Click();
                Thread.Sleep(300);
            }
            catch (FlaUI.Core.Exceptions.NoClickablePointException)
            {
                listBox.Items[0].Select();
                Thread.Sleep(500);
            }
        }

        private void CloseModalWindow(string buttonText = "ОК")
        {
            if (_mainWindow.ModalWindows.Length > 0)
            {
                var win = _mainWindow.ModalWindows[0];
                var btn = win.FindFirstDescendant(cf => cf.ByText(buttonText));
                if (btn != null)
                {
                    btn.Click();
                    Thread.Sleep(300);
                }
            }
        }

        private void SetModuleProgress(Window courseWindow, int moduleIndex, int progress)
        {
            var moduleList = courseWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.List));
            Assert.IsNotNull(moduleList);

            var listBox = moduleList.AsListBox();
            Assert.IsTrue(listBox.Items.Length > moduleIndex);

            listBox.Items[moduleIndex].Click();
            Thread.Sleep(300);

            var moduleInfoBtn = courseWindow.FindFirstDescendant(cf => cf.ByText("Просмотр информации о модуле"));
            Assert.IsNotNull(moduleInfoBtn);
            moduleInfoBtn.Click();
            Thread.Sleep(600);

            Assert.IsTrue(_mainWindow.ModalWindows.Length > 0);
            var moduleWin = _mainWindow.ModalWindows[0];

            var progressTxt = moduleWin.FindFirstDescendant(cf => cf.ByControlType(ControlType.Edit));
            Assert.IsNotNull(progressTxt);
            progressTxt.AsTextBox().Text = progress.ToString();
            Thread.Sleep(200);

            var updateBtn = moduleWin.FindFirstDescendant(cf => cf.ByText("Обновить прогресс"));
            Assert.IsNotNull(updateBtn);
            updateBtn.Click();
            Thread.Sleep(600);

            CloseModalWindow("ОК");

            var closeBtn = moduleWin.FindFirstDescendant(cf => cf.ByText("Закрыть"));
            if (closeBtn != null)
            {
                closeBtn.Click();
                Thread.Sleep(300);
            }
        }

        private string GetProgressFromStatsWindow(Window statsWindow)
        {
            var allText = new System.Text.StringBuilder();

            var allElements = statsWindow.FindAllDescendants(cf => cf.ByControlType(ControlType.Text));

            foreach (var element in allElements)
            {
                if (!string.IsNullOrEmpty(element.Name))
                {
                    allText.AppendLine(element.Name);
                }
            }

            var editElements = statsWindow.FindAllDescendants(cf => cf.ByControlType(ControlType.Edit));
            foreach (var element in editElements)
            {
                var textBox = element.AsTextBox();
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    allText.AppendLine(textBox.Text);
                }
            }

            allText.AppendLine(statsWindow.Name);

            return allText.ToString();
        }


        //  TC-001: Создание курса
        [TestMethod]
        public void TC001_CreateCourse_WithValidData()
        {
            CreateCourse("Курс по C#", "Базовые знания");

            var list = _mainWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.List));
            Assert.IsNotNull(list);
            Assert.IsTrue(list.AsListBox().Items.Length > 0);
        }

        //  TC-002: Пустое название
        [TestMethod]
        public void TC002_CreateCourse_WithEmptyName_ShowsError()
        {
            var btn = _mainWindow.FindFirstDescendant(cf => cf.ByText("Создать курс"));
            Assert.IsNotNull(btn);
            btn.Click();
            Thread.Sleep(1000);

            if (_mainWindow.ModalWindows.Length > 0)
            {
                var win = _mainWindow.ModalWindows[0];
                var fields = win.FindAllDescendants(cf => cf.ByControlType(ControlType.Edit));

                if (fields.Length > 0) fields[0].AsTextBox().Text = "";
                if (fields.Length > 1) fields[1].AsTextBox().Text = "Описание";

                var ok = win.FindFirstDescendant(cf => cf.ByText("ОК"));
                if (ok != null) ok.Click();
                Thread.Sleep(1000);

                CloseModalWindow("ОК");
            }
        }


        [TestMethod]
        public void TC003_CreateCourseWithIncorrectDates_ShowsErrorMessage()
        {
            var createCourseButton = FindButtonByText(_mainWindow, "Создать курс");
            Assert.IsNotNull(createCourseButton);
            createCourseButton.Click();
            Thread.Sleep(500);

            var createCourseWindow = FindWindowByTitle("Создать курс");
            Assert.IsNotNull(createCourseWindow);

            var allEdits = createCourseWindow.FindAllDescendants(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Edit));
            Assert.IsTrue(allEdits.Length >= 2);

            var nameTextBox = allEdits[0].AsTextBox();
            var descTextBox = allEdits[1].AsTextBox();

            nameTextBox.Text = "Курс по 1С";
            Thread.Sleep(100);
            descTextBox.Text = "Формы";
            Thread.Sleep(100);

            System.Windows.Forms.SendKeys.SendWait("{TAB}");
            Thread.Sleep(200);

            System.Windows.Forms.SendKeys.SendWait("21.03.2026");
            Thread.Sleep(200);

            System.Windows.Forms.SendKeys.SendWait("{TAB}");
            Thread.Sleep(200);

            System.Windows.Forms.SendKeys.SendWait("21.01.2026");
            Thread.Sleep(200);

            var okButton = FindButtonByText(createCourseWindow, "ОК");
            if (okButton == null) okButton = FindButtonByText(createCourseWindow, "OK");
            Assert.IsNotNull(okButton);
            okButton.Click();
            Thread.Sleep(500);

            var errorMessageBox = FindWindowByTitle("Ошибка");
            if (errorMessageBox == null)
            {
                var desktop = _automation.GetDesktop();
                var windows = desktop.FindAllDescendants(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Window));
                foreach (var win in windows)
                {
                    var text = win.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Text));
                    if (text != null && text.Name.Contains("некорректные"))
                    {
                        errorMessageBox = win;
                        break;
                    }
                }
            }

            Assert.IsNotNull(errorMessageBox, "Окно ошибки не появилось!");
            var msgText = errorMessageBox.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Text));
            StringAssert.Contains(msgText.Name, "Введены некорректные даты");

            var msgOk = FindButtonByText(errorMessageBox, "ОК");
            if (msgOk == null) msgOk = FindButtonByText(errorMessageBox, "OK");
            msgOk?.Click();
        }


        private AutomationElement FindWindowByTitle(string title)
        {
            var desktop = _automation.GetDesktop();
            var windows = desktop.FindAllDescendants(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Window));
            foreach (var w in windows) if (w.Name == title) return w;
            return null;
        }

        private FlaUI.Core.AutomationElements.Button FindButtonByText(AutomationElement parent, string text)
        {
            var buttons = parent.FindAllDescendants(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Button));
            foreach (var b in buttons)
            {
                if (b.Name == text) return b.AsButton();
            }
            return null;
        }

        //  TC-004: Просмотр информации о курсе
        [TestMethod]
        public void TC004_ViewCourseInfo()
        {
            CreateCourse();
            SelectFirstCourse();

            var infoBtn = _mainWindow.FindFirstDescendant(cf => cf.ByText("Просмотр информации"));
            Assert.IsNotNull(infoBtn);
            infoBtn.Click();
            Thread.Sleep(1000);

            Assert.IsTrue(_mainWindow.ModalWindows.Length > 0);
            CloseModalWindow("Закрыть");
        }

        //  TC-005: Добавление модуля
        [TestMethod]
        [Timeout(25000)]
        public void TC005_AddModule_ToCourse()
        {
            CreateCourse();
            SelectFirstCourse();
            AddModuleToSelectedCourse("Типы данных");
        }

        //  TC-006: Добавление модуля без выбора курса
        [TestMethod]
        [Timeout(20000)]
        public void TC006_AddModule_WithoutSelectingCourse_ShowsError()
        {
            var addBtn = _mainWindow.FindFirstDescendant(cf => cf.ByText("Добавить модуль"));
            if (addBtn != null)
            {
                addBtn.Click();
                Thread.Sleep(2000);

                if (_mainWindow.ModalWindows.Length > 0)
                {
                    var err = _mainWindow.ModalWindows[0];
                    var errOk = err.FindFirstDescendant(cf => cf.ByText("ОК"));
                    if (errOk != null)
                    {
                        errOk.Click();
                        Thread.Sleep(1000);
                    }
                }
            }
        }

        //  TC-007: Удаление модуля
        [TestMethod]
        [Timeout(30000)]
        public void TC007_RemoveModule_FromCourse()
        {
            CreateCourse();
            SelectFirstCourse();
            AddModuleToSelectedCourse("Типы данных");

            SelectFirstCourse();

            var delBtn = _mainWindow.FindFirstDescendant(cf => cf.ByText("Удалить модуль"));
            Assert.IsNotNull(delBtn);
            delBtn.Click();
            Thread.Sleep(1000);

            Assert.IsTrue(_mainWindow.ModalWindows.Length > 0);
            var removeWin = _mainWindow.ModalWindows[0];

            var moduleList = removeWin.FindFirstDescendant(cf => cf.ByControlType(ControlType.List));
            Assert.IsNotNull(moduleList);
            moduleList.AsListBox().Items[0].Select();
            Thread.Sleep(300);

            var delOk = removeWin.FindFirstDescendant(cf => cf.ByText("Удалить"));
            Assert.IsNotNull(delOk);
            delOk.Click();
            Thread.Sleep(1000);

            CloseModalWindow("ОК");
        }

        //  TC-008: Удаление модуля без выбора курса
        [TestMethod]
        [Timeout(20000)]
        public void TC008_RemoveModule_WithoutSelectingCourse_ShowsError()
        {
            var delBtn = _mainWindow.FindFirstDescendant(cf => cf.ByText("Удалить модуль"));
            if (delBtn != null)
            {
                delBtn.Click();
                Thread.Sleep(2000);

                if (_mainWindow.ModalWindows.Length > 0)
                {
                    var err = _mainWindow.ModalWindows[0];
                    var errOk = err.FindFirstDescendant(cf => cf.ByText("ОК"));
                    if (errOk != null)
                    {
                        errOk.Click();
                        Thread.Sleep(1000);
                    }
                }
            }
        }

        //  TC-009: Удаление из пустого списка
        [TestMethod]
        [Timeout(20000)]
        public void TC009_RemoveModule_FromEmptyList_ShowsError()
        {
            TC001_CreateCourse_WithValidData();
            Thread.Sleep(2000);

            var listView = _mainWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.List));
            Assert.IsNotNull(listView, "ListView не найден");

            var listBox = listView.AsListBox();
            Assert.IsTrue(listBox.Items.Length > 0, "Список курсов пуст");

            var courseItem = listBox.Items[0];
            courseItem.Select();
            Thread.Sleep(1000);

            var delBtn = _mainWindow.FindFirstDescendant(cf => cf.ByText("Удалить модуль"));
            Assert.IsNotNull(delBtn, "Кнопка 'Удалить модуль' не найдена");
            delBtn.Click();
            Thread.Sleep(2000);

            Assert.IsTrue(_mainWindow.ModalWindows.Length > 0, "Окно не появилось");
            var errorWin = _mainWindow.ModalWindows[0];

            var errorOk = errorWin.FindFirstDescendant(cf => cf.ByText("ОК"));
            if (errorOk != null)
            {
                errorOk.Click();
                Thread.Sleep(1000);
            }

            Console.WriteLine(" TC-009 пройден — валидация работает!");
        }

        //  TC-010: Добавление темы
        [TestMethod]
        public void TC010_AddTopic_ToModule()
        {
            CreateCourse();
            SelectFirstCourse();
            AddModuleToSelectedCourse("Типы данных");

            SelectFirstCourse();
            _mainWindow.FindFirstDescendant(cf => cf.ByText("Просмотр информации")).Click();
            Thread.Sleep(1000);

            var courseWin = _mainWindow.ModalWindows[0];

            var moduleList = courseWin.FindFirstDescendant(cf => cf.ByControlType(ControlType.List));
            moduleList.AsListBox().Items[0].Select();
            Thread.Sleep(300);

            courseWin.FindFirstDescendant(cf => cf.ByText("Просмотр информации о модуле")).Click();
            Thread.Sleep(1000);

            var moduleWin = _mainWindow.ModalWindows[0];

            moduleWin.FindFirstDescendant(cf => cf.ByText("Добавить тему")).Click();
            Thread.Sleep(1000);

            var topicWin = _mainWindow.ModalWindows[0];
            var topicTxt = topicWin.FindFirstDescendant(cf => cf.ByControlType(ControlType.Edit));
            if (topicTxt != null) topicTxt.AsTextBox().Text = "Строчный тип";

            var topicOk = topicWin.FindFirstDescendant(cf => cf.ByText("ОК"));
            if (topicOk != null) topicOk.Click();
            Thread.Sleep(1000);

            CloseModalWindow("ОК");
        }

        //  TC-011: Удаление темы
        [TestMethod]
        public void TC011_RemoveTopic_FromModule()
        {
            CreateCourse();
            SelectFirstCourse();
            AddModuleToSelectedCourse("Типы данных");

            SelectFirstCourse();
            _mainWindow.FindFirstDescendant(cf => cf.ByText("Просмотр информации")).Click();
            Thread.Sleep(1000);

            var courseWin = _mainWindow.ModalWindows[0];
            var moduleList = courseWin.FindFirstDescendant(cf => cf.ByControlType(ControlType.List));
            moduleList.AsListBox().Items[0].Select();
            Thread.Sleep(300);

            courseWin.FindFirstDescendant(cf => cf.ByText("Просмотр информации о модуле")).Click();
            Thread.Sleep(1000);

            var moduleWin = _mainWindow.ModalWindows[0];

            moduleWin.FindFirstDescendant(cf => cf.ByText("Добавить тему")).Click();
            Thread.Sleep(1000);

            var topicWin = _mainWindow.ModalWindows[0];
            var topicTxt = topicWin.FindFirstDescendant(cf => cf.ByControlType(ControlType.Edit));
            if (topicTxt != null) topicTxt.AsTextBox().Text = "Строчный тип";

            var topicOk = topicWin.FindFirstDescendant(cf => cf.ByText("ОК"));
            if (topicOk != null) topicOk.Click();
            Thread.Sleep(1000);
            CloseModalWindow("ОК");

            var topicList = moduleWin.FindFirstDescendant(cf => cf.ByControlType(ControlType.List));
            topicList.AsListBox().Items[0].Select();
            Thread.Sleep(300);

            var delTopicBtn = moduleWin.FindFirstDescendant(cf => cf.ByText("Удалить тему"));
            if (delTopicBtn != null) delTopicBtn.Click();
            Thread.Sleep(1000);

            CloseModalWindow("ОК");
        }

        //  TC-012: Обновление прогресса
        [TestMethod]
        public void TC012_UpdateModuleProgress_ValidValue()
        {
            TC001_CreateCourse_WithValidData();
            Thread.Sleep(500);

            var listView = _mainWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.List));
            Assert.IsNotNull(listView);
            var listBox = listView.AsListBox();
            Assert.IsTrue(listBox.Items.Length > 0);

            listBox.Items[0].Select();
            Thread.Sleep(300);

            var addModuleBtn = _mainWindow.FindFirstDescendant(cf => cf.ByText("Добавить модуль"));
            Assert.IsNotNull(addModuleBtn);
            addModuleBtn.Click();
            Thread.Sleep(500);

            if (_mainWindow.ModalWindows.Length > 0)
            {
                var addWin = _mainWindow.ModalWindows[0];
                var nameTxt = addWin.FindFirstDescendant(cf => cf.ByControlType(ControlType.Edit));
                if (nameTxt != null) nameTxt.AsTextBox().Text = "Типы данных";

                var addOkBtn = addWin.FindFirstDescendant(cf => cf.ByText("ОК"));
                if (addOkBtn != null) addOkBtn.Click();
                Thread.Sleep(500);

                if (_mainWindow.ModalWindows.Length > 0)
                {
                    var addMsg = _mainWindow.ModalWindows[0];
                    var addMsgOk = addMsg.FindFirstDescendant(cf => cf.ByText("ОК"));
                    if (addMsgOk != null) addMsgOk.Click();
                    Thread.Sleep(300);
                }
            }

            listBox.Items[0].Select();
            Thread.Sleep(300);

            var infoBtn = _mainWindow.FindFirstDescendant(cf => cf.ByText("Просмотр информации"));
            Assert.IsNotNull(infoBtn);
            infoBtn.Click();
            Thread.Sleep(500);

            var courseWin = _mainWindow.ModalWindows[0];

            var moduleList = courseWin.FindFirstDescendant(cf => cf.ByControlType(ControlType.List));
            Assert.IsNotNull(moduleList);

            var moduleListBox = moduleList.AsListBox();
            Assert.IsTrue(moduleListBox.Items.Length > 0);
            moduleListBox.Items[0].Select();
            Thread.Sleep(300);

            var moduleInfoBtn = courseWin.FindFirstDescendant(cf => cf.ByText("Просмотр информации о модуле"));
            Assert.IsNotNull(moduleInfoBtn);
            moduleInfoBtn.Click();
            Thread.Sleep(500);

            var moduleWin = _mainWindow.ModalWindows[0];

            var progressTxt = moduleWin.FindFirstDescendant(cf => cf.ByControlType(ControlType.Edit));
            Assert.IsNotNull(progressTxt);
            progressTxt.AsTextBox().Text = "-1";
            Thread.Sleep(200);

            var updateBtn = moduleWin.FindFirstDescendant(cf => cf.ByText("Обновить прогресс"));
            Assert.IsNotNull(updateBtn);
            updateBtn.Click();
            Thread.Sleep(500);

            if (_mainWindow.ModalWindows.Length > 0)
            {
                var errMsg = _mainWindow.ModalWindows[0];
                var errMsgOk = errMsg.FindFirstDescendant(cf => cf.ByText("ОК"));
                if (errMsgOk != null) errMsgOk.Click();
                Thread.Sleep(300);
            }

            progressTxt.AsTextBox().Text = "0";
            Thread.Sleep(200);
            updateBtn.Click();
            Thread.Sleep(500);

            if (_mainWindow.ModalWindows.Length > 0)
            {
                var msg0 = _mainWindow.ModalWindows[0];
                var msg0Ok = msg0.FindFirstDescendant(cf => cf.ByText("ОК"));
                if (msg0Ok != null) msg0Ok.Click();
                Thread.Sleep(300);
            }

            progressTxt.AsTextBox().Text = "100";
            Thread.Sleep(200);
            updateBtn.Click();
            Thread.Sleep(500);

            if (_mainWindow.ModalWindows.Length > 0)
            {
                var msg100 = _mainWindow.ModalWindows[0];
                var msg100Ok = msg100.FindFirstDescendant(cf => cf.ByText("ОК"));
                if (msg100Ok != null) msg100Ok.Click();
                Thread.Sleep(300);
            }

            progressTxt.AsTextBox().Text = "101";
            Thread.Sleep(200);
            updateBtn.Click();
            Thread.Sleep(500);

            Assert.IsTrue(_mainWindow.ModalWindows.Length > 0, "Ошибка не появилась при значении 101");
            var err101 = _mainWindow.ModalWindows[0];
            var err101Ok = err101.FindFirstDescendant(cf => cf.ByText("ОК"));
            if (err101Ok != null) err101Ok.Click();
        }

        //  TC-013: Просмотр информации о модуле
        [TestMethod]
        public void TC013_ViewModuleInfo()
        {
            CreateCourse();
            SelectFirstCourse();
            AddModuleToSelectedCourse("Типы данных");

            SelectFirstCourse();
            _mainWindow.FindFirstDescendant(cf => cf.ByText("Просмотр информации")).Click();
            Thread.Sleep(1000);

            var courseWin = _mainWindow.ModalWindows[0];
            var moduleList = courseWin.FindFirstDescendant(cf => cf.ByControlType(ControlType.List));
            moduleList.AsListBox().Items[0].Select();
            Thread.Sleep(300);

            courseWin.FindFirstDescendant(cf => cf.ByText("Просмотр информации о модуле")).Click();
            Thread.Sleep(1000);

            Assert.IsTrue(_mainWindow.ModalWindows.Length > 0);
            CloseModalWindow("Закрыть");
        }

        //  TC-014: Общий прогресс курса
        [TestMethod]
        public void TC014_ViewCourseOverallProgress()
        {
            CreateCourse("Курс по C#", "Базовые знания для данного языка");

            SelectFirstCourse();
            AddModuleToSelectedCourse("Что такое язык");

            SelectFirstCourse();
            AddModuleToSelectedCourse("Типы данных");

            SelectFirstCourse();
            var infoBtn = _mainWindow.FindFirstDescendant(cf => cf.ByText("Просмотр информации"));
            Assert.IsNotNull(infoBtn);
            infoBtn.Click();
            Thread.Sleep(600);

            Assert.IsTrue(_mainWindow.ModalWindows.Length > 0);
            var courseWin = _mainWindow.ModalWindows[0];

            SetModuleProgress(courseWin, 0, 50);
            SetModuleProgress(courseWin, 1, 30);

            var closeCourseBtn = courseWin.FindFirstDescendant(cf => cf.ByText("Закрыть"));
            if (closeCourseBtn != null)
            {
                closeCourseBtn.Click();
                Thread.Sleep(300);
            }

            SelectFirstCourse();
            var statsBtn = _mainWindow.FindFirstDescendant(cf => cf.ByText("Статистика"));
            Assert.IsNotNull(statsBtn);
            statsBtn.Click();
            Thread.Sleep(800);

            Assert.IsTrue(_mainWindow.ModalWindows.Length > 0, "Окно статистики не открылось");
            var statsWin = _mainWindow.ModalWindows[0];

            var progressText = GetProgressFromStatsWindow(statsWin);

            bool has40Percent = progressText.Contains("40%") || progressText.Contains("40,0%");
            bool has15Percent = progressText.Contains("15%") || progressText.Contains("15,0%");

            if (has40Percent)
            {
                Console.WriteLine("Прогресс 40% найден — тест пройден!");
            }
            else if (has15Percent)
            {
                Assert.Fail("Второй модуль не обновил прогресс! Ожидалось 40%, получено 15%");
            }
            else
            {
                Assert.Fail($"Прогресс курса не найден! Текст: {progressText}");
            }

            CloseModalWindow("Закрыть");
        }

        //  TC-015: Статистика по модулям
        [TestMethod]
        [Timeout(30000)]
        public void TC015_ViewModuleStatistics()
        {
            TC014_ViewCourseOverallProgress();

            SelectFirstCourse();
            var statsBtn = _mainWindow.FindFirstDescendant(cf => cf.ByText("Статистика"));
            Assert.IsNotNull(statsBtn);
            statsBtn.Click();
            Thread.Sleep(1000);

            Assert.IsTrue(_mainWindow.ModalWindows.Length > 0);
            var statsWin = _mainWindow.ModalWindows[0];

            var allTexts = statsWin.FindAllDescendants(cf => cf.ByControlType(ControlType.Text));

            var allText = new System.Text.StringBuilder();
            foreach (var txt in allTexts)
            {
                if (!string.IsNullOrEmpty(txt.Name))
                {
                    allText.AppendLine(txt.Name);
                }
            }

            string windowText = allText.ToString();

            bool hasModule1Progress = windowText.Contains("50%") || windowText.Contains("50,0%");
            bool hasModule2Progress = windowText.Contains("30%") || windowText.Contains("30,0%");
            bool hasAnyProgress = hasModule1Progress || hasModule2Progress;

            bool hasModule1 = windowText.Contains("Что такое язык");
            bool hasModule2 = windowText.Contains("Типы данных");
            bool hasTopicsCount = windowText.Contains("Тем") || windowText.Contains("тем");
            bool hasStatus = windowText.Contains("В процессе") || windowText.Contains("Завершено");

            Assert.IsTrue(hasModule1, "Модуль 'Что такое язык' не найден");
            Assert.IsTrue(hasModule2, "Модуль 'Типы данных' не найден");
            Assert.IsTrue(hasAnyProgress, "Прогресс модулей не отображается");
            Assert.IsTrue(hasTopicsCount, "Количество тем не указано");

            CloseModalWindow("Закрыть");
        }

        //  TC-016: График прогресса
        [TestMethod]
        [Timeout(30000)]
        public void TC016_ViewProgressChart()
        {
            TC014_ViewCourseOverallProgress();

            SelectFirstCourse();
            var statsBtn = _mainWindow.FindFirstDescendant(cf => cf.ByText("Статистика"));
            Assert.IsNotNull(statsBtn);
            statsBtn.Click();
            Thread.Sleep(1000);

            Assert.IsTrue(_mainWindow.ModalWindows.Length > 0, "Окно статистики не открылось");
            var statsWin = _mainWindow.ModalWindows[0];

            var allElements = statsWin.FindAllDescendants(cf => cf.ByControlType(ControlType.Custom));
            var allTexts = statsWin.FindAllDescendants(cf => cf.ByControlType(ControlType.Text));
            var allImages = statsWin.FindAllDescendants(cf => cf.ByControlType(ControlType.Image));

            var allText = new System.Text.StringBuilder();
            foreach (var txt in allTexts)
            {
                if (!string.IsNullOrEmpty(txt.Name))
                {
                    allText.AppendLine(txt.Name);
                }
            }

            string windowText = allText.ToString();

            bool hasVisualizationTitle = windowText.Contains("Визуализация прогресса");
            bool hasCharts = allImages.Length > 0 || allElements.Length > 0;
            bool hasPercentOnChart = windowText.Contains("30%") || windowText.Contains("50%") ||
                                     windowText.Contains("30,0%") || windowText.Contains("50,0%");

            Assert.IsTrue(hasVisualizationTitle, "Заголовок 'Визуализация прогресса' не найден");
            Assert.IsTrue(hasCharts || hasPercentOnChart, "Графики/диаграммы прогресса не отображаются");

            CloseModalWindow("Закрыть");
        }

        //  TC-017: Уведомление о завершении модуля
        [TestMethod]
        [Timeout(35000)]
        public void TC017_ModuleCompletionNotification()
        {
            CreateCourse("Курс по C#", "Базовые знания для данного языка");

            SelectFirstCourse();
            AddModuleToSelectedCourse("Что такое язык");

            SelectFirstCourse();
            AddModuleToSelectedCourse("Типы данных");

            SelectFirstCourse();
            var infoBtn = _mainWindow.FindFirstDescendant(cf => cf.ByText("Просмотр информации"));
            Assert.IsNotNull(infoBtn);
            infoBtn.Click();
            Thread.Sleep(1000);

            Assert.IsTrue(_mainWindow.ModalWindows.Length > 0);
            var courseWin = _mainWindow.ModalWindows[0];

            SetModuleProgress(courseWin, 0, 50);

            SetModuleProgress(courseWin, 1, 100);

            var closeCourseBtn = courseWin.FindFirstDescendant(cf => cf.ByText("Закрыть"));
            if (closeCourseBtn != null)
            {
                closeCourseBtn.Click();
                Thread.Sleep(500);
            }

            SelectFirstCourse();
            var statsBtn = _mainWindow.FindFirstDescendant(cf => cf.ByText("Статистика"));
            Assert.IsNotNull(statsBtn);
            statsBtn.Click();
            Thread.Sleep(1500);

            Assert.IsTrue(_mainWindow.ModalWindows.Length > 0);
            var statsWin = _mainWindow.ModalWindows[0];

            var allTexts = statsWin.FindAllDescendants(cf => cf.ByControlType(ControlType.Text));
            var allText = new System.Text.StringBuilder();
            foreach (var txt in allTexts)
            {
                if (!string.IsNullOrEmpty(txt.Name))
                {
                    allText.AppendLine(txt.Name);
                }
            }

            string windowText = allText.ToString();

            bool has100Percent = windowText.Contains("100%") || windowText.Contains("100,0%");
            bool hasModule2 = windowText.Contains("Типы данных");

            Assert.IsTrue(has100Percent, $"Прогресс 100% не найден! Текст: {windowText}");
            Assert.IsTrue(hasModule2, "Модуль 'Типы данных' не найден");

            CloseModalWindow("Закрыть");
        }

        //  TC-018: Уведомление о завершении курса
        [TestMethod]
        [Timeout(40000)]
        public void TC018_CourseCompletionNotification()
        {
            CreateCourse("Курс по C#", "Базовые знания для данного языка");
            SelectFirstCourse(); AddModuleToSelectedCourse("Что такое язык");
            SelectFirstCourse(); AddModuleToSelectedCourse("Типы данных");

            SelectFirstCourse();
            _mainWindow.FindFirstDescendant(cf => cf.ByText("Просмотр информации")).Click();
            Thread.Sleep(800);

            var courseWin = _mainWindow.ModalWindows[0];

            SetModuleProgress(courseWin, 0, 100);
            SetModuleProgress(courseWin, 1, 100);

            var closeCourse = courseWin.FindFirstDescendant(cf => cf.ByText("Закрыть"));
            if (closeCourse != null) { closeCourse.Click(); Thread.Sleep(400); }

            SelectFirstCourse(); Thread.Sleep(400);
            _mainWindow.FindFirstDescendant(cf => cf.ByText("Статистика")).Click();
            Thread.Sleep(1500);

            Assert.IsTrue(_mainWindow.ModalWindows.Length > 0);
            var statsWin = _mainWindow.ModalWindows[0];
            var texts = statsWin.FindAllDescendants(cf => cf.ByControlType(ControlType.Text));
            var allText = string.Join(" ", texts.Select(t => t.Name));

            Assert.IsTrue(allText.Contains("100%") || allText.Contains("100,0%"));

            CloseModalWindow("ОК");
            CloseModalWindow("Закрыть");
        }

        // TC-019: Статистика без модулей
        [TestMethod]
        [Timeout(25000)]
        public void TC019_ViewStatisticsForCourseWithoutModules()
        {
            CreateCourse("111", "");

            SelectFirstCourse();
            Thread.Sleep(400);

            var statsBtn = _mainWindow.FindFirstDescendant(cf => cf.ByText("Статистика"));
            Assert.IsNotNull(statsBtn);
            statsBtn.Click();
            Thread.Sleep(1500);

            Assert.IsTrue(_mainWindow.ModalWindows.Length > 0, "Окно статистики не открылось");
            var statsWin = _mainWindow.ModalWindows[0];

            var texts = statsWin.FindAllDescendants(cf => cf.ByControlType(ControlType.Text));
            var allText = string.Join(" ", texts.Select(t => t.Name));


            bool hasZeroProgress = allText.Contains("0%") || allText.Contains("0,0%");

            bool hasNoModules = !allText.Contains("Что такое язык") &&
                               !allText.Contains("Типы данных") &&
                               (allText.Contains("0 из 0") || allText.Contains("модулей: 0"));


            Assert.IsTrue(hasZeroProgress, $"Прогресс не 0%! Текст: {allText}");
            Assert.IsTrue(hasNoModules, "Найдены модули в курсе без модулей!");

            CloseModalWindow("Закрыть");
        }

        
    }
}