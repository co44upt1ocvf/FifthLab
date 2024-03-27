using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
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
    /// Логика взаимодействия для EmloyeesPage.xaml
    /// </summary>
    public partial class EmloyeesPage : Page
    {
        private CircusEntities context = new CircusEntities();

        public EmloyeesPage()
        {
            InitializeComponent();
            Employees.ItemsSource = context.Employees.ToList();
            ScheduleCbx.ItemsSource = context.EmloyeeWorkSchedule.ToList();
            RoleCbx.ItemsSource = context.Roles.ToList();
        }

        private void Employees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Employees.SelectedItem != null)
            {
                var selected = Employees.SelectedItem as Employees;
                Firstname.Text = selected.Firstname;
                Lastname.Text = selected.Lastname;
                Middlename.Text = selected.Middlename;
                ScheduleCbx.SelectedItem = selected.EmloyeeWorkSchedule;
                RoleCbx.SelectedItem = selected.Roles;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Firstname.Text) || string.IsNullOrWhiteSpace(Lastname.Text))
            {
                MessageBox.Show("Please enter first and last names.");
                return;
            }

            Employees employee = new Employees();

            employee.Firstname = Firstname.Text;
            employee.Lastname = Lastname.Text;
            employee.Middlename = Middlename.Text;

            if (ScheduleCbx.SelectedItem != null)
            {
                employee.Schedule_ID = (ScheduleCbx.SelectedItem as EmloyeeWorkSchedule).Schedule_ID;
            }
            else
            {
                MessageBox.Show("Please select a schedule.");
                return;
            }

            if (RoleCbx.SelectedItem != null)
            {
                employee.Role_ID = (RoleCbx.SelectedItem as Roles).Role_ID;
            }
            else
            {
                MessageBox.Show("Please select a schedule.");
                return;
            }

            context.Employees.Add(employee);
            context.SaveChanges();
            Employees.ItemsSource = context.Employees.ToList();

            Firstname.Clear();
            Lastname.Clear();
            Middlename.Clear();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Firstname.Text) || string.IsNullOrWhiteSpace(Lastname.Text))
            {
                MessageBox.Show("Please enter first and last names.");
                return;
            }

            var selected = Employees.SelectedItem as Employees;

            selected.Firstname = Firstname.Text;
            selected.Lastname = Lastname.Text;
            selected.Middlename = Middlename.Text;

            if (ScheduleCbx.SelectedItem != null)
            {
                selected.Schedule_ID = (ScheduleCbx.SelectedItem as EmloyeeWorkSchedule).Schedule_ID;
            }
            else
            {
                MessageBox.Show("Please select a schedule.");
                return;
            }

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
            Employees.ItemsSource = context.Employees.ToList();

            Firstname.Clear();
            Lastname.Clear();
            Middlename.Clear();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (Employees.SelectedItem != null)
            {
                var selected = Employees.SelectedItem as Employees;

                context.Employees.Remove(selected);
                context.SaveChanges();
                Employees.ItemsSource = context.Employees.ToList();
            }
            else
            {
                MessageBox.Show("Select an employee to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
