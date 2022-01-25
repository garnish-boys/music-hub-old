using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Mvc;
using MusicHub.IdentityServer.Data;

namespace MusicHub.IdentityServer.ViewComponents;

public class CreateResourceViewComponent : ViewComponent
{
    private readonly IdentityConfigDbContext _configDb;
    public CreateResourceViewComponent(IdentityConfigDbContext configDb)
    {
        _configDb = configDb;
    }

    public async Task<IViewComponentResult> InvokeAsync(IEnumerable<Resource> resources)
    {
        return View(resources);
    }
}
