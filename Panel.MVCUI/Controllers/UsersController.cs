using Panel.Bll;
using Panel.Dal.Concrete.EntityFramework.Repository;
using Panel.Entities.DataModels;
using Panel.Entities.Models;
using Panel.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;
using Panel.API;
using Panel.Bll;

namespace Panel.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : Controller
    {

        IUserService UsersGenericService = new UserManager(new EfUserRepository());

        private IWebHostEnvironment _hostEnvironment;




        ReturnCodeGenerator _returnCodes;
        TicketGenerator ticketGenerator;



        public UsersController(IWebHostEnvironment environment)
        {
            _returnCodes = new ReturnCodeGenerator();
            _hostEnvironment = environment;

        }



        [HttpGet("GetMe"), Authorize]
        public async Task<ActionResult> GetMe()
        {
            if (!User.Identity.IsAuthenticated)
                throw new AuthenticationException();

            string userId = User.Claims.FirstOrDefault(c => c.Type.Contains("name")).Value;

            UserLoggedIn current_user = new UserLoggedIn();
            var result = UsersGenericService.GetAll().Where(x => x.Username == userId || x.EMail == userId).FirstOrDefault();

            if (result != null)
            {
                current_user.Email = result.EMail;
                current_user.Username = result.Username;
                current_user.UserID = result.UserReferenceID;
                current_user.fullname = result.Fullname;
                current_user.userCode = result.UserCode;
                current_user.TokenExpires = result.TokenExpires;

                string webRootPath = _hostEnvironment.WebRootPath;
                string path = "";
                if (result.ProfilePictureLocation != null)
                {
                    try
                    {
                        path = Path.Combine(Directory.GetCurrentDirectory(), "../main_files/Content", result.ProfilePictureLocation);

                        var file = System.IO.File.ReadAllBytes(path);


                        current_user.ProfileImage = Convert.ToBase64String(file);
                    }
                    catch
                    {
                        path = Path.Combine(Directory.GetCurrentDirectory(), "../main_files/Content/Default", "default_profile_image.jpg");

                        var file = System.IO.File.ReadAllBytes(path);


                        current_user.ProfileImage = Convert.ToBase64String(file);
                    }
                }

                else
                {
                    try
                    {
                        path = Path.Combine(Directory.GetCurrentDirectory(),"../main_files/Content/Default", "default_profile_image.jpg");


                        var file = System.IO.File.ReadAllBytes(path);


                        current_user.ProfileImage = Convert.ToBase64String(file);


                        // act on the Base64 data


                    }

                    catch
                    {

                    }
                }






            }
            else
            {
                return BadRequest(_returnCodes.GenericFail(400, "Profil bilgileri getirilemedi."));
            }
            return Ok(current_user);



        }



        [HttpPost("GetUser"), Authorize]
        public async Task<ActionResult<User>> GetUser(int UserReferanceID)
        {


            var user = UsersGenericService.Get(UserReferanceID);

            if (user == null)
            {

                return BadRequest(_returnCodes.GenericFail(400, "Belirtilen Kullanıcı Bulunamadı."));
            }

            return user;
        }

        [HttpPost("RemoveUser"), Authorize]
        public async Task<ActionResult<User>> RemoveUser(int UserReferanceID)
        {


            var user = UsersGenericService.Get(UserReferanceID);

            user.IsActive = false;

            var resp = UsersGenericService.Update(user);

            if (resp == null)
            {

                return BadRequest(_returnCodes.GenericFail(500, "Kullanıcı Silinemedi."));
            }
            else
            {
                return user;
            }

        }


        [HttpGet("GetAllUsers"), Authorize]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {


            var allusers = UsersGenericService.GetAll();

            if (allusers == null)
            {

                return BadRequest(_returnCodes.GenericFail(400, "Şu anda kayıtlı hiçbir kullanıcı yok!"));
            }


            return allusers;
        }


        [HttpGet("GetBannedUsers"), Authorize]
        public async Task<ActionResult<List<User>>> GetBannedOrRemovedUsers()
        {


            var allusers = UsersGenericService.GetAll().Where(x => x.IsActive == false).ToList();

            if (allusers == null)
            {

                return BadRequest(_returnCodes.GenericFail(400, "Silinmiş ya da yasaklanmış hiçbir kullanıcı yok!"));
            }

            return allusers;
        }

        [HttpGet("GetUnverifiedUsers"), Authorize]
        public async Task<ActionResult<List<User>>> GetUnverifiedUsers()
        {


            var allusers = UsersGenericService.GetAll().Where(x => x.IsVisible == false).ToList();

            if (allusers == null)
            {

                return BadRequest(_returnCodes.GenericFail(400, "Doğrulanmamış hiçbir hesap yok!"));
            }

            return allusers;
        }


    }



}


