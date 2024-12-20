﻿// using HomeForPets.Application.Files;
// using HomeForPets.Core.Messaging;
// using HomeForPets.Volunteers.Application;
// using Microsoft.Extensions.Logging;
//
// namespace HomeForPets.Infrastructure.Files;
//
// public class FilesCleanerService : IFilesCleanerService
// {
//     private readonly IFileProvider _fileProvider;
//     private readonly ILogger<FilesCleanerService> _logger;
//     private readonly IMessageQueue<IEnumerable<FileInfoCommnad>> _messageQueue;
//
//     public FilesCleanerService(
//         IFileProvider fileProvider,
//         ILogger<FilesCleanerService> logger,
//         IMessageQueue<IEnumerable<FileInfoCommnad>> messageQueue)
//     {
//         _fileProvider = fileProvider;
//         _logger = logger;
//         _messageQueue = messageQueue;
//     }
//
//     public async Task Process(CancellationToken cancellationToken)
//     {
//         try
//         {
//             var filesInfos = await _messageQueue.ReadAsync(cancellationToken);
//
//             foreach (var fileInfoCommnad in filesInfos)
//             {
//                 await _fileProvider.RemoveFile(fileInfoCommnad, cancellationToken);
//             }
//             _logger.LogInformation("Files cleaned successfully");
//         }
//         catch (Exception ex)
//         {
//             _logger.LogError(ex.Message);
//         }
//     }
// }