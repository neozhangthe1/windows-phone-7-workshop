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
using System.Collections.ObjectModel;
using System.ComponentModel;
using TwitterApplication.Utils;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TwitterApplication.Model;

namespace TwitterApplication.ViewModels
{
    public class TweetsViewModel : INotifyPropertyChanged
    {

        private ObservableCollection<TweetModel> _tweets;
        private ObservableCollection<FlickrModel> _flicks;

        public string Keyword { get; set; }

        public ObservableCollection<TweetModel> Tweets 
        {
            get
            {
                return _tweets;
            }
            set
            {
                if (_tweets != value)
                {
                    _tweets = value;
                    NotifyPropertyChanged("Tweets");
                }
            }
        }

        public ObservableCollection<FlickrModel> Flicks
        {
            get
            {
                return _flicks;
            }
            set
            {
                if (_flicks != value)
                {
                    _flicks = value;
                    NotifyPropertyChanged("Flicks");
                }
            }
        }


        public TweetsViewModel()
        {
            this.Tweets = new ObservableCollection<TweetModel>();
            this.Flicks = new ObservableCollection<FlickrModel>();
        }

        public void LoadData()
        {
            WebClient webClient = new WebClient();
            webClient.DownloadStringCompleted += TwitterSearchCompleted;
            webClient.DownloadStringAsync(new Uri(Configuration.TWITTER_SEARCH_URL + Keyword, UriKind.Absolute));

            WebClient webClient2 = new WebClient();
            webClient2.DownloadStringCompleted += FlickrSearchCompleted;
            webClient2.DownloadStringAsync(new Uri(Configuration.FLICKR_SEARCH_URL + Keyword, UriKind.Absolute));
        }


        private void TwitterSearchCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if(e.Error != null)
                return;
            
            JObject json = JObject.Parse(e.Result);
            JArray results = (JArray)json["results"];
            TweetModel tweetModel = null;


            ObservableCollection<TweetModel> tweets = new ObservableCollection<TweetModel>();

            foreach(JObject result in results) {
                tweetModel = new TweetModel();
                tweetModel.Content = HttpUtility.HtmlDecode((string)result["text"]);
                tweetModel.Profile = (string)result["from_user"];
                tweetModel.ProfileImage = (string)result["profile_image_url"];
                tweetModel.TimeStamp = (string)result["created_at"];

                tweets.Add(tweetModel);
            }
            this.Tweets = tweets;
       
        }

        private void FlickrSearchCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
                return;

            JObject json = JObject.Parse(e.Result);
            JObject photoInfo = (JObject)json["photos"];
            JArray photos = (JArray)photoInfo["photo"];
            FlickrModel flickrModel = null;


            ObservableCollection<FlickrModel> flicks = new ObservableCollection<FlickrModel>();

            foreach (JObject photo in photos)
            {
                flickrModel = new FlickrModel();
                flickrModel.FlickrImage = (string)photo["url_m"];
                flickrModel.Title = (string)photo["title"];

                flicks.Add(flickrModel);
            }
            this.Flicks = flicks;

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
