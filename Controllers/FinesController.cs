using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using GosAutoAPI.IServices;
using Domain.Contracts.Requests.Fine;

namespace GosAutoAPI.Controllers
{
    public class FinesController : Controller
    {
        private readonly IFineService _fineService;
        private readonly IDriverService _driverService;

        public FinesController(IFineService fineService, IDriverService driverService)
        {
            _fineService = fineService;
            _driverService = driverService;
        }

        // GET: Fines
        public IActionResult Index()
        {
            var response = _fineService.GetAllFines().ToList();
            return View(response);
        }

        // GET: Fines/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var response = await _fineService.GetFineById(id);
            if (response == null)
            {
                return View("EntityNotFound");
            }
            return View(response);
        }

        // GET: Fines/Create
        public IActionResult Create()
        {
            var drivers = _driverService.GetAllDrivers();
            ViewBag.DriverId = new SelectList(drivers, "Id", "Name");
            return View();
        }

        // POST: Fines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Fines,IsPaid,DriverId")] FineRequestCreate request)
        {
            await _fineService.CreateFine(request);
            return RedirectToAction(nameof(Index));
        }

        // GET: Fines/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var response = await _fineService.GetFineById(id);
            if (response == null)
            {
                return View("EntityNotFound");
            }
            var drivers = _driverService.GetAllDrivers();
            ViewBag.DriverId = new SelectList(drivers, "Id", "Name", response.DriverId);
            FineRequestUpdate request = new(
                response.Id,
                response.Description,
                response.Fines,
                response.DriverId,
                response.IsPaid,
                response.DriverId
                );
            return View(request);
        }

        // POST: Fines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Description,Fines,IsPaid,DriverId")] FineRequestUpdate request)
        {
            await _fineService.UpdateFine(request);
            return RedirectToAction(nameof(Index));
        }

        // GET: Fines/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _fineService.GetFineById(id);
            if (response == null)
            {
                return View("EntityNotFound");
            }
            FineRequestDelete request = new(
                    response.Id, response.DriverId);
            return View(request);
        }

        // POST: Fines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(FineRequestDelete request)
        {
            await _fineService.DeleteFine(request);
            return RedirectToAction(nameof(Index));
        }

    }
}
