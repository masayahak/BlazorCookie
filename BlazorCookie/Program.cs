using Microsoft.AspNetCore.Authentication.Cookies;
using BlazorCookie.Components;
using BlazorCookie.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// ----------- Cookie�F�� --------------------------------
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
    });
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<MyAuthenticationService>();
// -------------------------------------------------------

// �ڍׂȃG���[��L����
builder.Services.AddServerSideBlazor(options =>
{
    options.DetailedErrors = true;
});

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

// �F�؁E�F��
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
