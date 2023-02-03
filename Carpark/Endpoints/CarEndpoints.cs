using AutoMapper;
using Carpark.Business.Repositories;
using Carpark.Business.Repositories.IRepositories;
using Carpark.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Carpark.WebAPI.Endpoints
{
    public static class CarEndpoints
    {
        public static void ConfigureCarEndpoints(this WebApplication app)
        {
            app.MapGet("/api/car", GetAllCars)
                .WithName("GetCars")
                .Produces<APIResponse>(200);


            app.MapGet("/api/car/{id:int}", GetCar)
                .WithName("GetCar")
                .Produces<APIResponse>(200);

            app.MapPost("/api/car", CreateCar)
                .WithName("CreateCar")
                .Accepts<CarCreateDTO>("application/json")
                .Produces<APIResponse>(201)
                .Produces(400);

            app.MapPut("/api/car", UpdateCar)
                .WithName("UpdateCar")
                .Accepts<CarUpdateDTO>("application/json")
                .Produces<APIResponse>(200)
                .Produces(400);

            app.MapDelete("/api/car/{id:int}", DeleteCar);
        }

        private async static Task<IResult> DeleteCar(ICarRepository carRepository, int id)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };

            CarDTO carFromStore = await carRepository.GetAsync(id);
            if (carFromStore != null)
            {
                await carRepository.RemoveAsync(carFromStore.Id);
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.NoContent;
                return Results.Ok(response);
            }
            else
            {
                response.ErrorMessages.Add("Invalid Id");
                return Results.BadRequest(response);
            }
        }

        private async static Task<IResult> UpdateCar(
            ICarRepository CarRepository,
            IMapper mapper,
            [FromBody] CarUpdateDTO car_U_DTO)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };

            await CarRepository.UpdateAsync(mapper.Map<CarDTO>(car_U_DTO));

            response.Result = mapper.Map<CarDTO>(await CarRepository.GetAsync(car_U_DTO.Id)); ;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }


        private async static Task<IResult> CreateCar(
            ICarRepository carRepository,
            IMapper mapper,
            [FromBody] CarCreateDTO car_C_DTO)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };

            if (carRepository.GetAsync(car_C_DTO.Manufacturer).GetAwaiter().GetResult() != null)
            {
                response.ErrorMessages.Add("Car already exists");
                return Results.BadRequest(response);
            }

            CarDTO car = mapper.Map<CarDTO>(car_C_DTO);

            await carRepository.CreateAsync(car);

            CarDTO carDTO = mapper.Map<CarDTO>(car);
            response.Result = carDTO;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.Created;
            return Results.Ok(response);


        }


        private async static Task<IResult> GetAllCars(
            ICarRepository _carRepo, ILogger<Program> _logger)
        {
            APIResponse response = new();
            _logger.Log(LogLevel.Information, "Getting all cars");
            response.Result = await _carRepo.GetAllAsync();
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }

        private async static Task<IResult> GetCar(
            ICarRepository _carRepo, ILogger<Program> _logger, int id)
        {
            APIResponse response = new();
            response.Result = await _carRepo.GetAsync(id);
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }

    }
}
