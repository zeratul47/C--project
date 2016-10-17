using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ex5_2
{
    public partial class ConnectionForm : Form
    {
        SqlConnection connection;
        StringBuilder errorMessages = new StringBuilder();

        public ConnectionForm(SqlConnection connection_to_init)
        {
            InitializeComponent();

            connection = connection_to_init;
        }

        private void Connectbutton_Click(object sender, EventArgs e)
        {
            StartStripStatusLabel.Text = "подключение к серверу...";

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = @"(localdb)\v11.0";
            builder.IntegratedSecurity = (PasswordtextBox.Text == String.Empty) && (LogintextBox.Text == String.Empty);
            builder.UserID = LogintextBox.Text;
            builder.Password = PasswordtextBox.Text;
            //if (DB_textBox.Text == String.Empty)
                builder.InitialCatalog = "University1";
            //else
            //    builder.InitialCatalog = DB_textBox.Text;
            builder.PersistSecurityInfo = false;

            connection.ConnectionString = builder.ConnectionString;

            try
            {
                connection.Open();
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
                Application.Exit();
            }

            StartStripStatusLabel.Text = "соединение установлено";

            connection.Close();

            Hide();
            MainForm f2 = new MainForm(connection);
            f2.ShowDialog();

            Dispose();
        }

        private void Cancelbutton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #region Status
        private void LogintextBox_MouseEnter(object sender, EventArgs e)
        {
            StartStripStatusLabel.Text = "Введите логин. По умолчанию аутентификация Windows";
        }

        private void LogintextBox_MouseLeave(object sender, EventArgs e)
        {
            StartStripStatusLabel.Text = "Статус";
        }

        private void PasswordtextBox_MouseEnter(object sender, EventArgs e)
        {
            StartStripStatusLabel.Text = "Введите пароль. По умолчанию аутентификация Windows";
        }

        private void PasswordtextBox_MouseLeave(object sender, EventArgs e)
        {
            StartStripStatusLabel.Text = "Статус";
        }

        private void Connectbutton_MouseEnter(object sender, EventArgs e)
        {
            StartStripStatusLabel.Text = "Нажмите для подключения";
        }

        private void Connectbutton_MouseLeave(object sender, EventArgs e)
        {
            StartStripStatusLabel.Text = "Статус";
        }

        private void Cancelbutton_MouseEnter(object sender, EventArgs e)
        {
            StartStripStatusLabel.Text = "Выход";
        }

        private void Cancelbutton_MouseLeave(object sender, EventArgs e)
        {
            StartStripStatusLabel.Text = "Статус";
        }
        #endregion
    }
}
