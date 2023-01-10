using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Web;

namespace Panel.Entities.Models
{
    public class SignReturnDto
    {




        [Key]
        public int UserReferenceID { get; set; }
        public string? Username { get; set; }
        public string? Fullname { get; set; }
        public string? UserCode { get; set; }
        public DateTime Registrationdate { get; set; }
        public bool IsActive { get; set; }
        public string? WorksOn { get; set; }

        public DateTime LastSignedIn { get; set; }
        public string? MobileNumber { get; set; }
        public string? EMail { get; set; }
        public string? TCNumber { get; set; }
        public int RoleID { get; set; }

        public string? RoleString { get; set; }


        public int? CompanyID { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsVisible { get; set; }

        public bool IsPrivate { get; set; }




    }
}
