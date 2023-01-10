using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Web;

namespace Panel.Entities.Models
{
    [Table("Ulke")]
    public partial class Ulke
    {



        [Key]
        public int UlkeID { get; set; }


        public string IsoKodu { get; set; }

        public string IsoAdi { get; set; }

        public string UlkeAdi { get; set; }






    }
}
