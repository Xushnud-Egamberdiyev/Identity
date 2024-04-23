namespace Identity.Models
{
    public interface IAuditable
    {
        public DateTimeOffset CreateData { get; set; }
        public DateTimeOffset ModifiedData { get; set;}
        public DateTimeOffset DeleteDate { get; set;}
        public bool IsDeleted { get; set;}
    }
}
