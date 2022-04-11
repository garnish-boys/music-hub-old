using Duende.IdentityServer.Models;

namespace MusicHub.IdentityServer.Models.IdentityServer;

public class IdentityResourcesViewModel : ResourceInputModel<IdentityResource>
{
    public List<IdentityResource> IdentityResources { get; set; } = new List<IdentityResource>();
    
}
