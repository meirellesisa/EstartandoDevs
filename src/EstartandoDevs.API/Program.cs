using Amazon;
using Amazon.CognitoIdentityProvider;
using EstartandoDevs.Domain.Repository;
using EstartandoDevs.Infrastructure.Context;
using EstartandoDevs.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.Load("EstartandoDevs.Application"));
});

// Swagger
// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();

//ISSO FAZ O BOTÃO APARECER
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "EstartandoDevs API",
        Description = "EstartandoDevs API ASP.NET Core API",
    });
    // JWT Bearer (Cognito) in Swagger
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "JWT via Cognito. Ex: Bearer {token}",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    // Include XML comments
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

// Controllers
builder.Services.AddControllers();

// Banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = Environment.GetEnvironmentVariable("DefaultConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// Repositórios
builder.Services.AddScoped<IFornecedorRepository, FornecedorRepository>();

// Add Authentication/Authorization (AWS Cognito)
var cognitoRegion = Environment.GetEnvironmentVariable("COGNITO_REGION") ?? builder.Configuration["Cognito:Region"] ?? "us-east-1";
var cognitoUserPoolId = Environment.GetEnvironmentVariable("COGNITO_USER_POOL_ID") ?? builder.Configuration["Cognito:UserPoolId"] ?? string.Empty;
var cognitoClientId = Environment.GetEnvironmentVariable("COGNITO_CLIENT_ID") ?? builder.Configuration["Cognito:ClientId"] ?? string.Empty;

if (!string.IsNullOrWhiteSpace(cognitoUserPoolId) && !string.IsNullOrWhiteSpace(cognitoClientId))
{
    var authority = $"https://cognito-idp.{cognitoRegion}.amazonaws.com/{cognitoUserPoolId}"; // atenção isso é crucial 

    builder.Services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // PACOTE DO JWT: Microsoft.AspNetCore.Authentication.JwtBearer
        .AddJwtBearer(options =>
        {
            options.Authority = authority;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = authority,
                ValidateAudience = true,
                ValidAudience = Environment.GetEnvironmentVariable("COGNITO_CLIENT_ID"),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(2)
            };
        });

    builder.Services.AddAuthorization();

    // Register Cognito client for AuthController
    builder.Services.AddSingleton<IAmazonCognitoIdentityProvider>(_ =>
        new AmazonCognitoIdentityProviderClient(RegionEndpoint.GetBySystemName(cognitoRegion)));
}

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

// AWS Cognito
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonCognitoIdentityProvider>();
builder.Services.AddHttpContextAccessor();

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

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();