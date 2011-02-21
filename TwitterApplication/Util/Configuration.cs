using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace TwitterApplication.Utils
{
    public class Configuration
    {
        public static string TWITTER_SEARCH_URL = "http://search.twitter.com/search.json?result_type=recent&q=";
        public static string FLICKR_SEARCH_URL = "http://api.flickr.com/services/rest/?method=flickr.photos.search&nojsoncallback=1&format=json&api_key=6b3b39e81d8f4b5f527250506e146d4b&sort=interestingness-asc&extras=url_m&per_page=10&tags=";
    }
}
