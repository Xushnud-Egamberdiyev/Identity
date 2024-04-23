using Microsoft.AspNetCore.Identity;

namespace Identity.Models
{
    public class ApUser : IdentityUser, IAuditable
    {
        public string FullName { get; set; }
        public string Status {  get; set; }
        public int Age { get; set; }
        public DateTimeOffset CreateData { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset ModifiedData { get; set; }
        public DateTimeOffset DeleteDate { get; set; }
        public bool IsDeleted { get; set; } = false; 
    }
}
