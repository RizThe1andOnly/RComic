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
     * Get the comics from user's downloads (or other) directory and put into the app's local directory.
     * This class will also make available the temp folder to the class that uses this class.
     * </summary>
     * 
     * Copy the storage files in the downloads (or other) folder into the application's local folder to have
     * access to them. This class will also construct a new temp folder in the local folder to hold everything
     * for this application in one place.
     * 
     */
    class ResourceGetter
    {

        private const string TEMP_FOLDER_NAME = "tempFolder";

        
        // ## instance variables ##
        List<StorageFile> comicFiles;
        private StorageFolder tempFolder;

        // ### exposed properties: ###
        public StorageFolder TempFolder { get { return tempFolder; } }
        
        
        // ## Constructors ##
        public ResourceGetter() { }
        public ResourceGetter(List<StorageFile> comicFilesInput)
        {
            this.comicFiles = comicFilesInput;
        }


        // ## Class Tasks (Instance Methods) ##

        /**
         * <summary>
         * Creates the new temporary folder in the local app directory and places all of the
         * input files into that folder.
         * </summary>
         * 
         * Utilizes the CreateTempFolder() method in this class to actually create the new folder.
         */
        public async void PutSFIntoLocalDirectory()
        {
            //create the temp folder:
            CreateTempFolder();

            //put(copy) the comics into the temp folder:
            foreach(var comicFile in this.comicFiles) 
            {
                try
                {
                    await comicFile.CopyAsync(this.tempFolder);
                }
                catch (UnauthorizedAccessException)
                {
                    throw;
                }
                catch (System.Exception) //this is exception thrown for name collision
                {
                    continue;
                }
            }
        }

        /**
         * <summary>Creates a new temporary folder in the local cache folder; will delete any previous version of the temp folder.</summary>
         * 
         * Uses the TempFolderExists() method to check if a temp folder is already in the local cache directory and delete if it is.
         */
        private async void CreateTempFolder()
        {
            //get the local folder in which the temporary folder will be created:
            StorageFolder localCacheFolder = ApplicationData.Current.LocalCacheFolder;

            //check if tempFolder already exists and if yes then delete:
            TempFolderExists(localCacheFolder);

            //create the new folder:
            this.tempFolder = await localCacheFolder.CreateFolderAsync(TEMP_FOLDER_NAME);
           
        }

        /**
         * <summary>
         * Goes through the local cache folder to check if a temp folder already exists there; it will have the same name
         * as the constant. If the folder exists then it is deleted.
         * </summary>
         */
        private async void TempFolderExists(StorageFolder sf)
        {
            var sfContents = await sf.GetFoldersAsync();
            StorageFolder toBeDeleted = null;
            foreach(var subfolder in sfContents)
            {
                if ((subfolder.Name).Equals(TEMP_FOLDER_NAME)) toBeDeleted = subfolder;
            }

            if(toBeDeleted != null)
            {
                await toBeDeleted.DeleteAsync();
            }
        }

    }
}
