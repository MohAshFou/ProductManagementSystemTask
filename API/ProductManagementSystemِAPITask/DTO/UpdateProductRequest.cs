namespace ProductManagementSystemِAPITask.DTO
{
    public class UpdateProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public IFormFile? NewImageProduct { get; set; } 
    }
}
