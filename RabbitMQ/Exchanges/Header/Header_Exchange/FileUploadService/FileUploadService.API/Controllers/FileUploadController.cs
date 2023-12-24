using AutoMapper;
using Contracts;
using FileUploadService.API.Dtos;
using FileUploadService.API.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace FileUploadService.API.Controllers;

[ApiController]
[Route("[controller]")]
public class FileUploadController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _endpoint;
    private readonly ILogger<FileUploadController> _logger;

    public FileUploadController(ILogger<FileUploadController> logger, IPublishEndpoint endpoint, IMapper mapper)
    {
        _logger = logger;
        _endpoint = endpoint;
        _mapper = mapper;
    }

    [HttpPost("UploadPdfFile")]
    public async Task<IActionResult> UploadPdfFile([FromBody] FileModel file)
    {
        var message = _mapper.Map<FileDto>(file);
        await _endpoint.Publish<IPdfFile>(
            message, 
            context =>
            {
                context.Headers.Set("format", "pdf");
                context.Headers.Set("type", "report");
            });;
        return Ok();
    }
    
    [HttpPost("UploadZipFile")]
    public async Task<IActionResult> UploadZipFile([FromBody] FileModel file)
    {
        var message = _mapper.Map<FileDto>(file);
        await _endpoint.Publish<IZipFile>(
            message, 
            context =>
            {
                context.Headers.Set("format", "zip");
                context.Headers.Set("type", "log");
            });
        return Ok();
    }
}