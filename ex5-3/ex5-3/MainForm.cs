using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using ex5_3;

namespace ex5_2
{
    public partial class MainForm : Form
    {
        SqlConnection connection;
        StringBuilder errorMessages = new StringBuilder();
        SqlCommandBuilder builder = new SqlCommandBuilder();
        BindingSource view_table_src;
        BindingSource search_table_src;
        UniversityDataSet UniDB = new UniversityDataSet();
        DataTable dataTable = new DataTable();
        SqlDataAdapter faculties1;
        SqlDataAdapter departments1;
        SqlDataAdapter teachers1;
        SqlDataAdapter students1;
        SqlDataAdapter grades1;
        SqlDataAdapter disciplines1;
        SqlDataAdapter stud_groups1;
        SqlDataAdapter dis_teach1;
        SqlDataAdapter stud_disc1;

        public MainForm(SqlConnection conn)
        {
            InitializeComponent();
            connection = conn;

            #region Tables
            view_table_src = new BindingSource();
            TableGridView.DataSource = view_table_src;
            TableGridView.ReadOnly = true;

            IList<string> namesTables = new List<string>();
            foreach (var table in UniDB.Tables)
            {
                namesTables.Add(table.ToString());
            }

            BindingSource src = new BindingSource();
            src.DataSource = namesTables;
            ChooseTableComboBox.DataSource = src;

            search_table_src = new BindingSource();
            SearchGridView1.DataSource = search_table_src;
            #endregion

            #region LoadTables
            //Полная загрузка
            faculties1 = new SqlDataAdapter("SELECT * FROM faculties", connection);
            departments1 = new SqlDataAdapter("SELECT * FROM departments", connection);
            teachers1 = new SqlDataAdapter("SELECT * FROM teachers", connection);
            students1 = new SqlDataAdapter("SELECT * FROM students", connection);
            grades1 = new SqlDataAdapter("SELECT * FROM grades", connection);
            disciplines1 = new SqlDataAdapter("SELECT * FROM disciplines", connection);
            stud_groups1 = new SqlDataAdapter("SELECT * FROM stud_groups", connection);
            dis_teach1 = new SqlDataAdapter("SELECT * FROM disciplines_teachers", connection);
            stud_disc1 = new SqlDataAdapter("SELECT * FROM students_disciplines", connection);

            faculties1.Fill(UniDB.faculties);
            departments1.Fill(UniDB.departments);
            teachers1.Fill(UniDB.teachers);
            students1.Fill(UniDB.students);
            grades1.Fill(UniDB.grades);
            disciplines1.Fill(UniDB.disciplines);
            stud_groups1.Fill(UniDB.stud_groups);
            dis_teach1.Fill(UniDB.disciplines_teachers);
            stud_disc1.Fill(UniDB.students_disciplines);
            #endregion
        }
            
        private void ViewTablebutton_Click(object sender, EventArgs e)
        {
            DataTable src_table;
            switch (ChooseTableComboBox.GetItemText(ChooseTableComboBox.SelectedItem))
            {
                case "faculties":
                    src_table = UniDB.faculties;
                    break;
                case "departments":
                    src_table = UniDB.departments;
                    break;
                case "teachers":
                    src_table = UniDB.teachers;
                    break;
                case "students":
                    src_table = UniDB.students;
                    break;
                case "grades":
                    src_table = UniDB.grades;
                    break;
                case "disciplines":
                    src_table = UniDB.disciplines;
                    break;
                case "stud_groups":
                    src_table = UniDB.stud_groups;
                    break;
                case "disciplines_teachers":
                    src_table = UniDB.disciplines_teachers;
                    break;
                default:   // "students_disciplines"
                    src_table = UniDB.students_disciplines;
                    break;
            }
            view_table_src.DataSource = src_table;

            BindingSource view_table_colums = new BindingSource();
            IList<string> namesColums = new List<string>();
            foreach (var colum in src_table.Columns)
            {
                namesColums.Add(colum.ToString());
            }
            view_table_colums.DataSource = namesColums;
            view_table_src.Filter = null;
            ChooseFieldComboBox.DataSource = view_table_colums;
        }
       
        private void Filterbutton_Click(object sender, EventArgs e)
        {
            if (EnterFieldtextBox.Text.Length != 0)
            {
                view_table_src.Filter = ChooseFieldComboBox.GetItemText(ChooseFieldComboBox.SelectedItem)
                    + "= '" + EnterFieldtextBox.Text + "'";
            }
            else
            {
                view_table_src.Filter = null;
            }
            
        }

        private void Updatebutton_Click(object sender, EventArgs e)
        {
            TableGridView.CurrentCell.Value = UpdatetextBox.Text;
            try
            {
                switch (ChooseTableComboBox.GetItemText(ChooseTableComboBox.SelectedItem))
                {
                    case "faculties":
                        builder.DataAdapter = faculties1;
                        faculties1.UpdateCommand = builder.GetUpdateCommand();
                        faculties1.Update(UniDB.faculties);
                        break;
                    case "departments":
                        builder.DataAdapter = departments1;
                        departments1.UpdateCommand = builder.GetUpdateCommand();
                        departments1.Update(UniDB.departments);
                        break;
                    case "teachers":
                        builder.DataAdapter = teachers1;
                        teachers1.UpdateCommand = builder.GetUpdateCommand();
                        teachers1.Update(UniDB.teachers);
                        break;
                    case "students":
                        builder.DataAdapter = students1;
                        students1.UpdateCommand = builder.GetUpdateCommand();
                        students1.Update(UniDB.students);
                        break;
                    case "grades":
                        builder.DataAdapter = grades1;
                        grades1.UpdateCommand = builder.GetUpdateCommand();
                        grades1.Update(UniDB.grades);
                        break;
                    case "disciplines":
                        builder.DataAdapter = disciplines1;
                        disciplines1.UpdateCommand = builder.GetUpdateCommand();
                        disciplines1.Update(UniDB.disciplines);
                        break;
                    case "stud_groups":
                        builder.DataAdapter = stud_groups1;
                        stud_groups1.UpdateCommand = builder.GetUpdateCommand();
                        stud_groups1.Update(UniDB.stud_groups);
                        break;
                    case "disciplines_teachers":
                        builder.DataAdapter = dis_teach1;
                        dis_teach1.UpdateCommand = builder.GetUpdateCommand();
                        dis_teach1.Update(UniDB.disciplines_teachers);
                        break;
                    default:   // "students_disciplines"
                        builder.DataAdapter = stud_disc1;
                        stud_disc1.UpdateCommand = builder.GetUpdateCommand();
                        stud_disc1.Update(UniDB.students_disciplines);
                        break;
                }
            }
            catch (SqlException ex)
            {
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                        "Message: " + ex.Errors[i].Message + "\n" +
                        "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                        "Source: " + ex.Errors[i].Source + "\n" +
                        "Procedure: " + ex.Errors[i].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString());
            }

        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder HelpMessage = new StringBuilder();
            HelpMessage.Append("Задание №5. \n" +
                "Выполнил студент 1 курса 2 группы \n" +
                "Гутников Александр");
            MessageBox.Show(HelpMessage.ToString());
        }

        private void Addbutton1_Click(object sender, EventArgs e)
        {
            //Немного извращения
            DataRow[] id = UniDB.students.Select();
            string iddd = (id[id.Length-1].ItemArray)[0].ToString();
            int idd = Convert.ToInt32(iddd, 10) + 1;
            string cmd = string.Format("Insert Into students" +
                "(stud_ID, FirstName, SecondName, LastName, group_ID)" +
                "Values('{0}',N'{1}',N'{2}', N'{3}', '{4}')",
                 idd, AddtextBox1.Text, AddtextBox2.Text, AddtextBox3.Text, AddtextBox4.Text);
            SqlCommand command2 = new SqlCommand(cmd, connection);
            try
            {
                connection.Open();
                command2.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                        "Message: " + ex.Errors[i].Message + "\n" +
                        "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                        "Source: " + ex.Errors[i].Source + "\n" +
                        "Procedure: " + ex.Errors[i].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString());
            }
            connection.Close();
            MessageBox.Show("Записи добавлены");

            SqlDataAdapter students1 = new SqlDataAdapter("SELECT * FROM students", connection);
            students1.Fill(UniDB.students);
            
        }

        private void Delbutton_Click(object sender, EventArgs e)
        {
            DataRow[] DelRows = UniDB.students.Select("FirstName LIKE '%" + DeltextBox1.Text + 
                "%' AND SecondName LIKE '%" + DeltextBox2.Text + 
                "%' AND LastName LIKE '%" + DeltextBox3.Text +
                "%' AND group_ID LIKE '%" + DeltextBox4.Text + "%'");
            DelRows[0].Delete();

            try
            {
                connection.Open();
                builder.DataAdapter = students1;
                students1.DeleteCommand = builder.GetDeleteCommand();
                students1.Update(UniDB.students);
            }
            catch (SqlException ex)
            {
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                        "Message: " + ex.Errors[i].Message + "\n" +
                        "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                        "Source: " + ex.Errors[i].Source + "\n" +
                        "Procedure: " + ex.Errors[i].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString());
            }
            connection.Close();
            MessageBox.Show("Записи удалены");
            students1.Fill(UniDB.students);
        }
        
        #region CSV
        //после добавления из CSV нужно перезаходить в приложение, ибо не перезагружаются данные
        private void CSVbutton1_Click_1(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog1.Title = "Выберите CSV файл";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                CSVtextBox.Text = openFileDialog1.FileName;
            }
        }

        public void ReadFromCsvFileParam(SqlConnection connection, string file_path)
        {
            FileStream stream = File.Open(file_path, FileMode.Open, FileAccess.Read);
            StreamReader fs = new StreamReader(stream);

            // Имя таблицы и имена атрибутов
            string table_name = fs.ReadLine();
            string[] attributes = fs.ReadLine().Split(';');

            StringBuilder query = new StringBuilder();
            query.Append("INSERT INTO " + "dbo." + table_name + " (");

            StringBuilder query_values = new StringBuilder();
            query_values.Append("VALUES (");

            //для определения типов
            StringBuilder types = new StringBuilder();
            types.Append("SET FMTONLY ON; select ");
            //Начинаем с i=1, т.к. id формируется автоматически
            //attributes.Length-1 - т.к. последний элемент - "\n"
            //query.Append(attributes[0] + ", ");
            //types.Append(attributes[0] + ", ");
            for (int i = 0; i < attributes.Length - 1; ++i)
            {
                query.Append(attributes[i]);
                types.Append(attributes[i]);
                query_values.Append("@" + attributes[i]);
                //проверяем конец ли строки?
                if (i != attributes.Length - 2)
                {
                    query.Append(", ");
                    types.Append(", ");
                    query_values.Append(", ");
                }
                else
                {
                    query.Append(")");
                    query_values.Append(")");
                    types.Append(" from " + table_name + "; SET FMTONLY OFF");
                }
            }
            //сформировали запрос
            query.Append(" " + query_values);

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = query.ToString();

            //Получение типов данных и заполнение параметров
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = types.ToString();
            SqlDataReader reader = cmd.ExecuteReader();

            for (int i = 0; i < attributes.Length - 1; ++i)
            {
                SqlDbType type = (SqlDbType)(int)reader.GetSchemaTable().Rows[i]["ProviderType"];
                int sizeT = (int)reader.GetSchemaTable().Rows[i]["ColumnSize"];
                command.Parameters.Add("@" + attributes[i], type, sizeT);
                // Console.WriteLine(type.ToString());
                // Console.WriteLine(sizeT.ToString());

            }
            reader.Close();
            //SELECT TOP 1 * FROM dbo.Object ORDER BY ID DESC
            string q = "SELECT TOP 1 * FROM " + table_name + " ORDER BY " + attributes[0] + " DESC "; //+ attributes[0] + " = (select max(" + attributes[0] + ") from " + table_name + ")";
            SqlCommand command1 = new SqlCommand(q, connection);
            SqlDataReader reader1 = command1.ExecuteReader();
            reader1.Read();
            int id; //= reader1.GetValue(0);
            if (reader1.HasRows)
            {
                id = reader1.GetInt32(0);
                //Console.WriteLine(id.ToString());
            }
            else
            {
                id = 0;
            }
            reader1.Close();
            while (!fs.EndOfStream)
            {
                string[] values = fs.ReadLine().Split(';');
                if (values.Length == 0)
                {
                    continue;
                }
                //Начинаем с i=1, т.к. id формируется автоматически
                //attributes.Length-1 - т.к. последний элемент - "\n"
                Console.WriteLine(id.ToString());
                id = id + 1;
                command.Parameters["@" + attributes[0]].Value = id;
                for (int i = 1; i < values.Length - 1; ++i)
                {
                    command.Parameters["@" + attributes[i]].Value = values[i];
                }

                //Ловим исключение, на случай каких-то проблем с запросом
                StringBuilder errorMessages = new StringBuilder();
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }
                    Console.WriteLine(errorMessages.ToString());
                }

            }

            fs.Close();

            switch (table_name)
            {
                case "faculties":
                    faculties1.Fill(UniDB.faculties);
                    break;
                case "departments":
                    departments1.Fill(UniDB.departments);
                    break;
                case "teachers":
                    teachers1.Fill(UniDB.teachers);
                    break;
                case "students":
                    students1.Fill(UniDB.students);
                    break;
                case "grades":
                    grades1.Fill(UniDB.grades);
                    break;
                case "disciplines":
                    disciplines1.Fill(UniDB.disciplines);
                    break;
                case "stud_groups":
                    stud_groups1.Fill(UniDB.stud_groups);
                    break;
                case "disciplines_teachers":
                    dis_teach1.Fill(UniDB.disciplines_teachers);
                    break;
                default:   // "students_disciplines"
                    stud_disc1.Fill(UniDB.students_disciplines);
                    break;
            }
        }

        private void CSVbutton2_Click(object sender, EventArgs e)
        {
            connection.Open();
            ReadFromCsvFileParam(connection, CSVtextBox.Text);
            connection.Close();
            MessageBox.Show("Данные загружены");
        }

        //Выгрузка в файл
        public void WriteToCsvFile(SqlConnection conn, string table_name, string file_path)
        {
            string query = "SELECT * FROM " + table_name;
            SqlCommand command = new SqlCommand(query, conn);
            SqlDataReader reader = command.ExecuteReader();


            int col_count = reader.GetSchemaTable().Rows.Count;

            StreamWriter writer = File.AppendText(file_path);
            writer.WriteLine(table_name);

            if (reader.HasRows)
            {
                for (int i = 0; i < col_count; ++i)
                {
                    writer.Write(reader.GetName(i) + ";");
                }
                writer.WriteLine();

                while (reader.Read())
                {
                    for (int i = 0; i < col_count; ++i)
                    {
                        if (reader.IsDBNull(i))
                        {
                            writer.Write(";");
                        }
                        else
                        {
                            var value = reader.GetValue(i);
                            writer.Write(value.ToString() + ";");
                        }
                    }
                    writer.WriteLine();
                }
            }
            reader.Close();
            writer.Close();
        }

        private void CSVbutton3_Click(object sender, EventArgs e)
        {
            connection.Open();
            WriteToCsvFile(connection, CSVcomboBox.GetItemText(CSVcomboBox.SelectedItem),
                "../../csv" + CSVcomboBox.GetItemText(CSVcomboBox.SelectedItem) + ".txt");
            connection.Close();
            MessageBox.Show("Данные загруженны в файл: " +
                "csv" + CSVcomboBox.GetItemText(CSVcomboBox.SelectedItem) + ".txt");
        }
        #endregion

        private void Searchbutton1_Click(object sender, EventArgs e)
        {
            dataTable.Clear();
            string cmd2 = string.Format("SELECT students.FirstName, students.LastName, stud_groups.GroupNum, grades.Year, grades.Degree " +
                "FROM students " +
                "INNER JOIN stud_groups ON students.group_ID = stud_groups.group_ID " +
                "INNER JOIN grades ON stud_groups.grade_ID = grades.grade_ID " +
                "WHERE students.FirstName = '{0}' AND " +
                "students.LastName = '{1}'"
                , SearchtextBox1.Text, SearchtextBox2.Text);
            SqlCommand command1 = new SqlCommand(cmd2, connection);
            connection.Open();
            SqlDataAdapter reader = new SqlDataAdapter(command1);
            reader.Fill(dataTable);
            if (dataTable.Rows.Count == 0)
                MessageBox.Show("Записи не найдены");
            search_table_src.DataSource = dataTable;
            SearchGridView1.Refresh();
            connection.Close();
        }

        #region Status
        private void ViewTablebutton_MouseEnter(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Отобразить выбранную таблицу";
        }

        private void ViewTablebutton_MouseLeave(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Статус";
        }

        private void ChooseTableComboBox_MouseEnter(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Выберите таблицу";
        }

        private void ChooseTableComboBox_MouseLeave(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Статус";
        }

        private void UpdatetextBox_MouseEnter(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Введите новое значение выбранной ячейки";
        }

        private void UpdatetextBox_MouseLeave(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Статус";
        }

        private void Updatebutton_MouseEnter(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Обновить значение выбранной ячейки";
        }

        private void Updatebutton_MouseLeave(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Статус";
        }

        private void ChooseFieldComboBox_MouseEnter(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Выберети столбец для фильтрации";
        }

        private void ChooseFieldComboBox_MouseLeave(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Статус";
        }

        private void EnterFieldtextBox_MouseEnter(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Введите значение для фильтрации";
        }

        private void EnterFieldtextBox_MouseLeave(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Статус";
        }

        private void Filterbutton_MouseEnter(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Отфильтровать таблицу";
        }

        private void Filterbutton_MouseLeave(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Статус";
        }

        private void SearchtextBox1_MouseEnter(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Введите имя";
        }

        private void SearchtextBox1_MouseLeave(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Статус";
        }

        private void SearchtextBox2_MouseEnter(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Введите фамилию";
        }

        private void SearchtextBox2_MouseLeave(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Статус";
        }

        private void Searchbutton1_MouseEnter(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Найти студента";
        }

        private void Searchbutton1_MouseLeave(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Статус";
        }

        private void AddtextBox1_MouseEnter(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Введите имя";
        }

        private void AddtextBox1_MouseLeave(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Статус";
        }

        private void AddtextBox2_MouseEnter(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Введите отчество";
        }

        private void AddtextBox2_MouseLeave(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Статус";
        }

        private void AddtextBox3_MouseEnter(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Введите фамилию";
        }

        private void AddtextBox3_MouseLeave(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Статус";
        }

        private void AddtextBox4_MouseEnter(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Введите id группы";
        }

        private void AddtextBox4_MouseLeave(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Статус";
        }

        private void Addbutton1_MouseEnter(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Добавить студента";
        }

        private void Addbutton1_MouseLeave(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Статус";
        }

        private void DeltextBox1_MouseEnter(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Введите имя";
        }

        private void DeltextBox1_MouseLeave(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Статус";
        }

        private void DeltextBox2_MouseEnter(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Введите отчество";
        }

        private void DeltextBox2_MouseLeave(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Статус";
        }

        private void DeltextBox3_MouseEnter(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Введите фамилию";
        }

        private void DeltextBox3_MouseLeave(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Статус";
        }

        private void DeltextBox4_MouseEnter(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Введите id группы";
        }

        private void DeltextBox4_MouseLeave(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Статус";
        }

        private void Delbutton_MouseEnter(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Удалить студента";
        }

        private void Delbutton_MouseLeave(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Статус";
        }

        private void CSVtextBox_MouseEnter(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Путь к файлу";
        }

        private void CSVtextBox_MouseLeave(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Статус";
        }

        private void CSVbutton1_MouseEnter(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Выбрать путь к файлу";
        }

        private void CSVbutton1_MouseLeave(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Статус";
        }

        private void CSVbutton2_MouseEnter(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Загрузить данные";
        }

        private void CSVbutton2_MouseLeave(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Статус";
        }

        private void CSVcomboBox_MouseEnter(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Выберите таблицу для выгрузки";
        }

        private void CSVcomboBox_MouseLeave(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Статус";
        }

        private void CSVbutton3_MouseEnter(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Выгрузить таблицу в файл";
        }

        private void CSVbutton3_MouseLeave(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Статус";
        }

        private void выходToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Хакуна матата";
        }

        private void выходToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Статус";
        }

        private void оПрограммеToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "О программе";
        }

        private void оПрограммеToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            MainStripStatusLabel.Text = "Статус";
        }
#endregion

    }
}
