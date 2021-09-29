namespace ShishaProject.Services.Data.Models.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Newtonsoft.Json;

    public class UserDto
    {
        [Required]
        [JsonProperty("username")]
        public string Username { get; set; }

        [Required]
        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
