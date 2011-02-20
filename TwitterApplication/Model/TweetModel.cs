using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace TwitterApplication
{
    public class TweetModel : INotifyPropertyChanged
    {

        private string _profileImage;
        private string _profile;
        private string _content;
        private string _timeStamp;

        public string ProfileImage
        {
            get
            {
                return _profileImage;
            }
            set
            {
                if (value != _profileImage)
                {
                    _profileImage = value;
                    NotifyPropertyChanged("ProfileImage");
                }
            }
        }

        public string Profile
        {
            get
            {
                return _profile;
            }
            set
            {
                if (value != _profile)
                {
                    _profile = value;
                    NotifyPropertyChanged("Profile");
                }
            }
        }

        public string Content
        {
            get
            {
                return _content;
            }
            set
            {
                if (value != _content)
                {
                    _content = value;
                    NotifyPropertyChanged("Content");
                }
            }
        }

        public string TimeStamp
        {
            get
            {
                return _timeStamp;
            }
            set
            {
                if (value != _timeStamp)
                {
                    _timeStamp = value;
                    NotifyPropertyChanged("TimeStamp");
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