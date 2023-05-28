using System;
using System.Collections.Generic;

namespace art_gallery_api.Models
{
    public class User
    {
        public User (){}
        public int UserId { get; set; }
        public string Email { get; set; } = null!;
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public bool HaveMembership { get; set; }
        public string? Role { get; set; }
        public DateTime Createddate { get; set; }
        public DateTime Modifieddate { get; set; }
    }
}
