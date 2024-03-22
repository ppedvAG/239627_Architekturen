using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ppedv.CubesPizza.Model;
using ppedv.CubesPizza.Model.Contracts;

namespace ppedv.CubesPizza.UI.ASP_MVC.Controllers
{
    public class PizzaController : Controller
    {
        private readonly IUnitOfWork uow;

        public PizzaController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        // GET: PizzaController
        public ActionResult Index()
        {
            var pizzen = uow.FoodRepository.GetAll();
            return View(pizzen);
        }

        // GET: PizzaController/Details/5
        public ActionResult Details(int id)
        {
            return View(uow.FoodRepository.GetById(id));
        }

        // GET: PizzaController/Create
        public ActionResult Create()
        {
            return View(new Pizza() { Name = "NEU" });
        }

        // POST: PizzaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pizza pizza)
        {
            try
            {
                uow.FoodRepository.Add(pizza);
                uow.SaveAll();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PizzaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(uow.FoodRepository.GetById(id));
        }

        // POST: PizzaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Pizza pizza)
        {
            try
            {
                uow.FoodRepository.Update(pizza);
                uow.SaveAll();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PizzaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(uow.FoodRepository.GetById(id));
        }

        // POST: PizzaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Pizza pizza)
        {
            try
            {
                uow.FoodRepository.Delete(pizza);
                uow.SaveAll();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
