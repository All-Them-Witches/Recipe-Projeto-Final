using Infrastructure; 
using Infrastructure.Configuration; 
using Microsoft.AspNetCore.Authentication.Cookies; 
using Microsoft.EntityFrameworkCore; 
using System.Security.Claims; 

var builder = WebApplication.CreateBuilder(args); // Cria um construtor para o aplicativo web

// Adiciona servi�os ao cont�iner.
builder.Services.AddRazorPages(); // Adiciona suporte a Razor Pages
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection"))); // Configura o contexto do banco de dados para usar o SQL Server

// Garante que o Bootstraper.Configure est� corretamente configurado
Bootstraper.Configure(builder.Services); // Configura o bootstrapper para servi�os

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("MainAdminOnly", policy =>
    {
        policy.RequireRole("MainAdmin"); // Define uma pol�tica que requer o papel "MainAdmin"
    });
    options.AddPolicy("UserOnly", policy =>
    {
        policy.RequireRole("User"); // Define uma pol�tica que requer o papel "User"
    });
    options.AddPolicy("AdminOnly", policy =>
    {
        policy.RequireRole("Admin"); // Define uma pol�tica que requer o papel "Admin"
    });
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/SignUp"; // Configura o caminho de login para autentica��o por cookie
    });

var app = builder.Build(); // Constr�i o aplicativo

// Configura o pipeline de requisi��es HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error"); // Usa uma p�gina de erro personalizada em produ��o
    app.UseHsts(); // Adiciona o middleware HSTS (HTTP Strict Transport Security)
}

app.UseHttpsRedirection(); // Redireciona HTTP para HTTPS
app.UseStaticFiles(); // Habilita o uso de arquivos est�ticos

app.UseRouting(); // Habilita o roteamento

app.UseAuthentication(); // Adiciona middleware de autentica��o
app.UseAuthorization(); // Adiciona middleware de autoriza��o

app.Map("/Info", builder =>
{
    builder.Run(async context =>
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            if (context.User.IsInRole("User") && context.User.IsInRole("Admin"))
            {
                var emailClaim = context.User.FindFirst(ClaimTypes.Email)?.Value; // Busca o claim de email do usu�rio
                await context.Response.WriteAsync(emailClaim ?? "Email not found"); // Responde com o email do usu�rio ou uma mensagem de "email n�o encontrado"
            }
            else
            {
                await context.Response.WriteAsync("admin"); // Responde com "admin" se o usu�rio n�o tiver os pap�is requeridos
            }
        }
        else
        {
            await context.Response.WriteAsync("unauthenticated"); // Responde com "unauthenticated" se o usu�rio n�o estiver autenticado
        }
    });
});

app.MapRazorPages(); // Mapeia as Razor Pages
app.MapControllers(); // Mapeia os controladores

app.Run(); // Executa o aplicativo
