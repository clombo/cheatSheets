using Contracts;

namespace FileUploadService.API.Dtos;

public class FileDto : IZipFile, IPdfFile
{
    public string Description { get; set; }
}