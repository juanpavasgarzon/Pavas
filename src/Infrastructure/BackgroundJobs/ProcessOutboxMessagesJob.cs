using Application.Abstractions.Data;
using Domain.Outbox;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;
using SharedKernel;

namespace Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
public sealed class ProcessOutboxMessagesJob(
    IApplicationDbContext applicationDbContext,
    ILogger<ProcessOutboxMessagesJob> logger,
    IDateTimeProvider dateTimeProvider,
    IPublisher publisher
) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        CancellationToken cancellationToken = context.CancellationToken;

        List<OutboxMessage> outboxMessages = await applicationDbContext.OutboxMessages
            .Where(message => message.ProcessedOnUtc == null)
            .OrderBy(message => message.OccurredOnUtc)
            .Take(100)
            .ToListAsync(cancellationToken);

        foreach (OutboxMessage message in outboxMessages.TakeWhile(_ => !cancellationToken.IsCancellationRequested))
        {
            await HandleMessage(message, context.CancellationToken);
        }

        await applicationDbContext.SaveChangesAsync(cancellationToken);
    }


    private async Task HandleMessage(OutboxMessage outboxMessage, CancellationToken cancellationToken = default)
    {
        JsonSerializerSettings settings = new() { TypeNameHandling = TypeNameHandling.All };

        try
        {
            IDomainEvent? domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(outboxMessage.Content, settings);
            if (domainEvent is null)
            {
                return;
            }

            await publisher.Publish(domainEvent, cancellationToken);
            outboxMessage.ProcessedOnUtc = dateTimeProvider.UtcNow;
        }
        catch (JsonException jsonEx)
        {
            logger.LogError(jsonEx, "Error processing outbox message.");

            outboxMessage.Error = $"Deserialization failed: {jsonEx.Message}";
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error processing outbox message.");

            outboxMessage.ProcessedOnUtc = dateTimeProvider.UtcNow;
            outboxMessage.Error = $"Error processing message: {ex.Message}";
        }
    }
}
