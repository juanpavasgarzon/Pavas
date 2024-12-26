using Infrastructure.Database;
using Infrastructure.Outbox;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz;
using SharedKernel;

namespace Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
public class ProcessOutboxMessagesJob(
    ApplicationDbContext applicationDbContext,
    IPublisher publisher,
    IDateTimeProvider dateTimeProvider
) : IJob
{
    private readonly JsonSerializerSettings _settings = new() { TypeNameHandling = TypeNameHandling.All };

    public async Task Execute(IJobExecutionContext context)
    {
        List<OutboxMessage> outboxMessages = await applicationDbContext.OutboxMessages
            .Where(message => message.ProcessedOnUtc == null)
            .OrderBy(message => message.OccurredOnUtc)
            .Take(20)
            .ToListAsync();

        foreach (OutboxMessage outboxMessage in outboxMessages)
        {
            IDomainEvent? domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(outboxMessage.Content, _settings);

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
