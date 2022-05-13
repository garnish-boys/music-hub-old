using Duende.IdentityServer.EntityFramework.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicHub.IdentityServer.Data;
using MusicHub.IdentityServer.Models.IdentityServer;

namespace MusicHub.IdentityServer.Controllers;
public class ApiScopesController : Controller
{
    private readonly IdentityConfigDbContext _identityDb;
    public ApiScopesController(IdentityConfigDbContext identityDb)
    {
        _identityDb = identityDb;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _identityDb.ApiScopes
            .Select(scope => scope.ToModel()).ToListAsync());
    }

    public async Task<ActionResult> Details(string id)
    {
        var scope = await _identityDb.ApiScopes
            .Include(s => s.UserClaims).Include(s => s.Properties)
                .FirstOrDefaultAsync(s => s.Name == id);

        if (scope is null) return NotFound();

        return View(scope.ToModel());
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(ApiScopeViewModel model)
    {
        var newScope = model.Resource;
        newScope.UserClaims = model.UserClaimsInput.ToUserClaimList();
        newScope.Properties = model.PropertiesInput.ToResourceProperties();

        _identityDb.ApiScopes.Add(newScope.ToEntity());

        await _identityDb.SaveChangesAsync();

        return RedirectToAction(nameof(Details), new {id = newScope.Name});
    }

    public async Task<ActionResult> Edit(string id)
    {
        var scope = await _identityDb.ApiScopes
            .Include(s => s.UserClaims).Include(s => s.Properties)
                .FirstOrDefaultAsync(s => s.Name == id);

        if (scope is null) return NotFound();

        var scopeModel = scope.ToModel();

        return View(new ApiScopeViewModel
        {
            Resource = scopeModel,
            UserClaimsInput = scopeModel.UserClaims.ToUserClaimString(),
            PropertiesInput = scopeModel.Properties.ToResourcePropertiesString()
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(string id, ApiScopeViewModel model)
    {
        var scopeEntity = await _identityDb.ApiScopes
            .Include(r => r.UserClaims).Include(r => r.Properties)
                .FirstOrDefaultAsync(r => r.Name == id);

        if (scopeEntity is null) return NotFound();

        //Delete all of the claims and properties first
        scopeEntity.UserClaims.Clear();
        scopeEntity.Properties.Clear();
        await _identityDb.SaveChangesAsync();

        //Update properties
        scopeEntity.Name = model.Resource.Name;
        scopeEntity.DisplayName = model.Resource.DisplayName;
        scopeEntity.Description = model.Resource.Description;
        scopeEntity.Enabled = model.Resource.Enabled;
        scopeEntity.Required = model.Resource.Required;
        scopeEntity.Emphasize = model.Resource.Emphasize;
        scopeEntity.ShowInDiscoveryDocument = model.Resource.ShowInDiscoveryDocument;

        var newClaimTypes = model.UserClaimsInput.ToUserClaimList();
        var newProperties = model.PropertiesInput.ToResourceProperties();

        foreach (var newClaim in newClaimTypes)
        {
            scopeEntity.UserClaims.Add(new Duende.IdentityServer.EntityFramework.Entities.ApiScopeClaim
            {
                Type = newClaim
            });
        }

        foreach (var newProp in newProperties)
        {
            scopeEntity.Properties.Add(new Duende.IdentityServer.EntityFramework.Entities.ApiScopeProperty
            {
                Key = newProp.Key,
                Value = newProp.Value
            });
        }

        await _identityDb.SaveChangesAsync();

        return RedirectToAction(nameof(Details), new { id = scopeEntity.Name });
    }

    // GET: IdentityResourcesController/Delete/5
    public async Task<ActionResult> Delete(string id)
    {
        var scope = await _identityDb.ApiScopes
            .FirstOrDefaultAsync(r => r.Name == id);

        if (scope is null) return NotFound();

        return View(scope.ToModel());
    }

    // POST: IdentityResourcesController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Delete(string id, IFormCollection collection)
    {
        var scope = await _identityDb.ApiScopes
            .FirstOrDefaultAsync(r => r.Name == id);

        if (scope is null) return NotFound();

        _identityDb.ApiScopes.Remove(scope);

        await _identityDb.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}
