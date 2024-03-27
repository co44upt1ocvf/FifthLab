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
    /// Логика взаимодействия для RolesPage.xaml
    /// </summary>
    public partial class RolesPage : Page
    {
        private CircusEntities context = new CircusEntities();

        public RolesPage()
        {
            InitializeComponent();
            Roles.ItemsSource = context.Roles.ToList();
        }

        private void Roles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Roles.SelectedItem != null)
            {
                var selected = Roles.SelectedItem as Roles;
                RoleName.Text = selected.Title;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(RoleName.Text))
            {
                MessageBox.Show("Please enter a role name.");
                return;
            }

            Roles role = new Roles();
            role.Title = RoleName.Text;

            context.Roles.Add(role);
            context.SaveChanges();
            Roles.ItemsSource = context.Roles.ToList();

            RoleName.Clear();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Roles.SelectedItem != null)
            {
                if (string.IsNullOrWhiteSpace(RoleName.Text))
                {
                    MessageBox.Show("Please enter a role name.");
                    return;
                }

                var selected = Roles.SelectedItem as Roles;
                selected.Title = RoleName.Text;

                context.SaveChanges();
                Roles.ItemsSource = context.Roles.ToList();

                RoleName.Clear();
            }
            else
            {
                MessageBox.Show("Select an role to change.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (Roles.SelectedItem != null)
            {
                context.Roles.Remove(Roles.SelectedItem as Roles);

                context.SaveChanges();
                Roles.ItemsSource = context.Roles.ToList();
            }
            else
            {
                MessageBox.Show("Select an role to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (InputValidator.IsInvalidInputText(e.Text))
            {
                e.Handled = true;
            }
        }
    }
}
