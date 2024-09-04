using CommunityToolkit.HighPerformance.Helpers;
using HomeForPets.Api.Extensions;
using HomeForPets.Application.File.Create;
using HomeForPets.Application.File.Delete;
using HomeForPets.Application.File.Get;
using HomeForPets.Infrastructure.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;

namespace HomeForPets.Api.Controllers;

public class FileController : ApplicationController
{
    private readonly IMinioClient _minioOptions;

    public FileController(IMinioClient minioOptions)
    {
        _minioOptions = minioOptions;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        IFormFile file,
        [FromServices] CreateFileHandler handler,
        CancellationToken cancellationToken = default)
    {
        await using var fileStream = file.OpenReadStream();
        var request = new CreateFileDto(fileStream,file.FileName);

        var result = await handler.Handle(request, cancellationToken);
        if (result.IsFailure)
        {
            return result.Error.ToResponse();
        }
        return Ok(result.Value);
    }

    [HttpGet("{fileID}")]
    public async Task<IActionResult> Get(
        [FromRoute] string fileID,
        [FromServices] GetFileHandler handler, 
        CancellationToken cancellationToken)
    {
        var request = new GetFileRequest(fileID);
        var result = await handler.Handle(request, cancellationToken);
        if (result.IsFailure)
        {
            return result.Error.ToResponse();
        }
        return Ok(result.Value);
    } 
    [HttpDelete("{fileID}")]
    public async Task<IActionResult> Delete(
        [FromRoute] string fileID,
        [FromServices] DeleteFileHandler handler, 
        CancellationToken cancellationToken)
    {
        var request = new DeleteFileRequest(fileID);
        var result = await handler.Handle(request, cancellationToken);
        if (result.IsFailure)
        {
            return result.Error.ToResponse();
        }
        return Ok(result.Value);
    }
}