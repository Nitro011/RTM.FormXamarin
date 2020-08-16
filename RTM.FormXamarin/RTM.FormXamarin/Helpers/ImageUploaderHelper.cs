using PCLStorage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RTM.FormXamarin.Helpers
{
    public static class ImageUploaderHelper
    {
        public static async Task<String> SaveImage(this string imageName, IFolder root, byte[] data)
        {
            IFolder folder = root ?? FileSystem.Current.LocalStorage;
            IFile file = await folder.CreateFileAsync(imageName, CreationCollisionOption.GenerateUniqueName);
            using (Stream stream = await file.OpenAsync(PCLStorage.FileAccess.ReadAndWrite))
            {
                stream.Write(data, 0, data.Length);
            }
            return file.Name;
        }
    }
}
