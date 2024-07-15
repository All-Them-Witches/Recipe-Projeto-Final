using Infrastructure; 
using Infrastructure.Configuration; 
using Microsoft.AspNetCore.Authentication.Cookies; 
using Microsoft.EntityFrameworkCore; 
using System.Security.Claims; 

var builder = WebApplication.CreateBuilder(args); // Cria um construtor para o aplicativo web

// Adiciona serviços ao contêiner.
builder.Services.AddRazorPages(); // Adiciona suporte a Razor Pages
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection"))); // Configura o contexto do banco de dados para usar o SQL Server

// Garante que o Bootstraper.Configure está corretamente configurado
Bootstraper.Configure(builder.Services); // Configura o bootstrapper para serviços

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("MainAdminOnly", policy =>
    {
        policy.RequireRole("MainAdmin"); // Define uma política que requer o papel "MainAdmin"
    });
    options.AddPolicy("UserOnly", policy =>
    {
        policy.RequireRole("User"); // Define uma política que requer o papel "User"
    });
    options.AddPolicy("AdminOnly", policy =>
    {
        policy.RequireRole("Admin"); // Define uma política que requer o papel "Admin"
    });
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/SignUp"; // Configura o caminho de login para autenticação por cookie
    });

var app = builder.Build(); // Constrói o aplicativo

// Configura o pipeline de requisições HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error"); // Usa uma página de erro personalizada em produção
    app.UseHsts(); // Adiciona o middleware HSTS (HTTP Strict Transport Security)
}

app.UseHttpsRedirection(); // Redireciona HTTP para HTTPS
app.UseStaticFiles(); // Habilita o uso de arquivos estáticos

app.UseRouting(); // Habilita o roteamento

app.UseAuthentication(); // Adiciona middleware de autenticação
app.UseAuthorization(); // Adiciona middleware de autorização

app.Map("/Info", builder =>
{
    builder.Run(async context =>
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            if (context.User.IsInRole("User") && context.User.IsInRole("Admin"))
            {
                var emailClaim = context.User.FindFirst(ClaimTypes.Email)?.Value; // Busca o claim de email do usuário
                await context.Response.WriteAsync(emailClaim ?? "Email not found"); // Responde com o email do usuário ou uma mensagem de "email não encontrado"
            }
            else
            {
                await context.Response.WriteAsync("admin"); // Responde com "admin" se o usuário não tiver os papéis requeridos
            }
        }
        else
        {
            await context.Response.WriteAsync("unauthenticated"); // Responde com "unauthenticated" se o usuário não estiver autenticado
        }
    });
});

app.MapRazorPages(); // Mapeia as Razor Pages
app.MapControllers(); // Mapeia os controladores

app.Run(); // Executa o aplicativo
