using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace FifthLab
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CircusEntities context = new CircusEntities();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Authorization_Click(object sender, RoutedEventArgs e)
        {
            string username = Login.Text;
            string password = Password.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both Username and Password.");
                return;
            }

            var user = context.LogPass.FirstOrDefault(u => u.LoginIN == username && u.PasswordIN == password);

            if (user != null)
            {
                switch (user.Role_ID)
                {
                    case 1:
                        AdminWindow adminWindow = new AdminWindow();
                        adminWindow.Show();
                        break;
                    case 2:
                        AccountantWindow accountantWindow = new AccountantWindow();
                        accountantWindow.Show();
                        break;
                    case 3:
                        CashierWindow cashierWindow = new CashierWindow();
                        cashierWindow.Show();
                        break;
                    default:
                        MessageBox.Show("There is no window for this role...");
                        break;
                }
            }
            else
            {
                MessageBox.Show("Incorrect login or password. Please try again.");
            }
        }

        private void Login_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (InputValidator.IsInvalidInputText(e.Text))
            {
                e.Handled = true;
            }
        }

        private void Password_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (InputValidator.IsInvalidInputPass(e.Text))
            {
                e.Handled = true;
            }
        }
    }
}
