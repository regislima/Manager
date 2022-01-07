using System;
using System.Text.Json.Serialization;
using Manager.Domain.Entities;

namespace Manager.Services.DTO
{
    public class UserDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        
        [JsonIgnore]
        public string Password { get; set; }
        public Role Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}