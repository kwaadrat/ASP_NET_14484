using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging; // Dodaj, jeśli chcesz logować operacje
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class SaleController : Controller
    {
        private static readonly List<ProductViewModel> products = new List<ProductViewModel>
        {
            new ProductViewModel { Id = 1, Name = "Koszulka Oversize", Price = 999.99m, Size = "L", ImageUrl = "koszulkan4d2.jpg" },
            new ProductViewModel { Id = 2, Name = "Bluza z Kapturem", Price = 199.99m, Size = "M", ImageUrl = "NEURONWSZYWKA1.png" },
            new ProductViewModel { Id = 3, Name = "Jeansy Slim Fit", Price = 249.99m, Size = "XL", ImageUrl = "koszulkan4d2.jpg" }
        };

        private readonly ILogger<SaleController> _logger;

        // Konstruktor do logowania operacji
        public SaleController(ILogger<SaleController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string search, string sortBy)
        {
            _logger.LogInformation("Wejście do Sale/Index - Search: {search}, SortBy: {sortBy}", search, sortBy);

            var filteredProducts = products;

            // Wyszukiwanie
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower(); // Zapobiega problemom z wielkością liter
                filteredProducts = filteredProducts
                    .Where(p => p.Name.ToLower().Contains(search))
                    .ToList();

                _logger.LogInformation("Filtrowanie po nazwie: {search}", search);
            }

            // Sortowanie
            filteredProducts = sortBy switch
            {
                "price-asc" => filteredProducts.OrderBy(p => p.Price).ToList(),
                "price-desc" => filteredProducts.OrderByDescending(p => p.Price).ToList(),
                "size" => filteredProducts.OrderBy(p => p.Size).ToList(),
                _ => filteredProducts.OrderBy(p => p.Name).ToList() // Domyślnie sortowanie po nazwie
            };

            _logger.LogInformation("Sortowanie według: {sortBy}", sortBy);

            // Przekazanie danych do widoku
            ViewData["SearchTerm"] = search;
            return View(filteredProducts);
        }
    }
}
