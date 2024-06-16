namespace WebApp.DTOs.Countries
{
    public class CountryDto
    {
        public string Name { get; set; }
        public int Population { get; set; }
        public DateTime Data { get; set; } = DateTime.UtcNow;
    }
}
