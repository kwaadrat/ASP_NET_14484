namespace WebApplication1.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Size { get; set; } // S, M, L, XL itd.
        public string ImageUrl { get; set; } // Ścieżka do zdjęcia

        // Ustaw domyślną grafikę, jeśli ImageUrl nie jest podane
        public string GetImagePath()
        {
            return string.IsNullOrEmpty(ImageUrl) ? "/imagine/koszulkan4d2.jpg" : $"/imagine/{ImageUrl}";
        }
    }
}
