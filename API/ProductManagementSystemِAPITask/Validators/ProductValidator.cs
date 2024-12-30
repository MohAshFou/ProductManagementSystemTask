using FastEndpoints;
using FluentValidation;
using ProductManagementSystemِAPITask.DTO;

namespace ProductManagementSystemِAPITask.Validators
{
    public class ProductValidator : Validator<ProductRequest>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(500);
            RuleFor(x => x.Price).GreaterThan(0);

           
            RuleFor(x => x.ImageProduct)
                .NotNull().WithMessage("Image is required.")
                .Must(BeAValidImage).WithMessage("Invalid image format.")
                .Must(BeWithinSizeLimit).WithMessage("Image size exceeds the limit.");
        }

     
        private bool BeAValidImage(IFormFile file)
        {
            if (file == null) return false;

          
            var validExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            return validExtensions.Contains(fileExtension);
        }

      
        private bool BeWithinSizeLimit(IFormFile file)
        {
            if (file == null) return false;

            const long maxSize = 5 * 1024 * 1024; 
            return file.Length <= maxSize;
        }
    }
}
