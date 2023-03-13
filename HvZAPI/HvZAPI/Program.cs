using HvZAPI.Contexts;
using HvZAPI.Services.Concrete;
using HvZAPI.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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

        builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

        var app = builder.Build();


        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();

        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
        app.UseCors();
        app.Run();

    }
}