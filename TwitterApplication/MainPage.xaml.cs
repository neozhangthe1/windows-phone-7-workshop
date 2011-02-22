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
        public MainViewModel Mvm { get; set; }

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        // Handle selection changed on ListBox
        private void MainListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected index is -1 (no selection) do nothing
            if (MainListBox.SelectedIndex == -1)
                return;

            // Navigate to the new page
            NavigateTo(Mvm.Items[MainListBox.SelectedIndex].Keyword);

            // Reset selected index to -1 (no selection)
            MainListBox.SelectedIndex = -1;
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            Mvm = (MainViewModel)DataContext;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Mvm.Items.Add(new MainModel() { Keyword = txtSearch.Text });
            TwitterApplication.Util.Utils.SaveKeywordList(Mvm.Items, App.STORAGE_FILE_NAME);

            NavigateTo(txtSearch.Text);
            txtSearch.Text = "";
        }

        private void NavigateTo(string keyword)
        {
            // Navigate to the new page
            NavigationService.Navigate(new Uri("/TwitterPage.xaml?keyword=" + keyword, UriKind.Relative));
        }
    }
}