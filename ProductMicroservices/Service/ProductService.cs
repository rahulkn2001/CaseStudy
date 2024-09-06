using Microsoft.EntityFrameworkCore;
using ProductMicroservices.Models;
using static ProductMicroservices.Controllers.ProductController;

namespace ProductMicroservices.Service
{
    public class ProductService
    {
        private readonly RetailapplicationContext _context;

        public ProductService(RetailapplicationContext context)
        {
            _context = context;
        }

        // Method to get products by category with average ratings
        //    public async Task<IEnumerable<ProductWithAverageRatingDto>> GetProductsByCategoryWithRatingsAsync(string categoryName)
        //    {
        //        var products = await _context.Products
        //     .Where(p => p.Category.CategoryName == categoryName)
        //     .Select(p => new
        //     {
        //         p.ProductId,
        //         p.ProductName,
        //         p.Price,
        //         p.Category.CategoryName
        //     })
        //     .ToListAsync();

        //        var averageRatings = await _context.Reviews
        //            .GroupBy(r => r.ProductId)
        //            .Select(g => new
        //            {
        //                ProductID = g.Key,
        //                AvgRating = g.Average(r => r.Rating)
        //            })
        //            .ToListAsync();

        //        // Join products and average ratings in memory
        //        var result = products
        //            .Select(p => new ProductWithAverageRatingDto
        //            {
        //                ProductId = p.ProductId,
        //                ProductName = p.ProductName,
        //                Price = p.Price,
        //                CategoryName = p.CategoryName,
        //                AvgRating = (double)averageRatings
        //                    .Where(ar => ar.ProductID == p.ProductId) // Compare using client-side evaluation
        //                    .Select(ar => ar.AvgRating)
        //                    .FirstOrDefault() // Default to 0 if no rating exists
        //            });

        //        return result;
        //    }

        //}
        //public class ProductWithAverageRatingDto
        //{
        //    public int ProductId { get; set; }
        //    public string ProductName { get; set; }
        //    public decimal Price { get; set; }
        //    public string CategoryName { get; set; }
        //    public double AvgRating { get; set; } // Assuming the rating is a double or float
        //}

    }
}
