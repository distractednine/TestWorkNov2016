using System.Collections.Generic;
using TestWorkNov2016.Models;

namespace TestWorkNov2016.Storage
{
    public interface IMetroStationStorage
    {
        ICollection<MetroStation> Get();

        int AddRange(IEnumerable<MetroStation> newStations);
    }
}