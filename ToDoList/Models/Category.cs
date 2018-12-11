using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;


namespace ToDoList.Models
{
    public class Category
    {
        private string _name;
        private int _id;

        public Category(string categoryName, int id = 0)
        {
            _name = categoryName;
            _id = id;
        }

        public string GetName()
        {
          return _name;
        }

        public int GetId()
        {
          return _id;
        }

        public static List<Category> GetAll() //READ
          {
            List<Category> allCategories = new List<Category> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM categories;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
              int CategoryId = rdr.GetInt32(0);
              string CategoryName = rdr.GetString(1);
              Category newCategory = new Category(CategoryName, CategoryId);
              allCategories.Add(newCategory);
            }
            conn.Close();
            if (conn != null)
            {
              conn.Dispose();
            }
            return allCategories;
          }

        public static Category Find(int searchId) //READ
        {
          MySqlConnection conn = DB.Connection();
          conn.Open();
          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"SELECT * FROM categories WHERE id = (@searchId);";
          cmd.Parameters.AddWithValue("@searchId",searchId);

          MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

          int CategoryId = 0;
          string CategoryName = "";

          while(rdr.Read())
          {
            CategoryId = rdr.GetInt32(0);
            CategoryName = rdr.GetString(1);
          }
          Category newCategory = new Category(CategoryName, CategoryId);
          conn.Close();
          if (conn != null)
          {
            conn.Dispose();
          }
          return newCategory;
        }

        public List<Item> GetItems() //READ
        {
          MySqlConnection conn = DB.Connection();
          conn.Open();
          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText= @"SELECT items.* FROM categories
            JOIN categories_items ON (categories.id = categories_items.category_id)
            JOIN items ON (categories_items.item_id = items.id)
            WHERE categories.id = @CategoryId;";
          cmd.Parameters.AddWithValue("@CategoryId", _id);
          var rdr = cmd.ExecuteReader() as MySqlDataReader;
          List<Item> items = new List<Item>{};
          while (rdr.Read())
          {
              int itemId = rdr.GetInt32(0);
              string itemDescription = rdr.GetString(1);
              Item newItem = new Item(itemDescription, itemId);
              items.Add(newItem);
          }
          conn.Close();
          if (conn != null)
          {
            conn.Dispose();
          }
          return items;
        }

        public static void ClearAll() //DELETE
        {
          MySqlConnection conn = DB.Connection();
          conn.Open();
          MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"DELETE FROM categories;";
          cmd.ExecuteNonQuery();
          conn.Close();
          if (conn !=null)
          {
            conn.Dispose();
          }
        }

        public void Save() //CREATE
        {
          MySqlConnection conn = DB.Connection();
          conn.Open();
          MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"INSERT INTO categories (name) VALUES (@name);";
          cmd.Parameters.AddWithValue("@name", this._name);
          cmd.ExecuteNonQuery();
          _id = (int) cmd.LastInsertedId;
          conn.Close();
          if (conn != null)
          {
            conn.Dispose();
          }
        }

        public override bool Equals(System.Object otherCategory)
        {
          if (!(otherCategory is Category))
          {
            return false;
          }
          else
          {
            Category newCategory = (Category) otherCategory;
            bool idEquality = this.GetId().Equals(newCategory.GetId());
            bool nameEquality = this.GetName().Equals(newCategory.GetName());
            return (nameEquality && idEquality);
          }
        }

        public void Delete()
        {
          MySqlConnection conn = DB.Connection();
          conn.Open();
          MySqlCommand cmd = new MySqlCommand(@"DELETE FROM categories WHERE id = @CategoryId; DELETE FROM categories_items WHERE category_id = @CategoryId;", conn);
          cmd.Parameters.AddWithValue("@CategoryId", this.GetId());
          cmd.ExecuteNonQuery();
          conn.Close();
          if (conn != null)
          {
            conn.Dispose();
          }
        }

        public void AddItem(Item newItem)
        {
          MySqlConnection conn = DB.Connection();
          conn.Open();
          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"INSERT INTO categories_items (category_id, item_id) VALUES (@CategoryId, @ItemId);";
          cmd.Parameters.AddWithValue("@CategoryId", _id);
          cmd.Parameters.AddWithValue("@ItemId", newItem.GetId());
          cmd.ExecuteNonQuery();
          conn.Close();
          if (conn != null)
          {
            conn.Dispose();
          }
        }

    }
}
