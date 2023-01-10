using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Web;

namespace Panel.Entities.Models
{
    [Table("SupportRecords")]
    public class SupportRecord
    {
        [Key]
        public int SupportID { get; set; }
        public int OwnerID { get; set; }

        public int CategoryID { get; set; }

        public string SupportString { get; set; }

        public string? TicketCode { get; set; }
        public DateTime DateCreated { get; set; }
        public int UploadedDeviceID { get; set; }
    }
}
