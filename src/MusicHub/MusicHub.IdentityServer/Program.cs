using Microsoft.EntityFrameworkCore;
using MusicHub.Data;

var builder = WebApplication.CreateBuilder(args);

// Add necessary services for MVC to function
builder.Services.AddControllersWithViews();

// Register DbContext for access to db using Sql Server and the 
// connection string labelled "Default" in appsettings.json
builder.Services.AddDbContext<MusicHubDbContext>(
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));


/*
 * Docs Link for Services and DI: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-6.0
 * 
 * Note: When reading lines like the above code, try to just take it chunk by chunk, e.g.:
 * - builder.Services.AddDbContext -- Tells us that we're adding db context service
 * - builder.Services.AddDbContext<MusicHubDbContext> -- This sets the actual Type of the DbContext
 * - opt => ... -- This is the action lambda to configure the DbContext
 * - opt.UseSqlServer -- We're using this DbContext with a MS Sql Server database
 * - builder.Configuration.GetConnectionString -- This pulls from the "connectionStrings" section of appsettings.json
 * 
 * So you basically just have a VERY conpact way of getting a lot of things done. MS kind of forces you into patterns like
 * this bc they work really well with all of the built-in services and dependency injection. 
 * 
 * It's all weird to look at when you start, but it's extremely useful for two big reasons:
 * 1. You can pick up all kinds of information and know exactly what's going on 
 *      with application lifycycle/service configuration/request pipeline processing with a quick glance
 * 2. If we continuously follow the patterns that are demonstrated by the built-in services and core components,
 *      our application will follow a very well thought out architecture and will have a very solid basis to expand upon
 *      and maintain. 
 *      
 * Also, in general, if syntax/operators/methods/etc seem unfamiliar, there's a good chance that they were created as a QOL type
 * improvement for developers. This newest C# version that we're working in has SO many things like this.. 
 */

// The webapplication needs to be "built" which essentially just connects all the backend system plumbing, this is always called
var app = builder.Build();

// Configure the HTTP request pipeline.
// Understanding services with ServiceCollection and DI is higher priority to get started than request pipeline but the way
// that the pipeline works is actually pretty simple
//
// Docs link for middleware: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-6.0
if (!app.Environment.IsDevelopment())
{
    // This just adds that developer exception page (Works for APIs and Razor Pages too)
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// All of the below methods that start with "Use" are special methods that add middleware to the application pipeline
// Middleware can be very useful because it runs both before AND after the actual processing in the controller code.
// IMPORTANT: The order in which middleware is added is actually relevant in this context. Middle executes in the order it's set up 
// (and then in reverse order on the way out) --> You can see this in the pipeline diagram in the MS docs linked above

// Auto redirects to https version when http is requested
app.UseHttpsRedirection();

// Automatically prepares to serve any of the static files from the wwwroot folder (e.g. css, js, images)
app.UseStaticFiles();

// Always need this, sets up the routing middleware
app.UseRouting();

// Sets up the authorization middleware, more on this later
app.UseAuthorization();

// This basically just sets up the routing template and works pretty much the same as framework with asp.net
// In this case, the pattern string is naming the different route parameters and assigning default values
// So you end up with the route template: {controller}/{actionMethodName}/{id} - where id is optional
// The {id?} component provides no default value, and is allowed to be null(?) which makes perfect sense when you look at the convention
// of the controllers and views
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Actually runs the application and blocks the calling thread
// (i.e. if you run this application, it will block the caller until it shuts down)
// The app itself and the runtime handle the multithreading of requests so we just have to configure, build, and finally run the app
app.Run();
