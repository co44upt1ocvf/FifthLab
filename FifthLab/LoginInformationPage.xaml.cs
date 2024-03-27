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

namespace FifthLab
{
    /// <summary>
    /// Логика взаимодействия для LoginInformationPage.xaml
    /// </summary>
    public partial class LoginInformationPage : Page
    {
        private CircusEntities context = new CircusEntities();

        public LoginInformationPage()
        {
            InitializeComponent();
            Logs.ItemsSource = context.LogPass.ToList();
            RoleCbx.ItemsSource = context.Roles.ToList();
        }

        private void Logs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Logs.SelectedItem != null)
            {
                var selected = Logs.SelectedItem as LogPass;
                Login.Text = selected.LoginIN;
                Password.Password = selected.PasswordIN;
                RoleCbx.SelectedItem = selected.Roles;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Login.Text) || string.IsNullOrWhiteSpace(Password.Password))
            {
                MessageBox.Show("Please enter login and pass.");
                return;
            }

            LogPass log = new LogPass();

            log.LoginIN = Login.Text;
            log.PasswordIN = Password.Password;

            if (RoleCbx.SelectedItem != null)
            {
                log.Role_ID = (RoleCbx.SelectedItem as Roles).Role_ID;
            }
            else
            {
                MessageBox.Show("Please select a role.");
                return;
            }

            context.LogPass.Add(log);
            context.SaveChanges();
            Logs.ItemsSource = context.LogPass.ToList();

            Login.Clear();
            Password.Clear();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Logs.SelectedItem != null)
            {
                if (string.IsNullOrWhiteSpace(Login.Text) || string.IsNullOrWhiteSpace(Password.Password))
                {
                    MessageBox.Show("Please enter login and pass.");
                    return;
                }

                var selected = Logs.SelectedItem as LogPass;
                selected.LoginIN = Login.Text;
                selected.PasswordIN = Password.Password;

                if (RoleCbx.SelectedItem != null)
                {
                    selected.Role_ID = (RoleCbx.SelectedItem as Roles).Role_ID;
                }
                else
                {
                    MessageBox.Show("Please select a role.");
                    return;
                }

                context.SaveChanges();
                Logs.ItemsSource = context.LogPass.ToList();

                Login.Clear();
                Password.Clear();
            }
            else
            {
                MessageBox.Show("Select an log to change.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (Logs.SelectedItem != null)
            {
                var selected = Logs.SelectedItem as LogPass;

                context.LogPass.Remove(selected);
                context.SaveChanges();
                Logs.ItemsSource = context.LogPass.ToList();
            }
            else
            {
                MessageBox.Show("Select an log to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
