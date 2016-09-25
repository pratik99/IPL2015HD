using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using IPL_2015_HD.Resources;
using Microsoft.Phone.Net.NetworkInformation;
using Microsoft.Phone.Tasks;

namespace IPL_2015_HD
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            web.Navigated += new EventHandler<System.Windows.Navigation.NavigationEventArgs>(web_Navigated);
            web.Navigating += new EventHandler<NavigatingEventArgs>(web_Navigating);
            web.ScriptNotify += new EventHandler<NotifyEventArgs>(web_ScriptNotify);

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }
        private void web_Loaded(object sender, RoutedEventArgs e)
        {
            web.Navigate(new Uri("http://www.iplt20.com/", UriKind.Absolute));
        }
        void web_ScriptNotify(object sender, NotifyEventArgs e)
        {
            web.Navigate(new Uri(e.Value, UriKind.Absolute));
        }

        void web_Navigating(object sender, NavigatingEventArgs e)
        {
            ProgBar.Visibility = Visibility.Visible;
            load.Visibility = Visibility.Visible;
        }

        void web_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            ProgBar.Visibility = Visibility.Collapsed;
            load.Visibility = Visibility.Collapsed;
        }
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            base.OnBackKeyPress(e);

            if (web.CanGoBack)
            {
                e.Cancel = true;
                web.GoBack();


            }
            else
            {
                e.Cancel = false;
            }
        }
        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            var currentSource = web.Source;
            web.Source = null;
            web.Source = currentSource;
        }

        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            web.Navigate(new Uri("http://www.iplt20.com/", UriKind.Absolute));
        }

        private void ApplicationBarIconButton_Click_2(object sender, EventArgs e)
        {
            MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();

            marketplaceReviewTask.Show();
        }

        private void ApplicationBarIconButton_Click_3(object sender, EventArgs e)
        {
            web.Navigate(new Uri("http://www.iplt20.com/schedule", UriKind.Absolute));
        }

        private void PhoneApplicationPage_Loaded_1(object sender, RoutedEventArgs e)
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                //Correct
            }
            else
            {
                MessageBoxResult ma = MessageBox.Show("Please enable internet and restart to use this application by going to settings - WiFi - or mobile network", "IPL 2015 HD", MessageBoxButton.OKCancel);
                if (ma == MessageBoxResult.OK)
                {
                    ConnectionSettingsTask connectionSettingsTask = new ConnectionSettingsTask();
                    connectionSettingsTask.ConnectionSettingsType = ConnectionSettingsType.WiFi;
                    connectionSettingsTask.Show();
                }
                if (ma == MessageBoxResult.Cancel)
                {
                    Application.Current.Terminate();
                }
            }
            ShellTile PinnedTile = ShellTile.ActiveTiles.First();
            FlipTileData UpdatedTileData = new FlipTileData
            {
                Title = "IPL 2015",


                BackTitle = "IPL",
                BackContent = "Check scores of your favourite team!",
                WideBackContent = "Use IPL 2015 to get the latest news!"
            };
            PinnedTile.Update(UpdatedTileData);
        }
        
            

            // Sample code for building a localized ApplicationBar
            //private void BuildLocalizedApplicationBar()
            //{
            //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
            //    ApplicationBar = new ApplicationBar();

            //    // Create a new button and set the text value to the localized string from AppResources.
            //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
            //    appBarButton.Text = AppResources.AppBarButtonText;
            //    ApplicationBar.Buttons.Add(appBarButton);

            //    // Create a new menu item with the localized string from AppResources.
            //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
            //    ApplicationBar.MenuItems.Add(appBarMenuItem);
            //}
        }
    }
