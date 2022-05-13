using Duende.IdentityServer.Models;

namespace MusicHub.IdentityServer.Models.IdentityServer;

public class ResourceInputModel<TResource>
    where TResource : Resource, new()
{
    public TResource Resource { get; set; } = new();
    public string UserClaimsInput { get; set; } = String.Empty;
    public string PropertiesInput { get; set; } = String.Empty;
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

    public static string ToUserClaimString(this ICollection<string> userClaims, char delimiter = ',') => String.Join(delimiter, userClaims);

    public static string ToResourcePropertiesString(this IDictionary<string, string> resourceProps, char propDelimiter = ',', char keyValueDelimiter = ':') =>
        String.Join(propDelimiter, resourceProps.Select(p => $"{p.Key}{keyValueDelimiter}{p.Value}"));
}