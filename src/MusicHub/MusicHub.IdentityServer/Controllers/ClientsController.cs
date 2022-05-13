using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicHub.IdentityServer.Data;
using Duende.IdentityServer.EntityFramework.Mappers;
using MusicHub.IdentityServer.Models.IdentityServer;

namespace MusicHub.IdentityServer.Controllers;
public class ClientsController : Controller
{
    private readonly IdentityConfigDbContext _identityDb;
    public ClientsController(IdentityConfigDbContext identityDb)
    {
        _identityDb = identityDb;
    }

    public async Task<ActionResult> Index()
    {
        return View(await _identityDb.Clients.Select(c => c.ToModel()).ToListAsync());
    }

    public ActionResult Create() => View();
}
