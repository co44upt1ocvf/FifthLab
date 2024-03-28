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
    /// Логика взаимодействия для FinancialStatementsPage.xaml
    /// </summary>
    public partial class FinancialStatementsPage : Page
    {
        private CircusEntities context = new CircusEntities();

        public FinancialStatementsPage()
        {
            InitializeComponent();
            Statements.ItemsSource = context.FinancialStatements.ToList();
            EmployeeCbx.ItemsSource = context.Employees.ToList();
        }

        private void Statements_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Statements.SelectedItem != null)
            {
                var selected = Statements.SelectedItem as FinancialStatements;
                EmployeeCbx.SelectedItem = selected.Employees;
                Quantity.Text = selected.Quantity.ToString();
                Revenue.Text = selected.Revenue.ToString();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Quantity.Text) || string.IsNullOrWhiteSpace(Revenue.Text))
            {
                MessageBox.Show("Please enter quantity and revenue.");
                return;
            }

            FinancialStatements financial = new FinancialStatements();

            if (EmployeeCbx.SelectedItem != null)
            {
                financial.Employee_ID = (EmployeeCbx.SelectedItem as Employees).Employee_ID;
            }
            else
            {
                MessageBox.Show("Please select a employee.");
                return;
            }

            if (InputValidator.IsNumeric(Quantity.Text))
            {
                financial.Quantity = Convert.ToInt32(Quantity.Text);
            }
            else
            {
                MessageBox.Show("Invalid quantity input.");
                return;
            }

            if (InputValidator.IsNumeric(Revenue.Text))
            {
                financial.Revenue = Convert.ToDecimal(Revenue.Text);
            }
            else
            {
                MessageBox.Show("Invalid revenue input.");
                return;
            }

            context.FinancialStatements.Add(financial);
            context.SaveChanges();
            Statements.ItemsSource = context.FinancialStatements.ToList();

            Quantity.Clear();
            Revenue.Clear();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Statements.SelectedItem != null)
            {
                if (string.IsNullOrWhiteSpace(Quantity.Text) || string.IsNullOrWhiteSpace(Revenue.Text))
                {
                    MessageBox.Show("Please enter quantity and revenue.");
                    return;
                }

                var selected = Statements.SelectedItem as FinancialStatements;

                if (EmployeeCbx.SelectedItem != null)
                {
                    selected.Employee_ID = (EmployeeCbx.SelectedItem as Employees).Employee_ID;
                }
                else
                {
                    MessageBox.Show("Please select a employee.");
                    return;
                }

                if (InputValidator.IsNumeric(Quantity.Text))
                {
                    selected.Quantity = Convert.ToInt32(Quantity.Text);
                }
                else
                {
                    MessageBox.Show("Invalid quantity input.");
                    return;
                }

                if (InputValidator.IsNumeric(Revenue.Text))
                {
                    selected.Revenue = Convert.ToDecimal(Revenue.Text);
                }
                else
                {
                    MessageBox.Show("Invalid revenue input.");
                    return;
                }

                context.SaveChanges();
                Statements.ItemsSource = context.FinancialStatements.ToList();

                Quantity.Clear();
                Revenue.Clear();
            }
            else
            {
                MessageBox.Show("Select an statement to change.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (Statements.SelectedItem != null)
            {
                var selected = Statements.SelectedItem as FinancialStatements;

                context.FinancialStatements.Remove(selected);
                context.SaveChanges();
                Statements.ItemsSource = context.FinancialStatements.ToList();
            }
            else
            {
                MessageBox.Show("Select an statement to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
