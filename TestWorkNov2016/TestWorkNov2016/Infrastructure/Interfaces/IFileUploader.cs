using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using TestWorkNov2016.Models.OperationResults;

namespace TestWorkNov2016.Infrastructure.Interfaces
{
    public interface IFileUploader
    {
        Task<MultipleOperationsResult> TryUploadFilesAsync(HttpContent content, HttpContext httpContext);

        Task<MultipleOperationsResult> UploadFilesAsync(HttpContent content, HttpContext httpContext);
    }
}