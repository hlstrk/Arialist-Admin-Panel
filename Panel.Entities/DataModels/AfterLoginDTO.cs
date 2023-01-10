

namespace Panel.API
{
    public class UserLoggedIn
    {

        public int UserID { get; set; }
        public string Username { get; set; } = string.Empty;
        public string fullname { get; set; } = string.Empty;

        public string userCode { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public DateTime TokenExpires { get; set; }
        public string ProfileImage { get; set; }
    }
}
