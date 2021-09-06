using System;
using System.IO;
using System.Threading.Tasks;
using Firebase.Storage;

namespace WiringDiagramConstructionMachine.Helper
{
    public class FirebaseStorageHelper
    {
        public string likeStorage = "wdcm-db.appspot.com";
        public async Task<string> UploadFile(Stream fileStream, string fileName)
        {
            FirebaseStorage firebaseStorage = new FirebaseStorage(likeStorage);
            var imageUrl = await firebaseStorage
                .Child(fileName)
                .PutAsync(fileStream);
            return imageUrl;
        }
        public async Task<string> GetFile(string fileName)
        {
            FirebaseStorage firebaseStorage = new FirebaseStorage(likeStorage);
            return await firebaseStorage
                .Child(fileName)
                .GetDownloadUrlAsync();
        }

        public async Task DeleteFile(string fileName)
        {
            FirebaseStorage firebaseStorage = new FirebaseStorage(likeStorage);
            await firebaseStorage.Child(fileName).DeleteAsync();
        }
    }
}
