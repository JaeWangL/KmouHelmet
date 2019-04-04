namespace KmouHelmet.Backend.Models
{
    public class LocationModel
    {
        public int Id { get; set; }

        public int DeviceId { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }
    }
}
