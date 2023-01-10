using Panel.API;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Web;
using static Panel.Entities.Models.ReturnCodes;

namespace Panel.Entities.Models
{
    public class ReturnCodeGenerator
    {


        public SigninSuccessModel SigninSuccess(int Code, string token, UserLoggedIn currentuser)
        {



            var returnwrap = new SigninSuccessModel();
            returnwrap.SignedUser = currentuser;
            returnwrap.Code = Code;
            returnwrap.token = token;
            returnwrap.status = "Başarıyla Giriş Yaptınız.";
            return returnwrap;




        }

        public SigninFailModel SigninFail(int Code, string status)
        {



            var returnwrap = new SigninFailModel();
            returnwrap.Code = Code;

            returnwrap.status = status;
            return returnwrap;




        }


        public RegisterSuccessModel RegisterSuccess(int Code, string token)
        {



            var returnwrap = new RegisterSuccessModel();
            returnwrap.Code = Code;
            returnwrap.status = "Başarıyla Kayıt Oldunuz.";
            returnwrap.token = token;
            return returnwrap;


        }



        public RegisterFailModel RegisterFail(int Code, string status)
        {



            var returnwrap = new RegisterFailModel();
            returnwrap.Code = Code;
            returnwrap.status = status;

            return returnwrap;




        }


        public GenericFailModel GenericFail(int Code, string status)
        {



            var returnwrap = new GenericFailModel();
            returnwrap.Code = Code;
            returnwrap.status = status;

            return returnwrap;




        }


        public GenericSuccessModel GenericSuccess(int Code, string status)
        {



            var returnwrap = new GenericSuccessModel();
            returnwrap.Code = Code;
            returnwrap.status = status;

            return returnwrap;




        }

        public GenericValueSuccessModel GenericValueSuccess(int Code, string status, string value)
        {



            var returnwrap = new GenericValueSuccessModel();
            returnwrap.Code = Code;
            returnwrap.status = status;
            returnwrap.Value = value;

            return returnwrap;




        }








    }
}
