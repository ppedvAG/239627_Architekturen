﻿using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ppedv.CubesPizza.Model;
using ppedv.CubesPizza.Model.Contracts;

namespace ppedv.CubesPizza.UI.ASP_MVC.Controllers
{
    public class PizzaController : Controller
    {
        private readonly IRepository repo;

        public PizzaController(IRepository repo)
        {
            this.repo = repo;
        }

        // GET: PizzaController
        public ActionResult Index()
        {
            var pizzen = repo.GetAll<Pizza>();
            return View(pizzen);
        }

        // GET: PizzaController/Details/5
        public ActionResult Details(int id)
        {
            return View(repo.GetById<Pizza>(id));
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
                repo.Add<Pizza>(pizza);
                repo.SaveAll();
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
            return View(repo.GetById<Pizza>(id));
        }

        // POST: PizzaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Pizza pizza)
        {
            try
            {
                repo.Update<Pizza>(pizza);
                repo.SaveAll();
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
            return View(repo.GetById<Pizza>(id));
        }

        // POST: PizzaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Pizza pizza)
        {
            try
            {
                repo.Delete<Pizza>(pizza);
                repo.SaveAll();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
