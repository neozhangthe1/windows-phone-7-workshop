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
using TwitterApplication.ViewModels;

namespace TwitterApplication
{
    public partial class TwitterPage : PhoneApplicationPage
    {
        private TweetsViewModel _tvm;

        public TwitterPage()
        {
            InitializeComponent();
            _tvm = new TweetsViewModel();
        }

        // When page is navigated to set data context to selected item in list
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            string keyword = "";
            if (NavigationContext.QueryString.TryGetValue("keyword", out keyword))
            {
                _tvm.Keyword = keyword;
                _tvm.LoadData();
            }

            DataContext = _tvm;
        }
    }
}