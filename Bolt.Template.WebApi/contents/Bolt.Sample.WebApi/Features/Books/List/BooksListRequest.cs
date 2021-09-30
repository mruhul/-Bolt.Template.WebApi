using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bolt.IocScanner.Attributes;
using Bolt.RequestBus;
using Bolt.Sample.WebApi.Features.Shared.Ports.Repositories;
using Microsoft.AspNetCore.Hosting;

namespace Bolt.Sample.WebApi.Features.Books.List
{
    [AutoBind]
    internal sealed class BooksListRequestHandler 
        : RequestHandlerAsync<BooksListRequest, BooksListViewModel>
    {
        private readonly IBooksRepository booksRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public BooksListRequestHandler(IBooksRepository booksRepository, 
            IWebHostEnvironment webHostEnvironment)
        {
            this.booksRepository = booksRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

        protected override async Task<BooksListViewModel> Handle(IRequestBusContext context, BooksListRequest request)
        {
            var books = await booksRepository.GetAll();

            return new BooksListViewModel
            {
                App = "__app_name__",
                Environment = webHostEnvironment.EnvironmentName,
                Data = books.Select(record => new BookViewModel 
                { 
                    Id = record.Id,
                    Title = record.Title
                }).ToArray()
            };
        }
    }

    public record BooksListRequest
    {
    }

    public record BooksListViewModel
    {
        public string App { get; init; }
        public string Environment { get; init; }
        public IReadOnlyCollection<BookViewModel> Data { get; init; }
    }

    public record BookViewModel
    {
        public string Id { get; init; }
        public string Title { get; init; }
    }
}
