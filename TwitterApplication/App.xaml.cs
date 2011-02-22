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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TwitterApplication.ViewModels;
using System.Collections.ObjectModel;
using TwitterApplication.Model;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;

namespace TwitterApplication
{
    public partial class App : Application
    {
        /// <summary>
        /// Provides easy access to the root frame of the Phone Application.
        /// </summary>
        /// <returns>The root frame of the Phone Application.</returns>
        public PhoneApplicationFrame RootFrame { get; private set; }

        public static string STORAGE_FILE_NAME = "Items.dat";
        private static string STATE_KEY = "UnsavedItems";
        /// <summary>
        /// Constructor for the Application object.
        /// </summary>
        public App()
        {
            // Global handler for uncaught exceptions. 
            UnhandledException += Application_UnhandledException;

            // Show graphics profiling information while debugging.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // Display the current frame rate counters.
                //Application.Current.Host.Settings.EnableFrameRateCounter = true;

                // Show the areas of the app that are being redrawn in each frame.
                //Application.Current.Host.Settings.EnableRedrawRegions = true;

                // Enable non-production analysis visualization mode, 
                // which shows areas of a page that are being GPU accelerated with a colored overlay.
                //Application.Current.Host.Settings.EnableCacheVisualization = true;
            }

            // Standard Silverlight initialization
            InitializeComponent();

            // Phone-specific initialization
            InitializePhoneApplication();
        }

        // Code to execute when the application is launching (eg, from Start)
        // This code will not execute when the application is reactivated
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
            //Create new data object variable
            MainViewModel _mvm = new MainViewModel();
            ObservableCollection<MainModel> items = null;

            //Try to load previously saved data from IsolatedStorage
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                //Check if file exits
                if (isf.FileExists(STORAGE_FILE_NAME))
                {
                    using (IsolatedStorageFileStream fs = isf.OpenFile(STORAGE_FILE_NAME, System.IO.FileMode.Open))
                    {
                        //Read the file contents and try to deserialize it back to data object
                        XmlSerializer ser = new XmlSerializer(typeof(ObservableCollection<MainModel>));
                        object obj = ser.Deserialize(fs);

                        //If successfully deserialized, initialize data object variable with it
                        if (null != obj && obj is ObservableCollection<MainModel>)
                        {
                            items = obj as ObservableCollection<MainModel>;
                            TwitterApplication.Util.Utils.Trace("Fant " + items.Count + " elementer");
                        }
                        else
                        {
                            items = new ObservableCollection<MainModel>();
                            TwitterApplication.Util.Utils.Trace("Fant ingen elementer");
                        }
                    }
                }
                else
                {
                    //If previous data not found, create new istance
                    items = new ObservableCollection<MainModel>();
                    TwitterApplication.Util.Utils.Trace("Fant ikke fil");
                }
                    
            }
            _mvm.Items = items;
            //Set data variable (either recovered or new) as a DataContext for all the pages of the application
            RootFrame.DataContext = _mvm;
        }

        // Code to execute when the application is activated (brought to foreground)
        // This code will not execute when the application is first launched
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
            //Create new data object variable
            MainViewModel _mvm = new MainViewModel();
            ObservableCollection<MainModel> items = null;

            //Try to locate previous data in transient state of the application
            if (PhoneApplicationService.Current.State.ContainsKey(STATE_KEY))
            {
                //If found, initialize the data variable and remove in from application's state
                items = PhoneApplicationService.Current.State[STATE_KEY] as ObservableCollection<MainModel>;
                PhoneApplicationService.Current.State.Remove(STATE_KEY);
            }

            //If found set it as a DataContext for all the pages of the application
            //An application is not guaranteed to be activated after it has been tombstoned, 
            //thus if not found create new data object
            if (null != items)                
                _mvm.Items = items;
            else
                _mvm.Items = new ObservableCollection<MainModel>();


            RootFrame.DataContext = _mvm;
        }

        // Code to execute when the application is deactivated (sent to background)
        // This code will not execute when the application is closing
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
            //Add current data object to Application state
            PhoneApplicationService.Current.State.Add(STATE_KEY, RootFrame.DataContext as ObservableCollection<MainModel>);
   
        }

        // Code to execute when the application is closing (eg, user hit Back)
        // This code will not execute when the application is deactivated
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
        }

        // Code to execute if a navigation fails
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // A navigation has failed; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        // Code to execute on Unhandled Exceptions
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        #region Phone application initialization

        // Avoid double-initialization
        private bool phoneApplicationInitialized = false;

        // Do not add any additional code to this method
        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            // Create the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            RootFrame = new PhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            // Handle navigation failures
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // Ensure we don't initialize again
            phoneApplicationInitialized = true;
        }

        // Do not add any additional code to this method
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Set the root visual to allow the application to render
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            // Remove this handler since it is no longer needed
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        #endregion
    }
}