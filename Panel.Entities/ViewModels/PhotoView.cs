using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Web;

namespace Panel.Entities.Models
{
    [Table("PhotosView")]
    public partial class PhotoView
    {
        public PhotoView()
        {


        }


        [Key]
        public int PhotoReferenceID { get; set; }
        public string OwnerID { get; set; }
        public string ServerLocation { get; set; }
        public string TicketCode { get; set; }
        public DateTime DateShared { get; set; }
        public bool IsVisible { get; set; }
        public bool IsDeleted { get; set; }
        public int Size { get; set; }
        public int UploadedDeviceID { get; set; }




    }
}
