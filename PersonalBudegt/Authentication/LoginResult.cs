﻿namespace PersonalBudget.Authentication
{
    public class LoginResult
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
