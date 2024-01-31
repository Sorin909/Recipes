using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Recipes_SQL.Models;
using Database;

namespace Recipes_SQL.Controllers
{
    public class RecipesController : Controller
    {
        private readonly Database.Database _database;

        public RecipesController()
        {
            _database = new Database.Database();
        }

        public IActionResult Index()
        {
            var recipes = _database.GetRecipes();
            return View(recipes);
        }

        public IActionResult Details(int id)
        {
            var recipe = _database.GetRecipeById(id);

            if (recipe == null)
            {
                return NotFound(); 
            }

            return View(recipe);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        
        public IActionResult Create([Bind("Name, Summary, Ingredients, Method, ImageUrl")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                recipe.ImageUrl = "/images/" + recipe.ImageUrl.Replace(" ", "") + ".jpg";
                _database.SaveRecipe(recipe.Name, recipe.Summary, recipe.Ingredients, recipe.Method, recipe.ImageUrl);
                return RedirectToAction(nameof(Index));
            }
            return View(recipe);
        }


        public IActionResult Edit(int id)
        {
            Recipe recipe = _database.GetRecipeById(id);
            if (recipe == null)
            {
                return NotFound();
            }
            return View(recipe);
        }

        [HttpPost]
        
        public IActionResult Edit(Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                _database.UpdateRecipe(recipe); 
                return RedirectToAction(nameof(Index));
            }
            return View(recipe);
        }

        public IActionResult Delete(int id)
        {
            Recipe recipe = _database.GetRecipeById(id); 
            if (recipe == null)
            {
                return NotFound();
            }
            return View(recipe);
        }

        [HttpPost]
        
        public IActionResult DeleteConfirmed(int id)
        {
            _database.DeleteRecipe(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
