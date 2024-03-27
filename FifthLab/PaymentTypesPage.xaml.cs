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
    /// Логика взаимодействия для PaymentTypesPage.xaml
    /// </summary>
    public partial class PaymentTypesPage : Page
    {
        private CircusEntities context = new CircusEntities();

        public PaymentTypesPage()
        {
            InitializeComponent();
            Payments.ItemsSource = context.PaymentTypes.ToList();
        }

        private void Payments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Payments.SelectedItem != null)
            {
                var selected = Payments.SelectedItem as PaymentTypes;
                PaymentType.Text = selected.Title;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PaymentType.Text))
            {
                MessageBox.Show("Please enter a payment name.");
                return;
            }

            PaymentTypes payment = new PaymentTypes();
            payment.Title = PaymentType.Text;

            context.PaymentTypes.Add(payment);
            context.SaveChanges();
            Payments.ItemsSource = context.PaymentTypes.ToList();

            PaymentType.Clear();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Payments.SelectedItem != null)
            {
                if (string.IsNullOrWhiteSpace(PaymentType.Text))
                {
                    MessageBox.Show("Please enter a payment name.");
                    return;
                }

                var selected = Payments.SelectedItem as PaymentTypes;
                selected.Title = PaymentType.Text;

                context.SaveChanges();
                Payments.ItemsSource = context.PaymentTypes.ToList();

                PaymentType.Clear();
            }
            else
            {
                MessageBox.Show("Select an payment to change.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (Payments.SelectedItem != null)
            {
                context.PaymentTypes.Remove(Payments.SelectedItem as PaymentTypes);

                context.SaveChanges();
                Payments.ItemsSource = context.PaymentTypes.ToList();
            }
            else
            {
                MessageBox.Show("Select an payment to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
