using EstartandoDevs.Domain.Repository;
using EstartandoDevs.Infrastructure.Context;
using EstartandoDevs.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.Load("EstartandoDevs.Application"));
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Controllers
builder.Services.AddControllers();

// Banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    if (!string.IsNullOrEmpty(connectionString))
    {
        // Usa versão específica do MySQL 8.0 para evitar problemas de detecção automática
        // Configura para não falhar na inicialização se não conseguir conectar imediatamente
        options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21)), mysqlOptions =>
        {
            mysqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
        });
    }
});

// Repositórios
builder.Services.AddScoped<IFornecedorRepository, FornecedorRepository>();

// Health Checks
builder.Services.AddHealthChecks()
    .AddDbContextCheck<AppDbContext>("database");

// CORS - Default Policy
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(config =>
    {
        config
            .AllowAnyOrigin()   // Em produção, substitua por .WithOrigins("https://seusite.com")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Middleware de ambiente
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHsts();
}

// HTTPS redirection apenas em ambientes com certificados configurados
// Em containers/AWS ALB, geralmente o SSL termina no load balancer
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

// Middleware para expirar a aplicação após data/hora limite (horário de São Paulo)
var cutoffLocalString = Environment.GetEnvironmentVariable("APP_CUTOFF_LOCAL");
// Padrão: hoje às 23:00 (America/Sao_Paulo)
if (string.IsNullOrWhiteSpace(cutoffLocalString))
{
    var saoPauloTz = TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo");
    var todayLocal = TimeZoneInfo.ConvertTime(DateTime.UtcNow, saoPauloTz).Date;
    var defaultCutoffLocal = todayLocal.AddHours(23);
    cutoffLocalString = defaultCutoffLocal.ToString("yyyy-MM-ddTHH:mm:ss");
}

DateTime cutoffLocal;
if (DateTime.TryParse(cutoffLocalString, out cutoffLocal))
{
    var tz = TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo");
    var cutoffUtc = TimeZoneInfo.ConvertTimeToUtc(cutoffLocal, tz);
    app.Use(async (context, next) =>
    {
        if (DateTimeOffset.UtcNow >= cutoffUtc)
        {
            context.Response.StatusCode = StatusCodes.Status410Gone;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync("{\"error\":\"expired\",\"message\":\"Aplicação expirada\"}");
            return;
        }
        await next();
    });
}

app.UseRouting();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Health Check endpoint
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";
        var result = System.Text.Json.JsonSerializer.Serialize(new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(e => new
            {
                name = e.Key,
                status = e.Value.Status.ToString(),
                exception = e.Value.Exception?.Message
            })
        });
        await context.Response.WriteAsync(result);
    }
});

app.Run();