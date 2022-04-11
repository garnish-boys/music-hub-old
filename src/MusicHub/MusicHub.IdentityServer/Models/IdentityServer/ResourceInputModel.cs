using Duende.IdentityServer.Models;

namespace MusicHub.IdentityServer.Models.IdentityServer;

public class ResourceInputModel<TResource>
    where TResource : Resource
{
    public TResource Resource { get; set; }
    public TResource UpdatedResource { get; set; }
    public string UserClaimsInput { get; set; }
    public string PropertiesInput { get; set; }
    public string UpdatedUserClaimsInput { get; set; }
    public string UpdatedPropertiesInput { get; set; }
}

public static class ResourceInputModelExtensions
{
    public static List<string> ToUserClaimList(this string userClaims, char delimiter = ',') 
    {
        if (string.IsNullOrEmpty(userClaims)) return new List<string>();
        return userClaims.Split(delimiter).Select(c => c.Trim()).ToList();
    }

    public static IDictionary<string, string> ToResourceProperties(this string resourceProps, char propDelimiter = ',', char keyValueDelimiter = ':') 
    {
        if (string.IsNullOrEmpty(resourceProps)) return new Dictionary<string, string>();
        return resourceProps.Split(propDelimiter)
                .Select(ps => ps.Trim().Split(keyValueDelimiter))
                    .ToDictionary(pair => pair[0], pair => pair[1]);
    }
}