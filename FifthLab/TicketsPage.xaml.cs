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
    /// Логика взаимодействия для TicketsPage.xaml
    /// </summary>
    public partial class TicketsPage : Page
    {
        private CircusEntities context = new CircusEntities();

        public TicketsPage()
        {
            InitializeComponent();
            Tickets.ItemsSource = context.Tickets.ToList();
            PerfomanceCbx.ItemsSource = context.Perfomances.ToList();
        }

        private void Tickets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Tickets.SelectedItem != null)
            {
                var selected = Tickets.SelectedItem as Tickets;
                Place.Text = Convert.ToString(selected.PlaceNumder);
                Status.Text = selected.TicketStatus;
                PerfomanceCbx.SelectedItem = selected.Perfomances;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Place.Text) || string.IsNullOrWhiteSpace(Status.Text))
            {
                MessageBox.Show("Please enter place and status.");
                return;
            }

            Tickets ticket = new Tickets();

            if (InputValidator.IsNumeric(Place.Text))
            {
                ticket.PlaceNumder = Convert.ToInt32(Place.Text);
            }
            else
            {
                MessageBox.Show("Invalid int input.");
                return;
            }

            ticket.TicketStatus = Status.Text;

            if (PerfomanceCbx.SelectedItem != null)
            {
                ticket.Perfomance_ID = (PerfomanceCbx.SelectedItem as Perfomances).Perfomance_ID;
            }
            else
            {
                MessageBox.Show("Please select a perfomance.");
                return;
            }

            context.Tickets.Add(ticket);
            context.SaveChanges();
            Tickets.ItemsSource = context.Tickets.ToList();

            Place.Clear();
            Status.Clear();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Tickets.SelectedItem != null)
            {
                if (string.IsNullOrWhiteSpace(Place.Text) || string.IsNullOrWhiteSpace(Status.Text))
                {
                    MessageBox.Show("Please enter place and status.");
                    return;
                }

                var selected = Tickets.SelectedItem as Tickets;

                if (InputValidator.IsNumeric(Place.Text))
                {
                    selected.PlaceNumder = Convert.ToInt32(Place.Text);
                }
                else
                {
                    MessageBox.Show("Invalid int input.");
                    return;
                }

                selected.TicketStatus = Status.Text;

                if (PerfomanceCbx.SelectedItem != null)
                {
                    selected.Perfomance_ID = (PerfomanceCbx.SelectedItem as Perfomances).Perfomance_ID;
                }
                else
                {
                    MessageBox.Show("Please select a perfomance.");
                    return;
                }

                context.SaveChanges();
                Tickets.ItemsSource = context.Tickets.ToList();

                Place.Clear();
                Status.Clear();
            }
            else
            {
                MessageBox.Show("Select an ticket to change.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (Tickets.SelectedItem != null)
            {
                var selected = Tickets.SelectedItem as Tickets;

                context.Tickets.Remove(selected);
                context.SaveChanges();
                Tickets.ItemsSource = context.Tickets.ToList();
            }
            else
            {
                MessageBox.Show("Select an ticket to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
