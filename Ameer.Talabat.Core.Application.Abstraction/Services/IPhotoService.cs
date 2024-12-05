using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace Ameer.Talabat.Core.Application.Abstraction.Services
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}
