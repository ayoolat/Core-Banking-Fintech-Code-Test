using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Innovectives.Group.Application.Layer;
using Innovectives.Groups.Business.Layer.Services;
using Innovectives.Groups.Business.Layer.Services.Intreface;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using static Innovectives.Group.Application.Layer.Middlewares.ErrorHandlerMiddelware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var root = Directory.GetCurrentDirectory();
var dotenv = Path.Combine(root, ".env");
DotEnv.Load(dotenv);

builder.Services.AddScoped<ICoreBankingService, CoreBankingService>();
builder.Services.AddScoped<IFirebaseAuthService, FirebaseUserAuthService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var logger = builder.Logging.Services.BuildServiceProvider().GetRequiredService<ILogger<ErrorHandlerMiddleware>>();

var firebase = FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile("firebase-json.json")
});

builder.Services.AddAuthentication("Bearer")
 .AddJwtBearer("Bearer", options =>
 {
     options.Authority = "https://securetoken.google.com/thecakeshop-fe7a3";
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidIssuer = "https://securetoken.google.com/thecakeshop-fe7a3",
         ValidAudience = "thecakeshop-fe7a3",
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
     };
 });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});


builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();
app.MapControllers();

app.Run();
