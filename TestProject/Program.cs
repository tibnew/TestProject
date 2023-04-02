using Dapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Data.SQLite;
using System.Text;
using TestProject;
using TestProject.Repositories;


File.WriteAllBytes(Constants.DBPath, new byte[0]);
using (var connection = new SQLiteConnection(Constants.ConnectionString))
{
    connection.Open();

    string sql = @"
                CREATE TABLE IF NOT EXISTS Data (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                    Item1 INT NOT NULL,
                    Item2 TEXT NOT NULL
                );
            ";
    connection.Execute(sql);
}

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDataRepository, DataRepository>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = AuthOptions.ISSUER,
            ValidateAudience = true,
            ValidAudience = AuthOptions.AUDIENCE,
            ValidateLifetime = true,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.MapControllers();

app.Run();

public class AuthOptions
{
    public const string ISSUER = "TestProjectServer";
    public const string AUDIENCE = "TestProjectClient";
    const string KEY = "mysupersecret_secretkey!123";
    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}
