using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace Testing
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public Admin()
        {
            InitializeComponent();
        }

        public void User_Fill()
        {
            Action action = () =>
            {
                DataBase dataBase = new DataBase();

                string query = @"select ID_User, Login, Password, First_Name, Second_Name, Middle_Name, Role_ID from [dbo].[User] inner join [dbo].[Role] on ID_Role = Role_ID";
                dataBase.command.CommandText = query;

                dataBase.sqlExecute(query, DataBase.act.select);
                dataBase.dependency.OnChange += Dependency_OnChange_User;
                dgUser.ItemsSource = dataBase.dataTable.DefaultView;
                dgUser.Columns[0].Visibility = Visibility.Hidden;
                dgUser.Columns[1].Header = "Логин";
                dgUser.Columns[2].Header = "Пароль";
                dgUser.Columns[3].Header = "Фамилия";
                dgUser.Columns[4].Header = "Имя";
                dgUser.Columns[5].Header = "Отчество";
                dgUser.Columns[6].Header = "Роль";
            };
            Dispatcher.Invoke(action);
        }

        public void Dependency_OnChange_User(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info != SqlNotificationInfo.Invalid) User_Fill();
        }

        public void Role_Fill()
        {
            Action action = () =>
            {
                DataBase dataBase = new DataBase();

                string query = "select * from [dbo].[Role]";
                dataBase.command.CommandText = query;

                dataBase.sqlExecute(query, DataBase.act.select);
                dataBase.dependency.OnChange += Dependency_OnChange_Role;
                cbRole.ItemsSource = dataBase.dataTable.DefaultView;
                cbRole.SelectedValuePath = dataBase.dataTable.Columns[0].ColumnName;
                cbRole.DisplayMemberPath = dataBase.dataTable.Columns[1].ColumnName;
            };
            Dispatcher.Invoke(action);
        }

        public void Dependency_OnChange_Role(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info != SqlNotificationInfo.Invalid) Role_Fill();
        }

        private void dgUser_Loaded(object sender, RoutedEventArgs e)
        {
            User_Fill();
            Role_Fill();
        }

        private void dgUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            { if (dgUser.SelectedItems.Count != 0 && dgUser.Items.Count != 0)
                {
                    DataRowView dataRowView = (DataRowView)dgUser.SelectedItems[0];
                    tbxLog.Text = dataRowView[1].ToString();
                    tbxPas.Text = dataRowView[2].ToString();
                    tbxFirst.Text = dataRowView[3].ToString();
                    tbxSecond.Text = dataRowView[4].ToString();
                    tbxMiddle.Text = dataRowView[5].ToString();
                    cbRole.Text = dataRowView[6].ToString();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataBase dataBase = new DataBase();
                string query = string.Format("insert into [dbo].[User] ([Login], [Password], [First_Name], [Second_Name], [Middle_Name], [Role_ID]) values ('{0}', '{1}', '{2}', '{3}', '{4}', {5})",
                    tbxLog.Text, tbxPas.Text, tbxFirst.Text, tbxSecond.Text, tbxMiddle.Text, cbRole.SelectedValue);

                dataBase.sqlExecute(query, DataBase.act.manipulation);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgUser.SelectedItems.Count !=0 && dgUser.Items.Count != 0)
            {
                DataBase database = new DataBase();
                DataRowView dataRow = (DataRowView)dgUser.SelectedItems[0];
                int user_id = Convert.ToInt32(dataRow["ID_User"]);

                string query = string.Format("update [dbo].[User] set [Login] = '{0}', [Password] = '{1}', [First_Name] = '{2}', [Second_Name] = '{3}', [Middle_Name] = '{4}', [Role_ID] = {5} where [ID_User] = {6}",
                    tbxLog.Text, tbxPas.Text, tbxFirst.Text, tbxSecond.Text, tbxMiddle.Text, cbRole.SelectedValue, user_id);

                database.sqlExecute(query, DataBase.act.manipulation);
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            switch(MessageBox.Show("Вы действительно хотите удалить запись?", DataBase.App_Name, MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                case MessageBoxResult.Yes:
                    if (dgUser.SelectedItems.Count != 0 && dgUser.Items.Count != 0)
                    {
                        DataBase database = new DataBase();
                        DataRowView dataRow = (DataRowView)dgUser.SelectedItems[0];
                        string query = String.Format("delete [dbo].[User] where [ID_User] = {0}", dataRow[0]);
                        database.sqlExecute(query, DataBase.act.manipulation);
                    }
                    break;
            }
        }
    }
}
