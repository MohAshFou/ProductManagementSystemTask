namespace ProductManagementSystemِAPITask.DTO
{
    public class ProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public IFormFile ImageProduct { get; set; }
     
    }
}
