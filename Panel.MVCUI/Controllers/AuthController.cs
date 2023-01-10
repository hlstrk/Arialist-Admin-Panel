using Panel.Bll;
using Panel.Dal.Concrete.EntityFramework.Repository;
using Panel.Entities.DataModels;
using Panel.Entities.Models;
using Panel.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Authentication;
using System.Security.Claims;
using System.Security.Cryptography;
using Panel.API;
using Panel.Cache;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace Panel.MVCUI.Controllers
{
   

    public class AuthController : Controller
    {
        public static User user = new User();
        public UserMinDTO usermin = new UserMinDTO();
        CacheFonksiyon cacheFonsiyon;
        ReturnCodeGenerator _returnCodes;
        TicketGenerator ticketGenerator;
     
        IUserService UsersGenericService = new UserManager(new EfUserRepository());

    
        public static string main_directory;



        public AuthController( )
        {
            main_directory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            ticketGenerator = new TicketGenerator();
            cacheFonsiyon = new CacheFonksiyon();
            _returnCodes = new ReturnCodeGenerator();
          

        }






        [HttpGet("auth_check"), Authorize]
        public async Task<ActionResult> auth_check()
        {

            return Ok();
        }




        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(UserDto request)
        {
            var user = await DoRegisterCheck(request);

            return user;

        }


        //[HttpPost("login")]
        //public async Task<ActionResult<string>> Login(User request)
        //{
        //    if (user.Username != request.Username)
        //    {
        //        return BadRequest("User not found.");
        //    }

        //    if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
        //    {
        //        return BadRequest("Wrong password.");
        //    }


        //    var token = request.ResetPasswordToken;


        //    return Ok(token);
        //}

        [AllowAnonymous]
     
        public IActionResult Login()
        {


            ClaimsPrincipal claimuser = HttpContext.User;
            if(claimuser.Identity.IsAuthenticated)
            {
                
                return RedirectToAction("Index", "Home");
            }
      







            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<JsonResult> Login(UserDtoLogin request)
        {

			

			var x= await DoLoginCheck(request.LoginID, request.Password);
          
            return x;
          
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult<string>> Logout()
        {

            
            try
            {

                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);


                return RedirectToAction("Login");
            }

            catch
            {
                return RedirectToAction("Login");
            }

        }




        [HttpPost("check-username")]
        public async Task<ActionResult<string>> CheckUsernameStatus(UserNameDto user)
        {

            var result = UsersGenericService.GetAll().Where(x => x.Username == user.Username);

            if (result.Any())
            {
                return BadRequest(_returnCodes.GenericFail(400, "Bu kullanıcı Adı Zaten Alınmış!"));
            }
            else
            {
                return Ok(_returnCodes.GenericSuccess(200, "Kullanıcı adı uygundur."));
            }


        }




        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (!usermin.RefreshToken.Equals(refreshToken))
            {
                return Unauthorized(_returnCodes.SigninFail(401, "Lütfen tekrar giriş yapın."));

            }
            else if (usermin.TokenExpires < DateTime.Now)
            {
                return Unauthorized(_returnCodes.SigninFail(401, "Oturumun süresi doldu. Lütfen tekrar giriş yapın."));

            }

            
            var newRefreshToken = GenerateRefreshToken();
            SetRefreshToken(newRefreshToken);


            return Ok(_returnCodes.GenericSuccess(200, "dd"));

        }

        RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            return refreshToken;
        }



        public async Task<JsonResult> DoLoginCheck(string usernameormail, string password)
        {
            string token = "";
			IslemSonucModel islem;
			
			var result = UsersGenericService.GetAll().Where(x => x.Username == usernameormail || x.EMail == usernameormail).FirstOrDefault();
            if (result == null)
            {
				islem = new IslemSonucModel()
				{
					IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
					Aciklama = "Kullanıcı bulunamadı",
					Baslik = IslemSonucuEnum.IslemSonucKodlari.Bilgilendirme.ToString()

				};
				return Json(islem);

			}

            else if(!VerifyPasswordHash(password, result.PasswordHash, result.PasswordSalt))
            {

				islem = new IslemSonucModel()
				{
					IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Hata,
					Aciklama = "Şifrenizi kontrol edip tekrar deneyin",
					Baslik = "Hatalı Şifre"

				};
				return Json(islem);


			}


            var session = CreateSession(result.Username, result.RoleString,result.KeepSession);

            
            UserLoggedIn loggedIn = new UserLoggedIn();
            loggedIn.UserID = result.UserReferenceID;
            loggedIn.fullname = result.Fullname;
            loggedIn.Username = result.Username;
            loggedIn.Email = result.EMail;
            loggedIn.userCode = result.UserCode;

            string path = "";

            if (result.ProfilePictureLocation != null)
            {
                try
                {

                    path = Path.Combine(main_directory, result.ProfilePictureLocation);
                    var file = System.IO.File.ReadAllBytes(path);


                    loggedIn.ProfileImage = Convert.ToBase64String(file);
                }
                catch
                {

                    path = Path.Combine(Directory.GetCurrentDirectory(), "Content\\Default", "default_profile_image.jpg");

                    var file = System.IO.File.ReadAllBytes(path);


                    loggedIn.ProfileImage = Convert.ToBase64String(file);
                }
            }

            else
            {
                try
                {
                    path = Path.Combine(main_directory, "Content\\Default", "default_profile_image.jpg");


                    var file = System.IO.File.ReadAllBytes(path);


                    loggedIn.ProfileImage = Convert.ToBase64String(file);

                    // act on the Base64 data


                }

                catch
                {

                }
            }





			islem = new IslemSonucModel()
			{
				IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
				Aciklama = "Başarıyla giriş yaptınız!",
				Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString()

			};
			return Json(islem);

		}



        public async Task<ActionResult> DoRegisterCheck(UserDto usertoregister)
        {

            var duplicatecheck = UsersGenericService.GetAll().Where(x => x.Username == usertoregister.Username || x.EMail == usertoregister.Email).FirstOrDefault();

            if (duplicatecheck == null)
            {
                User usercomplate = new User();
                CreatePasswordHash(usertoregister.Password, out byte[] passwordHash, out byte[] passwordSalt);


                usercomplate.PasswordHash = passwordHash;
                usercomplate.PasswordSalt = passwordSalt;
                usercomplate.IsPrivate = true;
                usercomplate.IsActive = true;//E Mail Confirmation Will Added In The Feature
                usercomplate.RoleID = (int)cacheFonsiyon.RolIDGetir("Admin");
                usercomplate.RoleString = "Admin";
                usercomplate.Username = usertoregister.Username;
                usercomplate.Registrationdate = DateTime.Now;
                usercomplate.LastSignedIn = DateTime.Now;
                usercomplate.EMail = usertoregister.Email;
                usercomplate.IsDeleted = false;
                usercomplate.IsVisible = true;
                usercomplate.UserCode = ticketGenerator.CreateUserCode();
                usercomplate.Fullname = usertoregister.FullName;

                usercomplate.TokenExpires = DateTime.Now;

                var result = UsersGenericService.Add(usercomplate);

                if (result != null)
                {
                    return Ok(/*_returnCodes.RegisterSuccess(200, CreateSession(result.Username, result.RoleString,result.KeepSession))*/);
                }
                else
                {
                    return BadRequest(_returnCodes.RegisterFail(400, "Kullanıcı Oluşturulamadı."));
                }
            }

            else
            {
                if (duplicatecheck.Username == usertoregister.Username && duplicatecheck.EMail == usertoregister.Email)
                {
                    return BadRequest(_returnCodes.RegisterFail(400, "Bu kullanıcı zaten kayıtlı. Lütfen giriş yapın."));
                }
                else if (duplicatecheck.Username == usertoregister.Username)
                {
                    return BadRequest(_returnCodes.RegisterFail(400, "Kullanıcı adı zaten alınmış!"));
                }
                else if (duplicatecheck.EMail == usertoregister.Email)
                {
                    return BadRequest(_returnCodes.RegisterFail(400, "Bu e-posta adresi ile zaten bir hesap bulunuyor..."));
                }
                else
                {
                    return BadRequest(_returnCodes.RegisterFail(400, "Bu kullanıcı zaten kayıtlı. Lütfen giriş yapın."));
                }

            }



        }



        private void SetRefreshToken(RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            usermin.RefreshToken = newRefreshToken.Token;
            usermin.TokenCreated = newRefreshToken.Created;
            usermin.TokenExpires = newRefreshToken.Expires;
        }

        private async Task CreateSession(string usernameoremail, string Role,bool keepsession)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usernameoremail),
                new Claim(ClaimTypes.Role, Role)
            };

    
            ClaimsIdentity claimsIdentity= new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme
                
                );
           AuthenticationProperties properties = new AuthenticationProperties()
           {
               AllowRefresh= true,
               IsPersistent= keepsession,
           };

           await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),properties
                
                );

           

          

         
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }




    }



}


