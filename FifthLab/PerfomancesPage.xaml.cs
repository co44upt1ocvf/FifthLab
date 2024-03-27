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
    /// Логика взаимодействия для PerfomancesPage.xaml
    /// </summary>
    public partial class PerfomancesPage : Page
    {
        private CircusEntities context = new CircusEntities();

        public PerfomancesPage()
        {
            InitializeComponent();
            Perfomances.ItemsSource = context.Perfomances.ToList();
            DateEN.SelectedDate = DateTime.Now;
        }

        private void Perfomances_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Perfomances.SelectedItem != null)
            {
                var selected = Perfomances.SelectedItem as Perfomances;
                Title.Text = selected.Title;
                DateEN.SelectedDate = selected.EventDate;
                Time.Text = selected.EventTime;
                Cost.Text = selected.Cost.ToString();
            }
        }

        private void Date(object sender, SelectionChangedEventArgs e)
        {
            DateTime selectedDate = DateEN.SelectedDate ?? DateTime.Today;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Title.Text) || string.IsNullOrWhiteSpace(Time.Text) || string.IsNullOrWhiteSpace(Cost.Text))
            {
                MessageBox.Show("Please enter first and last names.");
                return;
            }

            Perfomances perfomance = new Perfomances();
            perfomance.Title = Title.Text;
            perfomance.EventDate = (DateTime)DateEN.SelectedDate;

            if (InputValidator.IsValidTimeFormat(Time.Text))
            {
                perfomance.EventTime = Time.Text;
            }
            else
            {
                MessageBox.Show("Invalid time input.");
                return;
            }

            if (InputValidator.IsNumeric(Cost.Text))
            {
                perfomance.Cost = Convert.ToDecimal(Cost.Text);
            }
            else
            {
                MessageBox.Show("Invalid cost input.");
                return;
            }

            context.Perfomances.Add(perfomance);
            context.SaveChanges();
            Perfomances.ItemsSource = context.Perfomances.ToList();

            Title.Clear();
            Time.Clear();
            Cost.Clear();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Title.Text) || string.IsNullOrWhiteSpace(Time.Text) || string.IsNullOrWhiteSpace(Cost.Text))
            {
                MessageBox.Show("Please enter first and last names.");
                return;
            }

            var selected = Perfomances.SelectedItem as Perfomances;
            selected.Title = Title.Text;
            selected.EventDate = DateTime.Now;

            if (InputValidator.IsValidTimeFormat(Time.Text))
            {
                selected.EventTime = Time.Text;
            }
            else
            {
                MessageBox.Show("Invalid time input.");
                return;
            }

            if (InputValidator.IsNumeric(Cost.Text))
            {
                selected.Cost = Convert.ToDecimal(Cost.Text);
            }
            else
            {
                MessageBox.Show("Invalid input.");
                return;
            }

            context.SaveChanges();
            Perfomances.ItemsSource = context.Perfomances.ToList();

            Title.Clear();
            Time.Clear();
            Cost.Clear();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (Perfomances.SelectedItem != null)
            {
                var selected = Perfomances.SelectedItem as Perfomances;

                context.Perfomances.Remove(selected);
                context.SaveChanges();
                Perfomances.ItemsSource = context.Perfomances.ToList();
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
