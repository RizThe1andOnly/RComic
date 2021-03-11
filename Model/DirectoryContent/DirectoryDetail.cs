using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace RComic.Model.DirectoryContent
{
    class DirectoryDetail
    {
        public const string DOWNLOAD_DIRECTORY = "Downloads";
        //public static string DOWNLOAD_DIRECTORY_NAME = DOWNLOAD_DIRECTORY;
        private string DOWNLOAD_DIRECTORY_PATH = UserDataPaths.GetDefault().Downloads;
        private string CBR_EXTENSION = ".cbr";
        private string CBZ_EXTENSION = ".cbz";

        public static List<String> GetDirectoryNames()
        {
            List<string> toBeReturned = new List<string>();

            //append the static directory names to this list:
            toBeReturned.Add(DOWNLOAD_DIRECTORY);

            return toBeReturned;
        }


        // non-static components:

        public DirectoryDetail()
        {
            
        }


        /**
         * <summary>
         *  Create a list of storage files that will be used to create TitleTabs for the comics in a given
         *  folder.
         * </summary>
         * 
         * <param name="directoryChoice">Picked from the constants provided by this class.</param>
         * 
         * <returns>List of storage files</returns>
         */
        public async Task<List<StorageFile>> GetDirectoryContents(string directoryChoice)
        {
            string usingDirectoryPath = "";
            switch (directoryChoice)
            {
                case DOWNLOAD_DIRECTORY: usingDirectoryPath = DOWNLOAD_DIRECTORY_PATH; break;
                default: usingDirectoryPath = DOWNLOAD_DIRECTORY_PATH; break;
            }

            var directoryStorageFolder = await StorageFolder.GetFolderFromPathAsync(usingDirectoryPath);

            var allDirectoryFiles = (await directoryStorageFolder.GetFilesAsync()).ToList();
            List<StorageFile> toBeReturned = new List<StorageFile>();
            
            //get rid of all non cbr files:
            foreach (var file in allDirectoryFiles) { if (file.Name.EndsWith(CBR_EXTENSION)) toBeReturned.Add(file); }

            return toBeReturned;
        }

    }
}
