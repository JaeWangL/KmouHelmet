namespace KmouHelmet.Backend.Dtos
{
    public class AddDeviceDto
    {
        public string StreamingUrl { get; set; }
    }

    public class GetDeviceDto
    {
        public int Id { get; set; }

        public string StreamingUrl { get; set; }
    }
}
