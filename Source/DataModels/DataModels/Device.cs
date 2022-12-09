namespace DataModels
{
    public class Device
    {
        public int Id { get; set; }

        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;

        public Asset? Asset { get; set; }
    }
}
