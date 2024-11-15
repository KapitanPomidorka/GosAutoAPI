using Domain.Contracts.Requests.Fine;
using Domain.Contracts.Responses.Fine;
using Domain.Models;
using GosAutoAPI.IServices;
using Infrastructure.Data;
using Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace GosAutoAPI.Services
{
    public class FineService : IFineService
    {
        private readonly IFinesRepository _finesRepository;
        private readonly IDriverService _driverService;
        private readonly GosAutoDbContext _dbContext;
        public FineService(IFinesRepository fines, IDriverService driverService, GosAutoDbContext dbContext)
        {
            _finesRepository = fines;
            _driverService = driverService;
            _dbContext = dbContext;
        }

        public async Task CreateFine(FineRequestCreate request)
        {
            if (request.Description != null && request.Fines >= 0)
            {
                Fine fine = new(
                    request.Description,
                    request.Fines,
                    request.DriverId,
                    request.IsPaid);

                await _finesRepository.CreateAsync(fine);
                if (!fine.IsPaid)
                {
                    Fine.BondBetween.Add(fine.Id, fine.DriverId);
                    await _driverService.UpdateAfterFineUpdate(fine.DriverId);
                }
            }
        }

        public async Task DeleteFine(FineRequestDelete request)
        {
            Fine? fine = await _finesRepository.GetById(request.Id);
            if (fine != null)
            {
                Fine.BondBetween.Remove(fine.Id);

                await _finesRepository.DeleteAsync(fine);
                await _driverService.UpdateAfterFineUpdate(fine.DriverId);
            }
        }

        public IEnumerable<FineResponseGetAll> GetAllFines()
        {
            IEnumerable<Fine> fines = _finesRepository.GetAll();
            return fines.Select(fine => new FineResponseGetAll
                    (
                        fine.Id,
                        fine.Description,
                        fine.Fines,
                        fine.DriverId,
                        fine.IsPaid,
                        fine.Driver
                    ));
        }

        public async Task<FineResponseGetById?> GetFineById(Guid Id)
        {
            Fine? fine = await _finesRepository.GetById(Id);
            if (fine != null)
            {
                FineResponseGetById response = new(
                    fine.Id,
                    fine.Description,
                    fine.Fines,
                    fine.DriverId,
                    fine.IsPaid,
                    fine.Driver
                    );
                return response;
            }
            return null;
        }

        public async Task UpdateFine(FineRequestUpdate request)
        {
            if (request != null)
            {
                var fine = await _finesRepository.GetById(request.Id);
                fine.Description = request.Description;
                fine.Fines = request.Fines;
                fine.DriverId = request.DriverId;
                fine.IsPaid = request.IsPaid;
                Guid OldId = request.OldDriverId;
                if (!request.IsPaid)
                {

                    if (Fine.BondBetween.ContainsKey(fine.Id))
                    {
                        OldId = Fine.BondBetween[fine.Id];
                        Fine.BondBetween[fine.Id] = fine.DriverId;
                    }
                    else
                    {
                        Fine.BondBetween[fine.Id] = fine.DriverId;
                    }
                }
                else
                {
                    if (Fine.BondBetween.ContainsKey(fine.Id))
                    {
                        Fine.BondBetween.Remove(fine.Id);
                    }
                }
                await _finesRepository.UpdateAsync(fine);
                _dbContext.Database.ExecuteSqlRaw(@"UPDATE ""Fines"" SET ""DriverId"" = {0}::uuid WHERE ""Id"" = {1}::uuid", request.DriverId, request.Id);
                if(OldId != request.OldDriverId)
                {
                    await _driverService.UpdateAfterFineUpdate(OldId);
                }

                await _driverService.UpdateAfterFineUpdate(request.DriverId);

            }
        }
    }
}
