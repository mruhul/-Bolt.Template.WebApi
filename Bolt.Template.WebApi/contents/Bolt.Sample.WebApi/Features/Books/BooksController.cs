using System.Collections.Generic;
using System.Threading.Tasks;
using Bolt.RequestBus;
using Bolt.Sample.WebApi.Features.Books.List;
using Bolt.Sample.WebApi.Features.Shared.Ports.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Bolt.Sample.WebApi.Features.Books
{
    [ApiController]
    [Route("books")]
    public class BooksController : ControllerBase
    {
        private readonly IRequestBus bus;

        public BooksController(IRequestBus bus)
        {
            this.bus = bus;
        }

        /// <summary>
        /// Return all books
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(BooksListViewModel), 200)]
        public async Task<IActionResult> Get([FromQuery]BooksListRequest request)
        {
            var rsp = await bus.SendAsync<BooksListRequest, BooksListViewModel>(request);

            return Ok(rsp.Value);
        }
    }

    
}
