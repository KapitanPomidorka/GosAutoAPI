using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Infrastructure.Data;
using Domain.Contracts.Requests.Vehicle;
using GosAutoAPI.Services;
using Domain.Contracts.Responses.Vehicle;
using GosAutoAPI.IServices;
using Domain.Models;

namespace GosAutoAPI.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly IDriverService _driverService;

        public VehiclesController(IVehicleService vehicleService, IDriverService driverService)
        {
            _vehicleService = vehicleService;
            _driverService = driverService;
        }

        // GET: Vehicle
        public IActionResult Index()
        {
            var response = _vehicleService.GetAllVehicles().ToList();
            return View(response);
        }

        // GET: Vehicle/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
           var response = await _vehicleService.GetVehicleById(id);
            if(response == null)
            {
                return View("EntityNotFound");
            }
            return View(response);
        }

        // GET: Vehicle/Create
        public IActionResult Create()
        {
            var drivers = _driverService.GetAllDrivers();
            ViewBag.DriverId = new SelectList(drivers, "Id", "Name");
            return View();
        }

        // POST: Vehicle/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Model,CarNumbers,Description,DriverId")] VehicleRequestCreate request)
        {
            await _vehicleService.CreateVehicle(request);
            return RedirectToAction(nameof(Index));
        }

        // GET: Vehicle/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var response = await _vehicleService.GetVehicleById(id);
            if(response == null)
            {
                return View("EntityNotFound");
            }
            VehicleRequestUpdate request = new(
                    response.Id,
                    response.Model,
                    response.CarNumbers,
                    response.Description,
                    response.DriverId);
            var drivers = _driverService.GetAllDrivers();
            ViewBag.DriverId = new SelectList(drivers, "Id", "Name", response.DriverId);
            return View(request);
        }

        // POST: Vehicle/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Model,CarNumbers,Description,DriverId")] VehicleRequestUpdate request)
        {
            await _vehicleService.UpdateVehicle(request);
            return RedirectToAction(nameof(Index));
        }

        // GET: Vehicle/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var vehicle = await _vehicleService.GetVehicleById(id);
            if(vehicle == null)
            {
                return View("EntityNotFound");
            }
            VehicleRequestDelete request = new(
                    vehicle.Id);
            return View(request);

        }

        // POST: Vehicle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(VehicleRequestDelete request)
        {
            await _vehicleService.DeleteVehicle(request);
            return RedirectToAction(nameof(Index));
        }

    }
}
