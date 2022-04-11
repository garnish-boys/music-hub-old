

namespace MusicHub.IdentityServer.Models.Device
{
    public class DeviceAuthorizationViewModel : Consent.ConsentViewModel
    {
        public string UserCode { get; set; }
        public bool ConfirmUserCode { get; set; }
    }
}
