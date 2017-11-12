using System;
using System.Collections.Generic;
using System.Linq;
using TestWorkNov2016.Models;

namespace TestWorkNov2016.Storage
{
    public class MetroStationStorage : IMetroStationStorage
    {
        private static readonly List<MetroStation> Stations = new List<MetroStation>();
        private static Lazy<List<MetroStation>> StationsLazy => new Lazy<List<MetroStation>>(()=> Stations);

        public ICollection<MetroStation> Get()
        {
            return StationsLazy.Value;
        }

        public int AddRange(IEnumerable<MetroStation> newStations)
        {
            var ids = StationsLazy.Value.Select(x => x.Id);
            var stationsToAdd = newStations.Where(x => ids.All(y => y != x.Id)).ToList();
            StationsLazy.Value.AddRange(stationsToAdd);

            return stationsToAdd.Count;
        }
    }
}