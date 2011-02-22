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
using System.Diagnostics;
using System.Collections.ObjectModel;
using TwitterApplication.Model;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;

namespace TwitterApplication.Util
{
    public class Utils
    {
        public static void Trace(string msg)
        {
#if DEBUG
            Debug.WriteLine("TOMBSTONING EVENT: {0} at {1}", msg, DateTime.Now.ToLongTimeString());
#endif
        }

        public static void SaveKeywordList(ObservableCollection<MainModel> items, string fileName)
        {
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                //If user choose to save, create a new file
                using (IsolatedStorageFileStream fs = isf.CreateFile(fileName))
                {
                    //and serialize data
                    XmlSerializer ser = new XmlSerializer(typeof(ObservableCollection<MainModel>));
                    ser.Serialize(fs, items);
                    Trace("Lagret");
                }
            }
        }
    }
}
