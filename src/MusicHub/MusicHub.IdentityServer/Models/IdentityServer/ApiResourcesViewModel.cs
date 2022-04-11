using Duende.IdentityServer.Models;

namespace MusicHub.IdentityServer.Models.IdentityServer;

public class ApiResourcesViewModel : ResourceInputModel<ApiResource>
{
    public List<ApiResource> ApiResources { get; set; }
}
