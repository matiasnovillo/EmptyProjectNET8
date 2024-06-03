using EmptyProject.Areas.BasicCore.Repositories;
using EmptyProject.Areas.CMSCore.Repositories;
using EmptyProject.Components.Shared;
using EmptyProject.Components;
using EmptyProject.DatabaseContexts;
using EmptyProject.Areas.EmptyProject.Repositories;
using EmptyProject.Areas.EmptyProject.Services;
using EmptyProject.Middlewares;
using EmptyProject.Areas.BasicCore.Services;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//}).AddCookie();

//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromMinutes(30);
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
//});

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<EmptyProjectContext>(ServiceLifetime.Scoped);

//Set access to repositories
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<RoleRepository>();
builder.Services.AddScoped<MenuRepository>();
builder.Services.AddScoped<RoleMenuRepository>();
builder.Services.AddScoped<FailureRepository>();
builder.Services.AddScoped<FailureService>();
builder.Services.AddScoped<ParameterRepository>();

//Set access to repositories: EmptyProject
builder.Services.AddScoped<ClientRepository>();
builder.Services.AddScoped<ClientStatusRepository>();

//Set access to services: EmptyProject
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<ClientStatusService>();

//Set access to StateContainer to share data between Blazor components
builder.Services.AddScoped<StateContainer>();

//This line is to see other errors in the page, if appears
//builder.Services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithRedirects("/404");

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
