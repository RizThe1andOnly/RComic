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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace RComic.CustomControls
{
    public sealed partial class ComicTab : UserControl
    {
        public ComicTab(string comicPath)
        {
            this.InitializeComponent();
            string cTitle = GetComicTitle(comicPath);
        }

        public String ComicTitle
        {
            get { return (String)GetValue(ComicTitleProperty); }
            set { SetValue(ComicTitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ComicTitle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ComicTitleProperty =
            DependencyProperty.Register("ComicTitle", typeof(String), typeof(ComicTab), new PropertyMetadata(0));


        /**
         * <param name="path">Path to comic (obtained from storage folder details).</param>
         * <summary>
         *  Extract the comic title from the comic path.
         * </summary>
         * <returns>Title of comic.</returns>
         */
        private string GetComicTitle(string path)
        {
            //regex op to extrac the title
            string regexToken = @"";
            return "";
        }
    }
}
