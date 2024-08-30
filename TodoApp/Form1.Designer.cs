namespace TodoApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            buttonAdd = new InventorySystem.Controls.CustomButton();
            buttonEdit = new InventorySystem.Controls.CustomButton();
            buttonDelete = new InventorySystem.Controls.CustomButton();
            dateTimePickerDueDate = new InventorySystem.Controls.CustomDateTimePicker();
            textBoxName = new TextBox();
            dataGridViewTodos = new DataGridView();
            updateTimer = new System.Windows.Forms.Timer(components);
            label1 = new Label();
            buttonDeleteResolved = new InventorySystem.Controls.CustomButton();
            ((System.ComponentModel.ISupportInitialize)dataGridViewTodos).BeginInit();
            SuspendLayout();
            // 
            // buttonAdd
            // 
            buttonAdd.BackColor = Color.LightGreen;
            buttonAdd.BackgroundColor = Color.LightGreen;
            buttonAdd.BorderColor = Color.PaleVioletRed;
            buttonAdd.BorderRadius = 15;
            buttonAdd.BorderSize = 0;
            buttonAdd.FlatAppearance.BorderSize = 0;
            buttonAdd.FlatStyle = FlatStyle.Flat;
            buttonAdd.Font = new Font("Verdana", 12F, FontStyle.Bold);
            buttonAdd.ForeColor = Color.Black;
            buttonAdd.Location = new Point(12, 398);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(150, 40);
            buttonAdd.TabIndex = 1;
            buttonAdd.Text = "Add";
            buttonAdd.TextColor = Color.Black;
            buttonAdd.UseVisualStyleBackColor = false;
            buttonAdd.Click += buttonAdd_Click;
            // 
            // buttonEdit
            // 
            buttonEdit.BackColor = Color.Khaki;
            buttonEdit.BackgroundColor = Color.Khaki;
            buttonEdit.BorderColor = Color.PaleVioletRed;
            buttonEdit.BorderRadius = 15;
            buttonEdit.BorderSize = 0;
            buttonEdit.FlatAppearance.BorderSize = 0;
            buttonEdit.FlatStyle = FlatStyle.Flat;
            buttonEdit.Font = new Font("Verdana", 12F, FontStyle.Bold);
            buttonEdit.ForeColor = Color.Black;
            buttonEdit.Location = new Point(168, 398);
            buttonEdit.Name = "buttonEdit";
            buttonEdit.Size = new Size(150, 40);
            buttonEdit.TabIndex = 2;
            buttonEdit.Text = "Edit";
            buttonEdit.TextColor = Color.Black;
            buttonEdit.UseVisualStyleBackColor = false;
            buttonEdit.Click += buttonEdit_Click;
            // 
            // buttonDelete
            // 
            buttonDelete.BackColor = Color.Salmon;
            buttonDelete.BackgroundColor = Color.Salmon;
            buttonDelete.BorderColor = Color.PaleVioletRed;
            buttonDelete.BorderRadius = 15;
            buttonDelete.BorderSize = 0;
            buttonDelete.FlatAppearance.BorderSize = 0;
            buttonDelete.FlatStyle = FlatStyle.Flat;
            buttonDelete.Font = new Font("Verdana", 12F, FontStyle.Bold);
            buttonDelete.ForeColor = Color.Black;
            buttonDelete.Location = new Point(324, 398);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(150, 40);
            buttonDelete.TabIndex = 3;
            buttonDelete.Text = "Delete";
            buttonDelete.TextColor = Color.Black;
            buttonDelete.UseVisualStyleBackColor = false;
            buttonDelete.Click += buttonDelete_Click;
            // 
            // dateTimePickerDueDate
            // 
            dateTimePickerDueDate.BorderColor = Color.PaleVioletRed;
            dateTimePickerDueDate.BorderSize = 0;
            dateTimePickerDueDate.Font = new Font("Segoe UI", 9.5F);
            dateTimePickerDueDate.Location = new Point(554, 403);
            dateTimePickerDueDate.MinimumSize = new Size(0, 35);
            dateTimePickerDueDate.Name = "dateTimePickerDueDate";
            dateTimePickerDueDate.Size = new Size(234, 35);
            dateTimePickerDueDate.SkinColor = Color.CornflowerBlue;
            dateTimePickerDueDate.TabIndex = 4;
            dateTimePickerDueDate.TextColor = Color.White;
            // 
            // textBoxName
            // 
            textBoxName.Location = new Point(12, 359);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new Size(462, 23);
            textBoxName.TabIndex = 5;
            textBoxName.KeyPress += textBoxName_KeyPress;
            // 
            // dataGridViewTodos
            // 
            dataGridViewTodos.BackgroundColor = Color.Snow;
            dataGridViewTodos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewTodos.Location = new Point(12, 12);
            dataGridViewTodos.Name = "dataGridViewTodos";
            dataGridViewTodos.ReadOnly = true;
            dataGridViewTodos.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridViewTodos.Size = new Size(776, 309);
            dataGridViewTodos.TabIndex = 6;
            dataGridViewTodos.CellClick += dataGridViewTodos_CellClick;
            dataGridViewTodos.CellContentClick += dataGridViewTodos_CellContentClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Verdana", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 340);
            label1.Name = "label1";
            label1.Size = new Size(88, 16);
            label1.TabIndex = 7;
            label1.Text = "Todo Name";
            // 
            // buttonDeleteResolved
            // 
            buttonDeleteResolved.BackColor = Color.Tomato;
            buttonDeleteResolved.BackgroundColor = Color.Tomato;
            buttonDeleteResolved.BorderColor = Color.PaleVioletRed;
            buttonDeleteResolved.BorderRadius = 15;
            buttonDeleteResolved.BorderSize = 0;
            buttonDeleteResolved.FlatAppearance.BorderSize = 0;
            buttonDeleteResolved.FlatStyle = FlatStyle.Flat;
            buttonDeleteResolved.Font = new Font("Verdana", 12F, FontStyle.Bold);
            buttonDeleteResolved.ForeColor = Color.Black;
            buttonDeleteResolved.Location = new Point(609, 342);
            buttonDeleteResolved.Name = "buttonDeleteResolved";
            buttonDeleteResolved.Size = new Size(179, 40);
            buttonDeleteResolved.TabIndex = 8;
            buttonDeleteResolved.Text = "Delete Resolved";
            buttonDeleteResolved.TextColor = Color.Black;
            buttonDeleteResolved.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Snow;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonDeleteResolved);
            Controls.Add(label1);
            Controls.Add(dataGridViewTodos);
            Controls.Add(textBoxName);
            Controls.Add(dateTimePickerDueDate);
            Controls.Add(buttonDelete);
            Controls.Add(buttonEdit);
            Controls.Add(buttonAdd);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Apolz Todos App";
            FormClosing += Form1_FormClosing;
            ((System.ComponentModel.ISupportInitialize)dataGridViewTodos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private InventorySystem.Controls.CustomButton customButton1;
        private InventorySystem.Controls.CustomButton customButton2;
        private InventorySystem.Controls.CustomButton customButton3;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private InventorySystem.Controls.CustomButton buttonAdd;
        private InventorySystem.Controls.CustomButton buttonEdit;
        private InventorySystem.Controls.CustomButton buttonDelete;
        private InventorySystem.Controls.CustomDateTimePicker dateTimePickerDueDate;
        private TextBox textBoxName;
        private DataGridView dataGridViewTodos;
        private System.Windows.Forms.Timer updateTimer;
        private Label label1;
        private InventorySystem.Controls.CustomButton buttonDeleteResolved;
    }
}
