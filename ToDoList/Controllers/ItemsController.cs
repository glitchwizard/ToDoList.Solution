using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class ItemsController : Controller
    {

        [HttpGet("/items/new")]
        public ActionResult New()
        {
          return View();
        }

        [HttpPost("/items/delete")]
        public ActionResult DeleteAll()
        {
            Item.ClearAll();
            return View();
        }

        [HttpGet("/items/{id}")]
        public ActionResult Show(int id)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Item selectedItem = Item.Find(id);
            List<Category> itemCategories = selectedItem.GetCategories();
            List<Category> allCategories = Category.GetAll();
            model.Add("selectedItem", selectedItem);
            model.Add("itemCategories", itemCategories);
            model.Add("allCategories", allCategories);
            return View("Show", model);
      }

      [HttpGet("items/{itemId}/edit")]
      public ActionResult Edit(int itemId)
      {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Item item = Item.Find(itemId);
        model.Add("item", item);
        return View(model);
      }

      [HttpPost("/items/{itemId}")]
      public ActionResult Update(int itemId, string newDescription)
      {
        Item item = Item.Find(itemId);
        item.Edit(newDescription);
        return RedirectToAction("Show", new {id = itemId});
      }
      [HttpGet("/items")]
      public ActionResult Index()
      {
        List<Item> allItems = Item.GetAll();
        return View(allItems);
      }

      [HttpPost("/items")]
      public ActionResult Create (string itemDescription)
      {
        Item newItem = new Item(itemDescription);
        newItem.Save();
        // List<Item> allItems = Item.GetAll();
        return RedirectToAction("Index");
      }

      [HttpPost("/items/{itemId}/categories/new")]
      public ActionResult AddCategory(int itemId, int categoryId)
      {
        Item item = Item.Find(itemId);
        Category category = Category.Find(categoryId);
        item.AddCategory(category);
        return RedirectToAction("Show", new {id = itemId});
      }
    }
}
