using System.Windows.Forms;

namespace CourseManager
{
    partial class RemoveModuleForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Text = "Удалить модуль";
            this.Width = 300;
            this.Height = 250;
            this.StartPosition = FormStartPosition.CenterScreen;

            var moduleListBox = new ListBox { Location = new System.Drawing.Point(10, 10), Size = new System.Drawing.Size(260, 150) };
            foreach (var module in modules)
            {
                moduleListBox.Items.Add(module);
            }

            var removeButton = new Button { Text = "Удалить", Location = new System.Drawing.Point(10, 170), Size = new System.Drawing.Size(100, 30) };
            var cancelButton = new Button { Text = "Отмена", Location = new System.Drawing.Point(120, 170), Size = new System.Drawing.Size(100, 30) };

            removeButton.Click += (s, e) =>
            {
                if (moduleListBox.SelectedItem != null)
                {
                    Module = (Module)moduleListBox.SelectedItem;
                    this.DialogResult = DialogResult.OK;
                    MessageBox.Show(
                    $"Удален модуль '{Module}'.",
                    "Успех",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Выберите модуль для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };

            cancelButton.Click += (s, e) =>
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };

            this.Controls.AddRange(new Control[] { moduleListBox, removeButton, cancelButton });
        }

        #endregion
    }
}