using System;
using System.Collections.ObjectModel;
using System.IO;
using Windows.Storage.Pickers;
using Windows.Storage;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using SharpCompress.Archives.Rar;
using SharpCompress.Common;
using Windows.UI.Xaml;

namespace RComic.Model.Comic
{
    class ComicContent
    {
        private double IMAGE_SIZE_RATIO = 0.9;

        private string IMAGE_PATH_PREFIX = ApplicationData.Current.LocalFolder.Path + "/" + "currentImage";
        private string IMAGE_PATH_SUFFIX = ".jpg";
        private int numberOfImages = 0;

        public ObservableCollection<Image> images;
        public ComicPageContainer cpc;
        private double windowWidth;
       
        public ComicContent()
        {
            images = new ObservableCollection<Image>();
            this.cpc = new ComicPageContainer();
            this.windowWidth = ((Frame)Window.Current.Content).ActualWidth;
        }

        public async Task openFileFromDialog()
        {

            var localFolder = ApplicationData.Current.LocalFolder;
            string renamed = "renamed.rar";
            
            var file = await (this.SetupFileOpenPicker()).PickSingleFileAsync();
            if(file != null)
            {
                var openedFile = await file.CopyAsync(localFolder, renamed, NameCollisionOption.ReplaceExisting);
                ReadRarArchive(openedFile.Path);
                this.images = this.cpc.GetOrderedImages();
            }
        }

        /**
         * <summary>
         *  Creates a FileOpenPicker object and then sets it up to allow selection of a .cbr or .rar file.
         * </summary>
         * <returns>FileOpenPicker</returns>
         */
        private FileOpenPicker SetupFileOpenPicker()
        {
            var fp = new FileOpenPicker();
            fp.ViewMode = PickerViewMode.Thumbnail;
            fp.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            fp.FileTypeFilter.Add(".rar");
            fp.FileTypeFilter.Add(".cbr");
            return fp;
        }

        private void ReadRarArchive(string localPath)
        {
            using (RarArchive ra = RarArchive.Open(localPath))
            {
                foreach (RarArchiveEntry entry in ra.Entries)
                {
                    PopulateSelfList(entry);
                }
            }
        }

        private void PopulateSelfList(Entry entry)
        {
            //check the extension of the file:
            string entryName = entry.Key;

            if (entryName.EndsWith(".jpg", StringComparison.Ordinal))
            {
                try
                {                    
                    string currentImagePath = CopyImageToLocalFolder(entry);

                    Image img = new Image();

                    BitmapImage bim = new BitmapImage(new Uri(currentImagePath));
                    img.Width = bim.DecodePixelWidth = (Int32)(this.windowWidth*IMAGE_SIZE_RATIO);
                    img.Source = bim;

                    this.cpc.AddItem(new ComicPage(img, entryName));
                }
                catch
                {
                    Console.WriteLine("Something went wrong!");
                    return;
                }
            }
        }

        /**
         * <summary>
         * Copies image from the Entry object as obtained by SharpCompress's RARArchive.Open() and RarArchive.Entries to an image file
         * in the application's local folder. This is done to allow the bitmap image class to access the image inside the rar directory
         * (could not figure out how to do it any other way :( ).
         * </summary>
         * <param name="entry">Entry: the items within the Compressed File as obtained by SharpCompress's read methods.</param>
         * <returns>String : the path to the image in the local directory</returns>
         */
        private string CopyImageToLocalFolder(Entry entry)
        {
            string currentImagePath = this.GenerateImgPath();
            

            RarArchiveEntry raentry = (RarArchiveEntry)entry;
            using (var raentryStream = raentry.OpenEntryStream())
            {
                using (var imgFileStream = File.Create(currentImagePath, (int)raentryStream.Length))
                {
                    raentryStream.CopyTo(imgFileStream);
                }
            }

            return currentImagePath;
            
        }

        /**
         * <summary>
         *  Creates the string path for the local directory temp file to which the RAR folder contents will be copied to.
         * </summary>
         * 
         * <returns>String: The path to the temp file.</returns>
         */
        private string GenerateImgPath()
        {
            string imageId = "_" + (this.numberOfImages).ToString();
            (this.numberOfImages)++;
            return IMAGE_PATH_PREFIX + imageId + IMAGE_PATH_SUFFIX;
        }


    }
}
