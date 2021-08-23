using crudPITE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace crudPITE.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStudentService _studentService;

        public HomeController(ILogger<HomeController> logger, IStudentService studentService)
        {
            _studentService = studentService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        // GET api/values
        [HttpGet]
        public IEnumerable<Student> Get()
        {
            return _studentService.GetAll();
        }


        [HttpGet("{id}")]
        public Student Get(int id)
        {
            return _studentService.Get(id);
        }


        [HttpPost]
        public Response Post([FromForm] Student model)
        {
            var rp = new Response();
            if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.LastName) || string.IsNullOrEmpty(model.Email))
            {
                rp.response = false; ;
                rp.message = "Registro no agregado. Debe llenar todos los campos.";
                return rp;
            }
            rp.response = _studentService.Add(model);
            rp.message = rp.response ? "ok" : "error";
            return rp;

        }


        [HttpPut]
        public Response Put([FromForm] Student model)
        {
            var rp = new Response();
            if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.LastName) || string.IsNullOrEmpty(model.Email))
            {
                rp.response = false; ;
                rp.message = "Registro no agregado. Debe llenar todos los campos.";
                return rp;
            }
            rp.response = _studentService.Update(model);
            rp.message = rp.response ? "ok" : "error";
            return rp

            ;
        }


        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return
                _studentService.Delete(id)
            ;
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
    public class Response
    {
        public bool response { get; set; }
        public string message { get; set; }
    }
}
