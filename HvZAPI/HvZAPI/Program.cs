using HvZAPI.Contexts;
using HvZAPI.Services.Concrete;
using HvZAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // Add services to the container.




        builder.Services.AddControllers();

        builder.Services.AddDbContext<HvZDbContext>(options => options.UseSqlServer(
            builder.Configuration.GetConnectionString("SERVER_CONNECTION")
            )
        );

        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddTransient<IGameService, GameService>();
        builder.Services.AddTransient<IKillService, KillService>();
        builder.Services.AddTransient<IMissionService, MissionService>();
        builder.Services.AddTransient<IPlayerService, PlayerService>();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(
         policy =>
         {
             policy.WithOrigins("http://localhost:3000")
             .AllowAnyHeader()
             .AllowAnyMethod();
         });
        });

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(
         policy =>
         {
             policy.WithOrigins("http://localhost:3000")
             .AllowAnyHeader()
             .AllowAnyMethod();
         });
        });

        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Movie API",
                Description = "Movie API",
            });
            options.IncludeXmlComments(xmlPath);
        });

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = builder.Configuration["TokenSecrets:IssuerUrl"],
                    ValidAudience = "account",
                    IssuerSigningKeyResolver = (token, securityToken, kid, parameters) =>
                    {
                        var client = new HttpClient();
                        var keyuri = builder.Configuration["TokenSecrets:KeyUrl"];

                        var response = client.GetAsync(keyuri).Result;
                        var responseString = response.Content.ReadAsStringAsync().Result;
                        var keys = new JsonWebKeySet(responseString);
                        return keys.Keys;
                    }
                };
            });

        builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

        var app = builder.Build();
        app.UseCors();


        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();

        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        app.UseCors();
        app.Run();
    }
}