using Duende.IdentityServer.EntityFramework.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicHub.IdentityServer.Data;
using MusicHub.IdentityServer.Models.IdentityServer;

namespace MusicHub.IdentityServer.Controllers;

[Authorize]
public class IdentityResourcesController : Controller
{
    private readonly IdentityConfigDbContext _identityDb;
    public IdentityResourcesController(IdentityConfigDbContext identityDb)
    {
        _identityDb = identityDb;
    }

    // GET: IdentityResourcesController
    public async Task<ActionResult> Index()
    {
        return View(await _identityDb.IdentityResources
            .Select(r => r.ToModel()).ToListAsync());
    }

    // GET: IdentityResourcesController/Details/5
    public async Task<ActionResult> Details(string id)
    {
        var resource = await _identityDb.IdentityResources
            .Include(r => r.UserClaims).Include(r => r.Properties)
                .FirstOrDefaultAsync(r => r.Name == id);

        if (resource is null) return NotFound();

        return View(resource.ToModel());
    }

    // GET: IdentityResourcesController/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: IdentityResourcesController/Create
    [HttpPost]
    public async Task<ActionResult> Create(IdentityResourceViewModel model)
    {

        var newResource = model.Resource;
        newResource.UserClaims = model.UserClaimsInput.ToUserClaimList();
        newResource.Properties = model.PropertiesInput.ToResourceProperties();

        _identityDb.IdentityResources.Add(newResource.ToEntity());

        await _identityDb.SaveChangesAsync();

        return RedirectToAction(nameof(Details), new { id = newResource.Name });
    }

    // GET: IdentityResourcesController/Edit/5
    public async Task<ActionResult> Edit(string id)
    {
        var resource = await _identityDb.IdentityResources
            .Include(r => r.UserClaims).Include(r => r.Properties)
                .FirstOrDefaultAsync(r => r.Name == id);

        if (resource is null) return NotFound();

        var resourceModel = resource.ToModel();

        return View(new IdentityResourceViewModel
        {
            Resource = resourceModel,
            UserClaimsInput = resourceModel.UserClaims.ToUserClaimString(),
            PropertiesInput = resourceModel.Properties.ToResourcePropertiesString()
        });
    }

    // POST: IdentityResourcesController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(string id, IdentityResourceViewModel model)
    {
        var resourceEntity = await _identityDb.IdentityResources
            .Include(r => r.UserClaims).Include(r => r.Properties)
                .SingleOrDefaultAsync(r => r.Name == id);
        
        if (resourceEntity is null) return NotFound();

        //Delete all of the claims and properties first
        resourceEntity.UserClaims.Clear();
        resourceEntity.Properties.Clear();
        await _identityDb.SaveChangesAsync();

        //Update properties
        resourceEntity.Name = model.Resource.Name;
        resourceEntity.DisplayName = model.Resource.DisplayName;
        resourceEntity.Description = model.Resource.Description;
        resourceEntity.Enabled = model.Resource.Enabled;
        resourceEntity.Required = model.Resource.Required;
        resourceEntity.Emphasize = model.Resource.Emphasize;
        resourceEntity.ShowInDiscoveryDocument = model.Resource.ShowInDiscoveryDocument;

        var newClaimTypes = model.UserClaimsInput.ToUserClaimList();
        var newProperties = model.PropertiesInput.ToResourceProperties();
        
        foreach (var newClaim in newClaimTypes)
        {
            resourceEntity.UserClaims.Add(new Duende.IdentityServer.EntityFramework.Entities.IdentityResourceClaim
            {
                Type = newClaim
            });
        }

        foreach (var newProp in newProperties)
        {
            resourceEntity.Properties.Add(new Duende.IdentityServer.EntityFramework.Entities.IdentityResourceProperty
            {
                Key = newProp.Key,
                Value = newProp.Value
            });
        }

        await _identityDb.SaveChangesAsync();

        return RedirectToAction(nameof(Details), new { id = resourceEntity.Name });
    }

    // GET: IdentityResourcesController/Delete/5
    public async Task<ActionResult> Delete(string id)
    {
        var resource = await _identityDb.IdentityResources
            .FirstOrDefaultAsync(r => r.Name == id);

        if (resource is null) return NotFound();

        return View(resource.ToModel());
    }

    // POST: IdentityResourcesController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Delete(string id, IFormCollection collection)
    {
        var resource = await _identityDb.IdentityResources
            .FirstOrDefaultAsync(r => r.Name == id);

        if (resource is null) return NotFound();

        _identityDb.IdentityResources.Remove(resource);

        await _identityDb.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}
