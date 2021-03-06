﻿using System;
using System.Collections.Generic;
using System.Text;
using Taste.DataAccess.Data.Repository.IRepository;

namespace Taste.DataAccess.Data.Repository
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            FoodType = new FoodTypeRepository(_db);
            MenuItem = new MenuItemRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);

            ShoppingCart = new ShoppingCartRepository(_db);

            OrderHeader = new OrderHeaderRepository(_db);
            OrderDetails = new OrderDetailsRepository(_db);

            SP_Call = new SP_Call(_db);
        }

        public ICategoryRepository Category { get; private set; }

        public IFoodTypeRepository FoodType { get; set; }

        public IMenuItemRepository MenuItem { get; set; }

        public IApplicationUserRepository ApplicationUser { get; set; }

        public IShoppingCartRepository ShoppingCart { get; set; }

        public IOrderHeaderRepository OrderHeader { get; set; }

        public IOrderDetailsRepository OrderDetails { get; set; }

        public ISP_Call SP_Call { get; set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
