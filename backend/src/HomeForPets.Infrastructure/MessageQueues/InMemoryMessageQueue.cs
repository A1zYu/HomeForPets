﻿using System.Threading.Channels;
using HomeForPets.Application.Messaging;
using FileInfo = HomeForPets.Application.Files.FileInfo;

namespace HomeForPets.Infrastructure.MessageQueues;

public class InMemoryMessageQueue<TMessage> : IMessageQueue<TMessage>
{
    private readonly Channel<TMessage> _channel= Channel.CreateUnbounded<TMessage>();

    public async Task WriteAsync(TMessage message, CancellationToken cancellationToken = default)
    {
        await _channel.Writer.WriteAsync(message,cancellationToken);
    }

    public async Task<TMessage> ReadAsync(CancellationToken cancellationToken = default)
    {
       return await _channel.Reader.ReadAsync(cancellationToken);
    }
}