using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository(ApplicationDbContext context) : IStockRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();

            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            
            if (stockModel == null)
            {
                return null;
            }

            // Implement Delete comments when deleting a stock cascade delete
            var comments = await _context.Comments.Where(x => x.StockId == id).ToListAsync();
            _context.Comments.RemoveRange(comments);

            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();

            return stockModel;
        }

        public async Task<List<Stock>> GetAllAsync(StockQueryObject queryObject)
        {
            var stocks = _context.Stocks.Include(c => c.Comments).AsQueryable();

            if(!string.IsNullOrWhiteSpace(queryObject.Symbol))
            {
                stocks = stocks.Where(x => x.Symbol.Contains(queryObject.Symbol));
            }
            if(!string.IsNullOrWhiteSpace(queryObject.Name))
            {
                stocks = stocks.Where(x => x.Name.Contains(queryObject.Name));
            }
                // Sorting the stocks
            if(!string.IsNullOrWhiteSpace(queryObject.SortBy))
            {
                if (queryObject.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = queryObject.IsDescending ? stocks.OrderByDescending(x => x.Symbol) : stocks.OrderBy(x => x.Symbol);
                }
                else if (queryObject.SortBy == "name")
                {
                    stocks = queryObject.IsDescending ? stocks.OrderByDescending(x => x.Name) : stocks.OrderBy(x => x.Name);
                }
                else if (queryObject.SortBy == "price")
                {
                    stocks = queryObject.IsDescending ? stocks.OrderByDescending(x => x.Price) : stocks.OrderBy(x => x.Price);
                }
                else if (queryObject.SortBy == "lastDiv")
                {
                    stocks = queryObject.IsDescending ? stocks.OrderByDescending(x => x.LastDiv) : stocks.OrderBy(x => x.LastDiv);
                }
                else if (queryObject.SortBy == "industry")
                {
                    stocks = queryObject.IsDescending ? stocks.OrderByDescending(x => x.Industry) : stocks.OrderBy(x => x.Industry);
                }
                else if (queryObject.SortBy == "marketCap")
                {
                    stocks = queryObject.IsDescending ? stocks.OrderByDescending(x => x.MarketCap) : stocks.OrderBy(x => x.MarketCap);
                }
            }

            // Pagination
            stocks = stocks.Skip((queryObject.PageNumber - 1) * queryObject.PageSize).Take(queryObject.PageSize);


            return await stocks.ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> StockExists(int id)
        {
            return await _context.Stocks.AnyAsync(x => x.Id == id);
           
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockDto updateStockDto)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if (stockModel == null)
            {
                return null;
            }

            stockModel.Symbol = updateStockDto.Symbol;
            stockModel.Name = updateStockDto.Name;
            stockModel.Price = updateStockDto.Price;
            stockModel.LastDiv = updateStockDto.LastDiv;
            stockModel.Industry = updateStockDto.Industry;
            stockModel.MarketCap = updateStockDto.MarketCap;
            await _context.SaveChangesAsync();

            return stockModel;
        }
    }
}