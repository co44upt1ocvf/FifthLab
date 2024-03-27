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
    /// Логика взаимодействия для TypesOfBenefitsPage.xaml
    /// </summary>
    public partial class TypesOfBenefitsPage : Page
    {
        private CircusEntities context = new CircusEntities();

        public TypesOfBenefitsPage()
        {
            InitializeComponent();
            Benefits.ItemsSource = context.TypesOfBenefits.ToList();
        }

        private void Benefits_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Benefits.SelectedItem != null)
            {
                var selected = Benefits.SelectedItem as TypesOfBenefits;
                Benefit.Text = selected.Title;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Benefit.Text))
            {
                MessageBox.Show("Please enter a benefit name.");
                return;
            }

            TypesOfBenefits benefit = new TypesOfBenefits();
            benefit.Title = Benefit.Text;

            context.TypesOfBenefits.Add(benefit);
            context.SaveChanges();
            Benefits.ItemsSource = context.TypesOfBenefits.ToList();

            Benefit.Clear();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Benefits.SelectedItem != null)
            {
                if (string.IsNullOrWhiteSpace(Benefit.Text))
                {
                    MessageBox.Show("Please enter a benefit name.");
                    return;
                }

                var selected = Benefits.SelectedItem as TypesOfBenefits;
                selected.Title = Benefit.Text;

                context.SaveChanges();
                Benefits.ItemsSource = context.TypesOfBenefits.ToList();

                Benefit.Clear();
            }
            else
            {
                MessageBox.Show("Select an event to change.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (Benefits.SelectedItem != null)
            {
                context.TypesOfBenefits.Remove(Benefits.SelectedItem as TypesOfBenefits);

                context.SaveChanges();
                Benefits.ItemsSource = context.TypesOfBenefits.ToList();
            }
            else
            {
                MessageBox.Show("Select an event to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
