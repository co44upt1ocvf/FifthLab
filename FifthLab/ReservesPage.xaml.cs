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
    /// Логика взаимодействия для ReservesPage.xaml
    /// </summary>
    public partial class ReservesPage : Page
    {
        private CircusEntities context = new CircusEntities();

        public ReservesPage()
        {
            InitializeComponent();
            Reserves.ItemsSource = context.Reserves.ToList();
            TicketCbx.ItemsSource = context.Tickets.ToList();
            CustomerCbx.ItemsSource = context.Customers.ToList();
            DateRes.SelectedDate = DateTime.Now;
        }

        private void Reserves_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Reserves.SelectedItem != null)
            {
                var selected = Reserves.SelectedItem as Reserves;
                TicketCbx.SelectedItem = selected.Tickets;
                CustomerCbx.SelectedItem = selected.Customers;
                DateRes.SelectedDate = selected.ReserveDate;
            }
        }

        private void Date(object sender, SelectionChangedEventArgs e)
        {
            DateTime selectedDate = DateRes.SelectedDate ?? DateTime.Today;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Reserves reserve = new Reserves();

            if (TicketCbx.SelectedItem != null)
            {
                reserve.Ticket_ID = (TicketCbx.SelectedItem as Tickets).Ticket_ID;
            }
            else
            {
                MessageBox.Show("Please select a ticket number.");
                return;
            }

            if (CustomerCbx.SelectedItem != null)
            {
                reserve.Customer_ID = (CustomerCbx.SelectedItem as Customers).Customer_ID;
            }
            else
            {
                MessageBox.Show("Please select a customer.");
                return;
            }

            reserve.ReserveDate = (DateTime)DateRes.SelectedDate;

            context.Reserves.Add(reserve);
            context.SaveChanges();
            Reserves.ItemsSource = context.Reserves.ToList();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Reserves.SelectedItem != null)
            {
                var selected = Reserves.SelectedItem as Reserves;

                if (TicketCbx.SelectedItem != null)
                {
                    selected.Ticket_ID = (TicketCbx.SelectedItem as Tickets).Ticket_ID;
                }
                else
                {
                    MessageBox.Show("Please select a ticket number.");
                    return;
                }

                if (CustomerCbx.SelectedItem != null)
                {
                    selected.Customer_ID = (CustomerCbx.SelectedItem as Customers).Customer_ID;
                }
                else
                {
                    MessageBox.Show("Please select a customer.");
                    return;
                }

                selected.ReserveDate = (DateTime)DateRes.SelectedDate;

                context.SaveChanges();
                Reserves.ItemsSource = context.Reserves.ToList();
            }
            else
            {
                MessageBox.Show("Select an reserve to change.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (Reserves.SelectedItem != null)
            {
                var selected = Reserves.SelectedItem as Reserves;

                context.Reserves.Remove(selected);
                context.SaveChanges();
                Reserves.ItemsSource = context.Reserves.ToList();
            }
            else
            {
                MessageBox.Show("Select an reserve to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
