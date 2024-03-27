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
    /// Логика взаимодействия для CustomersPage.xaml
    /// </summary>
    public partial class CustomersPage : Page
    {
        private CircusEntities context = new CircusEntities();

        public CustomersPage()
        {
            InitializeComponent();
            Customers.ItemsSource = context.Customers.ToList();
        }

        private void Customers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Customers.SelectedItem != null)
            {
                var selected = Customers.SelectedItem as Customers;
                Firstname.Text = selected.Firstname;
                Lastname.Text = selected.Lastname;
                Middlename.Text = selected.Middlename;
                Email.Text = selected.Email;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Firstname.Text) || string.IsNullOrWhiteSpace(Lastname.Text) || string.IsNullOrWhiteSpace(Email.Text))
            {
                MessageBox.Show("Please enter first, last names and emal.");
                return;
            }

            Customers customer = new Customers();

            customer.Firstname = Firstname.Text;
            customer.Lastname = Lastname.Text;
            customer.Middlename = Middlename.Text;

            if (InputValidator.IsValidEmail(Email.Text))
            {
                customer.Email = Email.Text;
            }
            else
            {
                MessageBox.Show("Invalid email format.");
                return;
            }

            context.Customers.Add(customer);
            context.SaveChanges();
            Customers.ItemsSource = context.Customers.ToList();

            Firstname.Clear();
            Lastname.Clear();
            Middlename.Clear();
            Email.Clear();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Customers.SelectedItem != null)
            {
                if (string.IsNullOrWhiteSpace(Firstname.Text) || string.IsNullOrWhiteSpace(Lastname.Text) || string.IsNullOrWhiteSpace(Email.Text))
                {
                    MessageBox.Show("Please enter first, last names and emal.");
                    return;
                }

                var selected = Customers.SelectedItem as Customers;

                selected.Firstname = Firstname.Text;
                selected.Lastname = Lastname.Text;
                selected.Middlename = Middlename.Text;

                if (InputValidator.IsValidEmail(Email.Text))
                {
                    selected.Email = Email.Text;
                }
                else
                {
                    MessageBox.Show("Invalid email format.");
                    return;
                }

                context.SaveChanges();
                Customers.ItemsSource = context.Customers.ToList();

                Firstname.Clear();
                Lastname.Clear();
                Middlename.Clear();
                Email.Clear();
            }
            else
            {
                MessageBox.Show("Select an customer to change.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (Customers.SelectedItem != null)
            {
                var selected = Customers.SelectedItem as Customers;

                context.Customers.Remove(selected);
                context.SaveChanges();
                Customers.ItemsSource = context.Customers.ToList();
            }
            else
            {
                MessageBox.Show("Select an customer to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
