using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Practices.ObjectBuilder2;
using TestWorkNov2016.Infrastructure.Interfaces;
using TestWorkNov2016.Models.OperationResults;

namespace TestWorkNov2016.Infrastructure
{
    public class FileUploader : IFileUploader
    {
        private Lazy<string> _userUploadsPath;
        private readonly IDirectoryInfoProvider _directoryInfoProvider;

        public FileUploader(IDirectoryInfoProvider directoryInfoProvider)
        {
            _directoryInfoProvider = directoryInfoProvider;
        }

        public async Task<MultipleOperationsResult> TryUploadFilesAsync(HttpContent content, HttpContext httpContext)
        {
            try
            {
                var result = await Task.Run(() => UploadFilesAsync(content, httpContext));
                return result;
            }
            catch (IOException exception)
            {
                //log
                return new MultipleOperationsResult(exception);
            }
            catch (InvalidOperationException exception)
            {
                //log
                return new MultipleOperationsResult(exception);
            }
        }

        public async Task<MultipleOperationsResult> UploadFilesAsync(HttpContent content, HttpContext httpContext)
        {
            _userUploadsPath = new Lazy<string>(() => _directoryInfoProvider.GetUserUploadsPath(httpContext));

            var provider = new MultipartMemoryStreamProvider();

            await content.ReadAsMultipartAsync(provider);

            var uploadOperationResult = new MultipleOperationsResult();

            provider.Contents.ForEach(async fileHttpContent =>
            {
                var res = await TryProcesFileAsync(fileHttpContent);
                uploadOperationResult.AddFileResult(res);
            });

            return uploadOperationResult;
        }

        private async Task<OperationResultBase> TryProcesFileAsync(HttpContent content)
        {
            try
            {
                var path = await ProcesFileAsync(content);
                return new FileUploadResult(path);
            }
            catch (IOException exception)
            {
                //log
                return new FileUploadResult(exception);
            }
            catch (InvalidOperationException exception)
            {
                //log
                return new FileUploadResult(exception);
            }
        }

        private async Task<string> ProcesFileAsync(HttpContent content)
        {
            var destPath = GetFileDetinationPath(content);
            var fileContent = await content.ReadAsByteArrayAsync();

            using (var fs = new FileStream(destPath, FileMode.Create))
            {
                fs.Write(fileContent, 0, fileContent.Length);
            }

            return destPath;
        }

        private string GetFileDetinationPath(HttpContent content)
        {
            var filename = content.Headers.ContentDisposition.FileName;
            filename = filename.Trim('\"');
            filename = filename.Substring(filename.LastIndexOf('\\') + 1);
            var destPath = string.Format("{0}{1}-{2}", _userUploadsPath.Value, Guid.NewGuid(), filename);

            return destPath;
        }
    }
}