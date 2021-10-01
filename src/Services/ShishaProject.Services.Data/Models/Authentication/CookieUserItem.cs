namespace ShishaProject.Services.Data.Models.Authentication
{
    using System;

    public class CookieUserItem
    {
        public string UserId { get; set; }

        public string EmailAddress { get; set; }

        public string Name { get; set; }

        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
    }
}
