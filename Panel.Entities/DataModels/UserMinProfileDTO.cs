using Panel.Entities.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Panel.Entities.DataModels
{
    public partial class UserMinProfileDTO
    {
        [Key]
        public int UserReferenceID { get; set; }
        public string Username { get; set; } = string.Empty;
        public string EMail { get; set; } = string.Empty;


        public string? ProfilePictureLocation { get; set; }

        public string? RoleString { get; set; }

        public string? Fullname { get; set; }
        public string? UserCode { get; set; }
        public DateTime TokenExpires { get; set; }




    }
}