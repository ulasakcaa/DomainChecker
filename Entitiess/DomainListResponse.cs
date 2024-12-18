namespace Entitiess
{
    public class DomainListResponse
    {
        public int Id { get; set; }
        public string DomainName { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime LastCheckedDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public bool? IsFavorite { get; set; }
    }
}
