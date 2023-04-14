using GuanajuatoAdminUsuarios.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GuanajuatoAdminUsuarios.Controllers
{
    public class TransitoTransporteController : Controller
    {
        private readonly ITransitoTransporteService _transitoTransporteService;


        public TransitoTransporteController(ITransitoTransporteService transitoTransporteService)
        {
            _transitoTransporteService = transitoTransporteService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
