using System;

namespace TestWorkNov2016.Models.OperationResults
{
    public class StationParsingResult : SingleOperationResult<MetroStation>
    {
        public StationParsingResult(MetroStation station) : base(station)
        {
        }

        public StationParsingResult(Exception error) : base(error)
        {
        }
    }
}