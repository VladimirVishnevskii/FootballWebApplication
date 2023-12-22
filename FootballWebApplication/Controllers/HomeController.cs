using FootballWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FootballWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationContext db;

        public HomeController()
        {
            db = new ApplicationContext();
        }

        public IActionResult Index()
        {
            return View();
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

        public IActionResult Title()
        {
            var players = db.Players.ToList();
            return View(players);
        }

        [HttpGet]
        public IActionResult CreateTeam()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateTeam(Team team)
        {
            if (ModelState.IsValid)
            {
                db.Teams.Add(team);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            return View(team);
        }


        [HttpGet]
        public IActionResult Create()
        {
            // Получение списка команд для выпадающего списка
            ViewBag.Teams = db.Teams.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Player player)
        {
            if (ModelState.IsValid)
            {
                // Добавление футболиста
                db.Players.Add(player);
                db.SaveChanges();
                return RedirectToAction("Title");
            }

            //ViewBag.Teams = db.Teams.ToList();
            return View(player);
        }
        public IActionResult Delete(int Id)
        {
            Player? player = db.Players.FirstOrDefault(x => x.Id == Id);
            if (player != null)
            {
                // Удаление футболиста
                db.Players.Remove(player);
                db.SaveChanges();
                return RedirectToAction("Title");
            }
            return NotFound();
            }       
        

        public IActionResult Edit(int Id)
        {
            ViewBag.Teams = db.Teams.ToList();
            Player? player = db.Players.FirstOrDefault(x => x.Id == Id);
            if (player != null)
            {
                return View(player)
;
            }
            return View();

            
        }
        [HttpPost]
        public IActionResult Edit(Player player)
        {
            if (player != null)
            {
                // Редактирование футболиста
                db.Players.Update(player);
                db.SaveChanges();
                return RedirectToAction("Title");
            }
            return View(player);

        }

        // Отображение списка команд (вторая страница)
        public IActionResult TeamList()
        {
            var teams = db.Teams.ToList();
            return View(teams);
        }
        public IActionResult DeleteTeam(int Id)
        {
            Team? team = db.Teams.FirstOrDefault(x => x.Id == Id);
            if (team != null)
            {
                // Удаление команды
                db.Teams.Remove(team);
                db.SaveChanges();
                return RedirectToAction("TeamList");
            }
            return NotFound();
        }
    }
}
