
using Microsoft.EntityFrameworkCore;
using Carpark.Business.Mapper;
using Carpark.Business.Repositories;
using Carpark.Business.Repositories.IRepositories;
using Carpark.Data;
using Carpark.WebAPI.Endpoints;
using AutoMapper;

namespace Carpark
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(typeof(MappingConfig));
            builder.Services.AddDbContext<ApplicationDbContext>(option =>
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<ICarRepository, CarRepository>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.ConfigureCarEndpoints();

            app.Run();
        }
    }
}