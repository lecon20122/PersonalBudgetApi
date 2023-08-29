namespace PersonalBudget.DTO
{
    public class UserDTO
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
