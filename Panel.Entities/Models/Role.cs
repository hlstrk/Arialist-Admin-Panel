using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Web;

namespace Panel.Entities.Models
{
    [Table("Roles")]
    public partial class Role
    {
        public Role()
        {


        }


        [Key]
        public int RoleReferenceID { get; set; }
        public string RoleName { get; set; }
        public DateTime DateAdded { get; set; }
        public int AddedByID { get; set; }




    }
}
