using HomeForPets.Core.FilesDto;
using Microsoft.AspNetCore.Http;

namespace HomeForPets.Core.Processor;

public class FormFileProcessor : IAsyncDisposable
{
    private readonly List<UploadFileDto> _filesDto = [];

    public List<UploadFileDto> Process(IFormFileCollection files)
    {
        foreach (var file in files)
        {
            var stream = file.OpenReadStream();
            var fileDto = new UploadFileDto(stream, file.FileName);
            _filesDto.Add(fileDto);
        }

        return _filesDto;
    }

    public async ValueTask DisposeAsync()
    {
        foreach (var file in _filesDto)
        {
            await file.Content.DisposeAsync();
        }
    }
}