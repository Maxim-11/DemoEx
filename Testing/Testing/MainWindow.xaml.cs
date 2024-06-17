using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Testing
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void tbnAuth_Click(object sender, RoutedEventArgs e)
        {
            DataBase.Users_ID = "sa";
            DataBase.Password = "123";
            DataBase.connectionString = String.Format(DataBase.connectionString, DataBase.Users_ID, DataBase.Password);
            DataBase dataBase = new DataBase();
            try
            {
                Visibility = Visibility.Hidden;

                SqlCommand sqlCommand = new SqlCommand("select [Role_ID] from dbo.[User] where Login = @Login and Password = @Password");
                sqlCommand.Parameters.AddWithValue("@Login", tbxLogin.Text);
                sqlCommand.Parameters.AddWithValue("@Password", tbxPassword.Password);
                sqlCommand.Connection = dataBase.connection;
                dataBase.connection.Open();
                int role = (int)sqlCommand.ExecuteScalar();

                if (role != null)
                {
                    if(role == 1)
                    {
                        Admin window = new Admin();
                        window.Show();
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль!");
                }
            }
            catch
            {
                MessageBox.Show("Неверный логин или пароль!");
            }
            finally
            {
                dataBase.connection.Close();
                tbxLogin.Clear();
                tbxPassword.Clear();
            }
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            Reg window = new Reg();
            this.Close();
            window.Show();
        }
    }
}
