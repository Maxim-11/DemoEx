using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Testing
{
    /// <summary>
    /// Логика взаимодействия для Reg.xaml
    /// </summary>
    public partial class Reg : Window
    {
        public Reg()
        {
            InitializeComponent();
        }

        private void btnRegis_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(tbxLogin.Text) || string.IsNullOrWhiteSpace(tbxPassword.Password) || string.IsNullOrWhiteSpace(tbxFirst.Text) || string.IsNullOrWhiteSpace(tbxSecond.Text))
                {
                    MessageBox.Show("Заполните поля!");
                    return;
                }

                DataBase.Users_ID = "sa";
                DataBase.Password = "123";
                DataBase.connectionString = String.Format(DataBase.connectionString, DataBase.Users_ID, DataBase.Password);
                DataBase dataBase = new DataBase();
                string query = string.Format("insert into [dbo].[User] ([Login], [Password], [First_Name], [Second_Name], [Middle_Name], [Role_ID]) values ('{0}', '{1}', '{2}', '{3}', '{4}', {5})",
                    tbxLogin.Text, tbxPassword.Password, tbxFirst.Text, tbxSecond.Text, tbxMiddle.Text, 2);

                dataBase.sqlExecute(query, DataBase.act.manipulation);

                MessageBox.Show("Вы зарегестрировались!");

                MainWindow window = new MainWindow();
                this.Close();
                window.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAuth_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            this.Close();
            window.Show();
        }
    }
}
