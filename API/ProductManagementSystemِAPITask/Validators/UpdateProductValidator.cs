using FastEndpoints;
using ProductManagementSystemِAPITask.DTO;

using FluentValidation;


namespace ProductManagementSystemِAPITask.Validators
{
   
        public class UpdateProductValidator : Validator<UpdateProductRequest>
        {
            public UpdateProductValidator()
            {
                RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
                RuleFor(x => x.Description).NotEmpty().MaximumLength(500);
                RuleFor(x => x.Price).GreaterThan(0);

                RuleFor(x => x.NewImageProduct)
                    .Must(BeAValidImage).WithMessage("Invalid image format.")
                    .Must(BeWithinSizeLimit).WithMessage("Image size exceeds the limit.");
            }

            private bool BeAValidImage(IFormFile file)
            {
                if (file == null) return true;

                var validExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var fileExtension = Path.GetExtension(file.FileName).ToLower();
                return validExtensions.Contains(fileExtension);
            }

            private bool BeWithinSizeLimit(IFormFile file)
            {
                if (file == null) return true; 

                const long maxSize = 5 * 1024 * 1024; 
                return file.Length <= maxSize;
            }

        }
    }
