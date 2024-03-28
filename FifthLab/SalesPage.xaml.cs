using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для SalesPage.xaml
    /// </summary>
    public partial class SalesPage : Page
    {
        private CircusEntities context = new CircusEntities();

        public SalesPage()
        {
            InitializeComponent();
            Sales.ItemsSource = context.Sales.ToList();
            EmployeeCbx.ItemsSource = context.Employees.ToList();
            ReserveCbx.ItemsSource = context.Reserves.ToList();
            DiscountCbx.ItemsSource = context.Discounts.ToList();
            PaymentCbx.ItemsSource = context.PaymentTypes.ToList();
            DateS.SelectedDate = DateTime.Now;
        }

        private void Sales_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Sales.SelectedItem != null)
            {
                var selected = Sales.SelectedItem as Sales;
                EmployeeCbx.SelectedItem = selected.Employees;
                ReserveCbx.SelectedItem = selected.Reserves;
                DateS.SelectedDate = selected.SaleDate;
                DiscountCbx.SelectedItem = selected.Discounts;
                PaymentCbx.SelectedItem = selected.PaymentTypes;
                Amount.Text = selected.Amount.ToString();
                
            }
        }

        private void Date(object sender, SelectionChangedEventArgs e)
        {
            DateTime selectedDate = DateS.SelectedDate ?? DateTime.Today;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Amount.Text) || string.IsNullOrWhiteSpace(DateS.SelectedDate.ToString()))
            {
                MessageBox.Show("Please enter amount and date.");
                return;
            }

            Sales sales = new Sales();

            if (EmployeeCbx.SelectedItem != null)
            {
                sales.Employee_ID = (EmployeeCbx.SelectedItem as Employees).Employee_ID;
            }
            else
            {
                MessageBox.Show("Please select a employee.");
                return;
            }

            if (ReserveCbx.SelectedItem != null)
            {
                sales.Reserve_ID = (ReserveCbx.SelectedItem as Reserves).Reserve_ID;
            }
            else
            {
                MessageBox.Show("Please select a reserve code.");
                return;
            }

            sales.SaleDate = (DateTime)DateS.SelectedDate;

            if (DiscountCbx.SelectedItem != null)
            {
                sales.Discount_ID = (DiscountCbx.SelectedItem as Discounts).Discount_ID;
            }
            else
            {
                MessageBox.Show("Please select a ticket number.");
                return;
            }

            if (PaymentCbx.SelectedItem != null)
            {
                sales.Payment_ID = (PaymentCbx.SelectedItem as PaymentTypes).Payment_ID;
            }
            else
            {
                MessageBox.Show("Please select a ticket number.");
                return;
            }

            sales.Amount = Convert.ToInt32(Amount.Text);

            context.Sales.Add(sales);
            context.SaveChanges();
            Sales.ItemsSource = context.Sales.ToList();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Sales.SelectedItem != null)
            {
                if (string.IsNullOrWhiteSpace(Amount.Text) || string.IsNullOrWhiteSpace(DateS.SelectedDate.ToString()))
                {
                    MessageBox.Show("Please enter amount and date.");
                    return;
                }

                var selected = Sales.SelectedItem as Sales;

                if (EmployeeCbx.SelectedItem != null)
                {
                    selected.Employee_ID = (EmployeeCbx.SelectedItem as Employees).Employee_ID;
                }
                else
                {
                    MessageBox.Show("Please select a employee.");
                    return;
                }

                if (ReserveCbx.SelectedItem != null)
                {
                    selected.Reserve_ID = (ReserveCbx.SelectedItem as Reserves).Reserve_ID;
                }
                else
                {
                    MessageBox.Show("Please select a reserve code.");
                    return;
                }

                selected.SaleDate = (DateTime)DateS.SelectedDate;

                if (DiscountCbx.SelectedItem != null)
                {
                    selected.Discount_ID = (DiscountCbx.SelectedItem as Discounts).Discount_ID;
                }
                else
                {
                    MessageBox.Show("Please select a ticket number.");
                    return;
                }

                if (PaymentCbx.SelectedItem != null)
                {
                    selected.Payment_ID = (PaymentCbx.SelectedItem as PaymentTypes).Payment_ID;
                }
                else
                {
                    MessageBox.Show("Please select a ticket number.");
                    return;
                }

                selected.Amount = Convert.ToInt32(Amount.Text);

                context.SaveChanges();
                Sales.ItemsSource = context.Sales.ToList();
            }
            else
            {
                MessageBox.Show("Select an sale to change.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (Sales.SelectedItem != null)
            {
                var selected = Sales.SelectedItem as Sales;

                context.Sales.Remove(selected);
                context.SaveChanges();
                Sales.ItemsSource = context.Sales.ToList();
            }
            else
            {
                MessageBox.Show("Select an sale to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
