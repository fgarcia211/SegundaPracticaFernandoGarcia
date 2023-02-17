using Microsoft.AspNetCore.Mvc;
using SegundaPracticaFernandoGarcia.Models;
using SegundaPracticaFernandoGarcia.Repositories;

namespace SegundaPracticaFernandoGarcia.Controllers
{
    public class ComicsController : Controller
    {
        private IRepositoryComics repo;

        public ComicsController(IRepositoryComics repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            return View(this.repo.GetComics());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string nombre, string imagen, string descripcion)
        {
            this.repo.InsertComic(nombre, imagen, descripcion);
            return RedirectToAction("Index");
        }
    }
}
