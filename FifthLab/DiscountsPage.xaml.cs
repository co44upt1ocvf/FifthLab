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
    /// Логика взаимодействия для DiscountsPage.xaml
    /// </summary>
    public partial class DiscountsPage : Page
    {
        private CircusEntities context = new CircusEntities();

        public DiscountsPage()
        {
            InitializeComponent();
            Discounts.ItemsSource = context.Discounts.ToList();
            BenefitCbx.ItemsSource = context.TypesOfBenefits.ToList();
        }

        private void Discounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Discounts.SelectedItem != null)
            {
                var selected = Discounts.SelectedItem as Discounts;
                Discount.Text = selected.Amount.ToString();
                BenefitCbx.SelectedItem = selected.TypesOfBenefits;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Discount.Text))
            {
                MessageBox.Show("Please enter amount.");
                return;
            }

            Discounts discount = new Discounts();

            if (InputValidator.IsNumeric(Discount.Text))
            {
                discount.Amount = Convert.ToDecimal(Discount.Text);
            }
            else
            {
                MessageBox.Show("Invalid amount input.");
                return;
            }

            if (BenefitCbx.SelectedItem != null)
            {
                discount.Benefit_ID = (BenefitCbx.SelectedItem as TypesOfBenefits).Benefit_ID;
            }
            else
            {
                MessageBox.Show("Please select a benefit.");
                return;
            }

            context.Discounts.Add(discount);
            context.SaveChanges();
            Discounts.ItemsSource = context.Discounts.ToList();

            Discount.Clear();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Discounts.SelectedItem != null)
            {
                if (string.IsNullOrWhiteSpace(Discount.Text))
                {
                    MessageBox.Show("Please enter amount.");
                    return;
                }

                var selected = Discounts.SelectedItem as Discounts;

                if (InputValidator.IsNumeric(Discount.Text))
                {
                    selected.Amount = Convert.ToDecimal(Discount.Text);
                }
                else
                {
                    MessageBox.Show("Invalid amount input.");
                    return;
                }

                if (BenefitCbx.SelectedItem != null)
                {
                    selected.Benefit_ID = (BenefitCbx.SelectedItem as TypesOfBenefits).Benefit_ID;
                }
                else
                {
                    MessageBox.Show("Please select a benefit.");
                    return;
                }

                context.SaveChanges();
                Discounts.ItemsSource = context.Discounts.ToList();

                Discount.Clear();
            }
            else
            {
                MessageBox.Show("Select an discount to change.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (Discounts.SelectedItem != null)
            {
                var selected = Discounts.SelectedItem as Discounts;

                context.Discounts.Remove(selected);
                context.SaveChanges();
                Discounts.ItemsSource = context.Discounts.ToList();
            }
            else
            {
                MessageBox.Show("Select an discount to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
