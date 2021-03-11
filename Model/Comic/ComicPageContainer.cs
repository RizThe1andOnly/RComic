using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using System.Collections.ObjectModel;

namespace RComic.Model.Comic
{
    class ComicPageContainer
    {
        private List<ComicPage> pages;

        public ComicPageContainer()
        {
            this.pages = new List<ComicPage>();
        }

        /**
         * <summary>
         *  Adds ComicPage item to the internal list within the ComicPageContainer object.
         * </summary>
         */
        public void AddItem(object comicPage)
        {
            if ((comicPage == null) || (typeof(ComicPage) != comicPage.GetType()))
            {
                throw new ArgumentException();
            }

            this.pages.Add((ComicPage)comicPage);
        }

        
        /**
         * <summary>
         *  Sorts the internal ComicPage list based on page number and then creates an ObservableCollection using the ordered images.
         * </summary>
         * <returns>ObservableCollection : collection which has the images sorted in order of page number</returns>
         */
        public ObservableCollection<Image> GetOrderedImages()
        {
            var toBeReturned = new ObservableCollection<Image>();
            (this.pages).Sort();
            foreach(ComicPage cp in this.pages)
            {
                toBeReturned.Add(cp.JPGImage);
            }
            return toBeReturned;
        }
    }
}
