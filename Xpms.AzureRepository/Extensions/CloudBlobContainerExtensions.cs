using System.IO;
using System.Text;
using J.F.Libs.Extensions;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;

namespace Xpms.AzureRepository.Extensions
{
    public static class CloudBlobContainerExtensions
    {
        public static CloudBlockBlob Upload(this CloudBlockBlob blob, string text
            , AccessCondition accessCondition = null, BlobRequestOptions blobRequestOptions = null, OperationContext operationContext = null)
        {
            var bytes = Encoding.UTF8.GetBytes(text);
            using (var ms = new MemoryStream(bytes))
            {
                blob.UploadFromStream(ms, accessCondition, blobRequestOptions, operationContext);
            }
            return blob;
        }

        public static CloudBlockBlob Upload(this CloudBlockBlob blob, Stream stream
            , AccessCondition accessCondition = null, BlobRequestOptions blobRequestOptions = null, OperationContext operationContext = null)
        {
            blob.UploadFromStream(stream, accessCondition, blobRequestOptions, operationContext);
            return blob;
        }

        public static string GetBlobText(this CloudBlockBlob blob, AccessCondition accessCondition = null, BlobRequestOptions blobRequestOptions = null, OperationContext operationContext = null)
        {
            return blob.GetBlobBinary(accessCondition, blobRequestOptions, operationContext).ToUtf8String();
        }

        public static byte[] GetBlobBinary(this CloudBlockBlob blob, AccessCondition accessCondition = null, BlobRequestOptions blobRequestOptions = null, OperationContext operationContext = null)
        {
            byte[] bytes;

            blob.FetchAttributes();
            var blobSize = blob.Properties.Length;
            using (var memoryStream = new MemoryStream((int)blobSize))
            {
                blob.DownloadToStream(memoryStream, accessCondition, blobRequestOptions, operationContext);
                bytes = memoryStream.ToArray();
            }

            return bytes;
        }
    }
}
