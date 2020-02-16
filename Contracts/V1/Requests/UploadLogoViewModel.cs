using Microsoft.AspNetCore.Http;

namespace Acclaim.Api.Contracts.V1.Requests
{
    public class UploadedLogoViewModel
    {
        public IFormFile UploadedFile { get; set; }

    }
}
