using AutoMapper;
using FileUploadService.API.Dtos;
using FileUploadService.API.Models;

namespace FileUploadService.API.Mappings;

public class FileMappings : Profile
{
    public FileMappings()
    {
        CreateFileMapping();
    }

    private void CreateFileMapping()
    {
        CreateMap<FileModel, FileDto>()
            .ForMember(
                d => d.Description,
                mo => mo.MapFrom(s => s.Description)
            );
    }
}