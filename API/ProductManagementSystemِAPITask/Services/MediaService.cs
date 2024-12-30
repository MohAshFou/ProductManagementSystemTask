using static System.Net.Mime.MediaTypeNames;

namespace ProductManagementSystemِAPITask.Services
{

    public interface IMediaService
    {
        Task<string> SaveImageAsync(IFormFile imageFile, CancellationToken ct);
        Task DeleteImageAsync(string imagePath, CancellationToken ct);
        Task<string> GetImagesFolderPath();

    }
    public class MediaService : IMediaService
    {
        private readonly IWebHostEnvironment _env;
        private const string _nameFolder= "images";

        public MediaService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> SaveImageAsync(IFormFile imageFile, CancellationToken ct)
        {
        
            var allowedContentTypes = new[] { "image/jpeg", "image/png", "image/gif", "image/bmp" };
            if (!allowedContentTypes.Contains(imageFile.ContentType))
            {
                throw new BadHttpRequestException("Invalid file type. Please upload an image file.", StatusCodes.Status415UnsupportedMediaType);
            }


            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(_env.WebRootPath, _nameFolder, fileName);

       
            var directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

          
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream, ct);
            }

          
            return  fileName;
        }

        public async Task<string> GetImagesFolderPath()
        {
            return Path.Combine(_env.WebRootPath, _nameFolder);
        }

        public async Task DeleteImageAsync(string imagePath, CancellationToken ct)
        {
            if (File.Exists(imagePath))
            {
                await Task.Run(() => File.Delete(imagePath), ct);
            }
        }


    }
}
