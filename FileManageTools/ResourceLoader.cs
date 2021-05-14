using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Windows.Storage;

//SharpCompress library dependencies:
using SharpCompress.Archives.Rar;
using SharpCompress.Common;

namespace RComic.FileManageTools
{
    abstract class ResourceLoader
    {
        // # Constants #
        public const int FILE_TYPE_RAR = 0;
        public const int FILE_TYPE_ZIP = 1;

        // # Instance Variables #
        public string path;
        public string localFilePath;
        protected StorageFolder tempDir;

        // # Abstract Methods #
        public abstract void StartDecompression();
        public abstract void ObtainCompressedContents();
        public abstract void IterateThroughEntries(RarArchive ra);
        public abstract string GenerateTempFilePath(); //should no longer be necessary

        
        
        // # Instance Methods #

        public ResourceLoader() { }

        public ResourceLoader(string localPath)
        {
            this.path = localPath;
        }

        public ResourceLoader(StorageFolder sf)
        {
            this.tempDir = sf;
        }

        /**
         * <param name="entry"></param>
         * <param name="fileType"></param>
         * <summary></summary>
         * <returns></returns>
         * 
         * Should no longer be necessary if the ResourceGetter functionalities are working as intended.
         */
        private string CopyEntryContentToLocalFile(Entry entry, int fileType = FILE_TYPE_RAR)
        {
            string localFilePath = this.GenerateTempFilePath();

            //pick type of entry and based on type different method (for different type) will be called:
            switch (fileType)
            {
                case FILE_TYPE_RAR: CopyRarEntryContent(entry,localFilePath); break;
                default: CopyRarEntryContent(entry,localFilePath); break;
            }

            return localFilePath;
        }

        /**
         * <param name="entry"></param>
         * <param name="localFilePath"></param>
         * <summary></summary>
         * 
         * Should no longer be necessary if the ResourceGetter functionalities are working as intended.
         */
        private void CopyRarEntryContent(Entry entry, string localFilePath)
        {
            RarArchiveEntry rae = (RarArchiveEntry)entry;
            using(var rarEntryStream = rae.OpenEntryStream())
            {
                using (var targetFileStream = File.Create(localFilePath, (int)rarEntryStream.Length))
                {
                    rarEntryStream.CopyTo(targetFileStream);
                }
            }
        }


        


        
    }
}
