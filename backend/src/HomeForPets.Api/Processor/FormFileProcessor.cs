using HomeForPets.Application.Dtos;
using HomeForPets.Application.File.Create;
using HomeForPets.Application.Volunteers.AddPet;

namespace HomeForPets.Api.Processor;

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