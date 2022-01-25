
using Duende.IdentityServer.EntityFramework.Mappers;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Stores;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicHub.IdentityServer.Data;
using MusicHub.IdentityServer.Models.IdentityServer;

namespace MusicHub.IdentityServer.Controllers;

[Authorize]
public class IdentityServerController : Controller
{


    private readonly IdentityConfigDbContext _identityDb;
    public IdentityServerController(IdentityConfigDbContext identityDb)
    {
        _identityDb = identityDb;
    }

    public IActionResult Index()
    {
        return View();
    }

    
    //*********** Identity Resources **********
    //View
    public async Task<IActionResult> IdentityResources()
    {
        return View(new IdentityResourcesViewModel
        {
            IdentityResources = await _identityDb.IdentityResources
                .Include(r => r.UserClaims).Include(r => r.Properties)
                    .Select(r => r.ToModel()).ToListAsync()
        });
    }
    //Create
    [HttpPost]
    public async Task<IActionResult> IdentityResources(IdentityResourcesViewModel model)
    {
        var newResource = model.Resource;
        newResource.UserClaims = model?.UserClaimsInput.ToUserClaimList();
        newResource.Properties = model?.PropertiesInput.ToResourceProperties();
        
        _identityDb.IdentityResources.Add(newResource.ToEntity());

        var res = await _identityDb.SaveChangesAsync();

        return RedirectToAction(nameof(IdentityResources));
    }
    //Update
    [HttpPost]
    public async Task<IActionResult> UpdateIdentityResource(IdentityResourcesViewModel model)
    {
        var resource = await _identityDb.IdentityResources.Include(r => r.UserClaims)
            .Include(r => r.Properties).FirstOrDefaultAsync(r => r.Name == model.UpdatedResource.Name);

        resource.DisplayName = Request.Form["UpdatedResource.DisplayName"];
        resource.Description = Request.Form["UpdatedResource.Description"];

        resource.Required = string.IsNullOrEmpty(Request.Form["UpdatedResource.Required"]) ? false : true;
        resource.Enabled = string.IsNullOrEmpty(Request.Form["UpdatedResource.Enabled"]) ? false : true;
        resource.Emphasize = string.IsNullOrEmpty(Request.Form["UpdatedResource.Emphasize"]) ? false : true;
        resource.ShowInDiscoveryDocument = string.IsNullOrEmpty(Request.Form["UpdatedResource.ShowInDiscoveryDocument"]) ? false : true;

        var changesSaved = await _identityDb.SaveChangesAsync();

        return RedirectToAction(nameof(IdentityResources));
    }
    //Delete
    [HttpPost]
    public async Task<IActionResult> DeleteIdentityResource(string ResourceName)
    {
        var resource = await _identityDb.IdentityResources.FirstOrDefaultAsync(r => r.Name == ResourceName);

        _identityDb.Remove(resource);

        var changesSaved = await _identityDb.SaveChangesAsync();

        return RedirectToAction(nameof(IdentityResources));
    }

    //*********** API Scopes **********
    //View
    public async Task<IActionResult> ApiScopes()
    {
        var t = new ApiScope();
        var scopes = await _identityDb.ApiScopes.ToListAsync();
        var s = scopes.Select(s => s.ToModel()).ToList();

        var s1 = s.First();

        return View();
    }
    //Create
    //Update
    //Delete
}

