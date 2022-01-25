using Duende.IdentityServer.Models;

namespace MusicHub.IdentityServer.Models.IdentityServer;

public class ApiScopesViewModel : ResourceInputModel<ApiScope>
{
    public List<ApiScope> ApiScopes { get; set; } = new List<ApiScope>();

}
