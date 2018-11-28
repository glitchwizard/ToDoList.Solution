using System;
using System.Collections.Generic;


namespace ToDoList.Models
{
    public class Category
    {
        private static List<Category> _instances = new List<Category> { };
        private string _name;
        private int _id;
        private List<Item> _items;

        public Category(string categoryName)
        {
            _name = categoryName;
            _instances.Add(this);
            _id = _instances.Count;
            _items = new List<Item> { };
        }
    }
}
