using ADOProyect.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using LayerDAL.Repository;
using LayerDAL.Entities;

namespace ADOProyect.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _repository;

        public HomeController(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> ShowEmployees()
        {
            List<Employee> ListEmployees = await _repository.GetEmployees();

            return View(ListEmployees);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}