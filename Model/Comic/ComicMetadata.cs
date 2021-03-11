using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using System.Xml;

namespace RComic.Model.Comic
{
    /**
     * <summary>
     * Object model to hold metadata for a comicbook. This method will also provide static method(s) to
     * obtain the metadata.
     * </summary>
     */
    class ComicMetadata
    {
        // ## static constants ##
        // ### For the purposes of reading the xml file ###
        private static string CBR_XML_FIELD_NAME = "Series";
        private static string CBR_XML_FIELD_CHAPTER = "Number";
        private static string CBR_XML_FIELD_SUMMARY = "Summary";
        private static string CBR_XML_FIELD_PUBLISHER = "Publisher";
        private static string CBR_XML_FIELD_PAGENUM = "PageCount";

        // ## Object properties ##
        public string title { get; set; }
        public string summary { get; set; }

        public ComicMetadata() { }

        public ComicMetadata(string title, string summary)
        {
            this.title = title;
            this.summary = summary;
        }

        /**
         * <param name="cbrFile">.cbr obtained using Windows.Storage API's</param>
         * <summary>Creates and returns a ComicMetadata object. This is a static method.</summary>
         * <returns>ComicMetadata object</returns>
         * 
         * This is an interface method that utilizes other private methods in this class to construct the
         * new ComicMetadata object. Primarily it references the GenerateComicMetadata() method.
         */
        public static ComicMetadata GetComicMetadata(StorageFile cbrFile)
        {
            return GenerateComicMetadata(cbrFile.Path);
        }


        // # Section to obtain Comic metadata from cbr xml file #

        /**
         * 
         */
        private static ComicMetadata GenerateComicMetadata(string filepath)
        {
            return null;
        }

        
    }
}
