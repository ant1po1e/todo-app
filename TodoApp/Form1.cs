using System.Windows.Forms;
using System.IO;
using System.Linq;

namespace TodoApp
{
    public partial class Form1 : Form
    {
        private const string SAVE_FILE_PATH = "todos.csv";

        public Form1()
        {
            InitializeComponent();
            InitializeDataGridView();
            LoadTodos();

            updateTimer = new System.Windows.Forms.Timer();
            updateTimer.Interval = 60000; // Check every minute
            updateTimer.Tick += UpdateTimer_Tick;
            updateTimer.Start();
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            UpdateRowColors();
        }

        private void InitializeDataGridView()
        {
            dataGridViewTodos.ColumnCount = 3; // Change this to 3
            dataGridViewTodos.Columns[0].Name = "Name";
            dataGridViewTodos.Columns[1].Name = "Created Date";
            dataGridViewTodos.Columns[2].Name = "Due Date";
            // Remove any line that adds an "Action" column here

            dataGridViewTodos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewTodos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewTodos.MultiSelect = false;

            // Add the button column
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.HeaderText = "Action";
            buttonColumn.Name = "ActionButton";
            buttonColumn.UseColumnTextForButtonValue = false;
            buttonColumn.FlatStyle = FlatStyle.Flat;
            buttonColumn.DefaultCellStyle.Padding = new Padding(2);
            dataGridViewTodos.Columns.Add(buttonColumn);

            // Add event handler for CellFormatting
            dataGridViewTodos.CellFormatting += DataGridViewTodos_CellFormatting;
            dataGridViewTodos.CellMouseEnter += dataGridViewTodos_CellMouseEnter;
            dataGridViewTodos.CellMouseLeave += dataGridViewTodos_CellMouseLeave;
            dataGridViewTodos.CellEndEdit += dataGridViewTodos_CellEndEdit;
        }

        private void AddTodo()
        {
            string name = textBoxName.Text.Trim();
            DateTime dueDate = dateTimePickerDueDate.Value;

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter a todo name.");
                return;
            }

            TodoItem newTodo = new TodoItem
            {
                Name = name,
                CreatedDate = DateTime.Now,
                DueDate = dueDate,
                IsDone = false
            };

            dataGridViewTodos.Rows.Add(newTodo.Name, newTodo.CreatedDate.ToShortDateString(), newTodo.DueDate.ToShortDateString(), "");
            dataGridViewTodos.Rows[dataGridViewTodos.Rows.Count - 2].Tag = newTodo;

            UpdateRowColors();
            textBoxName.Clear();
        }

        private void EditTodo(int rowIndex)
        {
            if (rowIndex >= 0 && rowIndex < dataGridViewTodos.Rows.Count)
            {
                DataGridViewRow row = dataGridViewTodos.Rows[rowIndex];
                TodoItem todo = row.Tag as TodoItem;
                if (todo != null)
                {
                    // Update the todo properties
                    todo.Name = textBoxName.Text.Trim();
                    todo.DueDate = dateTimePickerDueDate.Value;

                    // Update the row cells
                    row.Cells["Name"].Value = todo.Name;
                    row.Cells["Due Date"].Value = todo.DueDate.ToShortDateString();

                    // Update the row color
                    UpdateRowColor(row);

                    // Refresh the DataGridView
                    dataGridViewTodos.InvalidateRow(rowIndex);
                }
            }
        }



        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddTodo();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewTodos.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridViewTodos.SelectedRows[0].Index;
                EditTodo(selectedIndex);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewTodos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a todo to delete.");
                return;
            } else
            {
                dataGridViewTodos.Rows.RemoveAt(dataGridViewTodos.SelectedRows[0].Index);
            }
        }

        private void dataGridViewTodos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && !dataGridViewTodos.Rows[e.RowIndex].IsNewRow)
            {
                DataGridViewRow row = dataGridViewTodos.Rows[e.RowIndex];
                textBoxName.Text = row.Cells["Name"].Value?.ToString() ?? "";

                if (DateTime.TryParse(row.Cells["Due Date"].Value?.ToString(), out DateTime dueDate))
                {
                    dateTimePickerDueDate.Value = dueDate;
                }
                else
                {
                    dateTimePickerDueDate.Value = DateTime.Now;
                }
            }
            else
            {
                // Clear the input fields when clicking on the new row or column headers
                textBoxName.Clear();
                dateTimePickerDueDate.Value = DateTime.Now;
            }
        }

        // Add this new method
        private void textBoxName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true; // Prevents the ding sound
                AddTodo();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveTodos();
            updateTimer.Stop();
        }

        private void SaveTodos()
        {
            using (StreamWriter writer = new StreamWriter(SAVE_FILE_PATH))
            {
                foreach (DataGridViewRow row in dataGridViewTodos.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        TodoItem todo = row.Tag as TodoItem;
                        if (todo != null)
                        {
                            string line = $"{todo.Name},{todo.CreatedDate},{todo.DueDate},{todo.IsDone}";
                            writer.WriteLine(line);
                        }
                    }
                }
            }
        }

        private void LoadTodos()
        {
            if (File.Exists(SAVE_FILE_PATH))
            {
                dataGridViewTodos.Rows.Clear();
                using (StreamReader reader = new StreamReader(SAVE_FILE_PATH))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] values = line.Split(',');
                        if (values.Length == 4)
                        {
                            TodoItem todo = new TodoItem
                            {
                                Name = values[0],
                                CreatedDate = DateTime.Parse(values[1]),
                                DueDate = DateTime.Parse(values[2]),
                                IsDone = bool.Parse(values[3])
                            };

                            int rowIndex = dataGridViewTodos.Rows.Add(todo.Name, todo.CreatedDate.ToShortDateString(), todo.DueDate.ToShortDateString(), "");
                            dataGridViewTodos.Rows[rowIndex].Tag = todo;
                        }
                    }
                }
                UpdateRowColors();
            }
        }
        private void UpdateRowColors()
        {
            foreach (DataGridViewRow row in dataGridViewTodos.Rows)
            {
                UpdateRowColor(row);
            }
            dataGridViewTodos.InvalidateColumn(dataGridViewTodos.Columns["ActionButton"].Index);
            dataGridViewTodos.InvalidateColumn(dataGridViewTodos.Columns["ActionButton"].Index);  // Force button column to redraw
        }

        private void UpdateRowColor(DataGridViewRow row)
        {
            if (row != null && !row.IsNewRow)
            {
                TodoItem todo = row.Tag as TodoItem;
                if (todo != null)
                {
                    if (todo.IsDone)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        TimeSpan timeUntilDue = todo.DueDate.Date - DateTime.Now.Date;

                        if (timeUntilDue.TotalDays < 0) // Past due
                        {
                            row.DefaultCellStyle.BackColor = Color.Red;
                            row.DefaultCellStyle.ForeColor = Color.White;
                        }
                        else if (timeUntilDue.TotalDays <= 1) // Due today or tomorrow
                        {
                            row.DefaultCellStyle.BackColor = Color.Yellow;
                            row.DefaultCellStyle.ForeColor = Color.Black;
                        }
                        else // Not due soon
                        {
                            row.DefaultCellStyle.BackColor = Color.White;
                            row.DefaultCellStyle.ForeColor = Color.Black;
                        }
                    }
                }
            }
        }

        private void dataGridViewTodos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewTodos.Columns["ActionButton"].Index && e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewTodos.Rows[e.RowIndex];
                TodoItem todo = row.Tag as TodoItem;

                if (todo != null)
                {
                    if (!todo.IsDone)
                    {
                        todo.IsDone = true;
                    }
                    else
                    {
                        dataGridViewTodos.Rows.RemoveAt(e.RowIndex);
                        return;  // Exit the method as the row no longer exists
                    }
                    dataGridViewTodos.InvalidateCell(e.ColumnIndex, e.RowIndex);  // Force cell to redraw
                    UpdateRowColors();  // Update colors for all rows
                }
            }
        }

        private void DataGridViewTodos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewTodos.Columns["ActionButton"].Index && e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewTodos.Rows[e.RowIndex];
                TodoItem todo = row.Tag as TodoItem;
                if (todo != null)
                {
                    if (todo.IsDone)
                    {
                        e.Value = "Delete";
                        (dataGridViewTodos.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCell).Style.BackColor = Color.Red;
                        (dataGridViewTodos.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCell).Style.ForeColor = Color.White;
                    }
                    else
                    {
                        e.Value = "Mark as Done";
                        (dataGridViewTodos.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCell).Style.BackColor = Color.Green;
                        (dataGridViewTodos.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCell).Style.ForeColor = Color.White;
                    }
                }
            }
        }

        private void dataGridViewTodos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewTodos.Columns["Due Date"].Index)
            {
                DataGridViewRow row = dataGridViewTodos.Rows[e.RowIndex];
                TodoItem todo = row.Tag as TodoItem;
                if (todo != null && DateTime.TryParse(row.Cells["Due Date"].Value?.ToString(), out DateTime newDueDate))
                {
                    todo.DueDate = newDueDate;
                    UpdateRowColor(row);
                }
            }
        }

        private void dataGridViewTodos_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewTodos.Columns["ActionButton"].Index && e.RowIndex >= 0)
            {
                dataGridViewTodos.Cursor = Cursors.Hand;
                var cell = dataGridViewTodos.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCell;
                if (cell != null)
                {
                    var originalColor = cell.Style.BackColor;
                    cell.Style.BackColor = ControlPaint.Light(originalColor);
                }
            }
        }

        private void dataGridViewTodos_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewTodos.Columns["ActionButton"].Index && e.RowIndex >= 0)
            {
                dataGridViewTodos.Cursor = Cursors.Default;
                dataGridViewTodos.InvalidateCell(e.ColumnIndex, e.RowIndex);  // Reset to original color
            }
        }
    }

    public class TodoItem
    {
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsDone { get; set; }
    }
}
