using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using System.Xml;
using SharpCompress.Archives.Rar;
using SharpCompress.Common;
using System.IO;

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
        private string title;
        private string summary;
        private RarArchive cbrFile;
        private StorageFolder tempDir;

        // ## Constructor(s) ##
        public ComicMetadata() { }

        public ComicMetadata(string title, string summary)
        {
            this.title = title;
            this.summary = summary;
        }

        /**
         * <param name="xml_rarEntry">
         * RAR entry of the xml file; Will be processed in this class.
         * </param>
         * 
         * <param name="comicFile">
         * RAR compressed comic file. This class will contain the object to be passed on to other classes.
         * </param>
         * 
         * <summary>
         * Creates a ComicMetadata object using xml rar entry and comicFile rar archive. This class has the 
         * methods to process these parameters into the necessary data.
         * </summary>
         */
        public ComicMetadata(RarArchiveEntry xml_rarEntry, RarArchive comicFile, StorageFolder tempDir)
        {
            this.cbrFile = comicFile;
            this.tempDir = tempDir;
            // @TODO : Call the class logic methods to extract metadata.
        }

        
        // ## Class Logic Methods ##

        /**
         * <param name="xml_rarEntry">
         * Rar entry of the xml file obtained from the calling class. The parameter of the same
         * name will be passed from the constructor.
         * </param>
         * 
         * <summary>
         * Generate the xml file and then extract the desired properties from it. The desired
         * properties are the ones corresponding to the constants defined in this class.
         * </summary>
         */
        private async void GenerateObjectProperties(RarArchiveEntry xml_rarEntry)
        {
            // create the xml file from rar entry
            string pathToXmlFile = this.CopyRaEntryToXMLFile(xml_rarEntry);

            // extract data from the xml file:

        }
        
        /**
         * <param name="xml_rarEntry">
         * Rar entry holding the xml file to be processed. The parameter of the same name will be passed in from
         * GenerateObjectProperties().
         * </param>
         * 
         * <summary>
         * Use the Windows file api and Sharpcompress provided functionalities to copy the contents of the 
         * rar entry into a temporary xml file to be extracted.
         * </summary>
         * 
         * <returns>String path to temporary xml file</returns>
         */
        private string CopyRaEntryToXMLFile(RarArchiveEntry xml_rarEntry)
        {
            string pathToTempFile = this.tempDir.Path + "/tempXml.xml";
            using (var rarStream = xml_rarEntry.OpenEntryStream())
            {
                using (var xmlStream = File.Create(pathToTempFile, (int)rarStream.Length))
                {
                    rarStream.CopyTo(xmlStream);
                }
            }
            return pathToTempFile;
        }
    }
}
