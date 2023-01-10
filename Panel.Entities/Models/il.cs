using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Web;

namespace Panel.Entities.Models
{
    [Table("Il")]
    public partial class il
    {



        [Key]
        public int IlID { get; set; }


        public int IlKodu { get; set; }


        public string IlAdi { get; set; }






    }
}
