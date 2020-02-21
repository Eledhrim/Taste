using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taste.DataAccess.Data.Repository.IRepository;
using Taste.Models;

namespace Taste.DataAccess.Data.Repository
{
    public class MenuItemRepository:Repository<MenuItem>, IMenuItemRepository
    {
        private readonly ApplicationDbContext _db;

        public MenuItemRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(MenuItem menuItem)
        {
            var objFromDb = _db.MenuItem.FirstOrDefault(s => s.Id == menuItem.Id);

            objFromDb.Name = menuItem.Name;
            objFromDb.CategoryId = menuItem.CategoryId;
            objFromDb.Description = menuItem.Description;
            objFromDb.FoodTypeId = menuItem.FoodTypeId;
            objFromDb.Price = menuItem.Price;

            if (menuItem.Image != null)
            {
                objFromDb.Image = menuItem.Name;
            }

            _db.SaveChanges();
        }
    }
}
