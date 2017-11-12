using System.IO;
using System.Web;
using TestWorkNov2016.Infrastructure.Interfaces;

namespace TestWorkNov2016.Infrastructure
{
    public class DirectoryInfoProvider : IDirectoryInfoProvider
    {
        private readonly string _userUploadsPath;
        private readonly string _testFileName;

        public DirectoryInfoProvider(string userUploadsPath, string testFileName)
        {
            _userUploadsPath = userUploadsPath;
            _testFileName = testFileName;
        }

        public string GetUserUploadsPath(HttpContext httpContext)
        {
            var mappedPath = httpContext.Server.MapPath(_userUploadsPath);

            if (!Directory.Exists(mappedPath))
            {
                Directory.CreateDirectory(mappedPath);
            }

            return mappedPath;
        }

        public string GetTestSeedDataFileName()
        {
            return _testFileName;
        }
    }
}