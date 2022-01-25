using Duende.IdentityServer.Models;

namespace MusicHub.IdentityServer.Models.IdentityServer;

public class IdentityResourcesViewModel : IdentityResourceInputModel
{
    public List<IdentityResource> IdentityResources { get; set; } = new List<IdentityResource>();
    public IdentityResource UpdatedResource { get; set; } = new IdentityResource();
    public string UpdatedUserClaimsInput { get; set; }
    public string UpdatedPropertiesInput { get; set; }
}
