namespace Entities
{
    public class DomainListResponse
    {
        public string DomainName { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime LastCheckedDate { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
}
