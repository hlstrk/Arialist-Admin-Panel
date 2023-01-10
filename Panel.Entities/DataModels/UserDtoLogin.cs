namespace Panel.Entities.DataModels
{
    public class UserDtoLogin
    {
        public string LoginID { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public bool KeepSession { get; set; }



    }
}
