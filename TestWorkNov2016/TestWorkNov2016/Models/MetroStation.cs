namespace TestWorkNov2016.Models
{
    public class MetroStation
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string ZoneId { get; set; }

        public string StopUrl { get; set; }

        public string LocationType { get; set; }

        public string ParentStation { get; set; }
    }
}