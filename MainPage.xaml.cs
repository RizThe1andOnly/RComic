using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.Activation;
using Windows.UI.ViewManagement;

using RComic.Model.Comic;
using RComic.Model.DirectoryContent;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RComic
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ListView lv;

        private double SCREEN_SIZE_RATIO = 0.8;
        private double LISTVIEW_SIZE_RATIO = 1;
        private double LISTVIEW_PADDING = 0.04;

        public MainPage()
        {
            this.MaximizeScreen();
            this.InitializeComponent();
            this.lv = (ListView) FindName("mainListContainer");
            this.Loaded += MainPage_Loaded;
        }

        
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.lv.Width = this.GenerateListViewWidth();
            this.lv.Padding = new Thickness(this.GenerateListPadding(), 0, this.GenerateListPadding(), 0);
        }

        private async void testButtonPress(object sender, RoutedEventArgs e)
        {
            ComicContent rl = new ComicContent();
            await rl.openFileFromDialog();
            (this.lv).ItemsSource = rl.images;
            
        }

        private void MaximizeScreen()
        {
            // set the application to open in maximized mode:
            var view = ApplicationView.GetForCurrentView();
            Size resizeSize = new Size(this.GenerateAppWidth(), this.GenerateAppHeight());
            if (!view.IsFullScreenMode)
            {
                if (view.TryResizeView(resizeSize))//view.TryEnterFullScreenMode()
                {
                    ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.Maximized;
                }
            }
        }


        private double GenerateListPadding()
        {
            return LISTVIEW_PADDING * GetWindowWidth();
        }

        private double GenerateListViewWidth()
        {
            return LISTVIEW_SIZE_RATIO * GetWindowWidth();
        }

        private double GenerateAppHeight()
        {
            return SCREEN_SIZE_RATIO * GetWindowHeight();
        }

        private double GenerateAppWidth()
        {
            return SCREEN_SIZE_RATIO * GetWindowWidth();
        }

        private double GetWindowWidth()
        {
            var windowWidth = ((Frame)Window.Current.Content).ActualWidth;

            return windowWidth;
        }

        private double GetWindowHeight()
        {
            var windowHeight = ((Frame)Window.Current.Content).ActualHeight;
            return windowHeight;
        }
    }
}
