using SharpCompress.Archives.Rar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace RComic.FileManageTools
{
    /**
     * <summary>
     * Read through the various comic files and obtain the xml files.
     * </summary>
     */
    class ComicMetadataLoader:ResourceLoader
    {

        // class variables (internal)
        private List<StorageFile> xmlList;
        private IReadOnlyList<StorageFile> cbrFileList;

        // class variables (external)
        public List<StorageFile> ComicXMLFileList => this.cloneFileList();

        public ComicMetadataLoader(StorageFolder sf) : base(sf) 
        {
            this.xmlList = new List<StorageFile>();
            StartDecompression();
        }

        /**
         * <summary>
         * Generate list of cbr files from the temporary directory for the app. Will also
         * call other methods in this class to set up the xml file list.
         * </summary>
         * 
         * Calls ObtainCompressedContents() to get the xml file out of the cbr files.
         */
        public override async void StartDecompression()
        {
            this.cbrFileList = await (this.tempDir).GetFilesAsync();
            ObtainCompressedContents();
        }

        /**
         * <summary>
         * Iterate through each cbr file in the storage file list
         * in cbrFileList and extract the xml file using other methods
         * in the class.
         * </summary>
         * 
         * This method will go through the cbrFileList property. For each
         * item the ".cbr" file will be renamed to ".rar" and fed into
         * IterateThroughEntries() to finally extract the xml file.
         */
        public override async void ObtainCompressedContents()
        {
            string rename = "tempCbrToRar.rar";
            foreach (var localSf in this.cbrFileList)
            {
                var localSf_renamed = await localSf.CopyAsync(this.tempDir, rename, NameCollisionOption.ReplaceExisting);
                using (RarArchive ra = RarArchive.Open(this.tempDir.Path + "/" + rename))
                {
                    IterateThroughEntries(ra);
                }
            }
        }

        public override string GenerateTempFilePath()
        {
            throw new NotImplementedException();
        }

        /**
         * <summary>
         * Extract the xml file from the rar (cbr) archive.
         * </summary>
         * 
         * Called from ObtainCompressedContents to go through the rar archive and take out the xml file.
         */
        public override void IterateThroughEntries(RarArchive ra)
        {
            foreach (var rae in ra.Entries)
            {
                if (rae.Key.EndsWith(".xml"))
                {
                    /**
                     * @TODO : Call constructor of ComicMetadata with the ra entry and the path to the corresponding ".cbr" file
                     * @TODO : Do the prep work in the ComicMetada file
                     */
                }
            }
        }


        // extra method for getters and or setters:
        private List<StorageFile> cloneFileList()
        {
            List<StorageFile> clist = new List<StorageFile>();
            foreach (StorageFile sf in this.ComicXMLFileList) 
            {
                clist.Add(sf);
            }
            return clist;
        }
    }
}
