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
using System.ComponentModel;

namespace TwitterApplication.Model
{
    public class FlickrModel : INotifyPropertyChanged
    {
        private string _flickrImage;
        private string _title;

        public string FlickrImage
        {
            get
            {
                return _flickrImage;
            }
            set
            {
                if (value != _flickrImage)
                {
                    _flickrImage = value;
                    NotifyPropertyChanged("FlickrImage");
                }
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                if (value != _title)
                {
                    _title = value;
                    NotifyPropertyChanged("Title");
                }
            }
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
