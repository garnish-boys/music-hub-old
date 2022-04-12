
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
        return View(new ApiScopesViewModel
        {
            ApiScopes = await _identityDb.ApiScopes
                .Include(s => s.UserClaims).Include(s => s.Properties)
                    .Select(s => s.ToModel()).ToListAsync()
        });
    }
    //Create
    [HttpPost]
    public async Task<IActionResult> ApiScopes(ApiScopesViewModel model)
    {
        var newScope = model.Resource;
        newScope.UserClaims = model?.UserClaimsInput.ToUserClaimList();
        newScope.Properties = model?.PropertiesInput.ToResourceProperties();

        _identityDb.ApiScopes.Add(newScope.ToEntity());
        await _identityDb.SaveChangesAsync();
        return RedirectToAction(nameof(ApiScopes));

    }
    //Update
    [HttpPost]
    public async Task<IActionResult> UpdateApiScope(ApiScopesViewModel model)
    {
        var resource = await _identityDb.ApiScopes.Include(r => r.UserClaims)
            .Include(r => r.Properties).FirstOrDefaultAsync(r => r.Name == model.UpdatedResource.Name);

        resource.DisplayName = Request.Form["UpdatedResource.DisplayName"];
        resource.Description = Request.Form["UpdatedResource.Description"];

        resource.Required = string.IsNullOrEmpty(Request.Form["UpdatedResource.Required"]) ? false : true;
        resource.Enabled = string.IsNullOrEmpty(Request.Form["UpdatedResource.Enabled"]) ? false : true;
        resource.Emphasize = string.IsNullOrEmpty(Request.Form["UpdatedResource.Emphasize"]) ? false : true;
        resource.ShowInDiscoveryDocument = string.IsNullOrEmpty(Request.Form["UpdatedResource.ShowInDiscoveryDocument"]) ? false : true;

        var changesSaved = await _identityDb.SaveChangesAsync();

        return RedirectToAction(nameof(ApiScopes));
    }
    //Delete
    [HttpPost]
    public async Task<IActionResult> DeleteApiScope(string ResourceName)
    {
        var resource = await _identityDb.ApiScopes.FirstOrDefaultAsync(r => r.Name == ResourceName);

        _identityDb.Remove(resource);

        var changesSaved = await _identityDb.SaveChangesAsync();

        return RedirectToAction(nameof(IdentityResources));
    }

    //*********** API Resources **********
    //View
    public async Task<IActionResult> ApiResources()
    {
        return View(new ApiResourcesViewModel
        {
            ApiResources = await _identityDb.ApiResources
                .Include(s => s.UserClaims).Include(s => s.Properties)
                    .Select(s => s.ToModel()).ToListAsync()
        });
    }
}

