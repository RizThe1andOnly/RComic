using System;
using Windows.UI.Xaml.Controls;
using System.Text.RegularExpressions;

namespace RComic.Model.Comic
{
    class ComicPage : IComparable
    {
        private const string PAGE_NUMBER_SECTION = @"-[0-9]{3}\.";
        private const string PAGE_NUMBER_RGX = @"[0-9]{3}";
        private const int PGNUM_SUBSTRING_START_INDEX = 1; // 1 so that the hyphen that is included above is discounted in the result
        

        public Image JPGImage { get; }
        private string imageName;
        private int imageIndex { get; }

        public ComicPage(Image source, string name)
        {
            this.JPGImage = source;
            this.imageName = name;
            this.imageIndex = this.GenerateIndex();
        }


        /**
         * <summary>
         *  <para>
         *      Parses the entry name of the entry and obtains the page number. The entry
         *      name contains a page number at the end. Will use regex to extract the page
         *      number and then convert it to an integer.
         *  </para>
         *  Note: Any entry without a page number will be assigned 999 as a value.
         * </summary>
         * <returns>int : The index corresponding to the page number.</returns>
         */
        private int GenerateIndex()
        {
            Regex rgx = new Regex(PAGE_NUMBER_SECTION);
            Match mtc = rgx.Match(this.imageName);

            Regex rgx_lv2 = new Regex(PAGE_NUMBER_RGX);
            Match mtc_lv2 = rgx_lv2.Match(mtc.Value);

            string indexString = "999";
            if (mtc_lv2.Success)
            {
                indexString = (mtc_lv2.Value).Substring(PGNUM_SUBSTRING_START_INDEX);
            }

            return int.Parse(indexString);
        }




        //                              -------------------Object Method Implementations---------------------

        /**
         * Used for sorting and etc. with lists.
         */
        int IComparable.CompareTo(object obj)
        {
            if((obj == null) || (obj.GetType() != typeof(ComicPage)))
            {
                throw new ArgumentException();
            }

            ComicPage cp = (ComicPage)obj;

            if (this.imageIndex == cp.imageIndex) return 0;

            var toBeReturned = (this.imageIndex > cp.imageIndex) ? 1 : -1;

            return toBeReturned;
        }
    }
}
