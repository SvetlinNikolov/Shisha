﻿namespace ShishaProject.Web.ViewModels.User
{
    using System;

    using Newtonsoft.Json;

    public class RegistrationInputModel
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("salt")]
        public string Salt { get; set; }

        [JsonProperty("repeat_password")]
        public string RepeatPassword { get; set; }

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
        public DateTime CreatedAt => DateTime.Now;

        [JsonProperty("password_reset_token")]
        public string PasswordResetToken => Guid.NewGuid().ToString();

        [JsonProperty("email_token")]
        public string EmailConfirmToken => Guid.NewGuid().ToString();
    }
}
