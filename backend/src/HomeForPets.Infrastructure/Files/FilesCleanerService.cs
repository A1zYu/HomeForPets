using HomeForPets.Application.Files;
using HomeForPets.Application.Messaging;
using Microsoft.Extensions.Logging;
using FileInfo = HomeForPets.Application.Files.FileInfo;

namespace HomeForPets.Infrastructure.Files;

public class FilesCleanerService : IFilesCleanerService
{
    private readonly IFileProvider _fileProvider;
    private readonly ILogger<FilesCleanerService> _logger;
    private readonly IMessageQueue<IEnumerable<FileInfo>> _messageQueue;

    public FilesCleanerService(
        IFileProvider fileProvider,
        ILogger<FilesCleanerService> logger,
        IMessageQueue<IEnumerable<FileInfo>> messageQueue)
    {
        _fileProvider = fileProvider;
        _logger = logger;
        _messageQueue = messageQueue;
    }

    public async Task Process(CancellationToken cancellationToken)
    {
        try
        {
            var filesInfos = await _messageQueue.ReadAsync(cancellationToken);

            foreach (var fileInfo in filesInfos)
            {
                await _fileProvider.RemoveFile(fileInfo, cancellationToken);
            }
            _logger.LogInformation("Files cleaned successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }
}