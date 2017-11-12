using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using TestWorkNov2016.Infrastructure.Interfaces;
using TestWorkNov2016.Models;
using TestWorkNov2016.Models.OperationResults;
using TestWorkNov2016.Storage;

namespace TestWorkNov2016.Api
{
    public class StationController : ApiController
    {
        private readonly IDirectoryInfoProvider _directoryInfoProvider;
        private readonly IFileUploader _fileUploader;
        private readonly ITextFileParser _textFileParser;
        private readonly IMetroStationStorage _metroStationStorage;

        public StationController(IDirectoryInfoProvider directoryInfoProvider, IFileUploader fileUploader, 
            ITextFileParser textFileParser, IMetroStationStorage metroStationStorage)
        {
            _directoryInfoProvider = directoryInfoProvider;
            _fileUploader = fileUploader;
            _textFileParser = textFileParser;
            _metroStationStorage = metroStationStorage;
        }

        public IHttpActionResult Get()
        {
            var data = _metroStationStorage.Get();

            var wasInitialDataSet = SetInitialTempData(data);
            if (wasInitialDataSet)
            {
                data = _metroStationStorage.Get();
            }

            var response = new
            {
                data = data
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Upload()
        {
            var uploadResult = await _fileUploader.TryUploadFilesAsync(Request.Content, HttpContext.Current);

            if (!uploadResult.IsSuccessful)
            {
                return BadRequest("File uploading error");
            }

            var parseResults = await _textFileParser.ProcessUploadedFilesAsync(uploadResult);

            var successfulOperations = parseResults.SelectMany(x => x.SuccessfulOperations).ToList();
            var unsuccessfulOperations = parseResults.SelectMany(x => x.UnsuccessfulOperations);
            var stations = successfulOperations.Select(x => ((StationParsingResult)x).Entity);
            // ONLY STATIONS WITH UNIQUE NAMES ARE ADDED!
            var addedCount = _metroStationStorage.AddRange(stations);

            var response = new
            {
                successfulOperationsCount = successfulOperations.Count,
                unsuccessfulOperationsCount = unsuccessfulOperations.Count(),
                addedCount = addedCount
            };

            return Ok(response);
        }

        private bool SetInitialTempData(ICollection<MetroStation> data)
        {
            if (data.Count != 0)
            {
                return false;
            }

            var directory = _directoryInfoProvider.GetUserUploadsPath(HttpContext.Current);
            var fileName = _directoryInfoProvider.GetTestSeedDataFileName();
            var path = string.Format("{0}{1}", directory, fileName);

            var parseResults = _textFileParser.TryParseFile(path);
            var successfulOperations = parseResults.SuccessfulOperations;
            var stations = successfulOperations.Select(x => ((StationParsingResult)x).Entity);
            _metroStationStorage.AddRange(stations);

            return true;
        }
    }
}