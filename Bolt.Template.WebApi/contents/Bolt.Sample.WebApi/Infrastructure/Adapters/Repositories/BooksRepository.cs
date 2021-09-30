using System.Collections.Generic;
using System.Threading.Tasks;
using Bolt.Common.Extensions;
using Bolt.IocScanner.Attributes;
using Bolt.Sample.WebApi.Features.Shared.Ports.Repositories;

namespace Bolt.Sample.WebApi.Infrastructure.Adapters.Repositories
{
    [AutoBind]
    internal sealed class BooksRepository : IBooksRepository
    {
        public async Task<IReadOnlyCollection<BookRecord>> GetAll()
        {
            return new[]
            {
                new BookRecord
                {
                    Id = "1001",
                    Title = "__app_name__"
                }
            };
        }
    }
}
