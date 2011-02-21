using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using TwitterApplication.Model;
using System.Windows.Navigation;

namespace TwitterApplication
{
    public partial class MainPage : PhoneApplicationPage
    {
        private MainViewModel _mvm;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            _mvm = new MainViewModel();
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);

        }

        // When page is navigated to set data context to selected item in list
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataContext = _mvm;
        }

        // Handle selection changed on ListBox
        private void MainListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected index is -1 (no selection) do nothing
            if (MainListBox.SelectedIndex == -1)
                return;

            // Navigate to the new page
            NavigationService.Navigate(new Uri("/TwitterPage.xaml?keyword=" + _mvm.Items[MainListBox.SelectedIndex].Keyword, UriKind.Relative));

            // Reset selected index to -1 (no selection)
            MainListBox.SelectedIndex = -1;
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!_mvm.IsDataLoaded)
            {
                _mvm.LoadData();
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            _mvm.Items.Add(new MainModel() { Keyword = txtSearch.Text});
        }
    }
}