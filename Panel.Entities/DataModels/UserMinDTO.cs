using Panel.Entities.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Panel.Entities.DataModels
{
    [Table("Users")]
    public class UserMinDTO
    {



        public int UserReferenceID { get; set; }
        public string Username { get; set; } = string.Empty;
        public string EMail { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;

        public byte[]? PasswordHash { get; set; }

        public byte[]? PasswordSalt { get; set; }


        public string? ProfilePictureLocation { get; set; }

        public string? RoleString { get; set; }

        public string? Fullname { get; set; }
        public string? UserCode { get; set; }

        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }



        public User User { get; set; }


    }
}