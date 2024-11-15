using Domain.Models;
using Domain.Contracts.Requests.Driver;
using Domain.Contracts.Responses.Driver;
using GosAutoAPI.IServices;
using Infrastructure.IRepositories;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace GosAutoAPI.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriversRepository _repository;
        private readonly IFinesRepository _finesRepository;
        public DriverService(IDriversRepository repository, IFinesRepository finesRepository)
        {
            _repository = repository;
            _finesRepository = finesRepository;
        }

        public async Task CreateDriver(DriverRequestCreate request)
        {
            if (request.Name != null && request.NumberDocuments.Length == Driver.CountOfNum && Regex.IsMatch(request.NumberDocuments, @"^\d+$"))
            {
                Driver driver = new Driver(
                    request.Name,
                    request.NumberDocuments,
                    request.Description,
                    request.Forfeit,
                    request.CountForfeit);
                await _repository.CreateAsync(driver);
            }
        }

        public async Task DeleteDriver(DriverRequestDelete request)
        {
            Driver? driver = await _repository.GetById(request.Id);
            if (driver != null)
            {
                await _repository.DeleteAsync(driver);
            }
        }

        public IEnumerable<DriverResponseGetAll> GetAllDrivers()
        {
            IEnumerable<Driver> drivers = _repository.GetAll();
            return drivers.Select(driver => new DriverResponseGetAll(
                driver.Id,
                driver.Name,
                driver.NumberDocuments,
                driver.Description,
                driver.Forfeit,
                driver.CountForfeit));
        }

        public IEnumerable<string> GetListDrivers()
        {
            return _repository.GetAll().Select(driver => driver.Name);

        }

        public IEnumerable<Guid> GetDriverId()
        {
            return _repository.GetAll().Select(driver => driver.Id);
        }

        public async Task <DriverResponseGetById?> GetDriverById(Guid id)
        {
            Driver? driver = await _repository.GetById(id);
            if (driver != null)
            {
                DriverResponseGetById response = new(
                    driver.Id,
                    driver.Name,
                    driver.NumberDocuments,
                    driver.Description,
                    driver.Forfeit,
                    driver.CountForfeit
                    );
                return response;
            }
            return null;
        }

        public async Task UpdateDriver(DriverRequestUpdate request)
        {
            if (request != null)
            {

                var driver = await _repository.GetById(request.Id);

                driver.Name = request.Name;
                driver.NumberDocuments = request.NumberDocuments;
                driver.Description = request.Description;
                driver.Forfeit = request.Forfeit;
                driver.CountForfeit = request.CountForfeit;

                await _repository.UpdateAsync(driver);
            }
        }

        public async Task UpdateAfterFineUpdate(Guid DriverId)
        {
            int count = 0;
            float sum = 0;
            var driver = await _repository.GetById(DriverId);

            foreach (var keyVal in Fine.BondBetween)
            {
                if (keyVal.Value == driver.Id)
                {
                    var fine = await _finesRepository.GetById(keyVal.Key);
                    count++;
                    sum = fine.Fines + sum;
                }
            }
            driver.CountForfeit = count;
            driver.Forfeit = sum;

            await _repository.UpdateAsync(driver);
        }

    }
}
