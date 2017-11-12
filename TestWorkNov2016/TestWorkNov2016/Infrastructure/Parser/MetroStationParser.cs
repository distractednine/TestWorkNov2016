using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TestWorkNov2016.Infrastructure.Interfaces;
using TestWorkNov2016.Models;
using TestWorkNov2016.Models.OperationResults;

namespace TestWorkNov2016.Infrastructure.Parser
{
    // indices of metro station entity propperties in parsed string
    public class MetroStationStringIndices
    {
        public const int Id = 0;
        public const int Name = 1;
        public const int Descr = 2;
        public const int Lat = 3;
        public const int Lon = 4;
        public const int Zone = 5;
        public const int Url = 6;
        public const int Type = 7;
        public const int Parent = 8;
    }

    public class MetroStationParser : ITextParser<StationParsingResult>
    {
        private const int ExpectedValuesCount = 9;

        public StationParsingResult TryParse(string str)
        {
            try
            {
                return Parse(str);
            }
            catch (CustomTextParsingException exception)
            {
                return new StationParsingResult(exception);
            }
        }

        public StationParsingResult Parse(string str)
        {
            var values = str.Split(',');
            ThrowOnInvalidValuesCount(values, str);

            double lon, lat;

            var lonParsed = double.TryParse(values[MetroStationStringIndices.Lon], 
                NumberStyles.Number, CultureInfo.InvariantCulture, out lon);

            var latParsed = double.TryParse(values[MetroStationStringIndices.Lat],
                NumberStyles.Number, CultureInfo.InvariantCulture, out lat);

            ThrowOnNotParsedCoordinates(lonParsed, latParsed, str);

            var parsed = new MetroStation
            {
                Id = values[MetroStationStringIndices.Id],
                Name = values[MetroStationStringIndices.Name].Trim('\"'),
                Description = values[MetroStationStringIndices.Descr],
                Latitude = lat,
                Longitude = lon,
                ZoneId = values[MetroStationStringIndices.Zone],
                StopUrl = values[MetroStationStringIndices.Url],
                LocationType = values[MetroStationStringIndices.Type],
                ParentStation = values[MetroStationStringIndices.Parent]
            };

            return new StationParsingResult(parsed);
        }

        private void ThrowOnInvalidValuesCount(IEnumerable<string> values, string str)
        {
            if (values.Count() == ExpectedValuesCount)
            {
                return;
            }

            throw new CustomTextParsingException("Unexpected values number", str);
        }

        private void ThrowOnNotParsedCoordinates(bool lonParsed, bool latParsed, string str)
        {
            if (lonParsed && latParsed)
            {
                return;
            }

            throw new CustomTextParsingException("Could not parse coordinates", str);
        }
    }
}