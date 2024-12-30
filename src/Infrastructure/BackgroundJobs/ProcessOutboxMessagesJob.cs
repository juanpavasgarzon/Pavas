using Application.Abstractions.Data;
using Domain.Outbox;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz;
using SharedKernel;

namespace Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
public sealed class ProcessOutboxMessagesJob(
    IApplicationDbContext applicationDbContext,
    IPublisher publisher,
    IDateTimeProvider dateTimeProvider
) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        JsonSerializerSettings settings = new()
        {
            TypeNameHandling = TypeNameHandling.All
        };

        List<OutboxMessage> outboxMessages = await applicationDbContext.OutboxMessages
            .Where(message => message.ProcessedOnUtc == null)
            .OrderBy(message => message.OccurredOnUtc)
            .Take(20)
            .ToListAsync();

        foreach (OutboxMessage outboxMessage in outboxMessages)
        {
            IDomainEvent? domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(outboxMessage.Content, settings);

            if (domainEvent is null)
            {
                continue;
            }

            await publisher.Publish(domainEvent, context.CancellationToken);
            outboxMessage.ProcessedOnUtc = dateTimeProvider.UtcNow;
        }

        await applicationDbContext.SaveChangesAsync();
    }
}
