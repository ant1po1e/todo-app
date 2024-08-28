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
            updateTimer.Interval = 60000;
            updateTimer.Tick += UpdateTimer_Tick;
            updateTimer.Start();
        }

        private void InitializeDataGridView()
        {
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            dataGridViewTodos.ColumnCount = 3;
            dataGridViewTodos.Columns[0].Name = "Name";
            dataGridViewTodos.Columns[1].Name = "Created Date";
            dataGridViewTodos.Columns[2].Name = "Due Date";
            dataGridViewTodos.Columns.Add(buttonColumn);
            dataGridViewTodos.Columns["Created Date"].Width = 200;
            dataGridViewTodos.Columns["Due Date"].Width = 200;

            dataGridViewTodos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewTodos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewTodos.MultiSelect = false;

            buttonColumn.HeaderText = "Action";
            buttonColumn.Name = "ActionButton";
            buttonColumn.UseColumnTextForButtonValue = false;
            buttonColumn.FlatStyle = FlatStyle.Flat;
            buttonColumn.DefaultCellStyle.Padding = new Padding(2);


            dataGridViewTodos.CellFormatting += dataGridViewTodos_CellFormatting;
            dataGridViewTodos.CellMouseEnter += dataGridViewTodos_CellMouseEnter;
            dataGridViewTodos.CellMouseLeave += dataGridViewTodos_CellMouseLeave;
            dataGridViewTodos.CellEndEdit += dataGridViewTodos_CellEndEdit;
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            UpdateRowColors();
        }

        #region TodoFunctions
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

            dataGridViewTodos.Rows.Add(newTodo.Name, FormatDate(newTodo.CreatedDate), FormatDate(newTodo.DueDate), "");
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
                    todo.Name = textBoxName.Text.Trim();
                    todo.DueDate = dateTimePickerDueDate.Value;

                    row.Cells["Name"].Value = todo.Name;
                    row.Cells["Due Date"].Value = FormatDate(todo.DueDate);

                    UpdateRowColor(row);
                    dataGridViewTodos.InvalidateRow(rowIndex);
                }
            }
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

                            int rowIndex = dataGridViewTodos.Rows.Add(todo.Name, FormatDate(todo.CreatedDate), FormatDate(todo.DueDate), "");
                            dataGridViewTodos.Rows[rowIndex].Tag = todo;
                        }
                    }
                }
                UpdateRowColors();
            }
        }
        #endregion

        #region ButtonEvents
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
        #endregion

        #region DatagridEvents
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
                textBoxName.Clear();
                dateTimePickerDueDate.Value = DateTime.Now;
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
                        return;
                    }
                    dataGridViewTodos.InvalidateCell(e.ColumnIndex, e.RowIndex);
                    UpdateRowColors();
                }
            }
        }

        private void dataGridViewTodos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
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
                    row.Cells["Due Date"].Value = FormatDate(newDueDate);
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
                dataGridViewTodos.InvalidateCell(e.ColumnIndex, e.RowIndex);
            }
        }

        #endregion

        #region UpdateFunctions
        private void UpdateRowColors()
        {
            foreach (DataGridViewRow row in dataGridViewTodos.Rows)
            {
                UpdateRowColor(row);
            }
            dataGridViewTodos.InvalidateColumn(dataGridViewTodos.Columns["ActionButton"].Index);
            dataGridViewTodos.InvalidateColumn(dataGridViewTodos.Columns["ActionButton"].Index); 
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

                        if (timeUntilDue.TotalDays < 0) 
                        {
                            row.DefaultCellStyle.BackColor = Color.Red;
                            row.DefaultCellStyle.ForeColor = Color.White;
                        }
                        else if (timeUntilDue.TotalDays <= 1) 
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
        #endregion

        #region OtherEvents
        private void textBoxName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                AddTodo();
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveTodos();
            updateTimer.Stop();
        }
        #endregion

        #region FormatDate
        private string FormatDate(DateTime date)
        {
            string dayName = date.ToString("dddd");
            string monthName = date.ToString("MMMM");
            string dayNumber = date.Day.ToString(); // This removes leading zero
            string year = date.ToString("yyyy");

            return $"{dayName}, {monthName} {dayNumber}{GetOrdinalSuffix(date.Day)} {year}";
        }

        private string GetOrdinalSuffix(int day)
        {
            switch (day % 100)
            {
                case 11:
                case 12:
                case 13:
                    return "th";
            }

            switch (day % 10)
            {
                case 1:
                    return "st";
                case 2:
                    return "nd";
                case 3:
                    return "rd";
                default:
                    return "th";
            }
        }
        #endregion
    }

    public class TodoItem
    {
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsDone { get; set; }
    }
}
