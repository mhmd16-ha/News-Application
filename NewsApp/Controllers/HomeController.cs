using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewsApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NewsApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly NewsContext _db;
        public HomeController(NewsContext context)
        {
            _db = context;          
        }

 

        public IActionResult Index()
        {
           var result= _db.Categories.ToList();
            return View(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SaveContact(ContentUs model)
        {
            _db.Contents.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Messages()
        {
            var result = _db.Contents.ToList();

            return View(result);
        }
        public IActionResult News(int id)
        {
            Category c = _db.Categories.Find(id);
            ViewBag.cat = c.Name;
            ViewData["Cat"] = c.Name;
            var result = _db.News.Where(x => x.CategoryId == id).OrderByDescending(x=>x.Date).ToList();

            return View(result);
        }
        public IActionResult Delete(int id)
        {
            var item = _db.News.Find(id);
            _db.Remove(item);
            _db.SaveChanges();
            return RedirectToAction("News");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
