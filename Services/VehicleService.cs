using Domain.Models;
using Domain.Contracts.Requests.Vehicle;
using Domain.Contracts.Responses.Vehicle;
using GosAutoAPI.IServices;
using Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;

namespace GosAutoAPI.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehiclesRepository _repository;
        private readonly GosAutoDbContext _dbContext;
        public VehicleService(IVehiclesRepository repository, GosAutoDbContext dbContext)
        {
            _repository = repository;
            _dbContext = dbContext;
        }

        public IEnumerable<VehicleResponseGetAll> GetAllVehicles()
        {
            IEnumerable<Vehicle> vehicles = _repository.GetAll();
            return vehicles.Select(vehicle => new VehicleResponseGetAll(
                vehicle.Id,
                vehicle.Model,
                vehicle.CarNumbers,
                vehicle.Description,
                vehicle.DriverId,
                vehicle.Driver
                ));
        }

        public async Task CreateVehicle(VehicleRequestCreate request)
        {
            if (request.Model != null && request.CarNumbers.Length == Vehicle.CountOfNum)
            {
                Vehicle vehicle = new(
                    request.Model,
                    request.CarNumbers,
                    request.Description,
                    request.DriverId);
                await _repository.CreateAsync(vehicle);
            }
        }
        public async Task DeleteVehicle(VehicleRequestDelete request)
        {
            Vehicle? vehicle = await _repository.GetById(request.Id);
            if (vehicle != null)
            {
                await _repository.DeleteAsync(vehicle);
            }

        }
        public async Task UpdateVehicle(VehicleRequestUpdate request)
        {
            if (request != null)
            {
                var vehicle = await _repository.GetById(request.Id);
                vehicle.Description = request.Description;
                vehicle.Model = request.Model;
                vehicle.CarNumbers = request.CarNumbers;
                vehicle.DriverId = request.DriverId;
                await _repository.UpdateAsync(vehicle);
                _dbContext.Database.ExecuteSqlRaw(@"UPDATE ""Vehicles"" SET ""DriverId"" = {0}::uuid WHERE ""Id"" = {1}::uuid", request.DriverId, request.Id);
            }

        }

        public async Task<VehicleResponseGetById?> GetVehicleById(Guid Id)
        {

            Vehicle? vehicle = await _repository.GetById(Id);
            if (vehicle != null)
            {
                VehicleResponseGetById response = new(
                    vehicle.Id,
                    vehicle.Model,
                    vehicle.CarNumbers,
                    vehicle.Description,
                    vehicle.DriverId,
                    vehicle.Driver);
                return response;
            }
            return null;
        }
    }
}
