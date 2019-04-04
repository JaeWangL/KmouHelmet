namespace KmouHelmet.Backend.Dtos
{
    public class AddLocationDto
    {
        public int DeviceId { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }

    public class GetLocationDto
    {
        public int Id { get; set; }

        public int DeviceId { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }

    public class PatchLocationDto
    {
        public double? Latitude { get; set; }

        public double? Longitude { get; set; }
    }
}
