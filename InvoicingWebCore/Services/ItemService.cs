﻿using InvoicingWebCore.Data;
using InvoicingWebCore.Interfaces;
using InvoicingWebCore.Models;

namespace InvoicingWebCore.Services
{
    public class ItemService : IItemService
    {
        private readonly ApplicationDbContext _db;
        public ItemService(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Product> GetAll(ApplicationUser loggedUser)
        {
            return _db.Products.Where(x => x.Company == loggedUser.Company).ToList();
        }
    }
}
