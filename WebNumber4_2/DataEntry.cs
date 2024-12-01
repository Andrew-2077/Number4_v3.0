namespace WebNumber4_2
{
    public class DataEntry
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
