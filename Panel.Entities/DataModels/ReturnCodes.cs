using Panel.API;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Web;

namespace Panel.Entities.Models
{
    public class ReturnCodes
    {
        public partial class SigninSuccessModel
        {



            public int Code { get; set; }

            public string? token { get; set; }

            public string? status { get; set; }

            public UserLoggedIn? SignedUser { get; set; }



        }



        public partial class SigninFailModel
        {


            public int Code { get; set; }

            public string? status { get; set; }



        }


        public partial class RegisterSuccessModel
        {


            public int Code { get; set; }

            public string? token { get; set; }

            public string? status { get; set; }



        }


        public partial class RegisterFailModel
        {


            public int Code { get; set; }

            public string? status { get; set; }

        }


        public partial class GenericSuccessModel
        {


            public int Code { get; set; }

            public string? status { get; set; }

        }


        public partial class GenericFailModel
        {


            public int Code { get; set; }

            public string? status { get; set; }

        }



        public partial class GenericValueSuccessModel
        {


            public int Code { get; set; }

            public string? status { get; set; }

            public string? Value { get; set; }

        }






    }
}
