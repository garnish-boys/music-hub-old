﻿namespace MusicHub.IdentityServer.Models.Account
{
    public class LogoutViewModel : LogoutInputModel
    {
        public bool ShowLogoutPrompt { get; set; } = true;
    }
}
