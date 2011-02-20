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
using System.Windows.Navigation;

namespace TwitterApplication
{
    public partial class TwitterPage : PhoneApplicationPage
    {
        public TwitterPage()
        {
            InitializeComponent();

            DataContext = App.TweetsViewModel;
        }

        // When page is navigated to set data context to selected item in list
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string keyword = "";
            if (NavigationContext.QueryString.TryGetValue("keyword", out keyword))
            {
                App.TweetsViewModel.Keyword = keyword;
                App.TweetsViewModel.LoadData();
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            App.TweetsViewModel.RemoveData();
        }
    }
}