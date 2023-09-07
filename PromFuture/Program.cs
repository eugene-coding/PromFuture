using Api.Extensions;

using Core.Exceptions;
using Core.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.ConfigureApiServices();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var authenticateService = services.GetRequiredService<IAuthenticationService>();

    var apiSection = app.Configuration.GetRequiredSection("Api")
        ?? throw new MissingConfigurationSectionException("Api");

    var login = apiSection.GetValue<string>("Login")
        ?? throw new MissingConfigurationKeyException("Login");

    var password = apiSection.GetValue<string>("Password")
        ?? throw new MissingConfigurationKeyException("Password");

    await authenticateService.AuthenticateAsync(login, password);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
