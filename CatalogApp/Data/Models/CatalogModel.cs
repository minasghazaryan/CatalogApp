namespace CatalogApp.Data.Models
{
    public class CatalogModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Coast { get; set; }
        public string Image { get; set; }
        public bool Deleted { get; set; }
    }
}
