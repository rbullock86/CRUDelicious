using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Crudelicious.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Crudelicious.Controllers
{
    public class CrudeliciousController : Controller
    {
        private CrudeliciousContext dbContext;

        public CrudeliciousController(CrudeliciousContext context)
        {
            dbContext= context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            Console.WriteLine("Hitting Index");
            List<Dish> AllDishes = dbContext.Dishes.OrderByDescending(Dish => Dish.CreatedAt).ToList();
            return View("Index", AllDishes);
        }

        [HttpGet("new")]
        public IActionResult New()
        {
            Console.WriteLine("Hitting New");
            return View("New");
        }

        [HttpGet("/{id}")]
        public IActionResult Show(int id){
            Console.WriteLine($"Hitting Show for {id}");
            var dish = dbContext.Dishes.Find(id);
            Console.WriteLine($"Found dish {dish.Name} in Show");
            return View("Show", dish);
        }

        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id){
            Console.WriteLine($"Hitting Edit For {id}");
            var dish = dbContext.Dishes.Find(id);
            return View("Edit", dish);
        }

        [HttpPost("create")]
        public IActionResult Create(Dish NewDish)
        {
            Console.WriteLine("Hitting Create");
            if(ModelState.IsValid)
            {
                Console.WriteLine("New Dish is Valid");
                dbContext.Add(NewDish);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                Console.WriteLine("New Dish had errors");
                return View("New");
            }
        }

        [HttpPost("edit/update/{id}")]
        public IActionResult Update(Dish EditDish, int id)
        {
            Dish DishToUpdate = dbContext.Dishes.SingleOrDefault(Dish => Dish.Id == id);
            DishToUpdate.Name = EditDish.Name;
            DishToUpdate.Chef = EditDish.Chef;
            DishToUpdate.Description = EditDish.Description;
            DishToUpdate.Calories = EditDish.Calories;
            DishToUpdate.Tastiness = EditDish.Tastiness;
            DishToUpdate.UpdatedAt = DateTime.Now;
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet("delete/{id}")]
        public IActionResult Delete(int id){
            Console.WriteLine($"Hitting Delete for {id}");
            Dish DishToDelete = dbContext.Dishes.SingleOrDefault(dish => dish.Id == id);
            dbContext.Dishes.Remove(DishToDelete);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}