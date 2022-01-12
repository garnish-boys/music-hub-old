namespace MusicHub.IdentityServer.Models.Device
{
    public class DeviceAuthorizationInputModel : Consent.ConsentInputModel
    {
        public string UserCode { get; set; }
    }
}
