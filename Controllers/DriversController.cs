using Microsoft.AspNetCore.Mvc;
using GosAutoAPI.IServices;
using Domain.Contracts.Requests.Driver;
using Domain.Contracts.Responses.Driver;

namespace GosAutoAPI.Controllers
{
    public class DriversController : Controller
    {
        private readonly IDriverService _driverService;

        public DriversController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        // GET: Drivers
        public IActionResult Index()                                         //response - передаем имя, документы, дату рождения,
                                                                             //описание(?), штрафы(?), кол-во штрафов(?)
        {
            var response = _driverService.GetAllDrivers().ToList();
            return View(response);
        }

        // GET: Drivers/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var response = await _driverService.GetDriverById(id);
            if(response == null)
            {
                return View("EntityNotFound");
            }
            return View(response);
        }

        // GET: Drivers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Drivers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,NumberDocuments,Description,Forfeit,CountForfeit")] DriverRequestCreate request)
        {
            await _driverService.CreateDriver(request);
            return RedirectToAction(nameof(Index));
        }

        // GET: Drivers/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var driver = await _driverService.GetDriverById(id);
            if (driver == null)
            {
                return View("EntityNotFound");
            }
            DriverRequestUpdate request = new(
                    driver.Id,
                    driver.Name,
                    driver.NumberDocuments,
                    driver.Description,
                    driver.Forfeit,
                    driver.CountForfeit
                    );
            return View(request);
        }

        // POST: Drivers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Name,NumberDocuments,Description,Forfeit,CountForfeit")] DriverRequestUpdate request)
        {
            await _driverService.UpdateDriver(request);
            return RedirectToAction(nameof(Index));
        }

        // GET: Drivers/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _driverService.GetDriverById(id);
            if (response == null)
            {
                return View("EntityNotFound");
            }
            DriverRequestDelete request = new(
                    response.Id);

            return View(request);
        }

        // POST: Drivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(DriverRequestDelete request)
        {
            await _driverService.DeleteDriver(request);
            return RedirectToAction(nameof(Index));
        }


    }
}
