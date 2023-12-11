using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Since .NET 8 Authentication and Authorization is seemingly used just by adding the services
builder.Services.AddAuthentication().AddOAuth("Any", options => {});
builder.Services.AddAuthorization(options =>
{
    var policy = new AuthorizationPolicyBuilder();
    policy.RequireAuthenticatedUser();
    options.FallbackPolicy = policy.Build();
});

var app = builder.Build();

app.Map("/api", area => HandleAPI(area, app.Environment));
HandleStaticFiles(app, app.Environment);

app.Run();

static void HandleStaticFiles(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.UseDefaultFiles();
    app.UseStaticFiles();
}

static void HandleAPI(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.UseRouting();

    // Based on UseAuthentication and UseAuthrorization .NET 6 uses Authentication and Authorization within the /api-Route
     app.UseAuthentication();
     app.UseAuthorization();

    app.UseEndpoints(e =>
    {
        e.MapControllers();
    });
}