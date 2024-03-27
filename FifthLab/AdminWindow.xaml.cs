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
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private Type currentPageType;

        public AdminWindow()
        {
            InitializeComponent();
            NavigateToPage(typeof(RolesPage));
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
            NavigateToPage(typeof(RolesPage));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigateToPage(typeof(EmloyeeWorkSchedulePage));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NavigateToPage(typeof(EmloyeesPage));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            NavigateToPage(typeof(LoginInformationPage));
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            NavigateToPage(typeof(PerfomancesPage));
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            NavigateToPage(typeof(TypesOfBenefitsPage));
        }
    }
}
