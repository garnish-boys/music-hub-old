# Introduction
This document will serve as an information guide 
as well as an informative index of the codebase for the MusicHub projects.  


To get started with understanding this project, we'll go head first into the identity server component
to get through the core parts of working in .NET 6.  
We'll start by focusing on the **MusicHub.IdentityServer** project in the solution.


### Starting Point (Building Identity Server)
We'll start with **Program.cs** since that's the starting point for the application and 
has a lot going on.

---

#### Structure of This File (Top Level Statements)
```csharp
using Microsoft.EntityFrameworkCore;
using MusicHub.Data;

var builder = WebApplication.CreateBuilder(args);

//... rest omitted

app.Run(); //Application runs from here
```
Notice that `Program.cs` is the entry point for the application but there's no main method.
This is a brand new C# feature (top level statements) that lets you define the entry point for the application, 
add the necessary usings, and jump right into the code.
  

---  

#### The WebApplicationBuilder 
```csharp
var builder = WebApplication.CreateBuilder(args);
```
The WebApplicationBuilder is essentially just a framework provided class that lets us set
up the web application through some compact and concise code.

Here the `builder` object is of type `Microsoft.AspNetCore.Builder.WebApplicationBuilder` and is
created using the static `WebApplication.CreateBuilder(args)` method.  
This `builder` is the basis for the application (whether MVC, API, etc.) and hooks up all
of the internal components based on how you configure it.  

---

#### Registering Services for Dependency Injection
```csharp
// Add necessary services for MVC to function
builder.Services.AddControllersWithViews();

// Register DbContext for access to db using Sql Server and the 
// connection string labelled "Default" in appsettings.json
builder.Services.AddDbContext<MusicHubDbContext>(
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
```
We always have to register services for dependency injection at this configuration stage of the application.
Consuming these DI services will be convered when dealing with the MVC controllers.


Here is some simple examples of adding services for DI. 
You'll notice that most of the extension methods that configure services will usually start with *Add*.  
By registering these services now, we allow them to accessed via **constructor injection** (covered in controllers) 
while the application is running.  

> **Useful Links**  
> [Services and DI](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-6.0)  
> [Middleware and Pipeline](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-6.0)

---

#### Setting Up the Middleware
```csharp
// Auto redirects to https version when http is requested
app.UseHttpsRedirection();

// Automatically prepares to serve any of the static files from the wwwroot folder
app.UseStaticFiles();

// Always need this, sets up the routing middleware
app.UseRouting();

// Sets up the authorization middleware, more on this later
app.UseAuthorization();
```
Middleware in a .NET Core application plays an important role. Middleware runs before the controller code, 
and then again after the controller code is done but before the response is sent.

Most of the things that the framework actually does in any of these .NET Core web app
is just done through middleware. **For example:** When requests come in, routing middleware is invoked
to choose which controller and method the request is destined for. Middleware is also what's used to 
implement authentication and sessions/cookies.

You won't need to worry about middleware too much up front

---

#### Notes on ASP.NET Core App Structure (DI, Middleware, etc.)
In modern .NET Core apps, dependency injection is key. The [MS docs](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-6.0) 
that are linked above do a great job showing you how it works and how to use it. The important thing 
to keep in mind while parsing through these docs is to not get bogged down in the details and syntax. 
Focus on big picture understanding of how the different parts of the apps function together and the examples 
you see will help fill in the rest in terms of syntax and implementation. Some key notes that I think 
are good to have said early:

* Most of the framework and system metehods are very *semantic* and get reused all over the place. The more you use them, the more you'll get used to naming and system APIs will be much easier to track down and remember.
* Make sure that you learn about **service lifetimes**. In practice, most stuff is pretty simple with service lifetimes, but it's good to know them well in cases where applications aren't so simple with service lifetimes.
* If you find yourself working in *cshtml* (**MVC views** or **Razor Pages** - same thing), the reference for tag helpers is [here](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/tag-helpers/built-in/?view=aspnetcore-6.0)
* If you want to find out a good way to do something, the [**Microsoft Docs Tutorials**](https://docs.microsoft.com/en-us/aspnet/core/tutorials/choose-web-ui?view=aspnetcore-6.0) cover a lot of different cases and you can usually find the GitHub links for the completed code on the first page
* You can ignore stuff about records, structs, and record-structs for now
* Starting out with the Identity Server might be challenging up-front, however it has opportunities to demostrate a ton of stuff in one place. The emphasis to start is definitely **big picture**.
* Once the Identity component is out of the way, building out the application will be easier and we'll have a real-world Identity system in place from the very start.
* The identity and .NET Core stuff is a **lot** to deal with but the benefits are HUGE. These applications will be full-scale production ready applications with real functionality. Learning this stuff using Identity Server and the .NET Identity stuff is really analagous to learning to drive in a top-tier racecar.


<br/>
<br/>
<br/>
<br/>
<br/>