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
using System.Windows.Shapes;

namespace FifthLab
{
    /// <summary>
    /// Логика взаимодействия для CashierWindow.xaml
    /// </summary>
    public partial class CashierWindow : Window
    {
        private Type currentPageType;

        public CashierWindow()
        {
            InitializeComponent();
            NavigateToPage(typeof(CustomersPage));
        }

        private void NavigateToPage(Type pageType)
        {
            if (currentPageType != pageType)
            {
                Pages.NavigationService.Navigate((Page)Activator.CreateInstance(pageType));
                currentPageType = pageType;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage(typeof(CustomersPage));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigateToPage(typeof(TicketsPage));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NavigateToPage(typeof(ReservesPage));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            NavigateToPage(typeof(SalesPage));
        }
    }
}
