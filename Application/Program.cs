using System.Text;
using FluentMigrator.Runner;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using O2GEN.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
            ValidAudience = builder.Configuration["JwtConfig:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:Key"])),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.Configure<DatabaseConnectionOptions>(builder.Configuration.GetSection("DatabaseConnectionOptions"));
builder.Services.AddMediatR(typeof(Program), typeof(DatabaseConnectionOptions));
builder.Services.AddScoped<IDbConnectionFactory<NpgsqlConnection>, NpgsqlConnectionFactory>();
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped<IChangeTracker,ChangeTracker>();
builder.Services.AddScoped<ICredentialRepository, CredentialRepository>();

var connectionString = builder.Configuration["DatabaseConnectionOptions:ConnectionString"];

builder.Services
    .AddFluentMigratorCore()
    .ConfigureRunner(r => 
        r.AddPostgres()
            .WithGlobalConnectionString(connectionString)
            .ScanIn(typeof(UserTable).Assembly)
            .For.Migrations());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    using var scope = ((IApplicationBuilder) app).ApplicationServices.CreateScope();
    var migrator = scope.ServiceProvider.GetService<IMigrationRunner>();
    migrator.MigrateUp();
}

app.UseMiddleware<JwtMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();