using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebMotors.Models;
using WebMotors.Teste.Domains;
using WebMotors.Teste.Entities;
using WebMotors.Teste.Entities.Exceptions;
using WebMotors.Teste.Services;

namespace WebMotors.Controllers
{
    public class HomeController : Controller
    {
        private readonly CarsDomain _domain;
        private readonly CarsService _service;

        public HomeController([FromServices]CarsDomain domain, [FromServices]CarsService service)
        {
            _domain = domain;
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ActionName("Include")]
        public async Task<IActionResult> IncludeAsync(Car car)
        {
            if (car == null)
                car = new Car();

            try
            {
                var makes = default(IEnumerable<Make>);
                using (_service)
                {
                    makes = await _service.GetMakesAsync();
                }

                car.Makes = makes;

                return View(car);
            }
            catch(CarException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [ActionName("Save")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveAsync(Car car)
        {
            try
            {
                if (car.ID == 0)
                {
                    var newCar = await _domain.SaveAsync(car);
                }
                else if(car.ID > 0)
                {
                    var updatedCar = await _domain.UpdateAsync(car);
                }

                return RedirectToAction("Listing");
            }
            catch (CarException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [ActionName("GetModels")]
        public async Task<IActionResult> GetModelsAsync(int id)
        {
            try
            {
                if (id == 0)
                {
                    return StatusCode(200, new List<Model>());
                }

                using (_service)
                {
                    var models = await _service.GetModelsAsync(id);
                    return StatusCode(200, models);
                }
            }
            catch(CarException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [ActionName("Listing")]
        public async Task<IActionResult> ListingAsync()
        {
            try
            {
                var cars = await _domain.GetAsync();

                return View(cars);
            }
            catch (CarException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [ActionName("Edit")]
        public async Task<IActionResult> EditAsync(int id)
        {
            try
            {
                var car = await _domain.GetByIdAsync(id);

                return RedirectToAction("Include", car);
            }
            catch (CarException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [ActionName("_Delete")]
        public async Task<IActionResult> _DeleteAsync(int id)
        {
            try
            {
                var car = await _domain.GetByIdAsync(id);

                return PartialView("_Delete", car ?? new Car());
            }
            catch (CarException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [ActionName("Delete")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _domain.DeleteAsync(id);

                return RedirectToAction("Listing");
            }
            catch (CarException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet]
        [ActionName("ListingWm")]
        public async Task<IActionResult> ListingWmAsync()
        {
            try
            {
                IList<Vehicle> vehicles = new List<Vehicle>();

                using (_service)
                {
                    for (int i = 1; i < 4; i++) //3 Páginas disponibilizadas pela WebMotors conforme http://desafioonline.webmotors.com.br.
                    {
                        var result = await _service.GetVehicliesAsync(i);
                        vehicles = vehicles.Concat(result).ToList();
                    }
                }

                return View(vehicles);
            }
            catch (CarException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return RedirectToAction("Listing");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
