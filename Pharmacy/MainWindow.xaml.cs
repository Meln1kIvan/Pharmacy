using System;
using System.Windows;
using System.Windows.Controls;
using Pharmacy.Views;

namespace Pharmacy
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NavigationListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem selectedItem = (ListBoxItem)navigationListBox.SelectedItem;

            if (selectedItem != null)
            {
                string selectedTabHeader = selectedItem.Content.ToString();

                switch (selectedTabHeader)
                {
                    case "Dosage":
                        Dosage dosagePage = new Dosage();
                        contentFrame.Content = dosagePage;
                        break;

                    case "Drugs":
                        Drugs drugsPage = new Drugs();
                        contentFrame.Content = drugsPage;
                        break;

                    case "Prices":
                        Prices pricesPage = new Prices();
                        contentFrame.Content = pricesPage;
                        break;

                    default:
                        break;
                }
            }
        }
    }
}