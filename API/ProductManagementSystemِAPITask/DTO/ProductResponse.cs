namespace ProductManagementSystemِAPITask.DTO
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
      
        public string? URLImageProduct { get; set; }
         public string? StatusCode { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
