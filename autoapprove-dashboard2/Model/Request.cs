namespace autoapprove_dashboard2.Models
{
    public class Request
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;  
        public string Status { get; set; } = string.Empty;  
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;  
        public bool IsChecked { get; set; } 

    }
}
