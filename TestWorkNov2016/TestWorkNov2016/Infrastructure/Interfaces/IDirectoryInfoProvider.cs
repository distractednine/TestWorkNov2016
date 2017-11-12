using System.Web;

namespace TestWorkNov2016.Infrastructure.Interfaces
{
    public interface IDirectoryInfoProvider
    {
        string GetUserUploadsPath(HttpContext httpContext);

        string GetTestSeedDataFileName();
    }
}