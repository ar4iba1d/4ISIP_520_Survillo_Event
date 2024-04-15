using _4ISIP_520_Survillo_Event.Database;
using _4ISIP_520_Survillo_Event.Page;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _4ISIP_520_Survillo_Event
{
    public partial class MainWindow : Window
    {
        private Entities db = new Entities();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            // Поиск пользователя в базе данных по введенным логину и паролю.
            UserID users = db.UserID.FirstOrDefault(u => u.Login == username && u.Password == password); 

            if (users != null)
            {
                if (users.Role == "Admin")
                {
                    AdminPage adminPage = new AdminPage();
                    adminPage.Show();
                    Close();
                }
                else
                {
                    UserPage userWindow = new UserPage();
                    userWindow.Show();
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Неверно введен логин или пароль.");
            }
        }
    }
}
