﻿using ApplicationCore.Interfaces;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Web.Services
{
    public class BasketService : IBasketService
    {
        private readonly CatalogContext _context;

        public BasketService(CatalogContext context)
        {
            _context = context;
        }
        public async Task<Basket> GetBasket(string basketId)
        {
            var basket = await _context.Baskets
                .Include(b => b.Items)
                .FirstOrDefaultAsync(b => b.Id == basketId);
            if (basket == null)
            {
                basket = new Basket();
                _context.Baskets.Add(basket);
                await _context.SaveChangesAsync();
            }
            return basket;
        }

        public Task<Basket> CreateBasket()
        {
            return CreateBasketForUser(null);
        }

        public async Task<Basket> CreateBasketForUser(string userId)
        {
            var basket = new Basket();
            _context.Baskets.Add(basket);
            await _context.SaveChangesAsync();

            return basket;
        }


        public async Task UpdateBasket(Basket basket)
        {
            // only need to save changes here
            await _context.SaveChangesAsync();
        }

    }
}
