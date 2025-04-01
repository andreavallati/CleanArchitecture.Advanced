using CleanArchitecture.Advanced.Api.Application.Interfaces.Caching;
using CleanArchitecture.Advanced.Api.Application.Interfaces.Repositories;
using CleanArchitecture.Advanced.Api.Domain.Entities;
using CleanArchitecture.Advanced.Api.Infrastructure.Context;
using CleanArchitecture.Advanced.Api.Infrastructure.Repositories.Base;

namespace CleanArchitecture.Advanced.Api.Infrastructure.Repositories
{
    public class EventLogRepository : RepositoryBase<EventLog>, IEventLogRepository
    {
        public EventLogRepository(LibraryContext context, ICustomMemoryCache cache) : base(context, cache)
        {

        }

        // Eventually implement specific methods for EventLogRepository
    }
}
