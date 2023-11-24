

using FluentMigrator.Runner;
using MediatR;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<DatabaseConnectionOptions>(builder.Configuration.GetSection("DatabaseConnectionOptions"));
builder.Services.AddMediatR(typeof(Program), typeof(DatabaseConnectionOptions));
builder.Services.AddScoped<IDbConnectionFactory<NpgsqlConnection>, NpgsqlConnectionFactory>();
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped<IChangeTracker,ChangeTracker>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();

var connectionString = builder.Configuration["DatabaseConnectionOptions:ConnectionString"];

builder.Services
    .AddFluentMigratorCore()
    .ConfigureRunner(r => 
        r.AddPostgres()
            .WithGlobalConnectionString(connectionString)
            .ScanIn(typeof(Program).Assembly)
            .For.Migrations());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    using var scope = ((IApplicationBuilder) app).ApplicationServices.CreateScope();
    var migrator = scope.ServiceProvider.GetService<IMigrationRunner>();
    //migrator.MigrateUp();
}

app.MapControllers();

await app.RunAsync();

//Джамиля Сергей