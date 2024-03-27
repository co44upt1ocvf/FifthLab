using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для EmloyeeWorkSchedulePage.xaml
    /// </summary>
    public partial class EmloyeeWorkSchedulePage : Page
    {
        private CircusEntities context = new CircusEntities();

        public EmloyeeWorkSchedulePage()
        {
            InitializeComponent();
            Schedule.ItemsSource = context.EmloyeeWorkSchedule.ToList();
        }

        private void Schedule_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Schedule.SelectedItem != null)
            {
                var selected = Schedule.SelectedItem as EmloyeeWorkSchedule;
                Time.Text = selected.WorkTime;
                Date.Text = selected.WorkDate;
            }
        }

        private void Date_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Regex.IsMatch(e.Text, @"^[A-Za-zА-Яа-я-]+$"))
            {
                e.Handled = true;
                return;
            }
        }

        private void Time_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0) && e.Text != ":" && e.Text != "-")
            {
                e.Handled = true;
                return;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Time.Text) || string.IsNullOrWhiteSpace(Date.Text))
            {
                MessageBox.Show("Please enter both work time and date.");
                return;
            }

            EmloyeeWorkSchedule schedule = new EmloyeeWorkSchedule();

            if (InputValidator.IsValidFormatCgDn(Date.Text))
            {
                schedule.WorkDate = Date.Text;
            }
            else
            {
                MessageBox.Show("Invalid date input.");
                return;
            }

            if (InputValidator.IsValidTimeIntervalFormat(Time.Text))
            {
                schedule.WorkTime = Time.Text;
            }
            else
            {
                MessageBox.Show("Invalid time input.");
                return;
            }

            context.EmloyeeWorkSchedule.Add(schedule);
            context.SaveChanges();
            Schedule.ItemsSource = context.EmloyeeWorkSchedule.ToList();

            Time.Clear();
            Date.Clear();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Schedule.SelectedItem != null)
            {
                if (string.IsNullOrWhiteSpace(Time.Text) || string.IsNullOrWhiteSpace(Date.Text))
                {
                    MessageBox.Show("Please enter both work time and date.");
                    return;
                }

                var selected = Schedule.SelectedItem as EmloyeeWorkSchedule;

                if (InputValidator.IsValidFormatCgDn(Date.Text))
                {
                    selected.WorkDate = Date.Text;
                }
                else
                {
                    MessageBox.Show("Invalid date input.");
                    return;
                }

                if (InputValidator.IsValidTimeIntervalFormat(Time.Text))
                {
                    selected.WorkTime = Time.Text;
                }
                else
                {
                    MessageBox.Show("Invalid time input.");
                    return;
                }

                context.SaveChanges();
                Schedule.ItemsSource = context.EmloyeeWorkSchedule.ToList();

                Time.Clear();
                Date.Clear();
            }
            else
            {
                MessageBox.Show("Select an item to update.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (Schedule.SelectedItem != null)
            {
                context.EmloyeeWorkSchedule.Remove(Schedule.SelectedItem as EmloyeeWorkSchedule);

                context.SaveChanges();
                Schedule.ItemsSource = context.EmloyeeWorkSchedule.ToList();
            }
            else
            {
                MessageBox.Show("Select an item to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
